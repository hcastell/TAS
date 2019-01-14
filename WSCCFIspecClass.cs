using System;
using System.Xml.Serialization;

using CEdotNetWsInfrastructure;

namespace wssfb_Ns
{
	/// <summary>
	/// Summary description for WSCCF.
	/// <methods className='WSCCF' >
	///  <method  hasInput='true' />
	/// </methods>
	/// </summary>
	public class WSCCF : HostSystem
	{
		public WSCCF()
		{
			// No constructor logic.
		}

		#region WSCCF
		/// <summary>
		/// Web Service method for WSCCF.
		/// </summary>
		/// <param name="inputObj">WSCCF_Input</param>
		/// <returns>WSCCF_Output</returns>
		public virtual WSCCF_Output M_WSCCF(WSCCF_Input inputObj)
		{
			if (inputObj == null)
				this.ThrowHostExceptionClient("Did not receive a 'WSCCF_Input' object.");

			// Create session with the host system.
			this.OpenHostSession();

			// Set the current ispec.
			this.SetCurrentIspec("WSCCF");

			// Set required input fields on current ispec.
			this.SetIspecFieldValue("DCO_OFICI", inputObj.DCO_OFICI, "DCO_OFICI");
			this.SetIspecFieldValue("ACOTASTAG", inputObj.ACOTASTAG, "ACOTASTAG");

			// Send transaction to the host.
			this.IspecTransaction();

			// Create the Output object.
			WSCCF_Output outputObj = new WSCCF_Output();

			// Get output fields from current ispec.
			outputObj.DCO_OFICI = this.GetIspecFieldFormattedValue("DCO_OFICI", true);
			outputObj.ACOTASTAG = this.GetIspecFieldFormattedValue("ACOTASTAG", true);
			outputObj.DCOMENSIS = this.GetIspecFieldFormattedValue("DCOMENSIS", true);

			// Create Copy-From line items output.
			int lineItemsCount = 20;
			WSCCF_Output_LineItems[] myLineItems = new WSCCF_Output_LineItems[lineItemsCount];
			for (int row = 0; row < lineItemsCount; row++)
			{
				WSCCF_Output_LineItems myLineItem = new WSCCF_Output_LineItems();
				myLineItem.ALF01DE04 = this.GetIspecFieldFormattedValue("ALF01DE04", row, false);
				myLineItem.DFE_FERIAD = this.GetIspecFieldFormattedValue("DFE_FERIAD", row, true);
				myLineItem.ALF01DE16 = this.GetIspecFieldFormattedValue("ALF01DE16", row, false);
				myLineItem.ALF02DE04 = this.GetIspecFieldFormattedValue("ALF02DE04", row, false);
				myLineItem.AFE_TRANS = this.GetIspecFieldFormattedValue("AFE_TRANS", row, true);
				myLineItem.ALF02DE16 = this.GetIspecFieldFormattedValue("ALF02DE16", row, false);
				myLineItem.ALF03DE04 = this.GetIspecFieldFormattedValue("ALF03DE04", row, false);
				myLineItem.AFE_ALTA = this.GetIspecFieldFormattedValue("AFE_ALTA", row, true);
				myLineItem.ALF03DE16 = this.GetIspecFieldFormattedValue("ALF03DE16", row, false);
				myLineItems[row] = myLineItem;
			}
			outputObj.LineItems = myLineItems;

			// Get a Status Message object.
			outputObj.StatusMessages = this.GetStatusMessages();

			// That's it. We don't need the Host System any more.
			this.CloseHostSession();

			return outputObj;
		}
		#endregion
	}

	#region Input Data Types
	/// <summary>
	/// <inputParameters className='WSCCF_Input' >
	///  <inputParameter name='DCO_OFICI' hostDataType='number' length='3' />
	///  <inputParameter name='ACOTASTAG' hostDataType='number' length='6' />
	/// </inputParameters>
	/// </summary>
	public class WSCCF_Input
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
	}
	#endregion

	#region Output Data Types
	public class WSCCF_Output
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
		[XmlElement(DataType="integer")] public string DCOMENSIS;
		[XmlElement(ElementName="LineItems")] public WSCCF_Output_LineItems[] LineItems;
		public General_StatusMessages StatusMessages;
	}

	public class WSCCF_Output_LineItems
	{
		public string ALF01DE04;
		[XmlElement(DataType="integer")] public string DFE_FERIAD;
		public string ALF01DE16;
		public string ALF02DE04;
		[XmlElement(DataType="integer")] public string AFE_TRANS;
		public string ALF02DE16;
		public string ALF03DE04;
		[XmlElement(DataType="integer")] public string AFE_ALTA;
		public string ALF03DE16;
	}
	#endregion
}
