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
using System.Threading.Tasks;
using DevExpress.ExpressApp.Blazor.AmbientContext;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Core.Internal;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Utils;
using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Native.ClientControls.Services;
using DevExpress.XtraReports.Web.WebDocumentViewer.Native.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	public class XafDocumentBuilder : DocumentBuilder {
		readonly IScopedXafApplicationProvider<IXafApplicationProvider> scopedApplicationProvider;
		private readonly ReportViewerContainerDataHolder reportViewerContainerHolder;
		readonly IPrincipalProvider principalProvider;
		private readonly IServiceProvider serviceProviderParent;
		private readonly IOptions<ReportOptions> options;
		readonly IValueManagerStorageContext valueManagerStorageContext;
		public XafDocumentBuilder(IDocumentManagementService documentManagementService,
								  UrlResolver urlResolver,
								  ILocalizerService localizerService,
								  ILoggerService logger,
								  IAsyncLockerService reportAsyncLocker,
								  IReportProvider reportProvider,
								  IErrorStorage errorStorage,
								  IScopedXafApplicationProvider<IXafApplicationProvider> scopedApplicationProvider,
								  ReportViewerContainerDataHolder reportViewerContainerHolder,
								  IPrincipalProvider principalProvider,
								  IServiceProvider serviceProvider,
								  IValueManagerStorageContext valueManagerStorageContext,
								  IOptions<ReportOptions> options)
			: base(documentManagementService,
				   urlResolver,
				   localizerService,
				   logger,
				   reportAsyncLocker,
				   reportProvider,
				   errorStorage) {
			this.valueManagerStorageContext = valueManagerStorageContext;
			this.scopedApplicationProvider = scopedApplicationProvider ?? throw new ArgumentNullException(nameof(scopedApplicationProvider));
			this.reportViewerContainerHolder = reportViewerContainerHolder;
			this.principalProvider = principalProvider;
			this.serviceProviderParent = serviceProvider;
			this.options = options;
		}
		public override async Task<string> StartBuildAsync(XtraReport report, Action customizeBuildingThread) {
			if(await NeedToSetupReportAsync(report)) {
				await SetupReportAsync(report);
			}
			return await base.StartBuildAsync(report, customizeBuildingThread);
		}
		protected virtual Task<bool> NeedToSetupReportAsync(XtraReport report) {
			var serviceProviderScopeLifetimeHelper = report.GetService<ExpressApp.Services.Core.Internal.ServiceProviderScopeLifetimeHelperLink>();
			return Task.FromResult(serviceProviderScopeLifetimeHelper == null || serviceProviderScopeLifetimeHelper.IsScopeDisposed);
		}
		protected virtual Task SetupReportAsync(XtraReport report) {
			IReportDataSourceHelper reportDataSourceHelper;
			if(valueManagerStorageContext.RunWithStorage(() => ValueManager.GetValueManager<bool>("ApplicationCreationMarker").Value)) {
				reportDataSourceHelper = serviceProviderParent.GetRequiredService<IReportDataSourceHelper>();
			}
			else {
				var applicationScope = scopedApplicationProvider.GetXafApplicationScope(principalProvider.User);
				report.Disposed += (s, e) => {
					applicationScope.Dispose();
				};
				var objectSpaceProviderFactory = applicationScope.Scope.ServiceProvider.GetRequiredService<IObjectSpaceProviderFactory>();
				if(objectSpaceProviderFactory is IEmptyObjectSpaceProviderFactory) {
					_ = applicationScope.ApplicationProvider.GetApplication();
				}
				reportDataSourceHelper = applicationScope.Scope.ServiceProvider.GetRequiredService<IReportDataSourceHelper>();
				ReportViewerContainerDataHolder reportViewerContainerHolderNested = applicationScope.Scope.ServiceProvider.GetRequiredService<ReportViewerContainerDataHolder>();
				reportViewerContainerHolderNested.Container = reportViewerContainerHolder.Container;
				reportViewerContainerHolderNested.NewReportParameters = reportViewerContainerHolder.NewReportParameters;
				((BlazorReportModuleEvents)options.Value.Events).NestedReportScopeCreated(new ReportNestedReportScopeContext(report, serviceProviderParent, applicationScope.Scope.ServiceProvider));
			}
			reportDataSourceHelper.SetupBeforePrint(report);
			return Task.CompletedTask;
		}
	}
}
