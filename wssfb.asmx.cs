using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

using CEdotNetWsInfrastructure;

namespace wssfb_Ns
{
	/// <summary>
	/// Summary description for wssfb.
    /// Prueba Git3
	/// </summary>
	[WebService(Namespace="http://www.wssfb.com/",
		Description="sfb system wssfb web services.")]
	[SoapDocumentService(RoutingStyle=SoapServiceRoutingStyle.RequestElement)]
	public class wssfb : System.Web.Services.WebService
	{
		public wssfb()
		{
			// Constructor. This is intentionally left blank.
		}

		#region WS401
		[WebMethod(Description="Web Service method for WS401." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />" +
			"<inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />" +
			"<inputParameter name='TNU_REVER' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='ACO_CONCE' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONED' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVER' hostDataType='number' length='1' />" +
			"<inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />" +
			"</inputParameters>")]
		public virtual WS401_Output WS401(
			[XmlElement(ElementName="WS401Request")] WS401_Input inputObj)
		{
			WS401 WS401_Obj = new WS401();

			try
			{
				return WS401_Obj.M_WS401(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WSCCL
		[WebMethod(Description="Web Service method for WSCCL." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"</inputParameters>")]
		public virtual WSCCL_Output WSCCL(
			[XmlElement(ElementName="WSCCLRequest")] WSCCL_Input inputObj)
		{
			WSCCL WSCCL_Obj = new WSCCL();

			try
			{
				return WSCCL_Obj.M_WSCCL(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WSCEC
		[WebMethod(Description="Web Service method for WSCEC." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='ACO_CONCE' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONED' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVER' hostDataType='number' length='1' />" +
			"<inputParameter name='TFE_TRANS' hostDataType='number' length='6' />" +
			"<inputParameter name='AFE_VALOR' hostDataType='number' length='6' />" +
			"<inputParameter name='ACO_TRANS' hostDataType='number' length='3' />" +
			"<inputParameter name='ACO_CAUSA' hostDataType='number' length='3' />" +
			"<inputParameter name='TFE_MCP' hostDataType='number' length='8' />" +
			"<inputParameter name='THO_TRANS' hostDataType='number' length='8' />" +
			"<inputParameter name='ACOBASIVA' hostDataType='number' length='2' />" +
			"<inputParameter name='ANUORDIVA' hostDataType='number' length='2' />" +
			"<inputParameter name='TNU_TRANS' hostDataType='number' length='6' />" +
			"<inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />" +
			"</inputParameters>")]
		public virtual WSCEC_Output WSCEC(
			[XmlElement(ElementName="WSCECRequest")] WSCEC_Input inputObj)
		{
			WSCEC WSCEC_Obj = new WSCEC();

			try
			{
				return WSCEC_Obj.M_WSCEC(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WS201
		[WebMethod(Description="Web Service method for WS201." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />" +
			"<inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />" +
			"<inputParameter name='TNU_REVER' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='ACO_CONCE' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONED' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVER' hostDataType='number' length='1' />" +
			"<inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />" +
			"<inputParameter name='CCNCHEING' hostDataType='number' length='3' />" +
			"<inputParameter name='ANU_SOBRE' hostDataType='number' length='5' />" +
			"<inputParameter name='ASECONFIR' hostDataType='alpha' length='1' />" +
			"</inputParameters>")]
		public virtual WS201_Output WS201(
			[XmlElement(ElementName="WS201Request")] WS201_Input inputObj)
		{
			WS201 WS201_Obj = new WS201();

			try
			{
				return WS201_Obj.M_WS201(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WS200
		[WebMethod(Description="Web Service method for WS200." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />" +
			"<inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />" +
			"<inputParameter name='TNU_REVER' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='ACO_CONCE' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONED' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVER' hostDataType='number' length='1' />" +
			"<inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />" +
			"<inputParameter name='ANU_SOBRE' hostDataType='number' length='5' />" +
			"<inputParameter name='ASECONFIR' hostDataType='alpha' length='1' />" +
			"</inputParameters>")]
		public virtual WS200_Output WS200(
			[XmlElement(ElementName="WS200Request")] WS200_Input inputObj)
		{
			WS200 WS200_Obj = new WS200();

			try
			{
				return WS200_Obj.M_WS200(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WSCCC
		[WebMethod(Description="Web Service method for WSCCC." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"</inputParameters>")]
		public virtual WSCCC_Output WSCCC(
			[XmlElement(ElementName="WSCCCRequest")] WSCCC_Input inputObj)
		{
			WSCCC WSCCC_Obj = new WSCCC();

			try
			{
				return WSCCC_Obj.M_WSCCC(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WS150
		[WebMethod(Description="Web Service method for WS150." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />" +
			"<inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />" +
			"<inputParameter name='TNU_REVER' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='ACO_CONDB' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONDB' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFIDB' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCDB' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVDB' hostDataType='number' length='1' />" +
			"<inputParameter name='ACO_CONCR' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONCR' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICR' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCCR' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVCR' hostDataType='number' length='1' />" +
			"<inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />" +
			"<inputParameter name='ASECONFIR' hostDataType='alpha' length='1' />" +
			"</inputParameters>")]
		public virtual WS150_Output WS150(
			[XmlElement(ElementName="WS150Request")] WS150_Input inputObj)
		{
			WS150 WS150_Obj = new WS150();

			try
			{
				return WS150_Obj.M_WS150(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WS402
		[WebMethod(Description="Web Service method for WS402." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />" +
			"<inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />" +
			"<inputParameter name='TNU_REVER' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='ACO_CONCE' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONED' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVER' hostDataType='number' length='1' />" +
			"<inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />" +
			"<inputParameter name='CCNCHEING' hostDataType='number' length='3' />" +
			"<inputParameter name='CCNSERVIC' hostDataType='number' length='2' />" +
			"<inputParameter name='ANU_SOBRE' hostDataType='number' length='5' />" +
			"<inputParameter name='ASECONFIR' hostDataType='alpha' length='1' />" +
			"</inputParameters>")]
		public virtual WS402_Output WS402(
			[XmlElement(ElementName="WS402Request")] WS402_Input inputObj)
		{
			WS402 WS402_Obj = new WS402();

			try
			{
				return WS402_Obj.M_WS402(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WSTTC
		[WebMethod(Description="Web Service method for WSTTC." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />" +
			"<inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />" +
			"<inputParameter name='TNU_REVER' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='TNU_TARJE' hostDataType='alpha' length='16' />" +
			"<inputParameter name='ACOTIPUTI' hostDataType='number' length='1' />" +
			"<inputParameter name='TNUCUENTA' hostDataType='number' length='10' />" +
			"<inputParameter name='ACO_CONCE' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONED' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVER' hostDataType='number' length='1' />" +
			"<inputParameter name='TVA_MOVIM' hostDataType='number' length='14' fractionDigits='2' />" +
			"<inputParameter name='CCNCHEING' hostDataType='number' length='3' />" +
			"<inputParameter name='ANU_SOBRE' hostDataType='number' length='5' />" +
			"<inputParameter name='ASECONFIR' hostDataType='alpha' length='1' />" +
			"</inputParameters>")]
		public virtual WSTTC_Output WSTTC(
			[XmlElement(ElementName="WSTTCRequest")] WSTTC_Input inputObj)
		{
			WSTTC WSTTC_Obj = new WSTTC();

			try
			{
				return WSTTC_Obj.M_WSTTC(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

		#region WSTAT
		[WebMethod(Description="Web Service method for WSTAT." +
			"<inputParameters>" +
			"<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
			"<inputParameter name='TNU_ORDEN' hostDataType='number' length='6' />" +
			"<inputParameter name='TSE_REVER' hostDataType='alpha' length='1' />" +
			"<inputParameter name='TNU_REVER' hostDataType='number' length='6' />" +
			"<inputParameter name='ANU_TARJE' hostDataType='alpha' length='19' />" +
			"<inputParameter name='SCOTIPDOC' hostDataType='number' length='2' />" +
			"<inputParameter name='SNU_DOCUM' hostDataType='number' length='12' />" +
			"<inputParameter name='TNU_TARJE' hostDataType='alpha' length='16' />" +
			"<inputParameter name='ACO_CONCE' hostDataType='number' length='2' />" +
			"<inputParameter name='ACO_MONED' hostDataType='number' length='2' />" +
			"<inputParameter name='ACU_OFICI' hostDataType='number' length='3' />" +
			"<inputParameter name='ACUNUMCUE' hostDataType='number' length='6' />" +
			"<inputParameter name='ACUDIGVER' hostDataType='number' length='1' />" +
			"<inputParameter name='TSETIPASO' hostDataType='alpha' length='1' />" +
			"</inputParameters>")]
		public virtual WSTAT_Output WSTAT(
			[XmlElement(ElementName="WSTATRequest")] WSTAT_Input inputObj)
		{
			WSTAT WSTAT_Obj = new WSTAT();

			try
			{
				return WSTAT_Obj.M_WSTAT(inputObj);
			}
			catch (HostSystemException e)
			{
				throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
			}
			catch (Exception e)
			{
				throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
			}
		}
		#endregion

        #region WSECO
        [WebMethod(Description = "Web Service method for WSECO." +
            "<inputParameters>" +
            "<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
            "<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
            "</inputParameters>")]
        public virtual WSECO_Output WSECO(
            [XmlElement(ElementName = "WSECORequest")] WSECO_Input inputObj)
        {
            WSECO WSECO_Obj = new WSECO();

            try
            {
                return WSECO_Obj.M_WSECO(inputObj);
            }
            catch (HostSystemException e)
            {
                throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
            }
            catch (Exception e)
            {
                throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
            }
        }
        #endregion

        #region WSCCF
        [WebMethod(Description = "Web Service method for WSCCF." +
            "<inputParameters>" +
            "<inputParameter name='DCO_OFICI' hostDataType='number' length='3' />" +
            "<inputParameter name='ACOTASTAG' hostDataType='number' length='6' />" +
            "</inputParameters>")]
        public virtual WSCCF_Output WSCCF(
            [XmlElement(ElementName = "WSCCFRequest")] WSCCF_Input inputObj)
        {
            WSCCF WSCCF_Obj = new WSCCF();

            try
            {
                return WSCCF_Obj.M_WSCCF(inputObj);
            }
            catch (HostSystemException e)
            {
                throw SoapError.SoapFault(e.Message, e.FaultCode, e.Detail);
            }
            catch (Exception e)
            {
                throw SoapError.SoapFault(e.Message, HostSystemFaultCode.Server, e.StackTrace);
            }
        }
        #endregion
	}
}
