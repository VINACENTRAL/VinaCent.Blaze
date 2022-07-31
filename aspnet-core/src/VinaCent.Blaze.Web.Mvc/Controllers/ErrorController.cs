using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Models;
using Abp.Web.Mvc.Models;

namespace VinaCent.Blaze.Web.Controllers
{
    public class ErrorController : AbpController
    {
        private readonly IErrorInfoBuilder _errorInfoBuilder;

        public ErrorController(IErrorInfoBuilder errorInfoBuilder)
        {
            _errorInfoBuilder = errorInfoBuilder;
        }

        public ActionResult Index([FromQuery] int statusCode = 0)
        {
            var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = exHandlerFeature != null
                                ? exHandlerFeature.Error
                                : new Exception("Unhandled exception!");

            ViewBag.StatusCode = statusCode;

            return View(
                "Error",
                new ErrorViewModel(
                    _errorInfoBuilder.BuildForException(exception),
                    exception
                )
            );
        }
    }
}
