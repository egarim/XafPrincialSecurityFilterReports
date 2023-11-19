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

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DevExpress.Blazor.Reporting;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ReportsV2.Blazor.Components.Models;
using DevExpress.ExpressApp.ReportsV2.Blazor.Components.Models.Renderers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	public class ReportViewerViewItem : ViewItem {
		[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
		class DxDocumentViewerEx : DxDocumentViewer {
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
		public class DxDocumentViewerAdapter : IComponentContentHolder {
			private RenderFragment componentContent;
			public DxDocumentViewerAdapter(DxDocumentViewerModel componentModel) {
				ComponentModel = componentModel;
				DxDocumentViewerCallbacksModel callbacksModel = new DxDocumentViewerCallbacksModel();
				DxDocumentViewerClientSideModelSettingsModel clientSideSettingsModel = new DxDocumentViewerClientSideModelSettingsModel();
				CallbacksModel = callbacksModel;
				ClientSideSettingsModel = clientSideSettingsModel;
			}
			public DxDocumentViewerModel ComponentModel { get; }
			public DxDocumentViewerCallbacksModel CallbacksModel { get; }
			public DxDocumentViewerClientSideModelSettingsModel ClientSideSettingsModel { get; }
			RenderFragment IComponentContentHolder.ComponentContent {
				get {
					if(componentContent is null) {
						RenderFragment viewer = builder => builder.AddComponent<DxDocumentViewerEx>(0, ComponentModel);
						componentContent = ComponentModelObserver.Create(ComponentModel, viewer);
					}
					return componentContent;
				}
			}
		}
		public ReportViewerViewItem(string id) : base(null, id) { }
		protected override object CreateControlCore() {
			DxDocumentViewerModel componentModel = new DxDocumentViewerModel();
			DxDocumentViewerAdapter adapter = new DxDocumentViewerAdapter(componentModel);
			componentModel.ReportName = ReportName;
			RenderFragment callbacks = adapter.CallbacksModel.GetComponentContent();
			RenderFragment clientSideSettings = adapter.ClientSideSettingsModel.GetComponentContent();
			componentModel.ChildContent = builder => {
				callbacks(builder);
				clientSideSettings(builder);
			};
			adapter.CallbacksModel.BeforeRender = "xaf.onBeforeRenderReportViewer";
			return adapter;
		}
		public string ReportName { get; set; }
	}
}
