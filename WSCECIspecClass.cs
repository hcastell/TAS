using System;
using System.Xml.Serialization;

using CEdotNetWsInfrastructure;

namespace wssfb_Ns
{
    /// <summary>
    /// Summary description for WSCEC.
    /// <methods className='WSCEC' >
    ///  <method  hasInput='true' />
    /// </methods>
    /// </summary>
    public class WSCEC : HostSystem
    {
        public WSCEC()
        {
            // No constructor logic.
        }

        #region WSCEC
        /// <summary>
        /// Web Service method for WSCEC.
        /// </summary>
        /// <param name="inputObj">WSCEC_Input</param>
        /// <returns>WSCEC_Output</returns>
        public virtual WSCEC_Output M_WSCEC(WSCEC_Input inputObj)
        {
            if (inputObj == null)
                this.ThrowHostExceptionClient("Did not receive a 'WSCEC_Input' object.");

            // Create session with the host system.
            this.OpenHostSession();

            // Set the current ispec.
            this.SetCurrentIspec("WSCEC");

            // Set required input fields on current ispec.
            this.SetIspecFieldValue("DCO_OFICI", inputObj.DCO_OFICI, "DCO_OFICI");
            this.SetIspecFieldValue("ACOTASTAG", inputObj.ACOTASTAG, "ACOTASTAG");
            this.SetIspecFieldValue("ANU_TARJE", inputObj.ANU_TARJE, "ANU_TARJE");
            this.SetIspecFieldValue("SCOTIPDOC", inputObj.SCOTIPDOC, "SCOTIPDOC");
            this.SetIspecFieldValue("SNU_DOCUM", inputObj.SNU_DOCUM, "SNU_DOCUM");
            this.SetIspecFieldValue("ACO_CONCE", inputObj.ACO_CONCE, "ACO_CONCE");
            this.SetIspecFieldValue("ACO_MONED", inputObj.ACO_MONED, "ACO_MONED");
            this.SetIspecFieldValue("ACU_OFICI", inputObj.ACU_OFICI, "ACU_OFICI");
            this.SetIspecFieldValue("ACUNUMCUE", inputObj.ACUNUMCUE, "ACUNUMCUE");
            this.SetIspecFieldValue("ACUDIGVER", inputObj.ACUDIGVER, "ACUDIGVER");
            this.SetIspecFieldValue("TFE_TRANS", inputObj.TFE_TRANS, "TFE_TRANS");
            this.SetIspecFieldValue("AFE_VALOR", inputObj.AFE_VALOR, "AFE_VALOR");
            this.SetIspecFieldValue("ACO_TRANS", inputObj.ACO_TRANS, "ACO_TRANS");
            this.SetIspecFieldValue("ACO_CAUSA", inputObj.ACO_CAUSA, "ACO_CAUSA");
            this.SetIspecFieldValue("TFE_MCP", inputObj.TFE_MCP, "TFE_MCP");
            this.SetIspecFieldValue("THO_TRANS", inputObj.THO_TRANS, "THO_TRANS");
            this.SetIspecFieldValue("ACOBASIVA", inputObj.ACOBASIVA, "ACOBASIVA");
            this.SetIspecFieldValue("ANUORDIVA", inputObj.ANUORDIVA, "ANUORDIVA");
            this.SetIspecFieldValue("TNU_TRANS", inputObj.TNU_TRANS, "TNU_TRANS");
            this.SetIspecFieldValue("TVA_MOVIM", inputObj.TVA_MOVIM.ToString(), "TVA_MOVIM");

            // Send transaction to the host.
            this.IspecTransaction();

            // Create the Output object.
            WSCEC_Output outputObj = new WSCEC_Output();

            // Get output fields from current ispec.
            outputObj.DCO_OFICI = this.GetIspecFieldFormattedValue("DCO_OFICI", true);
            outputObj.ACOTASTAG = this.GetIspecFieldFormattedValue("ACOTASTAG", true);
            outputObj.ANU_TARJE = this.GetIspecFieldFormattedValue("ANU_TARJE", false);
            outputObj.SCOTIPDOC = this.GetIspecFieldFormattedValue("SCOTIPDOC", true);
            outputObj.SNU_DOCUM = this.GetIspecFieldFormattedValue("SNU_DOCUM", true);
            outputObj.ACO_CONCE = this.GetIspecFieldFormattedValue("ACO_CONCE", true);
            outputObj.ACO_MONED = this.GetIspecFieldFormattedValue("ACO_MONED", true);
            outputObj.ACU_OFICI = this.GetIspecFieldFormattedValue("ACU_OFICI", true);
            outputObj.ACUNUMCUE = this.GetIspecFieldFormattedValue("ACUNUMCUE", true);
            outputObj.ACUDIGVER = this.GetIspecFieldFormattedValue("ACUDIGVER", true);
            outputObj.TFE_TRANS = this.GetIspecFieldFormattedValue("TFE_TRANS", true);
            outputObj.AFE_VALOR = this.GetIspecFieldFormattedValue("AFE_VALOR", true);
            outputObj.ACO_TRANS = this.GetIspecFieldFormattedValue("ACO_TRANS", true);
            outputObj.ACO_CAUSA = this.GetIspecFieldFormattedValue("ACO_CAUSA", true);
            outputObj.TFE_MCP = this.GetIspecFieldFormattedValue("TFE_MCP", true);
            outputObj.THO_TRANS = this.GetIspecFieldFormattedValue("THO_TRANS", true);
            outputObj.ACOBASIVA = this.GetIspecFieldFormattedValue("ACOBASIVA", true);
            outputObj.ANUORDIVA = this.GetIspecFieldFormattedValue("ANUORDIVA", true);
            outputObj.TNU_TRANS = this.GetIspecFieldFormattedValue("TNU_TRANS", true);
            outputObj.TVA_MOVIM = Convert.ToDecimal(this.GetIspecFieldFormattedValue("TVA_MOVIM", true));
            outputObj.DCOMENSIS = this.GetIspecFieldFormattedValue("DCOMENSIS", true);

            // Create Copy-From line items output.
            int lineItemsCount = 30;
            WSCEC_Output_LineItems[] myLineItems = new WSCEC_Output_LineItems[lineItemsCount];
            for (int row = 0; row < lineItemsCount; row++)
            {
                WSCEC_Output_LineItems myLineItem = new WSCEC_Output_LineItems();
                myLineItem.TFE_TRAN1 = this.GetIspecFieldFormattedValue("TFE_TRAN1", row, true);
                myLineItem.AFE_VALO1 = this.GetIspecFieldFormattedValue("AFE_VALO1", row, true);
                myLineItem.ACO_TRAN1 = this.GetIspecFieldFormattedValue("ACO_TRAN1", row, true);
                myLineItem.ACO_CAU01 = this.GetIspecFieldFormattedValue("ACO_CAU01", row, true);
                myLineItem.DNOTRAABR = this.GetIspecFieldFormattedValue("DNOTRAABR", row, false);
                myLineItem.TFE_MCP1 = this.GetIspecFieldFormattedValue("TFE_MCP1", row, true);
                myLineItem.THO_TRAN1 = this.GetIspecFieldFormattedValue("THO_TRAN1", row, true);
                myLineItem.ACOBASIV1 = this.GetIspecFieldFormattedValue("ACOBASIV1", row, true);
                myLineItem.ANUORDIV1 = this.GetIspecFieldFormattedValue("ANUORDIV1", row, true);
                myLineItem.TNU_TRAN1 = this.GetIspecFieldFormattedValue("TNU_TRAN1", row, true);
                myLineItem.TVA_MOVI2 = this.GetIspecFieldFormattedValue("TVA_MOVI2", row, false);
                myLineItem.ACOTIPDOC = this.GetIspecFieldFormattedValue("ACOTIPDOC", row, false);
                myLineItem.ANUDOCDEP = this.GetIspecFieldFormattedValue("ANUDOCDEP", row, true);
                myLineItem.ANODEPOSI = this.GetIspecFieldFormattedValue("ANODEPOSI", row, false);
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
    /// <inputParameters className='WSCEC_Input' >
    ///  <inputParameter name='DCO_OFICI' hostDataType='number' length='3' />
    ///  <inputParameter name='ACOTASTAG' hostDataType='number' length='6' />
    ///  <inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />
    ///  <inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />
    ///  <inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />
    ///  <inputParameter name='ACO_CONCE' hostDataType='number' length='2' />
    ///  <inputParameter name='ACO_MONED' hostDataType='number' length='2' />
    ///  <inputParameter name='ACU_OFICI' hostDataType='number' length='3' />
    ///  <inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />
    ///  <inputParameter name='ACUDIGVER' hostDataType='number' length='1' />
    ///  <inputParameter name='TFE_TRANS' hostDataType='number' length='6' />
    ///  <inputParameter name='AFE_VALOR' hostDataType='number' length='6' />
    ///  <inputParameter name='ACO_TRANS' hostDataType='number' length='3' />
    ///  <inputParameter name='ACO_CAUSA' hostDataType='number' length='3' />
    ///  <inputParameter name='TFE_MCP' hostDataType='number' length='8' />
    ///  <inputParameter name='THO_TRANS' hostDataType='number' length='8' />
    ///  <inputParameter name='ACOBASIVA' hostDataType='number' length='2' />
    ///  <inputParameter name='ANUORDIVA' hostDataType='number' length='2' />
    ///  <inputParameter name='TNU_TRANS' hostDataType='number' length='6' />
    ///  <inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />
    /// </inputParameters>
    /// </summary>
    public class WSCEC_Input
    {
        [XmlElement(DataType = "integer")]
        public string DCO_OFICI;
        [XmlElement(DataType = "integer")]
        public string ACOTASTAG;
        public string ANU_TARJE;
        [XmlElement(DataType = "integer")]
        public string SCOTIPDOC;
        [XmlElement(DataType = "integer")]
        public string SNU_DOCUM;
        [XmlElement(DataType = "integer")]
        public string ACO_CONCE;
        [XmlElement(DataType = "integer")]
        public string ACO_MONED;
        [XmlElement(DataType = "integer")]
        public string ACU_OFICI;
        [XmlElement(DataType = "integer")]
        public string ACUNUMCUE;
        [XmlElement(DataType = "integer")]
        public string ACUDIGVER;
        [XmlElement(DataType = "integer")]
        public string TFE_TRANS;
        [XmlElement(DataType = "integer")]
        public string AFE_VALOR;
        [XmlElement(DataType = "integer")]
        public string ACO_TRANS;
        [XmlElement(DataType = "integer")]
        public string ACO_CAUSA;
        [XmlElement(DataType = "integer")]
        public string TFE_MCP;
        [XmlElement(DataType = "integer")]
        public string THO_TRANS;
        [XmlElement(DataType = "integer")]
        public string ACOBASIVA;
        [XmlElement(DataType = "integer")]
        public string ANUORDIVA;
        [XmlElement(DataType = "integer")]
        public string TNU_TRANS;
        public decimal TVA_MOVIM;
    }
    #endregion

    #region Output Data Types
    public class WSCEC_Output
    {
        [XmlElement(DataType = "integer")]
        public string DCO_OFICI;
        [XmlElement(DataType = "integer")]
        public string ACOTASTAG;
        public string ANU_TARJE;
        [XmlElement(DataType = "integer")]
        public string SCOTIPDOC;
        [XmlElement(DataType = "integer")]
        public string SNU_DOCUM;
        [XmlElement(DataType = "integer")]
        public string ACO_CONCE;
        [XmlElement(DataType = "integer")]
        public string ACO_MONED;
        [XmlElement(DataType = "integer")]
        public string ACU_OFICI;
        [XmlElement(DataType = "integer")]
        public string ACUNUMCUE;
        [XmlElement(DataType = "integer")]
        public string ACUDIGVER;
        [XmlElement(DataType = "integer")]
        public string TFE_TRANS;
        [XmlElement(DataType = "integer")]
        public string AFE_VALOR;
        [XmlElement(DataType = "integer")]
        public string ACO_TRANS;
        [XmlElement(DataType = "integer")]
        public string ACO_CAUSA;
        [XmlElement(DataType = "integer")]
        public string TFE_MCP;
        [XmlElement(DataType = "integer")]
        public string THO_TRANS;
        [XmlElement(DataType = "integer")]
        public string ACOBASIVA;
        [XmlElement(DataType = "integer")]
        public string ANUORDIVA;
        [XmlElement(DataType = "integer")]
        public string TNU_TRANS;
        public decimal TVA_MOVIM;
        [XmlElement(DataType = "integer")]
        public string DCOMENSIS;
        [XmlElement(ElementName = "LineItems")]
        public WSCEC_Output_LineItems[] LineItems;
        public General_StatusMessages StatusMessages;
    }

    public class WSCEC_Output_LineItems
    {
        [XmlElement(DataType = "integer")]
        public string TFE_TRAN1;
        [XmlElement(DataType = "integer")]
        public string AFE_VALO1;
        [XmlElement(DataType = "integer")]
        public string ACO_TRAN1;
        [XmlElement(DataType = "integer")]
        public string ACO_CAU01;
        public string DNOTRAABR;
        [XmlElement(DataType = "integer")]
        public string TFE_MCP1;
        [XmlElement(DataType = "integer")]
        public string THO_TRAN1;
        [XmlElement(DataType = "integer")]
        public string ACOBASIV1;
        [XmlElement(DataType = "integer")]
        public string ANUORDIV1;
        [XmlElement(DataType = "integer")]
        public string TNU_TRAN1;
        public string TVA_MOVI2;
        public string ACOTIPDOC;
        [XmlElement(DataType = "integer")]
        public string ANUDOCDEP;
        public string ANODEPOSI;
    }
    #endregion
}
