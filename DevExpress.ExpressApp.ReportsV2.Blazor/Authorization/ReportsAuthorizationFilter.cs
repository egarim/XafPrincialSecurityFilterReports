#region Copyright (c) 2000-2023 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2023 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2023 Developer Express Inc.

using DevExpress.ExpressApp.AspNetCore.Security.Authentication;
using DevExpress.ExpressApp.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace DevExpress.ExpressApp.ReportsV2.Blazor.Authorization {
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	public class ReportsAuthorizationFilter : IActionFilter {
		readonly IXafAuthenticationHandler xafAuthenticationHelper;
		readonly IPrincipalProvider principalProvider;
		public ReportsAuthorizationFilter(IXafAuthenticationHandler xafAuthenticationHelper, IPrincipalProvider principalProvider) {
			this.xafAuthenticationHelper = xafAuthenticationHelper;
			this.principalProvider = principalProvider;
		}
		public void OnActionExecuting(ActionExecutingContext context) {
			if(!xafAuthenticationHelper.IsAuthenticated(principalProvider.User)) {
				context.Result = new ContentResult() {
					Content = "Unauthorized: Access is denied",
					StatusCode = 401
				};
			}
		}
		public void OnActionExecuted(ActionExecutedContext context) { }
	}
}
