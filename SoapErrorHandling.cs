using System;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;

namespace CEdotNetWsInfrastructure
{
	/// <summary>
	/// Summary description for SoapError.
    /// This class has been modified to allow show CE error on a new node.<br/>
	/// </summary>
	public class SoapError
	{
		public static SoapException SoapFault(string message, HostSystemFaultCode faultCode, string faultDetailsText)
		{
			XmlNode detailNode = null;

            /* HC - 13.08.2014 - Begin
             * These lines are changed to always display the CE error code on a new node */
            // Build the detail element of the Soap fault.
            XmlDocument doc = new XmlDocument();
            detailNode = doc.CreateNode(XmlNodeType.Element,
                SoapException.DetailElementName.Name,
                SoapException.DetailElementName.Namespace);
            /* HC - 13.08.2014 - End */

			if (faultDetailsText != null && faultDetailsText != "")
			{
                /* HC - 13.08.2014 - Begin
                 * These lines are changed to always display the CE error code on a new node */
                /*
				// Build the detail element of the Soap fault.
				XmlDocument doc = new XmlDocument();
				detailNode = doc.CreateNode(XmlNodeType.Element,
					SoapException.DetailElementName.Name,
					SoapException.DetailElementName.Namespace);
                 * HC - 13.08.2014 - End
                */

                // Build specific details for the SoapException.
				// Create child of detail XML element.
				XmlNode faultDetails = doc.CreateNode(XmlNodeType.Element,
					"faultdetails", "");
				faultDetails.InnerText = faultDetailsText;

				// Append the child elements to the detail node.
				detailNode.AppendChild(faultDetails);
			}

            /* HC - 13.08.2014 - Begin
             * These lines are added to always display the CE error code on a new node */
            String CEresponseCode = "";
            int pos = message.IndexOf("CE response code:");

            if (pos > 0)
            {
                CEresponseCode = message.Substring(pos + 18, 3);
            }

            XmlNode errorCode = doc.CreateNode(XmlNodeType.Element,
                "errorcode", "");
            errorCode.InnerText = CEresponseCode;

            // Append the child elements to the detail node.
            detailNode.AppendChild(errorCode);
            /* HC - 13.08.2014 - End */

			XmlQualifiedName soapFaultCode = SoapException.ServerFaultCode;
			if (faultCode == HostSystemFaultCode.Client)
				soapFaultCode = SoapException.ClientFaultCode;

			return new SoapException(message, soapFaultCode, HttpContext.Current.Request.Url.AbsoluteUri, detailNode);
		}
	}
}
