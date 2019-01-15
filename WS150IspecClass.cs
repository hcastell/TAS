using System;
using System.Xml.Serialization;

using CEdotNetWsInfrastructure;

namespace wssfb_Ns
{
	/// <summary>
	/// Summary description for WS150.
	/// <methods className='WS150' >
	///  <method  hasInput='true' />
	/// </methods>
	/// </summary>
	public class WS150 : HostSystem
	{
		public WS150()
		{
			// No constructor logic.
		}

		#region WS150
		/// <summary>
		/// Web Service method for WS150.
		/// </summary>
		/// <param name="inputObj">WS150_Input</param>
		/// <returns>WS150_Output</returns>
		public virtual WS150_Output M_WS150(WS150_Input inputObj)
		{
			if (inputObj == null)
				this.ThrowHostExceptionClient("Did not receive a 'WS150_Input' object.");

			// Create session with the host system.
			this.OpenHostSession();

			// Set the current ispec.
			this.SetCurrentIspec("WS150");

			// Set required input fields on current ispec.
			this.SetIspecFieldValue("DCO_OFICI", inputObj.DCO_OFICI, "DCO_OFICI");
			this.SetIspecFieldValue("ACOTASTAG", inputObj.ACOTASTAG, "ACOTASTAG");
			this.SetIspecFieldValue("TNU_ORDEN", inputObj.TNU_ORDEN, "TNU_ORDEN");
			this.SetIspecFieldValue("TSE_REVER", inputObj.TSE_REVER, "TSE_REVER");
			this.SetIspecFieldValue("TNU_REVER", inputObj.TNU_REVER, "TNU_REVER");
			this.SetIspecFieldValue("ANU_TARJE", inputObj.ANU_TARJE, "ANU_TARJE");
			this.SetIspecFieldValue("SCOTIPDOC", inputObj.SCOTIPDOC, "SCOTIPDOC");
			this.SetIspecFieldValue("SNU_DOCUM", inputObj.SNU_DOCUM, "SNU_DOCUM");
			this.SetIspecFieldValue("ACO_CONDB", inputObj.ACO_CONDB, "ACO_CONDB");
			this.SetIspecFieldValue("ACO_MONDB", inputObj.ACO_MONDB, "ACO_MONDB");
			this.SetIspecFieldValue("ACU_OFIDB", inputObj.ACU_OFIDB, "ACU_OFIDB");
			this.SetIspecFieldValue("ACUNUMCDB", inputObj.ACUNUMCDB, "ACUNUMCDB");
			this.SetIspecFieldValue("ACUDIGVDB", inputObj.ACUDIGVDB, "ACUDIGVDB");
			this.SetIspecFieldValue("ACO_CONCR", inputObj.ACO_CONCR, "ACO_CONCR");
			this.SetIspecFieldValue("ACO_MONCR", inputObj.ACO_MONCR, "ACO_MONCR");
			this.SetIspecFieldValue("ACU_OFICR", inputObj.ACU_OFICR, "ACU_OFICR");
			this.SetIspecFieldValue("ACUNUMCCR", inputObj.ACUNUMCCR, "ACUNUMCCR");
			this.SetIspecFieldValue("ACUDIGVCR", inputObj.ACUDIGVCR, "ACUDIGVCR");
			this.SetIspecFieldValue("TVA_MOVIM", inputObj.TVA_MOVIM.ToString(), "TVA_MOVIM");
			this.SetIspecFieldValue("ASECONFIR", inputObj.ASECONFIR, "ASECONFIR");

			// Send transaction to the host.
			this.IspecTransaction();

			// Create the Output object.
			WS150_Output outputObj = new WS150_Output();

			// Get output fields from current ispec.
			outputObj.DCO_OFICI = this.GetIspecFieldFormattedValue("DCO_OFICI", true);
			outputObj.ACOTASTAG = this.GetIspecFieldFormattedValue("ACOTASTAG", true);
			outputObj.TNU_ORDEN = this.GetIspecFieldFormattedValue("TNU_ORDEN", true);
			outputObj.TSE_REVER = this.GetIspecFieldFormattedValue("TSE_REVER", false);
			outputObj.TNU_REVER = this.GetIspecFieldFormattedValue("TNU_REVER", true);
			outputObj.ANU_TARJE = this.GetIspecFieldFormattedValue("ANU_TARJE", false);
			outputObj.SCOTIPDOC = this.GetIspecFieldFormattedValue("SCOTIPDOC", true);
			outputObj.SNU_DOCUM = this.GetIspecFieldFormattedValue("SNU_DOCUM", true);
			outputObj.ACO_CONDB = this.GetIspecFieldFormattedValue("ACO_CONDB", true);
			outputObj.ACO_MONDB = this.GetIspecFieldFormattedValue("ACO_MONDB", true);
			outputObj.ACU_OFIDB = this.GetIspecFieldFormattedValue("ACU_OFIDB", true);
			outputObj.ACUNUMCDB = this.GetIspecFieldFormattedValue("ACUNUMCDB", true);
			outputObj.ACUDIGVDB = this.GetIspecFieldFormattedValue("ACUDIGVDB", true);
			outputObj.ACO_CONCR = this.GetIspecFieldFormattedValue("ACO_CONCR", true);
			outputObj.ACO_MONCR = this.GetIspecFieldFormattedValue("ACO_MONCR", true);
			outputObj.ACU_OFICR = this.GetIspecFieldFormattedValue("ACU_OFICR", true);
			outputObj.ACUNUMCCR = this.GetIspecFieldFormattedValue("ACUNUMCCR", true);
			outputObj.ACUDIGVCR = this.GetIspecFieldFormattedValue("ACUDIGVCR", true);
			outputObj.TVA_MOVIM = Convert.ToDecimal(this.GetIspecFieldFormattedValue("TVA_MOVIM", true));
			outputObj.ASECONFIR = this.GetIspecFieldFormattedValue("ASECONFIR", false);
			outputObj.SD_PARTE1 = this.GetIspecFieldFormattedValue("SD_PARTE1", false);
			outputObj.SD_ENTE1 = this.GetIspecFieldFormattedValue("SD_ENTE1", true);
			outputObj.SD_PARTE2 = this.GetIspecFieldFormattedValue("SD_PARTE2", false);
			outputObj.SD_ENTE2 = this.GetIspecFieldFormattedValue("SD_ENTE2", true);
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
	/// <inputParameters className='WS150_Input' >
	///  <inputParameter name='DCO_OFICI' hostDataType='number' length='3' />
	///  <inputParameter name='ACOTASTAG' hostDataType='number' length='6' />
	///  <inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />
	///  <inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />
	///  <inputParameter name='TNU_REVER' hostDataType='number' length='6' />
	///  <inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />
	///  <inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />
	///  <inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />
	///  <inputParameter name='ACO_CONDB' hostDataType='number' length='2' />
	///  <inputParameter name='ACO_MONDB' hostDataType='number' length='2' />
	///  <inputParameter name='ACU_OFIDB' hostDataType='number' length='3' />
	///  <inputParameter name='ACUNUMCDB' hostDataType='number' length='6' />
	///  <inputParameter name='ACUDIGVDB' hostDataType='number' length='1' />
	///  <inputParameter name='ACO_CONCR' hostDataType='number' length='2' />
	///  <inputParameter name='ACO_MONCR' hostDataType='number' length='2' />
	///  <inputParameter name='ACU_OFICR' hostDataType='number' length='3' />
	///  <inputParameter name='ACUNUMCCR' hostDataType='number' length='6' />
	///  <inputParameter name='ACUDIGVCR' hostDataType='number' length='1' />
	///  <inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />
	///  <inputParameter name='ASECONFIR' hostDataType='alpha' length='1' />
	/// </inputParameters>
	/// </summary>
	public class WS150_Input
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
		[XmlElement(DataType="integer")] public string TNU_ORDEN;
		public string TSE_REVER;
		[XmlElement(DataType="integer")] public string TNU_REVER;
		public string ANU_TARJE;
		[XmlElement(DataType="integer")] public string SCOTIPDOC;
		[XmlElement(DataType="integer")] public string SNU_DOCUM;
		[XmlElement(DataType="integer")] public string ACO_CONDB;
		[XmlElement(DataType="integer")] public string ACO_MONDB;
		[XmlElement(DataType="integer")] public string ACU_OFIDB;
		[XmlElement(DataType="integer")] public string ACUNUMCDB;
		[XmlElement(DataType="integer")] public string ACUDIGVDB;
		[XmlElement(DataType="integer")] public string ACO_CONCR;
		[XmlElement(DataType="integer")] public string ACO_MONCR;
		[XmlElement(DataType="integer")] public string ACU_OFICR;
		[XmlElement(DataType="integer")] public string ACUNUMCCR;
		[XmlElement(DataType="integer")] public string ACUDIGVCR;
		public decimal TVA_MOVIM;
		public string ASECONFIR;
	}
	#endregion

	#region Output Data Types
	public class WS150_Output
	{
		[XmlElement(DataType="integer")] public string DCO_OFICI;
		[XmlElement(DataType="integer")] public string ACOTASTAG;
		[XmlElement(DataType="integer")] public string TNU_ORDEN;
		public string TSE_REVER;
		[XmlElement(DataType="integer")] public string TNU_REVER;
		public string ANU_TARJE;
		[XmlElement(DataType="integer")] public string SCOTIPDOC;
		[XmlElement(DataType="integer")] public string SNU_DOCUM;
		[XmlElement(DataType="integer")] public string ACO_CONDB;
		[XmlElement(DataType="integer")] public string ACO_MONDB;
		[XmlElement(DataType="integer")] public string ACU_OFIDB;
		[XmlElement(DataType="integer")] public string ACUNUMCDB;
		[XmlElement(DataType="integer")] public string ACUDIGVDB;
		[XmlElement(DataType="integer")] public string ACO_CONCR;
		[XmlElement(DataType="integer")] public string ACO_MONCR;
		[XmlElement(DataType="integer")] public string ACU_OFICR;
		[XmlElement(DataType="integer")] public string ACUNUMCCR;
		[XmlElement(DataType="integer")] public string ACUDIGVCR;
		public decimal TVA_MOVIM;
		public string ASECONFIR;
		public string SD_PARTE1;
		[XmlElement(DataType="integer")] public string SD_ENTE1;
		public string SD_PARTE2;
		[XmlElement(DataType="integer")] public string SD_ENTE2;
		[XmlElement(DataType="integer")] public string DCOMENSIS;
		public General_StatusMessages StatusMessages;
	}
	#endregion
}
