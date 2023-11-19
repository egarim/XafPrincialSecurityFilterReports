//namespace XafPrincialSecurityFilterReports.Blazor.Server
//{

//    using global::DevExpress.ExpressApp.AspNetCore.Security.Authentication;
//    using global::DevExpress.ExpressApp.Security;
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.AspNetCore.Mvc.Filters;
//    namespace DevExpress.ExpressApp.ReportsV2.Blazor.Authorization
//    {
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
//        public class CustomReportsAuthorizationFilter : IActionFilter
//        {
//            readonly IXafAuthenticationHandler xafAuthenticationHelper;
//            readonly IPrincipalProvider principalProvider;
//            public CustomReportsAuthorizationFilter(IXafAuthenticationHandler xafAuthenticationHelper, IPrincipalProvider principalProvider)
//            {
//                this.xafAuthenticationHelper = xafAuthenticationHelper;
//                this.principalProvider = principalProvider;
//            }
//            public void OnActionExecuting(ActionExecutingContext context)
//            {
//                if (!xafAuthenticationHelper.IsAuthenticated(principalProvider.User))
//                {
//                    context.Result = new ContentResult()
//                    {
//                        Content = "Unauthorized: Access is denied",
//                        StatusCode = 401
//                    };
//                }
//            }
//            public void OnActionExecuted(ActionExecutedContext context) { }
//        }
//    }

//}
