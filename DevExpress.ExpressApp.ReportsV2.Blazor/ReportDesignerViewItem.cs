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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DevExpress.Blazor.Reporting;
using DevExpress.Blazor.Reporting.Base;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ReportsV2.Blazor.Components;
using DevExpress.ExpressApp.ReportsV2.Blazor.Components.Models;
using DevExpress.ExpressApp.ReportsV2.Blazor.Components.Models.Renderers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	public class ReportDesignerViewItem : ViewItem {
		[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
		class DxReportDesignerEx : DxReportDesigner {
			[Inject] IJSRuntime JSRuntime { get; set; }
			protected override async Task OnAfterRenderAsync(bool firstRender) {
				if(firstRender) {
					await JSRuntime.InvokeVoidAsync("xaf.reportViewerCreated");
				}
				await base.OnAfterRenderAsync(firstRender);
			}
			protected override async ValueTask DisposeAsyncCore() {
				await base.DisposeAsyncCore();
				await JSRuntime.InvokeVoidAsync("xaf.reportViewerDestroyed");
			}
		}
		public class DxReportDesignerAdapter : IComponentContentHolder {
			private readonly bool isNewReport;
			private RenderFragment componentContent;
			public DxReportDesignerAdapter(DxReportDesignerModel componentModel, bool isNewReport) {
				ComponentModel = componentModel;
				CallbacksModel = new DxReportDesignerCallbacksModel();
				WizardSettingsModel = new DxReportDesignerWizardSettingsModel();
				DesignerSettingsModel = new DxReportDesignerModelSettingsModel();
				this.isNewReport = isNewReport;
			}
			public DxReportDesignerModel ComponentModel { get; }
			public DxReportDesignerCallbacksModel CallbacksModel { get; }
			public DxReportDesignerWizardSettingsModel WizardSettingsModel { get; }
			public DxReportDesignerModelSettingsModel DesignerSettingsModel { get; }
			RenderFragment IComponentContentHolder.ComponentContent {
				get {
					if(componentContent is null) {
						RenderFragment designer = builder => builder.AddComponent<DxReportDesignerEx>(0, ComponentModel);
						componentContent = ComponentModelObserver.Create(ComponentModel, builder => {
							builder.OpenComponent<DxReportDesignerContainer>(0);
							builder.AddAttribute(1, nameof(DxReportDesignerContainer.IsNewReport), isNewReport);
							builder.AddAttribute(2, nameof(DxReportDesignerContainer.ChildContent), designer);
							builder.CloseComponent();
						});
					}
					return componentContent;
				}
			}
		}
		public ReportDesignerViewItem(string id) : base(null, id) { }
		public ReportDesignerViewItem(IModelViewItem model, Type objectType) : base(objectType, model.Id) { }
		protected override object CreateControlCore() {
			DxReportDesignerModel componentModel = new DxReportDesignerModel();
			componentModel.ReportName = ReportName;
			if (ReportDataSource is not null) {
				componentModel.DataSources = new Dictionary<string, object>() { { ReportDataSource.GetType().Name, ReportDataSource } };
			}
			DxReportDesignerAdapter adapter = new DxReportDesignerAdapter(componentModel, !string.IsNullOrEmpty(DisplayName));
			RenderFragment callbacks = adapter.CallbacksModel.GetComponentContent();
			RenderFragment wizardSettings = adapter.WizardSettingsModel.GetComponentContent();
			RenderFragment reportDesignerModelSettings = adapter.DesignerSettingsModel.GetComponentContent();
			componentModel.ChildContent = builder => {
				callbacks(builder);
				wizardSettings(builder);
				reportDesignerModelSettings(builder);
			};
			adapter.DesignerSettingsModel.AllowMDI = false;
			adapter.CallbacksModel.BeforeRender = "xaf.onBeforeRenderReportDesigner";
			adapter.CallbacksModel.CustomizeMenuActions = "xaf.onCustomizeMenuActionsReportDesigner";
			adapter.WizardSettingsModel.EnableObjectDataSource = true;
			return adapter;
		}
		public string ReportName { get; set; }
		public string DisplayName { get; set; }
		internal object ReportDataSource { get; set; }
	}
}
