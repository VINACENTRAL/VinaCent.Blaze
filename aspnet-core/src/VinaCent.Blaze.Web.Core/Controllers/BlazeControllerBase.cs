using System;
using System.Collections.Generic;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Json;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using VinaCent.Blaze.Models;

namespace VinaCent.Blaze.Controllers
{
    public abstract class BlazeControllerBase: AbpController
    {
        private const string SavedNotificationListKey = "vinacent.com_toastr_saved_notifications";
        protected BlazeControllerBase()
        {
            LocalizationSourceName = BlazeConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        
        protected void AddSuccessNotify(string message, string title = "", object options = null)
        {
            AddNotifyMessage("success", message, title, options);
        }

        protected void AddErrorNotify(string message, string title = "", object options = null)
        {
            AddNotifyMessage("error", message, title, options);
        }

        protected void AddPrimaryNotify(string message, string title = "", object options = null)
        {
            AddNotifyMessage("primary", message, title, options);
        }

        protected void AddInfoNotify(string message, string title = "", object options = null)
        {
            AddNotifyMessage("info", message, title, options);
        }

        private void AddNotifyMessage(string type, string message, string title, object options)
        {
            Request.Cookies.TryGetValue(SavedNotificationListKey, out var raw);
            var notifyStack = new List<NotifyModel>();
            
            if (!raw.IsNullOrWhiteSpace())
            {
                notifyStack.AddRange(JsonConvert.DeserializeObject<NotifyModel[]>(raw ?? "") ?? Array.Empty<NotifyModel>());
            }
            
            notifyStack.Add(new NotifyModel
            {
                Type = type,
                Message = message,
                Title = title,
                Options = options
            });
            
            // Remove old
            Response.Cookies.Delete(SavedNotificationListKey);
            
            // Add new again
            Response.Cookies.Append(SavedNotificationListKey, notifyStack.ToJsonString(true));
        }
    }
}
