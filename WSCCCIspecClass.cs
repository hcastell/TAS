using System;
using System.IO;
using System.Xml.Serialization;
using System.Text;

using CEdotNetWsInfrastructure;

namespace wssfb_Ns
{
    /// <summary>
    /// Summary description for WSCCC.
    /// <methods className='WSCCC' >
    ///  <method  hasInput='true' />
    /// </methods>
    /// </summary>
    public class WSCCC : HostSystem
    {
        public WSCCC()
        {
            // No constructor logic.
        }

        #region WSCCC
        /// <summary>
        /// Web Service method for WSCCC.
        /// </summary>
        /// <param name="inputObj">WSCCC_Input</param>
        /// <returns>WSCCC_Output</returns>
        public virtual WSCCC_Output M_WSCCC(WSCCC_Input inputObj)
        {
            int row = 0, aco_conce = 0, index, lastrow, backnumber = 1;
            bool next = true;

            if (inputObj == null)
                this.ThrowHostExceptionClient("Did not receive a 'WSCCC_Input' object.");

            // Create session with the host system.
            this.OpenHostSession();

            // Set the current ispec.
            this.SetCurrentIspec("WSCCC");

            // Create the Output object.
            WSCCC_Output outputObj = new WSCCC_Output();

            // Create Copy-From object, ¡999 lines max!.
            WSCCC_Output_LineItems[] myLineItems = new WSCCC_Output_LineItems[999];

            do
            {
                // Set required input fields on current ispec.
                this.SetIspecFieldValue("DCO_OFICI", inputObj.DCO_OFICI, "DCO_OFICI"); // 500
                this.SetIspecFieldValue("ACOTASTAG", inputObj.ACOTASTAG, "ACOTASTAG"); // 2 
                this.SetIspecFieldValue("ANU_TARJE", inputObj.ANU_TARJE, "ANU_TARJE"); // 4100820149007009
                this.SetIspecFieldValue("SCOTIPDOC", inputObj.SCOTIPDOC, "SCOTIPDOC");
                this.SetIspecFieldValue("SNU_DOCUM", inputObj.SNU_DOCUM, "SNU_DOCUM");

                // Send transaction to the host.
                this.IspecTransaction();

                // Get output fields from current ispec.
                outputObj.DCO_OFICI = this.GetIspecFieldFormattedValue("DCO_OFICI", true);
                outputObj.ACOTASTAG = this.GetIspecFieldFormattedValue("ACOTASTAG", true);
                outputObj.ANU_TARJE = this.GetIspecFieldFormattedValue("ANU_TARJE", false);
                outputObj.SCOTIPDOC = this.GetIspecFieldFormattedValue("SCOTIPDOC", true);
                outputObj.SNU_DOCUM = this.GetIspecFieldFormattedValue("SNU_DOCUM", true);
                outputObj.DCOMENSIS = this.GetIspecFieldFormattedValue("DCOMENSIS", true);

                // Create Copy-From line items output.
                int lineItemsCount = 15;
                index = lineItemsCount * backnumber - lineItemsCount;

                for (row = 0; row < lineItemsCount; row++)
                {
                    WSCCC_Output_LineItems myLineItem = new WSCCC_Output_LineItems();
                    myLineItem.ACO_CONCE = this.GetIspecFieldFormattedValue("ACO_CONCE", row, true);
                    myLineItem.ACO_MONED = this.GetIspecFieldFormattedValue("ACO_MONED", row, true);
                    myLineItem.ACU_OFICI = this.GetIspecFieldFormattedValue("ACU_OFICI", row, true);
                    myLineItem.ACU_PRODU = this.GetIspecFieldFormattedValue("ACU_PRODU", row, true);
                    myLineItem.WCUNUMCUE = this.GetIspecFieldFormattedValue("WCUNUMCUE", row, true);
                    myLineItem.ACUDIGVER = this.GetIspecFieldFormattedValue("ACUDIGVER", row, true);
                    myLineItem.TNUCUENTA = this.GetIspecFieldFormattedValue("TNUCUENTA", row, true);
                    myLineItem.TSE_CONSU = this.GetIspecFieldFormattedValue("TSE_CONSU", row, false);
                    myLineItem.TSEDEBITO = this.GetIspecFieldFormattedValue("TSEDEBITO", row, false);
                    myLineItem.TSECREEFE = this.GetIspecFieldFormattedValue("TSECREEFE", row, false);
                    myLineItem.TSECRECHE = this.GetIspecFieldFormattedValue("TSECRECHE", row, false);
                    myLineItem.TSETRANSF = this.GetIspecFieldFormattedValue("TSETRANSF", row, false);
                    myLineItem.ALF01DE22 = this.GetIspecFieldFormattedValue("ALF01DE22", row, false);
                    myLineItem.CSA_DISPO = this.GetIspecFieldFormattedValue("CSA_DISPO", row, false);
                    myLineItem.CSA_CONFO = this.GetIspecFieldFormattedValue("CSA_CONFO", row, false);
                    myLineItem.CSAEFEHOY = this.GetIspecFieldFormattedValue("CSAEFEHOY", row, false);
                    myLineItem.CSAFLOH2 = this.GetIspecFieldFormattedValue("CSAFLOH2", row, false);
                    myLineItem.CSAFLOH1 = this.GetIspecFieldFormattedValue("CSAFLOH1", row, false);
                    myLineItem.CSAFLOE1 = this.GetIspecFieldFormattedValue("CSAFLOE1", row, false);
                    myLineItem.WNO_CLIE1 = this.GetIspecFieldFormattedValue("WNO_CLIE1", row, false);
                    myLineItem.WNO_CLIE2 = this.GetIspecFieldFormattedValue("WNO_CLIE2", row, false);
                    myLineItem.WNO_CLIE3 = this.GetIspecFieldFormattedValue("WNO_CLIE3", row, false);
                    myLineItem.CNU_CBUP1 = this.GetIspecFieldFormattedValue("CNU_CBUP1", row, false);
                    myLineItem.CNU_CBUP2 = this.GetIspecFieldFormattedValue("CNU_CBUP2", row, false);
                    myLineItem.FFE_VENCI = this.GetIspecFieldFormattedValue("FFE_VENCI", row, true);
                    myLineItem.SFECIERRE = this.GetIspecFieldFormattedValue("SFECIERRE", row, true);

                    // Set 0+ when the field is null or empty
                    if (String.IsNullOrEmpty(myLineItem.CSA_DISPO)) { myLineItem.CSA_DISPO = "00+"; }
                    if (String.IsNullOrEmpty(myLineItem.CSA_CONFO)) { myLineItem.CSA_CONFO = "00+"; }
                    if (String.IsNullOrEmpty(myLineItem.CSAEFEHOY)) { myLineItem.CSAEFEHOY = "00+"; }
                    if (String.IsNullOrEmpty(myLineItem.CSAFLOH2)) { myLineItem.CSAFLOH2 = "00+"; }
                    if (String.IsNullOrEmpty(myLineItem.CSAFLOH1)) { myLineItem.CSAFLOH1 = "00+"; }
                    if (String.IsNullOrEmpty(myLineItem.CSAFLOE1)) { myLineItem.CSAFLOE1 = "00+"; }

                    // No errors
                    string dcomensis = outputObj.DCOMENSIS.Trim();

                    if (dcomensis == "0" || dcomensis == "0000" || dcomensis == "")
                    {
                        // Exclude empty lines
                        string conce = myLineItem.ACO_CONCE.Trim();

                        if (conce != "0" && conce != "00" && conce != "")
                        {
                            myLineItems[index] = myLineItem;
                            index++;
                        }
                    }
                    else
                    {
                        myLineItems[index] = myLineItem;
                        break;
                    }
                }

                // Get concepto
                lastrow = row - 1;
                aco_conce = Convert.ToUInt16(this.GetIspecFieldFormattedValue("ACO_CONCE", lastrow, true));

                switch (aco_conce)
                {
                    case 0: // no hay mas registros
                        next = false;
                        break;
                    case 1:
                    case 2: //1 = Cuenta Corriente 2 = Caja de Ahorro
                        this.SetIspecFieldValue("VCO_CONCE", this.GetIspecFieldFormattedValue("ACO_CONCE", lastrow, true), "VCO_CONCE");
                        this.SetIspecFieldValue("TCO_PRODU", this.GetIspecFieldFormattedValue("ACU_PRODU", lastrow, true), "TCO_PRODU");
                        this.SetIspecFieldValue("TCO_MONED", this.GetIspecFieldFormattedValue("ACO_MONED", lastrow, true), "TCO_MONED");
                        this.SetIspecFieldValue("TCU_OFIC1", this.GetIspecFieldFormattedValue("ACU_OFICI", lastrow, true), "TCU_OFIC1");
                        this.SetIspecFieldValue("TCUNUMCU1", Convert.ToString(Convert.ToUInt32(this.GetIspecFieldFormattedValue("WCUNUMCUE", lastrow, true)) + 1), "TCUNUMCU1");
                        this.SetIspecFieldValue("TNU_OPERA", "0", "TNU_OPERA");
                        this.SetIspecFieldValue("SREOTRCUE", " ", "SREOTRCUE");
                        break;
                    case 4: // Plazo Fijo
                        this.SetIspecFieldValue("VCO_CONCE", this.GetIspecFieldFormattedValue("ACO_CONCE", lastrow, true), "VCO_CONCE");
                        this.SetIspecFieldValue("TCO_PRODU", this.GetIspecFieldFormattedValue("ACU_PRODU", lastrow, true), "TCO_PRODU");
                        this.SetIspecFieldValue("TCO_MONED", this.GetIspecFieldFormattedValue("ACO_MONED", lastrow, true), "TCO_MONED");
                        this.SetIspecFieldValue("TCU_OFIC1", this.GetIspecFieldFormattedValue("ACU_OFICI", lastrow, true), "TCU_OFIC1");
                        this.SetIspecFieldValue("TCUNUMCU1", Convert.ToString(Convert.ToUInt32(this.GetIspecFieldFormattedValue("TNUCUENTA", lastrow, true)) + 1), "TCUNUMCU1");
                        this.SetIspecFieldValue("TNU_OPERA", this.GetIspecFieldFormattedValue("WCUNUMCUE", lastrow, true), "TNU_OPERA");
                        this.SetIspecFieldValue("SREOTRCUE", " ", "SREOTRCUE");
                        break;
                    case 25: // Tarjeta de Crédito
                        this.SetIspecFieldValue("VCO_CONCE", this.GetIspecFieldFormattedValue("ACO_CONCE", lastrow, true), "VCO_CONCE");
                        this.SetIspecFieldValue("TCO_PRODU", this.GetIspecFieldFormattedValue("ACU_PRODU", lastrow, true), "TCO_PRODU");
                        this.SetIspecFieldValue("TCO_MONED", this.GetIspecFieldFormattedValue("ACO_MONED", lastrow, true), "TCO_MONED");
                        this.SetIspecFieldValue("TCU_OFIC1", this.GetIspecFieldFormattedValue("ACU_OFICI", lastrow, true), "TCU_OFIC1");
                        this.SetIspecFieldValue("TCUNUMCU1", "0", "TCUNUMCU1");
                        this.SetIspecFieldValue("TNU_OPERA", "0", "TNU_OPERA");
                        this.SetIspecFieldValue("SREOTRCUE", Convert.ToString(Convert.ToUInt64(this.GetIspecFieldFormattedValue("ALF01DE22", lastrow, false)) + 1), "SREOTRCUE");
                        break;
                }

                backnumber++;

            } while (next);

            // Set line items to output object
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
    /// <inputParameters className='WSCCC_Input' >
    ///  <inputParameter name='DCO_OFICI' hostDataType='number' length='3' />
    ///  <inputParameter name='ACOTASTAG' hostDataType='number' length='6' />
    ///  <inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />
    ///  <inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />
    ///  <inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />
    ///  <inputParameter name='VCO_CONCE' hostDataType='number' length='2' />
    ///  <inputParameter name='TCO_PRODU' hostDataType='number' length='3' />
    ///  <inputParameter name='TCO_MONED' hostDataType='number' length='2' />
    ///  <inputParameter name='TCU_OFIC1' hostDataType='number' length='3' />
    ///  <inputParameter name='TCUNUMCU1' hostDataType='number' length='6' />
    ///  <inputParameter name='TNU_OPERA' hostDataType='number' length='8' />
    ///  <inputParameter name='SREOTRCUE' hostDataType='alpha' length='22' />
    /// </inputParameters>
    /// </summary>
    public class WSCCC_Input
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
    }
    #endregion

    #region Output Data Types
    public class WSCCC_Output
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
        public string DCOMENSIS;
        [XmlElement(ElementName = "LineItems")]
        public WSCCC_Output_LineItems[] LineItems;
        public General_StatusMessages StatusMessages;
    }

    public class WSCCC_Output_LineItems
    {
        [XmlElement(DataType = "integer")]
        public string ACO_CONCE;
        [XmlElement(DataType = "integer")]
        public string ACO_MONED;
        [XmlElement(DataType = "integer")]
        public string ACU_OFICI;
        [XmlElement(DataType = "integer")]
        public string ACU_PRODU;
        [XmlElement(DataType = "integer")]
        public string WCUNUMCUE;
        [XmlElement(DataType = "integer")]
        public string ACUDIGVER;
        [XmlElement(DataType = "integer")]
        public string TNUCUENTA;
        public string TSE_CONSU;
        public string TSEDEBITO;
        public string TSECREEFE;
        public string TSECRECHE;
        public string TSETRANSF;
        public string ALF01DE22;
        public string CSA_DISPO;
        public string CSA_CONFO;
        public string CSAEFEHOY;
        public string CSAFLOH2;
        public string CSAFLOH1;
        public string CSAFLOE1;
        public string WNO_CLIE1;
        public string WNO_CLIE2;
        public string WNO_CLIE3;
        public string CNU_CBUP1;
        public string CNU_CBUP2;
        [XmlElement(DataType = "integer")]
        public string FFE_VENCI;
        [XmlElement(DataType = "integer")]
        public string SFECIERRE;
    }
    #endregion
}
