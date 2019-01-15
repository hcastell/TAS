using System;
using System.Runtime.Serialization;

namespace CEdotNetWsInfrastructure
{
	/// <summary>
	/// HostSystemException allows the HostSystem to throw an exception
	/// that, in addition to the standard ApplicationException, include
	/// information about who is responsible for the error and additional
	/// details about the error.
	/// </summary>
	[Serializable]
	public class HostSystemException : ApplicationException, ISerializable
	{
		HostSystemFaultCode faultCode;
		string detail;

		// Normal 3 constructors that makes this exception a well-behaved
		// exception.
		public HostSystemException()
		{
		}

		public HostSystemException(string message): base(message)
		{
		}

		public HostSystemException(string message, Exception inner): base(message, inner)
		{
		}

		/// <summary>
		/// This constructor takes the additional parameters.
		/// </summary>
		/// <param name="message">Short message describing the error.</param>
		/// <param name="faultCode">Code that indicates who is responsible
		/// for the error. Client means the one consuming and providing
		/// input to the HostSystem is causing the error. Server means the
		/// HostSystem caused the error.</param>
		/// <param name="detail">Additional details that describes the error.</param>
		public HostSystemException(string message, HostSystemFaultCode faultCode, string detail): base(message)
		{
			this.faultCode = faultCode;
			this.detail = detail;
		}

		/// <summary>
		/// Deserialization constructor. This is required in case this 
		/// exception needs to be sent to anoter system. In that case,
		/// this constructor is called by the .NET Framework.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public HostSystemException(SerializationInfo info, StreamingContext context):
			base(info, context)
		{
			// Get our two additional fields from the serialized exception.
			faultCode = (HostSystemFaultCode)info.GetValue("FaultCode", faultCode.GetType());
			detail = info.GetString("Detail");
		}

		/// <summary>
		/// Holds the fault code, which describes who is responsible for
		/// causing the error.
		/// </summary>
		public HostSystemFaultCode FaultCode
		{
			get { return faultCode; }
		}

		/// <summary>
		/// Holds details that describes the error.
		/// </summary>
		public string Detail
		{
			get { return detail; }
		}

		/// <summary>
		/// Called by the framework during serialization to fetch the 
		/// data from an object.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public override void GetObjectData(
			SerializationInfo info,
			StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FaultCode", faultCode, faultCode.GetType());	
			info.AddValue("Detail", detail);	
		}

		/// <summary>
		/// Overridden Message property. This returns the textual 
		/// representation of the exception in response to 
		/// message.toString()
		/// </summary>
		public override string Message
		{
			get { return base.Message; }
		}
	}

	#region Host System FaultCode Enumerations
	/// <summary>
	/// This enumeration defines valid Host System FaultCode selections
	/// </summary>
	public enum HostSystemFaultCode
	{
		Client,
		Server
	}
	#endregion
}
