using System;
using System.Xml.Serialization;

using CEdotNetWsInfrastructure;

namespace wssfb_Ns
{
	/// <summary>
	/// Summary description for WSCCL.
	/// <methods className='WSCCL' >
	///  <method  hasInput='true' />
	/// </methods>
	/// </summary>
	public class WSCCL : HostSystem
	{
		public WSCCL()
		{
			// No constructor logic.
		}

		#region WSCCL
		/// <summary>
		/// Web Service method for WSCCL.
		/// </summary>
		/// <param name="inputObj">WSCCL_Input</param>
		/// <returns>WSCCL_Output</returns>
		public virtual WSCCL_Output M_WSCCL(WSCCL_Input inputObj)
		{
			if (inputObj == null)
				this.ThrowHostExceptionClient("Did not receive a 'WSCCL_Input' object.");

			// Create session with the host system.
			this.OpenHostSession();

			// Set the current ispec.
			this.SetCurrentIspec("WSCCL");

			// Set required input fields on current ispec.
			this.SetIspecFieldValue("DCO_OFICI", inputObj.DCO_OFICI, "DCO_OFICI");
			this.SetIspecFieldValue("ACOTASTAG", inputObj.ACOTASTAG, "ACOTASTAG");
			this.SetIspecFieldValue("ANU_TARJE", inputObj.ANU_TARJE, "ANU_TARJE");
			this.SetIspecFieldValue("SCOTIPDOC", inputObj.SCOTIPDOC, "SCOTIPDOC");
			this.SetIspecFieldValue("SNU_DOCUM", inputObj.SNU_DOCUM, "SNU_DOCUM");

			// Send transaction to the host.
			this.IspecTransaction();

			// Create the Output object.
			WSCCL_Output outputObj = new WSCCL_Output();

			// Get output fields from current ispec.
			outputObj.DCO_OFICI = this.GetIspecFieldFormattedValue("DCO_OFICI", true);
			outputObj.ACOTASTAG = this.GetIspecFieldFormattedValue("ACOTASTAG", true);
			outputObj.ANU_TARJE = this.GetIspecFieldFormattedValue("ANU_TARJE", false);
			outputObj.SCOTIPDOC = this.GetIspecFieldFormattedValue("SCOTIPDOC", true);
			outputObj.SNU_DOCUM = this.GetIspecFieldFormattedValue("SNU_DOCUM", true);
			outputObj.SCO_IDENT = this.GetIspecFieldFormattedValue("SCO_IDENT", false);
			outputObj.SNO_CLIEN = this.GetIspecFieldFormattedValue("SNO_CLIEN", false);
			outputObj.DCOMENSIS = this.GetIspecFieldFormattedValue("DCOMENSIS", true);

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
	/// <inputParameters className='WSCCL_Input' >
	///  <inputParameter name='DCO_OFICI' hostDataType='number' length='3' />
	///  <inputParameter name='ACOTASTAG' hostDataType='number' length='6' />
	///  <inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />
	///  <inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />
	///  <inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />
	/// </inputParameters>
	/// </summary>
	public class WSCCL_Input
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
		public string ANU_TARJE;
		[XmlElement(DataType="integer")] public string SCOTIPDOC;
		[XmlElement(DataType="integer")] public string SNU_DOCUM;
	}
	#endregion

	#region Output Data Types
	public class WSCCL_Output
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
		public string ANU_TARJE;
		[XmlElement(DataType="integer")] public string SCOTIPDOC;
		[XmlElement(DataType="integer")] public string SNU_DOCUM;
		public string SCO_IDENT;
		public string SNO_CLIEN;
		[XmlElement(DataType="integer")] public string DCOMENSIS;
		public General_StatusMessages StatusMessages;
	}
	#endregion
}
