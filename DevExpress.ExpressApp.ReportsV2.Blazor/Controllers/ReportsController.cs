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

using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Mvc;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	[NonController]
	public class ReportsController : ReportsControllerCore {
		private SimpleAction showReportAction;
		private void ExecuteReportAction_Execute(object sender, SimpleActionExecuteEventArgs args) {
			ShowReportPreview(args);
		}
		protected override void InitializeActions() {
			showReportAction = new SimpleAction(this, "ExecuteReport", PredefinedCategory.Reports);
			showReportAction.Caption = "Execute Report";
			showReportAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
			showReportAction.ImageName = "Action_Printing_Preview";
			showReportAction.Execute += ExecuteReportAction_Execute;
			ExecuteReportAction = showReportAction;
		}
	}
}
