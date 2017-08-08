 public class BaseController : Controller
    {
        /// <summary>
        /// Method executed before every controller action
        /// </summary>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, Object state)
        {
            try
            {
                Localization.SetLanguage(Request, Response);
            }
            catch (Exception)
            {
            }

            return base.BeginExecuteCore(callback, state);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                ContentType = contentType,
                MaxJsonLength = int.MaxValue
            };
        }
    }