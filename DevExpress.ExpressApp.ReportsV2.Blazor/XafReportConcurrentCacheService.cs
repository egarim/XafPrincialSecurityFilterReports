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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.WebDocumentViewer.Native;
using DevExpress.XtraReports.Web.WebDocumentViewer.Native.Services;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class ScopedReportCacheService : IReportCacheService {
		readonly ReportConcurrentCacheService cacheService;
		readonly IReportDataSourceHelper reportDataSourceHelper;
		public ScopedReportCacheService(ReportConcurrentCacheService cacheService, IReportDataSourceHelper reportDataSourceHelper) {
			this.cacheService = cacheService;
			this.reportDataSourceHelper = reportDataSourceHelper;
		}
		CachedReportInfo IReportCacheService.AddOrUpdate(string id, CachedReportInfo addValue, Func<string, CachedReportInfo, CachedReportInfo> updateValueFactory)
			=> cacheService.AddOrUpdate(id, addValue, updateValueFactory);
		CachedReportInfo IReportCacheService.AddOrUpdate(string id, Func<string, CachedReportInfo> addValueFactory, Func<string, CachedReportInfo, CachedReportInfo> updateValueFactory)
			=> cacheService.AddOrUpdate(id, addValueFactory, updateValueFactory);
		IEnumerator<KeyValuePair<string, CachedReportInfo>> IEnumerable<KeyValuePair<string, CachedReportInfo>>.GetEnumerator()
			=> cacheService.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator()
			=> cacheService.GetEnumerator();
		bool IReportCacheService.TryAdd(string id, CachedReportInfo reportInfo) {
			InitParameters(reportInfo.Report);
			return cacheService.TryAdd(id, reportInfo);
		}
		bool IReportCacheService.TryGetValue(string id, out CachedReportInfo reportInfo)
			=> cacheService.TryGetValue(id, out reportInfo);
		bool IReportCacheService.TryRemove(string id, out CachedReportInfo info)
			=> cacheService.TryRemove(id, out info);
		void InitParameters(XtraReport report) {
			if(report != null) {
				reportDataSourceHelper.SetupReport(report);
				ReportParametersDataSourceInitializer.SetupParametersDataSources(report);
			}
		}
	}
}
