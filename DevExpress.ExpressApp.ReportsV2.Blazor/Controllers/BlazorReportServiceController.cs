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
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.AspNetCore.Core.Internal;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	[NonController]
	public class BlazorReportServiceController : ReportServiceController {
		private IReportDataV2 GetReportData(string reportContainerHandle, IObjectSpace objectSpace) {
			return GetReportStorage().GetReportData(reportContainerHandle, objectSpace);
		}
		protected IObjectSpaceFactory GetReportsObjectSpaceFactory() {
			Guard.ArgumentNotNull(Application, nameof(Application));
			return Application.ServiceProvider.GetRequiredService<IObjectSpaceCreator>();
		}
		private DetailView CreateReportPreviewDetailView(string reportContainerHandle) {
			IObjectSpace objectSpace = GetReportsObjectSpaceFactory().CreateObjectSpace(GetReportStorage().GetReportDataTypeFromHandle(reportContainerHandle));
			DetailView previewDetailView = new DetailView((IModelDetailView)Application.Model.Views[ReportsBlazorModuleV2.ReportViewerDetailViewName], objectSpace, null, Application, true);
			previewDetailView.DelayedItemsInitialization = false;
			return previewDetailView;
		}
		private DetailView CreateReportDesignerDetailView(IObjectSpace objectSpace) {
			Guard.ArgumentNotNull(Application, "Application");
			DetailView view = new DetailView((IModelDetailView)Application.Model.Views[ReportsBlazorModuleV2.ReportDesignerDetailViewName], objectSpace, null, Application, true);
			if(ShowReportViewInPopup) {
				view.Closed += DesignerView_Closed;
			}
			return view;
		}
		private void DesignerView_Closed(object sender, EventArgs e) {
			if(Frame?.View?.ObjectSpace != null) {
				Frame.View.ObjectSpace.Refresh();
			}
		}
		private void ShowReportView(DetailView detailView, string reportContainerHandle) {
			if(Frame.View != detailView) {
				if(ShowReportViewInPopup || Frame is NestedFrame) {
					ShowReportDialog(detailView);
				}
				else {
					if(reportContainerHandle != null) {
						IReportDataV2 reportData = GetReportStorage().GetReportData(reportContainerHandle, detailView.ObjectSpace);
						detailView.CurrentObject = reportData;
					}
					Frame.SetView(detailView);
				}
			}
		}
		private void ShowReportDialog(DetailView detailView) {
			DialogController dialogController = CreateReportDesignerDialogController();
			dialogController.WindowTemplateChanged += (sender, e) => {
				WindowController controller = (WindowController)sender;
				if(controller.Window.Template is IPopupWindowTemplateSize size) {
					size.Width = "100%";
					size.MaxWidth = "100vw";
					size.Height = "100%";
					size.MaxHeight = "100vh";
				}
			};
			ShowViewParameters showViewParameters = new ShowViewParameters();
			showViewParameters.Controllers.AddRange(dialogController.Controllers);
			showViewParameters.Controllers.Add(dialogController);
			showViewParameters.CreatedView = detailView;
			showViewParameters.Context = TemplateContext.PopupWindow;
			showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
#pragma warning disable XAF0022
			Application.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(Frame, null));
#pragma warning restore XAF0022
		}
		private void ConfigureReportName(XtraReport report) {
			QueryRootReportComponentNameEventArgs raiseQueryRootReportComponentNameArgs = new QueryRootReportComponentNameEventArgs(report);
			RaiseQueryRootReportComponentNameCore(raiseQueryRootReportComponentNameArgs);
			if(!raiseQueryRootReportComponentNameArgs.Handled) {
				raiseQueryRootReportComponentNameArgs.Name = raiseQueryRootReportComponentNameArgs.GetDefaultName();
			}
			report.Name = raiseQueryRootReportComponentNameArgs.Name;
		}
		private bool ShowReportViewInPopup {
			get {
				ReportsBlazorModuleV2 module = (ReportsBlazorModuleV2)Frame.Application.Modules.FindModule(typeof(ReportsBlazorModuleV2));
				if(module != null) {
					return module.DesignAndPreviewDisplayMode == DesignAndPreviewDisplayModes.Popup;
				}
				return true;
			}
		}
		protected virtual DialogController CreateReportDesignerDialogController() {
			DialogController dialogController = Application.CreateController<PreviewReportDialogController>();
			dialogController.AcceptAction.Active["ReportDialog"] = false;
			return dialogController;
		}
		protected override void ShowReportPreviewCore(string reportContainerHandle, ReportParametersObjectBase parametersObject, CriteriaOperator criteria, bool canApplyCriteria, SortProperty[] sortProperty, bool canApplySortProperty, ShowViewParameters showViewParameters) {
			DetailView previewDetailView = CreateReportPreviewDetailView(reportContainerHandle);
			ReportViewerContainer reportViewContainer = new ReportViewerContainer(parametersObject, criteria, canApplyCriteria, sortProperty, canApplySortProperty);
			ReportViewerContainerDataHolder reportViewerContainerHolder = Application.ServiceProvider.GetRequiredService<ReportViewerContainerDataHolder>();
			reportViewerContainerHolder.Container = reportViewContainer;
			ReportViewerViewItem reportViewer = new ReportViewerViewItem(ReportsBlazorModuleV2.ReportViewerId);
			reportViewer.ReportName = reportContainerHandle;
			previewDetailView.AddItem(reportViewer);
			previewDetailView.CurrentObject = GetReportData(reportContainerHandle, previewDetailView.ObjectSpace);
			ShowReportView(previewDetailView, reportContainerHandle);
		}
		protected override void ShowDesignerCore(XtraReport report, string reportContainerHandle) {
			IObjectSpace reportDataObjectSpace = GetReportsObjectSpaceFactory().CreateObjectSpace(GetReportStorage().GetReportDataTypeFromHandle(reportContainerHandle));
			ShowReportDesignerDetailView(reportDataObjectSpace, reportContainerHandle, null, report?.DataSource);
		}
		protected virtual void ShowDesignerCore(IObjectSpace reportDataObjectSpace, string displayName, object dataSource = null) {
			ShowReportDesignerDetailView(reportDataObjectSpace, null, displayName, dataSource);
		}
		private void ShowReportDesignerDetailView(IObjectSpace reportDataObjectSpace, string reportContainerHandle, string displayName, object dataSource) {
			DetailView designerDetailView = CreateReportDesignerDetailView(reportDataObjectSpace);
			designerDetailView.DelayedItemsInitialization = false;
			ReportDesignerViewItem designerDetailItem = designerDetailView.FindItem("ReportDesigner") as ReportDesignerViewItem;
			designerDetailItem.ReportName = reportContainerHandle ?? ReportStorageBlazorExtension.IsNewReportName;
			designerDetailItem.DisplayName = displayName;
			designerDetailItem.ReportDataSource = dataSource;
			ShowReportView(designerDetailView, reportContainerHandle);
		}
		protected override ShowViewParameters CreateParameters() {
			ShowViewParameters showViewParameters = new ShowViewParameters();
			showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
			showViewParameters.Context = TemplateContext.PopupWindow;
			return showViewParameters;
		}
		protected XtraReport CreateNewReport(Type reportDataType) {
			XtraReport newReport = null;
			using(IObjectSpace objectSpace = GetReportsObjectSpaceFactory().CreateObjectSpace(reportDataType)) {
				IReportDataV2 reportData = (IReportDataV2)objectSpace.CreateObject(reportDataType);
				newReport = GetReportStorage().LoadReport(reportData);
			}
			return newReport;
		}
		protected override void ShowWizardCore(Type reportDataType) {
			Guard.TypeArgumentIs(typeof(IReportDataV2), reportDataType, "reportDataType");
			Guard.ArgumentNotNull(Application, "Application");
			NewReportWizardParameters wizardParameters = new NewReportWizardParameters(CreateNewReport(reportDataType), reportDataType);
			BlazorNewReportWizardShowingEventArgs wizardArgs = new BlazorNewReportWizardShowingEventArgs(reportDataType, wizardParameters);
			ConfigureReportName(wizardArgs.WizardParameters.Report);
			OnNewReportWizardShowing(wizardArgs);
			IObjectSpace objectSpace = GetReportsObjectSpaceFactory().CreateObjectSpace(reportDataType);
			ShowViewParameters showViewParameters = new ShowViewParameters();
			DialogController windowController = Application.CreateController<BlazorReportWizardDialogController>();
			windowController.Accepting += OnWizardParamsAccepting;
			showViewParameters.Controllers.Add(windowController);
			showViewParameters.CreatedView = Application.CreateDetailView(objectSpace, wizardArgs.WizardParameters);
#pragma warning disable XAF0022
			((DetailView)showViewParameters.CreatedView).ViewEditMode = ViewEditMode.Edit;
			showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
			Application.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(Frame, null));
#pragma warning restore XAF0022
		}
		protected virtual void OnNewReportWizardShowing(BlazorNewReportWizardShowingEventArgs args) {
			NewReportWizardShowing?.Invoke(this, args);
		}
		private void OnWizardParamsAccepting(object sender, DialogControllerAcceptingEventArgs e) {
			INewReportWizardParameters reportParams = (INewReportWizardParameters)e.AcceptActionArgs.CurrentObject;
			IObjectSpace os = GetReportsObjectSpaceFactory().CreateObjectSpace(reportParams.ReportDataType);
#if DebugTest
			IServiceProvider serviceProvider = Application?.ServiceProvider;
#else
			IServiceProvider serviceProvider = Application.ServiceProvider;
#endif
			Validator.GetService(serviceProvider).ValidateAll(os, new object[] { reportParams }, "Accept");
			ReportViewerContainerDataHolder reportViewerContainerHolder = Application.ServiceProvider.GetRequiredService<ReportViewerContainerDataHolder>();
			reportViewerContainerHolder.NewReportParameters = reportParams;
			ShowDesignerCore(os, reportParams.DisplayName, reportParams.Report.DataSource);
		}
		public event EventHandler<BlazorNewReportWizardShowingEventArgs> NewReportWizardShowing;
	}
}
