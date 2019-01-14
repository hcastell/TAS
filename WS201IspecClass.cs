using System;
using System.Xml.Serialization;

using CEdotNetWsInfrastructure;

namespace wssfb_Ns
{
	/// <summary>
	/// Summary description for WS201.
	/// <methods className='WS201' >
	///  <method  hasInput='true' />
	/// </methods>
	/// </summary>
	public class WS201 : HostSystem
	{
		public WS201()
		{
			// No constructor logic.
		}

		#region WS201
		/// <summary>
		/// Web Service method for WS201.
		/// </summary>
		/// <param name="inputObj">WS201_Input</param>
		/// <returns>WS201_Output</returns>
		public virtual WS201_Output M_WS201(WS201_Input inputObj)
		{
			if (inputObj == null)
				this.ThrowHostExceptionClient("Did not receive a 'WS201_Input' object.");

			// Create session with the host system.
			this.OpenHostSession();

			// Set the current ispec.
			this.SetCurrentIspec("WS201");

			// Set required input fields on current ispec.
			this.SetIspecFieldValue("DCO_OFICI", inputObj.DCO_OFICI, "DCO_OFICI");
			this.SetIspecFieldValue("ACOTASTAG", inputObj.ACOTASTAG, "ACOTASTAG");
			this.SetIspecFieldValue("TNU_ORDEN", inputObj.TNU_ORDEN, "TNU_ORDEN");
			this.SetIspecFieldValue("TSE_REVER", inputObj.TSE_REVER, "TSE_REVER");
			this.SetIspecFieldValue("TNU_REVER", inputObj.TNU_REVER, "TNU_REVER");
			this.SetIspecFieldValue("ANU_TARJE", inputObj.ANU_TARJE, "ANU_TARJE");
			this.SetIspecFieldValue("SCOTIPDOC", inputObj.SCOTIPDOC, "SCOTIPDOC");
			this.SetIspecFieldValue("SNU_DOCUM", inputObj.SNU_DOCUM, "SNU_DOCUM");
			this.SetIspecFieldValue("ACO_CONCE", inputObj.ACO_CONCE, "ACO_CONCE");
			this.SetIspecFieldValue("ACO_MONED", inputObj.ACO_MONED, "ACO_MONED");
			this.SetIspecFieldValue("ACU_OFICI", inputObj.ACU_OFICI, "ACU_OFICI");
			this.SetIspecFieldValue("ACUNUMCUE", inputObj.ACUNUMCUE, "ACUNUMCUE");
			this.SetIspecFieldValue("ACUDIGVER", inputObj.ACUDIGVER, "ACUDIGVER");
			this.SetIspecFieldValue("TVA_MOVIM", inputObj.TVA_MOVIM.ToString(), "TVA_MOVIM");
			this.SetIspecFieldValue("CCNCHEING", inputObj.CCNCHEING, "CCNCHEING");
			this.SetIspecFieldValue("ANU_SOBRE", inputObj.ANU_SOBRE, "ANU_SOBRE");
			this.SetIspecFieldValue("ASECONFIR", inputObj.ASECONFIR, "ASECONFIR");

			// Send transaction to the host.
			this.IspecTransaction();

			// Create the Output object.
			WS201_Output outputObj = new WS201_Output();

			// Get output fields from current ispec.
			outputObj.DCO_OFICI = this.GetIspecFieldFormattedValue("DCO_OFICI", true);
			outputObj.ACOTASTAG = this.GetIspecFieldFormattedValue("ACOTASTAG", true);
			outputObj.TNU_ORDEN = this.GetIspecFieldFormattedValue("TNU_ORDEN", true);
			outputObj.TSE_REVER = this.GetIspecFieldFormattedValue("TSE_REVER", false);
			outputObj.TNU_REVER = this.GetIspecFieldFormattedValue("TNU_REVER", true);
			outputObj.ANU_TARJE = this.GetIspecFieldFormattedValue("ANU_TARJE", false);
			outputObj.SCOTIPDOC = this.GetIspecFieldFormattedValue("SCOTIPDOC", true);
			outputObj.SNU_DOCUM = this.GetIspecFieldFormattedValue("SNU_DOCUM", true);
			outputObj.ACO_CONCE = this.GetIspecFieldFormattedValue("ACO_CONCE", true);
			outputObj.ACO_MONED = this.GetIspecFieldFormattedValue("ACO_MONED", true);
			outputObj.ACU_OFICI = this.GetIspecFieldFormattedValue("ACU_OFICI", true);
			outputObj.ACUNUMCUE = this.GetIspecFieldFormattedValue("ACUNUMCUE", true);
			outputObj.ACUDIGVER = this.GetIspecFieldFormattedValue("ACUDIGVER", true);
			outputObj.TVA_MOVIM = Convert.ToDecimal(this.GetIspecFieldFormattedValue("TVA_MOVIM", true));
			outputObj.CCNCHEING = this.GetIspecFieldFormattedValue("CCNCHEING", true);
			outputObj.ANU_SOBRE = this.GetIspecFieldFormattedValue("ANU_SOBRE", true);
			outputObj.ASECONFIR = this.GetIspecFieldFormattedValue("ASECONFIR", false);
			outputObj.SD_PARTE1 = this.GetIspecFieldFormattedValue("SD_PARTE1", false);
			outputObj.SD_ENTE1 = this.GetIspecFieldFormattedValue("SD_ENTE1", true);
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
	/// <inputParameters className='WS201_Input' >
	///  <inputParameter name='DCO_OFICI' hostDataType='number' length='3' />
	///  <inputParameter name='ACOTASTAG' hostDataType='number' length='6' />
	///  <inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />
	///  <inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />
	///  <inputParameter name='TNU_REVER' hostDataType='number' length='6' />
	///  <inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />
	///  <inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />
	///  <inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />
	///  <inputParameter name='ACO_CONCE' hostDataType='number' length='2' />
	///  <inputParameter name='ACO_MONED' hostDataType='number' length='2' />
	///  <inputParameter name='ACU_OFICI' hostDataType='number' length='3' />
	///  <inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />
	///  <inputParameter name='ACUDIGVER' hostDataType='number' length='1' />
	///  <inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />
	///  <inputParameter name='CCNCHEING' hostDataType='number' length='3' />
	///  <inputParameter name='ANU_SOBRE' hostDataType='number' length='5' />
	///  <inputParameter name='ASECONFIR' hostDataType='alpha' length='1' />
	/// </inputParameters>
	/// </summary>
	public class WS201_Input
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
		[XmlElement(DataType="integer")] public string TNU_ORDEN;
		public string TSE_REVER;
		[XmlElement(DataType="integer")] public string TNU_REVER;
		public string ANU_TARJE;
		[XmlElement(DataType="integer")] public string SCOTIPDOC;
		[XmlElement(DataType="integer")] public string SNU_DOCUM;
		[XmlElement(DataType="integer")] public string ACO_CONCE;
		[XmlElement(DataType="integer")] public string ACO_MONED;
		[XmlElement(DataType="integer")] public string ACU_OFICI;
		[XmlElement(DataType="integer")] public string ACUNUMCUE;
		[XmlElement(DataType="integer")] public string ACUDIGVER;
		public decimal TVA_MOVIM;
		[XmlElement(DataType="integer")] public string CCNCHEING;
		[XmlElement(DataType="integer")] public string ANU_SOBRE;
		public string ASECONFIR;
	}
	#endregion

	#region Output Data Types
	public class WS201_Output
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
		[XmlElement(DataType="integer")] public string TNU_ORDEN;
		public string TSE_REVER;
		[XmlElement(DataType="integer")] public string TNU_REVER;
		public string ANU_TARJE;
		[XmlElement(DataType="integer")] public string SCOTIPDOC;
		[XmlElement(DataType="integer")] public string SNU_DOCUM;
		[XmlElement(DataType="integer")] public string ACO_CONCE;
		[XmlElement(DataType="integer")] public string ACO_MONED;
		[XmlElement(DataType="integer")] public string ACU_OFICI;
		[XmlElement(DataType="integer")] public string ACUNUMCUE;
		[XmlElement(DataType="integer")] public string ACUDIGVER;
		public decimal TVA_MOVIM;
		[XmlElement(DataType="integer")] public string CCNCHEING;
		[XmlElement(DataType="integer")] public string ANU_SOBRE;
		public string ASECONFIR;
		public string SD_PARTE1;
		[XmlElement(DataType="integer")] public string SD_ENTE1;
		[XmlElement(DataType="integer")] public string DCOMENSIS;
		public General_StatusMessages StatusMessages;
	}
	#endregion
}
