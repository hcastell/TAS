using System;
using System.Xml;
using System.Xml.Schema;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.Services.Description;
using System.Configuration;
using System.IO;

namespace CEdotNetWsInfrastructure
{
	/// <summary>
	/// SchemaValidation implements schema validation processing.
	/// </summary>
	public class SchemaValidation : SoapExtension
	{
		// Initializer object (context for each request)
		// generated by GetInitializer.
		private SchemaValidationContext _context;

		private Stream oldStream;
		private Stream newStream;

		#region ChainStream Overridden Method
		/// <summary>
		/// Save the Stream representing the SOAP request or SOAP response 
		/// into a local memory buffer. This is only required for when
		/// SoapTracing is enabled in web.config.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public override Stream ChainStream(Stream stream)
		{
			if (_context != null)
			{
				if (_context.EnableSoapTrace)
				{
					// Save the passed in Stream in a member variable.
					oldStream = stream;

					// Create a new instance of a Stream and save that in 
					// a member variable.
					newStream = new MemoryStream();
					return newStream;
				}
			}

			return stream;
		}
		#endregion

		#region GetInitializer Overridden Methods
		/// <summary>
		/// This GetInitializer is called when the SoapExtension is 
		/// registered in the config file (web/machine.config). It is called 
		/// the first time the class is invoked.
		/// </summary>
		/// <param name="serviceType"></param>
		/// <returns></returns>
		public override object GetInitializer(System.Type serviceType)
		{
			SchemaValidationAttribute attribute = new SchemaValidationAttribute();
			return GetInitializerHelper(serviceType, null, attribute);
		}

		/// <summary>
		/// This GetInitializer is called when the SoapExtension is 
		/// configured via a custom SoapExtensionAttribute. It is called 
		/// the first time the WebMethod is invoked.
		/// </summary>
		/// <param name="methodInfo"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
		{
			return GetInitializerHelper(methodInfo.DeclaringType, methodInfo, attribute);
		}
		#endregion

		#region Initialize Overridden Method
		/// <summary>
		/// Called each time a givne [WebMethod] is used (will recieve a 
		/// SchemaValidationContext object).
		/// </summary>
		/// <param name="initializer"></param>
		public override void Initialize(object initializer)
		{
			// Context object passed in by initialize.
			_context = (SchemaValidationContext)initializer;
		}
		#endregion

		#region ProcessMessage Overridden Method
		/// <summary>
		/// This will be called four times (before/after 
		/// serialize/deserialize) for each [WebMethod].
		/// </summary>
		/// <param name="message"></param>
		public override void ProcessMessage(SoapMessage message)
		{
                switch (message.Stage)
                {
                    // Just received the Soap Request message from client.
                    case SoapMessageStage.BeforeDeserialize:
                    {
                        if (_context != null)
                        {
                            if (_context.EnableSoapTrace)
                                LogSoapRequest(message);

                            // Check to see if the user wants schema validation.
                            if (_context.SchemaValidationAttribute != null && _context.SchemaValidationAttribute.SchemaValidation)
                            {
                                // Perform schema validation.
                                try
                                {
                                    // Configure XmlReader for validating against schema.
                                    XmlTextReader tr = new XmlTextReader(message.Stream);
                                    XmlReaderSettings settings = new XmlReaderSettings();
                                    settings.Schemas.Add(_context.SchemaSet);
                                    settings.ValidationType = ValidationType.Schema;
                                    XmlReader vr = XmlReader.Create(tr, settings);

                                    // Read through the stream to do schema validation
                                    // and return.
                                    while (vr.Read()) ; // read through stream
                                }
                                catch (Exception e)
                                {
                                    // Schema validation error detected.
                                    // Throw a SoapException.
                                    SoapException error = new SoapException(
                                        "Validation of soap request message against schema of requested web service failed. " + e.Message,
                                        SoapException.ClientFaultCode,
                                        HttpContext.Current.Request.Url.AbsoluteUri,
                                        (XmlNode)null);
                                    throw error;
                                }
                                finally
                                {
                                    // Make sure we leave the stream the way we found it.
                                    message.Stream.Position = 0L;
                                }
                            }
                        }
                        break;
                    }

                    // About to call methods.
                    case SoapMessageStage.AfterDeserialize:                       
                        break;

                    // After Method call.
                    case SoapMessageStage.BeforeSerialize:
                        // Check if there is an exception in the message that was read by the Web Server
                        // This could happen if the Soap request was not formatted correctly (e.g. special characters 
                        // like ampersands which shuld be encoded as &amp;
                        // This exception is distinguished from other standard exceptions by checking for an empty Node attribute,
                        // which shows that the exception is generated by the system
                        if (message.Exception != null && message.Exception.Node.Length==0)
                        {
                            // Log the exception in the SAOP Trace file
                            if (_context != null)
                            {
                                if (_context.EnableSoapTrace)
                                    LogSoapException(message.Exception);
                            }
                        } 
                        break;

                    // Outgoing to client.
                    case SoapMessageStage.AfterSerialize:
                        {
                            if (_context != null)
                            {
                                if (_context.EnableSoapTrace)
                                    LogSoapResponse(message);
                            }
                            break;
                        }

                    default:
                        break;
                }
            
		}
		#endregion

		#region Load and Cache Schema Methods
		/// <summary>
		/// Called by both versions of GetInitializer to generate initializer
		/// object (SchemaValidationContext) which serves as the context for 
		/// future requests.
		/// </summary>
		/// <param name="serviceType"></param>
		/// <param name="methodInfo"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		private object GetInitializerHelper(Type serviceType, LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
		{
			// Create a context object.
			SchemaValidationContext ctx = new SchemaValidationContext();

			// Set the validation attribute.
			ctx.SchemaValidationAttribute = (SchemaValidationAttribute)attribute;

			// Get EnableSoapTrace parameter configured in web.config
			// and set it on the context object.
			ctx.EnableSoapTrace = Config.GetBoolean("EnableSoapTrace", false);

			// Get EnableSchemaValidation parameter configured in web.config
			bool enableSchemaValidation = Config.GetBoolean("EnableSchemaValidation", false);

			// When Schema Validation is required, then create the schema
			// for the web service and cache it in the context object. 
			if (enableSchemaValidation)
			{
				// Set SchemaValidation flag.
				ctx.SchemaValidationAttribute.SchemaValidation = enableSchemaValidation;

				// Get HTTP Context.
				HttpContext httpctx = HttpContext.Current;

				// XML Schema cache for schema validation.
				XmlSchemaSet sc = new XmlSchemaSet();

				// Load automatically-generated XML Schema for current endpoint. 
				LoadReflectedSchemas(sc, serviceType, httpctx.Request.RawUrl);

				// Save schema cache for future use.
				ctx.SchemaSet = sc;
			}

			// The System.Web.Services infrastructure will cache this 
			// context object and supply it in each future call to Initialize.
			return ctx;
		}

		/// <summary>
		/// Load automatically-generated XML Schema for specified endpoint.
		/// </summary>
		/// <param name="sc"></param>
		/// <param name="type"></param>
		/// <param name="url"></param>
		private void LoadReflectedSchemas(XmlSchemaSet sc, Type type, string url)
		{
			ServiceDescriptionReflector r = new ServiceDescriptionReflector();
			r.Reflect(type, url);
			foreach (XmlSchema xsd in r.Schemas)
			{
				sc.Add(xsd);
			}
			foreach (ServiceDescription sd in r.ServiceDescriptions)
			{
				LoadSchemasFromServiceDescriptions(sc, sd);
			}
		}

		/// <summary>
		/// Loading schemas embedded in the specified service description.
		/// </summary>
		/// <param name="sc">Schema Collection object to add Web Service schema to</param>
		/// <param name="sd">Web Service Description object</param>
		private void LoadSchemasFromServiceDescriptions(XmlSchemaSet sc, ServiceDescription sd)
		{
			foreach(XmlSchema embeddedXsd in sd.Types.Schemas)
			{
				sc.Add(embeddedXsd);				
			}
		}
		#endregion

		#region Logging Soap Request/Response Methods
		private void LogSoapRequest(SoapMessage message)
		{
			// Copy the Soap Request to new stream.
			Copy(oldStream, newStream);
			// Rewind new stream.
			newStream.Position = 0L;
			// Write Soap Request to trace file.
			TextReader reader = new StreamReader(newStream);
			Tracing.Write("Soap Request\r\n" + reader.ReadToEnd());
			// Rewind new stream.
			newStream.Position = 0L;
		}

		private void LogSoapResponse(SoapMessage message)
		{
			// Rewind new stream.
			newStream.Position = 0L;
			// Write Soap Response to trace file.
			TextReader reader = new StreamReader(newStream);
			Tracing.Write("Soap Response\r\n" + reader.ReadToEnd() + "\r\n");
			// Rewind new stream.
			newStream.Position = 0L;
			// Copy the Soap Response to original stream.
			Copy(newStream, oldStream);
		}


        private void LogSoapException(SoapException e)
        {
            Tracing.Write("Soap Fault\r\n" + e.Message + "\r\n" + "Fault Details: " + e.InnerException);
        }

		private void Copy(Stream from, Stream to) 
		{
			TextReader reader = new StreamReader(from);
			TextWriter writer = new StreamWriter(to);
			writer.WriteLine(reader.ReadToEnd());
			writer.Flush();
		}
		#endregion
	}

	#region SchemaValidationContext Class
	/// <summary>
	/// Intializer object generated by GetInitializer, stored by the 
	/// infrastructure, and provided to each call to Initialize 
	/// holds state used to perform schema validation.
	/// </summary>
	internal class SchemaValidationContext
	{
		internal SchemaValidationAttribute SchemaValidationAttribute = null;
		internal XmlSchemaSet SchemaSet = null;
		internal bool EnableSoapTrace = false;
	}
	#endregion

	#region SchemaValidationAttribute Class
	/// <summary>
	/// SchemaValidationAttribute triggers schema validation through 
	/// the SchemaValidation class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class SchemaValidationAttribute :  SoapExtensionAttribute
	{
		int priority = 0;
		bool _schemaValidation = false;

		// Enable schema validation?
		public bool SchemaValidation
		{
			get { return _schemaValidation; }
			set { _schemaValidation = value; }
		}

		// Used by soap extension to get the type of object to be created.
		public override System.Type ExtensionType
		{
			get
			{
				// Tell the System.Web.Services infrastructure
				// to use the SchemaValidation class.
				return typeof(SchemaValidation);
			}
		}
		public override int Priority
		{
			get { return priority; }
			set { priority = value;	}
		}
	}
	#endregion
}
