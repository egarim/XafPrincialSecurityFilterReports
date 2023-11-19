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

using DevExpress.ExpressApp.Blazor.SystemModule;
using Microsoft.AspNetCore.Mvc;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	[NonController]
	public class ReportProcessViewShortcutController : WindowController {
		private static bool IsReport(string viewId) {
			return IsReportViewer(viewId) || IsReportDesigner(viewId);
		}
		private static bool IsReportViewer(string viewId) {
			return viewId == ReportsBlazorModuleV2.ReportViewerDetailViewName;
		}
		private static bool IsReportDesigner(string viewId) {
			return viewId == ReportsBlazorModuleV2.ReportDesignerDetailViewName;
		}
		protected override void OnActivated() {
			base.OnActivated();
			ProcessViewShortcutController processViewShortcutController = Frame.GetController<ProcessViewShortcutController>();
			if(processViewShortcutController != null) {
				processViewShortcutController.CustomProcessShortcut += ProcessViewShortcutController_CustomProcessShortcut;
			}
		}
		private void ProcessViewShortcutController_CustomProcessShortcut(object sender, CustomProcessShortcutEventArgs e) {
			string viewId = e.Shortcut?.ViewId;
			if(IsReport(viewId)) {
				ReportServiceController serviceController = Frame.GetController<ReportServiceController>();
				if(serviceController != null) {
					ShowView(viewId, e.Shortcut.ObjectKey, serviceController);
					e.Handled = true;
				}
			}
		}
		private void ShowView(string viewId, string objectKey, ReportServiceController serviceController) {
			IReportDataV2 reportData = GetReportData(objectKey);
			if(reportData == null) return;
			IReportStorage reportStorage = ReportDataProvider.GetReportStorage(Application.ServiceProvider);
			if(IsReportViewer(viewId)) {
				serviceController.ShowPreview(reportStorage.GetReportContainerHandle(reportData));
			}
			else {
				serviceController.ShowDesigner(reportStorage.LoadReport(reportData), reportStorage.GetReportContainerHandle(reportData));
			}
		}
		private IReportDataV2 GetReportData(string objectKey) {
			ReportsModuleV2 reportsModuleV2 = Application.Modules.FindModule<ReportsModuleV2>();
			if(reportsModuleV2 == null) return null;
			IObjectSpace objectSpace = Application.CreateObjectSpace(reportsModuleV2.ReportDataType);
			return objectSpace.GetObjectByKey(reportsModuleV2.ReportDataType, objectSpace.GetObjectKey(reportsModuleV2.ReportDataType, objectKey)) as IReportDataV2;
		}
		protected override void OnDeactivated() {
			ProcessViewShortcutController processViewShortcutController = Frame.GetController<ProcessViewShortcutController>();
			if(processViewShortcutController != null) {
				processViewShortcutController.CustomProcessShortcut -= ProcessViewShortcutController_CustomProcessShortcut;
			}
			base.OnDeactivated();
		}
	}
}
