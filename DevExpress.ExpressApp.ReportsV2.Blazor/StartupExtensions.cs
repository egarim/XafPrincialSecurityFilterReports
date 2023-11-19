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

using System;
using DevExpress.AspNetCore.Reporting;
using DevExpress.Blazor.Reporting;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.ReportsV2.Blazor.Authorization;
using DevExpress.ExpressApp.ReportsV2.Blazor.Services;
using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.Web.ReportDesigner.Native.Services;
using DevExpress.XtraReports.Web.WebDocumentViewer.Native.Services;
using Microsoft.Extensions.DependencyInjection;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	public static class StartupExtensions {
		public static IServiceCollection AddXafReporting(this IServiceCollection services) {
			return services.AddXafReporting(_ => { });
		}
		public static IServiceCollection AddXafReporting(this IServiceCollection services, Action<ReportOptions> options) {
			if(services == null) {
				throw new ArgumentNullException(nameof(services));
			}
			services.ConfigureReportingServices(configurator => {
				configurator.UseAsyncEngine();
			});
			services.AddDevExpressBlazorReporting();
			services.AddSingleton<IScopedXafApplicationProvider<IXafApplicationProvider>, ScopedXafApplicationProvider<IXafApplicationProvider>>();
			services.AddSingleton<ReportConcurrentCacheService>();
			services.AddScoped<IReportCacheService, ScopedReportCacheService>();
			services.AddScoped<ReportStorageWebExtension, ReportStorageBlazorExtension>();
			services.AddScoped<IDocumentBuilderAsync, XafDocumentBuilder>();
			services.AddScoped<ReportsAuthorizationFilter>();
			services.AddScoped<ReportViewerContainerDataHolder>();
			services.AddScoped<IReportProvider, XafReportProvider>();
			services.AddScoped<IReportProviderAsync, XafReportProviderAsync>();
			services.AddScoped<IReportDataSourceHelper, ReportDataSourceHelperBlazorService>();
			services.AddScoped<DevExpress.ExpressApp.ReportsV2.IReportStorage, BlazorReportStorageService>();
			services.AddScoped<IObjectSpaceCreator, BlazorObjectSpaceCreatorLegacy>();
			services.AddXafReportingCore((o) => {
				o.Events = new BlazorReportModuleEvents();
				options.Invoke(o);
			});
			return services;
		}
	}
}
