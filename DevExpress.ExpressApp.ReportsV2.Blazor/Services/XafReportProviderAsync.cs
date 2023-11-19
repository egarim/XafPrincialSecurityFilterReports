﻿#region Copyright (c) 2000-2023 Developer Express Inc.
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

using System.Threading.Tasks;
using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.Web.Native.ClientControls;
using DevExpress.XtraReports.Web.Native.ClientControls.Services;
using DevExpress.XtraReports.Web.WebDocumentViewer.Native.Services;
using DevExpress.XtraReports.Web.ReportDesigner.Native;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	public class XafReportProviderAsync : ReportProviderAsyncProxy {
		readonly ReportStorageWebExtension reportStorageWebExtension;
		readonly bool useAsync;
		public XafReportProviderAsync(ReportStorageWebExtension reportStorageWebExtension, AsyncModeSettings asyncModeSettings) : base(reportStorageWebExtension, asyncModeSettings) {
			this.reportStorageWebExtension = reportStorageWebExtension;
			useAsync = asyncModeSettings.UseReportResolverAsync;
		}
		public async override Task<XtraReport> GetReportAsync(string id, ReportProviderContext context) {
			if(!reportStorageWebExtension.IsRegistered() && !ReportStorageWebService.IsRegister) {
				string reportProviderTypeExpected = useAsync ? nameof(IReportProviderAsync) : nameof(IReportProvider);
				throw ActionHelper.CreateFaultException($"{reportProviderTypeExpected} or ReportStorageWebExtension services are not registered", nameof(GetReportAsync));
			}
			var report = ((ReportStorageBlazorExtension)reportStorageWebExtension).GetReport(id);
			if(report == null)
				return null;
			await AfterGetDataAsync(id, report).ConfigureAwait(false);
			return report;
		}
	}
}
