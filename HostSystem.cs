using System;
using System.Configuration;
using System.Xml.Serialization;
using System.Reflection;

using CEWindowsAPI;
using CEWindowsAPIPool;

namespace CEdotNetWsInfrastructure
{
	/// <summary>
	/// This class provides the host interface functions required to 
	/// access Ispecs/Classes on the host.<br/>
	/// Functions provided are:<br/>
	/// - Open a session with the host system.<br/>
	/// - Set current Ispec for the session.<br/>
	/// - Get/Set field values on current Ispec.<br/>
	/// - Send transaction to current Ispec.<br/>
	/// - Get status of transaction.<br/>
	/// - Get list data.<br/>
	/// - Close the host session.<br/>
	/// </summary>
	public class HostSystem
	{
		private LINCEnvironment objLINC = null;
		private LINCStatus objStatusLine = null;
		private IspecModelRef objCurrentIspecRef = null;
		private IspecModel objCurrentIspec = null;
		private ListRepository objListManager = null;
		private CEWindowsAPI.LINCEnvironmentPooled objLINCPooled = null;

		public HostSystem()
		{
		}

		/// <summary>
		/// Depending on the type of LINCEnvironment object,
		/// a new LINCEnvironment object is created and a session with the 
		/// host system is established in a non-pooled environment and in 
		/// a pooled environment a LINCEnvironment object is obtained and 
		/// reused from the pool.
		/// Host connection parameters are read from the web.config file. 
		/// </summary>
		public void OpenHostSession()
		{
			ResponseCodes iResult = 0;
            bool assemblyLoaded = false;
            int maxRetries = 10;
            int count = 0;


			// Create a LINCEnvironment object
			// Check for object pooling
			if (Config.GetBoolean("ObjectPoolingEnabled", false))
			{
                while (!assemblyLoaded && count < maxRetries)
                {
                    count++;

                    // Get assembly name of the object pool.
                    string poolAssemblyName = Config.GetString("ObjectPoolingAssembly", "CEWindowsAPIPool, Version=2.0.1000.1, Culture=neutral, PublicKeyToken=e786d48c08ed6963");
                    // Load the assembly for the pool.
                    Assembly a = Assembly.Load(poolAssemblyName);

                    // Get type of the object pool to instantiate.
                    string poolType = Config.GetString("ObjectPoolingType", "CEWindowsAPIPool.LINCEnvironmentPooled");
                    // Get a pool of LINCEnvironment objects of the specified type.
                    try
                    {
                        this.objLINCPooled = (CEWindowsAPI.LINCEnvironmentPooled)a.CreateInstance(poolType);
                       
                    }
                    catch (Exception ex)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    // Get a LINCEnvironment object from the pool.
                    if (this.objLINCPooled != null)
                    {
                        this.objLINC = this.objLINCPooled.Session;
                        assemblyLoaded = true;
                    }

                }

			}
			else
			{
				try
				{
					string progid = Config.GetString("LINCEnvironmentProgId", "");
					if (progid == "")
						// Default when blank or not specified.
						// Get a non-pooled .NET LINCEnvironment object.
						this.objLINC = new LINCEnvironment();
					else
						// Get a non-pooled Java LINCEnvironment object.
						this.objLINC = new LINCEnvironment(progid);
				}
				catch (Exception ex)
				{
					this.CloseHostSession();
					throw HostError.ServerFault("A LINCEnvironment could not be created. Check the configuration parameters.", ex.Message);
				}
			}

            // Retrieve RATL start version setting
            string RATLStartVersion = Config.GetString("RATLStartVersion", "12");
            this.objLINC.RATLStartVersion = RATLStartVersion;

            // Retrieve the IspecModelRootDirectory setting from Web.config.
            string ispecModelRootDirectory = Config.GetString("IspecModelRootDirectory", "");
			if (ispecModelRootDirectory != "")
				this.objLINC.DownloadDestination = ispecModelRootDirectory;

			// Enable logging
			if (Config.GetBoolean("LoggingEnabled", false))
			{
				Log objLog = objLINC.GetLogObject();
				try
				{
					int logLevel = Config.GetInt("LogLevel", 0);
					if (objLog.def.LogLevel != logLevel)
					{
						objLog.def.OutputFile = Config.GetString("LogFile", "");
						objLog.def.LogLevel = logLevel;
						objLog.def.Flags = 62;
					}
				}
				catch (Exception ex)
				{
					this.CloseHostSession();
					throw HostError.ServerFault("Cannot enable CE logging. Check the configuration parameters.", ex.Message);
				}
			}

			// Create status line object
			this.objStatusLine = this.objLINC.MakeLINCStatus();
			// Create list manager object
			this.objListManager = this.objLINC.GetListRepository();
			// Create Ispec reference object
			this.objCurrentIspecRef = this.objLINC.MakeIspecModelRef();

			// When this LINC object is not connected, connect it.
			if (!this.objLINC.IsConnected)
			{
				// Get Host connection parameters from web.config file.
				this.objLINC.ApplicationName = Config.GetString("ApplicationName", "");
				this.objLINC.BundleName = Config.GetString("BundleName", "");
				this.objLINC.Name = Config.GetString("DisplayName", "");
				this.objLINC.PackagePrefix = Config.GetString("PackagePrefix", "");

				// Get URI to the host. TCP/IP or MSMQ connection.
				string serverURL = Config.GetString("HostURI", "");

				// Retrieve the ConnectionMode setting from Web.config.
				int connectionMode = Config.GetInt("ConnectionMode", 0); // Defaut connection mode is PCE

				// Create Login attributes object
				ObjectRef objLoginAttrsRef = this.objLINC.MakeObjectRef();

				// Connect to the RATL server.
				if (this.objLINC.IsObjectPooled)
					iResult = this.objLINC.ConnectPooled(serverURL, Config.GetString("HostViewName", ""), objLoginAttrsRef, objStatusLine, connectionMode);
				else
					iResult = this.objLINC.Connect(serverURL, Config.GetString("HostViewName", ""), objLoginAttrsRef, objStatusLine, connectionMode);

				// OK_LOGIN (101) indicates that login info must be supplied.
				if (iResult == ResponseCodes.OK_LOGIN)
				{
					LoginAttributeArray objLoginAttrs = objLoginAttrsRef.GetLoginAttributeArrayObject();

					objLoginAttrs.SetAttribute("US", Config.GetString("HostLogin", ""));
					objLoginAttrs.SetAttribute("PW", Config.GetString("HostPassword", ""));
					string hostDomain = Config.GetString("HostDomain", "");
					if (hostDomain != "")
						objLoginAttrs.SetAttribute("DO", hostDomain);

					// Do the login.
					if (this.objLINC.IsObjectPooled)
						iResult = objLINC.LoginPooled(objLoginAttrs, objLoginAttrsRef, this.objStatusLine);
					else
						iResult = objLINC.Login(objLoginAttrs, objLoginAttrsRef, this.objStatusLine);
				}

				// When bad response (100), throw exception
				if (iResult != ResponseCodes.OK)
				{
					this.CloseHostSession();
					throw HostError.CEServerSideError(iResult);
				}

				// It is a good response. Say HI to the LINC system.
				iResult = this.objLINC.Hello();

				// When not a good response (100) and a fireup ispec was not returned (803), throw exception.
				if (iResult != ResponseCodes.OK && iResult != ResponseCodes.OK_NO_FIREUP)
				{
					this.CloseHostSession();
					throw HostError.CEServerSideError(iResult);
				}
			}
		}

		/// <summary>
		/// This method get the Ispec Model for the specified ispec and
		/// loads it from the host.
		/// </summary>
		/// <param name="ispecName">The name of the Ispec to load.</param>
		public void SetCurrentIspec(string ispecName)
		{
			ResponseCodes iResult = 0;
			this.objCurrentIspec = null;

			// When a LINC Environment is not established, throw exception.
			if (this.objLINC == null)
			{
				this.CloseHostSession();
				throw HostError.ServerFault("A LINC Environment has not been created.", "");
			}

			// Get the Ispec Model
			this.objCurrentIspec = this.objLINC.GetIspec(ispecName);
			if (this.objCurrentIspec == null)
			{
				this.CloseHostSession();
				throw HostError.ServerFault("Could not get IspecModel for Ispec '"+ispecName+"'.", "");
			}

			// Get empty Ispec from the host.
			if (this.objLINC.IsObjectPooled)
				iResult = this.objLINC.LoadIspecPooled(this.objCurrentIspec);
			else
				iResult = this.objLINC.LoadIspec(this.objCurrentIspec);
			// When not a good response (100), throw exception.
			if (iResult != ResponseCodes.OK)
			{
				this.CloseHostSession();
				throw HostError.ServerFault("Cannot load Ispec '" + ispecName + "'. (CE response code: " + iResult + ")", "");
			}
		}

		/// <summary>
		/// Returns the value of a field from the current ispec.
		/// </summary>
		/// <param name="fieldName">Name of the field to get.</param>
		/// <param name="zeroWhenBlank">When true, zero is returned when the value is blank.</param>
		/// <returns>The value of the field.</returns>
		public string GetIspecFieldFormattedValue(string fieldName, bool zeroWhenBlank)
		{
			string fieldValue;

			// When current Ispec is not set, throw exception.
			if (this.objCurrentIspec == null)
			{
				this.CloseHostSession();
				throw HostError.ServerFault("A current Ispec has not been set for field '"+fieldName+"'.", "");
			}

			// Get field value
			fieldValue = this.objCurrentIspec.GetFieldFormattedValue(fieldName);
			if (zeroWhenBlank && fieldValue == "")
				fieldValue = "0";

			// Return value of the field
			return fieldValue;
		}

		/// <summary>
		/// Returns the value of a copy-from field from the current ispec.
		/// </summary>
		/// <param name="fieldName">Name of the copy-from field to get.</param>
		/// <param name="row">Row number of the copy-from row.</param>
		/// <param name="zeroWhenBlank">When true, zero is returned when the value is blank.</param>
		/// <returns>The value of the field.</returns>
		public string GetIspecFieldFormattedValue(string fieldName, int row, bool zeroWhenBlank)
		{
			string copyFromRowNo = row.ToString().PadLeft(2, '0');
			string copyFromFieldName = fieldName + "__AT_" + copyFromRowNo;

			return GetIspecFieldFormattedValue(copyFromFieldName, zeroWhenBlank);
		}

		/// <summary>
		/// Sets the value of a field on the current ispec.
		/// </summary>
		/// <param name="fieldName">Name of the field to set.</param>
		/// <param name="fieldValue">The value to set on the field.</param>
		/// <param name="longName">Long name of the field.</param>
		public void SetIspecFieldValue(string fieldName, string fieldValue, string longFieldname)
		{
			ResponseCodes iResult = 0;

			// When current Ispec is not set, throw exception.
			if (this.objCurrentIspec == null)
			{
				this.CloseHostSession();
				throw HostError.ServerFault("A current Ispec has not been set for field '"+fieldName+"'.", "");
			}

			// Set the field value 
			iResult = this.objCurrentIspec.SetFieldValue(fieldName, fieldValue);

			// When not a good response (100), throw exception.
			if (iResult != ResponseCodes.OK)
			{
				this.CloseHostSession();
				throw IspecFieldError(iResult, longFieldname);
			}
		}

		/// <summary>
		/// Sets the value of a copy-from field on the current ispec.
		/// </summary>
		/// <param name="fieldName">Name of the copy-from field to set.</param>
		/// <param name="row">Row number of the copy-from row.</param>
		/// <param name="fieldValue">The value to set on the field.</param>
		/// <param name="longName">Long name of the field.</param>
		public void SetIspecFieldValue(string fieldName, int row, string fieldValue, string longFieldname)
		{
			string copyFromRowNo = row.ToString().PadLeft(2, '0');
			string copyFromFieldName = fieldName + "__AT_" + copyFromRowNo;

			SetIspecFieldValue(copyFromFieldName, fieldValue, longFieldname);
		}

		/// <summary>
		/// Sends a transaction to the current Ispec on the host.<br/>
		/// Soap Exception is thrown when the ispec returned from the host
		/// is different from the ispec sent to the host.
		/// </summary>
		public void IspecTransaction()
		{
			ResponseCodes iResult = 0;

			// When a LINC Environment is not established, throw exception.
			if (this.objLINC == null)
			{
				this.CloseHostSession();
				throw HostError.ServerFault("A LINC Environment has not been created.", "");
			}

			// Cache the name of the current Ispec.
			string ispecName = this.objCurrentIspec.IspecModelName;

			// Send transaction to current ispec on the host
			if (this.objLINC.IsObjectPooled)
				iResult = this.objLINC.TransactionPooled(this.objCurrentIspec, this.objCurrentIspecRef, this.objStatusLine);
			else
				iResult = this.objLINC.Transaction(this.objCurrentIspec, this.objCurrentIspecRef, this.objStatusLine);

			// When not a good response (100), determine how to handle it.
			if (iResult != ResponseCodes.OK)
			{
				// When transaction failed (911) and configured to return 911 as
				// normal reponse, then just return.
				if (iResult == ResponseCodes.ERR_TRANSACTION_FAILED && !Config.GetBoolean("ReturnTransactionErrorAsSoapFault", true))
					return;

				// Otherwise, throw exception and the error is returned as
				// a standard Soap Fault message.
				this.CloseHostSession();
				throw TransactionError(iResult);
			}

			// Set current Ispec to Ispec returned from the host. 
			this.objCurrentIspec = this.objCurrentIspecRef.GetObject();

			// When host returns a different ispec, throw exception.
			if (ispecName != this.objCurrentIspec.IspecModelName)
			{
				string returnIspecName = this.objCurrentIspec.IspecModelName;
				this.CloseHostSession();
				throw HostError.ServerFault("The host did not return the same Ispec as the Ispec called. ",
					"Ispec called is '"+ispecName+"'. Ispec returned is '"+returnIspecName+"'.");
			}
		}

		/// <summary>
		/// Returns an array of errors messages returned from the last 
		/// transaction.
		/// </summary>
		/// <returns>String array of errors returned from the last transaction.</returns>
		public General_StatusMessages GetStatusMessages()
		{
			General_StatusMessages statusMessages = new General_StatusMessages();
			string [] messages = null;
			string rs = ((char)30).ToString();   // Record Separator character

			int errorCount = this.objStatusLine.ErrorCount;

			if (errorCount == 0)
			{
				// When there are no errors, then just get the status line message.
				messages = new string[1];
				// Escape Record separator character
				messages[0] = this.objStatusLine.Status.Replace(rs, "&#30;");
			}
			else
			{
				// When there are errors, then get all the error messages.
				messages = new string[errorCount];
				for (int i = 0; i < errorCount; i++)
				{
					// Escape Record separator character
					messages[i] = this.objStatusLine.GetError(i).Replace(rs, "&#30;");
				}
			}

			statusMessages.StatusMessages = messages;
			return statusMessages;
		}

		/// <summary>
		/// Depending on the type of LINCEnvironment object,
		/// this method returns the LINCEnvironment object to the pool or 
		/// it says bye to the host system and closes the session.
		/// </summary>
		public void CloseHostSession()
		{
			if (this.objLINC != null)
			{
				if (this.objLINC.IsObjectPooled)
				{
					// Return it to the pool.
					this.objLINCPooled.Dispose();
				}
				else
				{
					// Log off
					if (this.objLINC.IsLoggedIn)
						this.objLINC.Bye();

					// Disconnect
					if (this.objLINC.IsConnected)
						this.objLINC.Close();
				}
			}
		}

		/// <summary>
		/// This method throws a HostSystemException containing the 
		/// specified message and indicating a fault causes by the client.
		/// </summary>
		/// <param name="message">Message describing the problem.</param>
		public void ThrowHostExceptionClient(string message)
		{
			this.CloseHostSession();
			throw HostError.ClientFault(message, "");
		}

		/// <summary>
		/// This method returns a SoapException containing all error messages
		/// returned from the transaction.
		/// </summary>
		/// <param name="errorCode">Component Enabler Error code.</param>
		/// <returns>A Soap Exception</returns>
		private Exception TransactionError(ResponseCodes errorCode)
		{
			string rs = ((char)30).ToString();   // Record Separator character

			// When transaction failed (911), return client fault.
			if (errorCode == ResponseCodes.ERR_TRANSACTION_FAILED)
			{
				string errorText = "";
				for (int i = 0; i < this.objStatusLine.ErrorCount; i++)
				{
					// Collect all error messages from the transaction
					// Escape Record separator character
					errorText += "[" + this.objStatusLine.GetError(i).Replace(rs, "&#30;") + "] ";
				}
				return HostError.ClientFault("The transaction failed. Check faultdetails for error message(s). (CE response code: 911)", errorText);
			}

			return HostError.CEServerSideError(errorCode);
		}

		/// <summary>
		/// This method returns a SoapException containing all error messages
		/// returned from the transaction.
		/// </summary>
		/// <param name="errorCode">Component Enabler Error code.</param>
		/// <returns>A Soap Exception</returns>
		private Exception IspecFieldError(ResponseCodes errorCode, string longFieldName)
		{
			// When bad field value (933), throw client side error
			if (errorCode == ResponseCodes.ERR_BAD_FIELD_VALUE)
			{
				return HostError.CEClientSideError(this.objCurrentIspec.IspecModelName,
					longFieldName,
					this.objCurrentIspec.GetLINCFieldExceptionReason());
			}

			return HostError.CEServerSideError(errorCode);
		}

		#region List Items Manager
		/// <summary>
		/// Get the specified list from the host system and returns a 
		/// List Items object containing rows and columns from the list.
		/// </summary>
		/// <param name="listName">Name of list.</param>
		/// <returns>List Items object.</returns>
		public General_ListItems GetListFromHost(string listName)
		{
			// Create a List Items object.
			General_ListItems listItems = new General_ListItems();

			// Get a list manager object
			ListModel objList = objListManager.GetList(listName);

			int rowCount = this.GetListRowCount(objList);
			if (rowCount != 0)
			{
				listItems.Rows = new General_ListColumns[rowCount];
				for (int i = 0; i < rowCount; i++)
				{
					General_ListColumns listRow = new General_ListColumns();
					listRow.Columns = this.GetListColumns(objList, i);
					listItems.Rows[i] = listRow;
				}
			}

			return listItems;
		}

		/// <summary>
		/// Returns the number of rows returned from the host in the 
		/// specified list.
		/// </summary>
		/// <param name="objList">Name of the ListModel object to return row count for.</param>
		/// <returns>Number of rows in specified list.<br/>
		/// Zero is returned when the list is empty or does not exist.</returns>
		private int GetListRowCount(ListModel objList)
		{
			int rowCount = 0;
			if (objList != null)
				rowCount = objList.Size;

			return rowCount;
		}

		/// <summary>
		/// Returns an array of columns from the specified row number in
		/// the specified list. 
		/// </summary>
		/// <param name="objList">Name of the ListModel object to return columns from.</param>
		/// <param name="rowNumber">Row number to return columns from.</param>
		/// <returns>String array of columns.</returns>
		private string[] GetListColumns(ListModel objList, int rowNumber)
		{
			string [] columns = null;

			if (objList != null)
			{
				ListItemModel objListRow = objList.GetItem(rowNumber);
				if (objListRow != null)
				{
					int columnCount = objListRow.Size;
					columns = new string[columnCount];
					for (int i = 0; i < columnCount; i++)
						columns[i] = objListRow.GetColumn(i);
				}
				else
					objList = null;
			}

			return columns;
		}

		/// <summary>
		/// Returns a List Items object containing rows and columns 
		/// from the specified array of list item pairs.
		/// </summary>
		/// <param name="listItemPairs">String array containing pairs a list items.
		/// Each pair will be put into a row on the List Items object.</param>
		/// <returns>List Items object.</returns>
		public General_ListItems GetListInline(string[] listItemPairs)
		{
			// Create a List Items object.
			General_ListItems listItems = new General_ListItems();

			// Inline list has just two columns.
			int columnCount = 2;

			int rowCount = listItemPairs.Length;
			if (rowCount != 0)
			{
				listItems.Rows = new General_ListColumns[rowCount];
				for (int i = 0; i < rowCount; i+=2)
				{
					General_ListColumns listRow = new General_ListColumns();
					string [] columns = new string[columnCount];
					columns[0] = listItemPairs[i];
					// Check for odd pair.
					if (i+1 < rowCount)
						columns[1] = listItemPairs[i+1];
					listRow.Columns = columns;
					listItems.Rows[i] = listRow;
				}
			}

			return listItems;
		}
		#endregion
	}

	#region Config utilities
	public class Config
	{
		/// <summary>Returns the value of the specified parameter as an integer.</summary>
		/// <param name="paramKey">The parameter to retrieve from web.config.</param>
		/// <param name="defaultValue">The value to return when parameter is not found or cannot be parsed.</param>
		/// <returns>The value of the parameter.</returns>
		public static int GetInt(string paramKey, int defaultValue)
		{
			try
			{
				string paramValue = ConfigParam(paramKey);
				if (paramValue == null || paramValue.Length == 0)
					return defaultValue;

				return System.Int32.Parse(paramValue);
			}
			catch
			{}

			return defaultValue;
		}

		/// <summary>Returns the value of the specified parameter as a boolean.</summary>
		/// <param name="paramKey">The parameter to retrieve from web.config.</param>
		/// <param name="defaultValue">The value to return when parameter is not found or cannot be parsed.</param>
		/// <returns>The value of the parameter.</returns>
		public static bool GetBoolean(string paramKey, bool defaultValue)
		{
			try
			{
				string paramValue = ConfigParam(paramKey);
				if (paramValue == null || paramValue.Length == 0)
					return defaultValue;

				return System.Boolean.Parse(paramValue);
			}
			catch
			{}

			return defaultValue;
		}

		/// <summary>Returns the value of the specified parameter as a string.</summary>
		/// <param name="paramKey">The parameter to retrieve from web.config.</param>
		/// <param name="defaultValue">The value to return when parameter is not found or cannot be parsed.</param>
		/// <returns>The value of the parameter.</returns>
		public static string GetString(string paramKey, string defaultValue)
		{
			try
			{
				string paramValue = ConfigParam(paramKey);
				if (paramValue == null || paramValue.Length == 0)
					return defaultValue;

				return paramValue;
			}
			catch
			{}

			return defaultValue;
		}

		/// <summary>
		/// Retrieves the specified parameter keyword from the appSettings
		/// of the web.config file and returns the value.
		/// </summary>
		/// <param name="paramKey">Parameter keyword in Web.config</param>
		/// <returns>Value of parameter keyword.</returns>
		public static string ConfigParam(string paramKey)
		{
			// Retrieve the IspecModelRootDirectory setting from Web.config.
			string paramValue = ConfigurationManager.AppSettings[paramKey];
			if (paramValue == null || paramValue.Length == 0)
				return null;
			else
				return paramValue.Trim();
		}
	}
	#endregion

	#region General Status Messages Response Type
	public class General_StatusMessages
	{
		[XmlElement(ElementName="StatusMessage")]
		public string[] StatusMessages;
	}
	#endregion

	#region General List Items Response Type
	public class General_ListItems
	{
		[XmlElement(ElementName="row")]
		public General_ListColumns[] Rows;
	}

	public class General_ListColumns
	{
		[XmlElement(ElementName="column")]
		public string[] Columns;
	}
	#endregion
}
