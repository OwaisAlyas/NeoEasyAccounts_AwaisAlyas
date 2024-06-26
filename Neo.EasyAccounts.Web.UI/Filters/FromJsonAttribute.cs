﻿using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Neo.EasyAccounts.Web.UI.Filters
{
	public sealed class FromJsonAttribute : CustomModelBinderAttribute
	{
		private readonly static JavaScriptSerializer serializer = new JavaScriptSerializer();

		public override IModelBinder GetBinder()
		{
			return new JsonModelBinder();
		}

		private class JsonModelBinder : IModelBinder
		{
			public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
			{
				var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
				if (string.IsNullOrEmpty(stringified))
					return null;
				return serializer.Deserialize(stringified, bindingContext.ModelType);
			}
		}
	}
}