using System;

using CEWindowsAPI;

namespace CEdotNetWsInfrastructure
{
	/// <summary>
	/// Summary description for HostError.
	/// </summary>
	internal class HostError
	{
		/// <summary>
		/// Returns a HostSystemException for errors caused by operations 
		/// on the server side.
		/// </summary>
		/// <param name="message">The error message</param>
		/// <param name="detail">Further details of the error</param>
		/// <returns>A HostSystemException</returns>
		public static HostSystemException ServerFault (string message, string detail)
		{
			return new HostSystemException(message, HostSystemFaultCode.Server, detail);
		}

		/// <summary>
		/// Returns a HostSystemException for errors caused by operations 
		/// by the client.
		/// </summary>
		/// <param name="message">The error message</param>
		/// <param name="detail">Further details of the error</param>
		/// <returns>A HostSystemException</returns>
		public static HostSystemException ClientFault (string message, string detail)
		{
			return new HostSystemException(message, HostSystemFaultCode.Client, detail);
		}

		/// <summary>
		/// These are all the Component Enabler response code with their 
		/// associate error message.<br/> 
		/// As long as the return code from CE for any CE related action 
		/// is not 100 or 801, it is assumed that some kind of "abnormal" 
		/// result occurred and an error will be return to the client with 
		/// the details of the CE response code and its message.
		/// </summary>
		/// <param name="errorCode">Component Enabler Error code</param>
		/// <returns>A HostSystemException</returns>
		public static HostSystemException CEServerSideError (ResponseCodes errorCode)
		{
			switch(errorCode)
			{
				case ResponseCodes.OK_LOGIN: // 101
					return ServerFault("The connect succeeded, but the web service is required to login. (CE response code: 101)", "");
				case ResponseCodes.OK_SECURE_LOGIN: // 102
					return ServerFault("The connect succeeded, but the web service is required to do a secure login. (CE response code: 102)", "");
				case ResponseCodes.OK_MORE_LOGIN_INFO: // 103
					return ServerFault("The login has succeeded so far, but the server requires more info to complete the login. (CE response code: 103)", "");
				case ResponseCodes.ERR_NO_SUCH_SYSTEM: // 201
					return ServerFault("The Enterprise Application system name is unknown. (CE response code: 201)", "");
				case ResponseCodes.ERR_CANT_START_SYSTEM: // 202
					return ServerFault("The server could not start the Enterprise Application System. (CE response code: 202)", "");
				case ResponseCodes.ERR_SYSTEM_NOT_RESPONDING: // 203
					return ServerFault("The Enterprise Application Systen is not responding. (CE response code: 203)", "");
				case ResponseCodes.ERR_ACCESS_DENIED: // 204
					return ServerFault("The login id and password are not valid for the Enterprise Application System. (CE response code: 204)", "");
				case ResponseCodes.ERR_ACCESS_DENIED_CLOSING: // 205
					return ServerFault("The login id and password are not valid for the Enterprise Application System and the session is being closed. (CE response code: 205)", "");
				case ResponseCodes.ERR_PROTOCOL_VERSION: // 206
					return ServerFault("The protocol version is not valid. (CE response code: 206)", "");
				case ResponseCodes.ERR_CANT_CHANGE_LANG: // 301
					return ServerFault("The specified language is incorrect. (CE response code: 301)", "");
				case ResponseCodes.ERR_PROTOCOL: // 302
					return ServerFault("The server detects a protocol error. (CE response code: 302)", "");
				case ResponseCodes.ERR_SERVER: // 303
					return ServerFault("An error was detected by the server. (CE response code: 303)", "");
				case ResponseCodes.OK_BYE: // 802
					return ServerFault("The transaction succeeded and the server said Bye. (CE response code: 802)", "");
				case ResponseCodes.ERR_EXCEPTION: // 901
					return ServerFault("An exception occurred when processing the transaction. (CE response code: 901)", "");
				case ResponseCodes.ERR_FATAL_EXCEPTION: // 902
					return ServerFault("A fatal exception occurred when processing the transaction. (CE response code: 902)", "");
				case ResponseCodes.ERR_CLIENT_PROTOCOL: // 903
					return ServerFault("The web service encountered a protocol error talking to the server. (CE response code: 903)", "");
				case ResponseCodes.ERR_CANT_CONNECT: // 904
					return ServerFault("The web service could not connect to the server. (CE response code: 904)", "");
				case ResponseCodes.ERR_TRANSPORT: // 905
					return ServerFault("A communication error occurred. (CE response code: 905)", "");
				case ResponseCodes.ERR_TIMED_OUT: // 906
					return ServerFault("The web service timed out while waiting for the server to respond to a message. (CE response code: 906)", "");
				case ResponseCodes.ERR_CONNECTION_CLOSED: // 907
					return ServerFault("The connection to the server was unexpectedly closed. (CE response code: 907)", "");
				case ResponseCodes.ERR_TRANSACTION_FAILED: // 911
					return ServerFault("The transaction failed. Check the LINCStatus object for error message. (CE response code: 911)", "");
				case ResponseCodes.ERR_TRANSACTION_VETOED: // 912
					return ServerFault("The transaction was vetoed by a transaction listener. (CE response code: 912)", "");
				case ResponseCodes.ERR_APPLICATION_CHANGED: // 913
					return ServerFault("The Ispec that sent the transaction is from different application from the one we are currently connected to. (CE response code: 913)", "");
				case ResponseCodes.ERR_BYE_RETURNED_ISPEC: // 914
					return ServerFault("A BYE message returned an Ispec when we weren't expecting it. (CE response code: 914)", "");
				case ResponseCodes.ERR_ISPEC_DIFFERENT_TYPE: // 915
					return ServerFault("The Ispec returned by a transaction was not of the expected type. (CE response code: 915)", "");
				case ResponseCodes.ERR_LIST_NOT_FOUND: // 916
					return ServerFault("The requested list could not be found. (CE response code: 916)", "");
				case ResponseCodes.ERR_CANT_LOAD_ISPEC: // 921
					return ServerFault("The Ispec specified in loadIspec or the Ispec sent back from the transaction could not be loaded. (CE response code: 921)", "");
				case ResponseCodes.ERR_ISPEC_VERSION: // 925
					return ServerFault("The Ispec specified in loadIspec or the Ispec sent back from the transaction is the wrong version and will not be loaded. (CE response code: 925)", "");
				case ResponseCodes.ERR_PROPERTY_CHANGE_VETOED: // 931
					return ServerFault("An attempt to set a field of an IspecModel was vetoed by a vetoableChangeListener (CE response code: 931)", "");
				case ResponseCodes.ERR_NO_SUCH_FIELD: // 932
					return ServerFault("An attempt to set a field of an IspecModel failed because the field does not exist. (CE response code: 932)", "");
				case ResponseCodes.ERR_BAD_FIELD_VALUE: // 933
					return ServerFault("An attempt to set a field of an IspecModel failed because the new value is bad. (CE response code: 933)", "");
				case ResponseCodes.ERR_MISSING_LOGIN_ATTR: // 934
					return ServerFault("The value of a login attribute is not set. (CE response code: 934)", "");
				default:
					return ServerFault("Unspecified error. (CE response code: " + errorCode + ")", "");
			}
		}

		/// <summary>
		/// These are all the Component Enabler error codes related to 
		/// field exception.
		/// </summary>
		/// <param name="ispecName">Name of the ispec that the field belongs to.</param>
		/// <param name="fieldName">Name of the field causing the error.</param>
		/// <param name="errorCode">Component Enabler Error code.</param>
		/// <returns>A HostSystemException</returns>
		public static HostSystemException CEClientSideError (string ispecName, string fieldName, LINCFieldExceptionReasons errorCode)
		{
			string message = "An attempt to set field '" + fieldName + "' of Ispec '" + ispecName + "' failed because the new value is bad. Check faultdetails for reason. (CE response code: 933)";

			switch(errorCode)
			{
				case LINCFieldExceptionReasons.NOTNUMERIC: // 200
					return ClientFault(message, "The field is not numeric, but you are trying to set it using a numeric method. (CE error code: 200)");
				case LINCFieldExceptionReasons.READONLY: // 205
					return ClientFault(message, "You are trying to set a read-only field. (CE error code: 205)");
				case LINCFieldExceptionReasons.TOOLONG: // 210
					return ClientFault(message, "The data is too long for the field. (CE error code: 210)");
				case LINCFieldExceptionReasons.UNSIGNED: // 215
					return ClientFault(message, "You cannot set an unsigned field to a negative value. (CE error code: 215)");
				case LINCFieldExceptionReasons.NOTNUMBER: // 220
					return ClientFault(message, "The data supplied is not a number. (CE error code: 220)");
				case LINCFieldExceptionReasons.INFINITE: // 225
					return ClientFault(message, "The number supplied is infinite. (CE error code: 225)");
				case LINCFieldExceptionReasons.TOOBIG: // 230
					return ClientFault(message, "The number supplied is too big for the field. (CE error code: 230)");
				case LINCFieldExceptionReasons.INVALIDCHAR: // 235
					return ClientFault(message, "The number supplied contains an invalid character. (CE error code: 235)");
				case LINCFieldExceptionReasons.TOOMANYDECIMALS: // 240
					return ClientFault(message, "There are too many decimal digits. (CE error code: 240)");
				case LINCFieldExceptionReasons.SEPARATOR: // 245
					return ClientFault(message, "The separators are in the wrong position. (CE error code: 245)");
				case LINCFieldExceptionReasons.NONE: // 666
					return ClientFault(message, "No reason given. (CE error code: 666)");
				default:
					return ClientFault(message, "Unspecified error. (CE response code: " + errorCode + ")");
			}
		}
	}
}
