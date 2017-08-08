public class HandleError : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            
            // if the request is AJAX return JSON else view.
            if (IsAjax(filterContext))
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                filterContext.Result = new JsonNetResult()
                {
                    Data = new { ErrorMessage = filterContext.Exception.Message},
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.ExceptionHandled = true;
                //filterContext.HttpContext.Response.Clear();
                
            }
            else
            {        
                base.OnException(filterContext);

            }
            
        }

        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }