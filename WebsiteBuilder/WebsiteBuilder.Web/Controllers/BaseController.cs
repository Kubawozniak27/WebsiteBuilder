using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBuilder.Core;
using WebsiteBuilder.Core.Service;

namespace WebsiteBuilder.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected TCmd GetCommand<TCmd>() where TCmd : ICommandBase
        {
            return IoCContainer.Container.Resolve<TCmd>();
        }

        protected TQuery GetQuery<TQuery>() where TQuery : IQueryBase
        {
            return IoCContainer.Container.Resolve<TQuery>();
        }

        protected JsonResult JsonSuccessResult()
        {
            return Json(new
            {
                Success = true
            }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult ConvertOperationResultToJson(OperationResult result)
        {
            return ConvertOperationResultToJson<object>(result, null);
        }

        protected JsonResult ConvertOperationResultToJson<T>(OperationResult<T> result)
        {
            return ConvertOperationResultToJson(result, result.Result);
        }

        private JsonResult ConvertOperationResultToJson<T>(OperationResult operationResult, T result)
        {
            if (operationResult.Success)
            {
                return Json(new
                {
                    Success = operationResult.Success,
                    Result = result,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Success = operationResult.Success,
                    Result = result,
                    Error = !operationResult.Success,
                    ErrorMessages = operationResult.Errors
                }, JsonRequestBehavior.AllowGet);
            }
        }


        




    }
}