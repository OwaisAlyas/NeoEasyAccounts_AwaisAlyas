using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	public static class AlertMessages
	{
		private const string RESOURCE_FILENAME = "ActionMessages";

		public static string DeleteErrorExistsMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Delete_Error_Exists_Message") as string;
				return resource;
			}
		}
		public static string DeleteErrorMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Delete_Error_Message") as string;
				return resource;
			}
		}
		public static string DeleteErrorTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Delete_Error_Title") as string;
				return resource;
			}
		}
		public static string DeleteSuccessfullMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Delete_Successfull_Message") as string;
				return resource;
			}
		}
		public static string DeleteTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Delete_Title") as string;
				return resource;
			}
		}
		public static string EditErrorMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Edit_Error_Message") as string;
				return resource;
			}
		}
		public static string EditErrorTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Edit_Error_Title") as string;
				return resource;
			}
		}
		public static string EditSuccessfullMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Edit_Successfull_Message") as string;
				return resource;
			}
		}
		public static string ExceptionNullReference
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Exception_NullReference") as string;
				return resource;
			}
		}
		public static string ExceptionSQL
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Exception_SQL") as string;
				return resource;
			}
		}
		public static string GeneralErrorMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "GeneralError_Message") as string;
				return resource;
			}
		}
		public static string GeneralErrorTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "GeneralError_Title") as string;
				return resource;
			}
		}
		public static string LoadErrorMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Load_Error_Message") as string;
				return resource;
			}
		}
		public static string LoadErrorTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Load_Error_Title") as string;
				return resource;
			}
		}
		public static string LoadSuccessMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Load_Success_Message") as string;
				return resource;
			}
		}
		public static string LoadSuccessTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Load_Success_Title") as string;
				return resource;
			}
		}
		public static string NotFoundMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "NotFound_Message") as string;
				return resource;
			}
		}
		public static string NotFoundTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "NotFound_Title") as string;
				return resource;
			}
		}
		public static string SaveErrorExistsMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Save_Error_Exists_Message") as string;
				return resource;
			}
		}
		public static string SaveErrorMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Save_Error_Message") as string;
				return resource;
			}
		}
		public static string SaveErrorTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Save_Error_Title") as string;
				return resource;
			}
		}
		public static string SaveSuccessfullMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Save_Successfull_Message") as string;
				return resource;
			}
		}
		public static string SaveMasterSuccessfullMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "SaveMaster_Successfull_Message") as string;
				return resource;
			}
		}
		public static string SaveErrorDataNotReceived
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Save_Error_DataNotReceived") as string;
				return resource;
			}
		}
		
		public static string SaveTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Save_Title") as string;
				return resource;
			}
		}
		public static string UpdateErrorMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Update_Error_Message") as string;
				return resource;
			}
		}
		public static string UpdateErrorTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Update_Error_Title") as string;
				return resource;
			}
		}
		public static string UpdateSuccessfullMessage
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Update_Successfull_Message") as string;
				return resource;
			}
		}
		public static string UpdateTitle
		{
			get
			{
				var resource = HttpContext.GetGlobalResourceObject(RESOURCE_FILENAME, "Update_Title") as string;
				return resource;
			}
		}
	}
}