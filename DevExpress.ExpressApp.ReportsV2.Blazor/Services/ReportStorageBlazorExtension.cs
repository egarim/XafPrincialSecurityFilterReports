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
using System.ComponentModel;
using System.IO;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.AspNetCore.Core.Internal;
using DevExpress.ExpressApp.ReportsV2.Services;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using Microsoft.Extensions.Options;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ReportStorageBlazorExtension : ReportStorageWebExtension {
		internal const string IsNewReportName = "IsNewReportName";
		private readonly IObjectSpaceFactoryWrapper objectSpaceFactory;
		private readonly IReportStorage reportStorage;
		private readonly IReportDataSourceHelper reportDataSourceHelper;
		private readonly ReportViewerContainerDataHolder reportViewerContainerHolder;
		private readonly IOptions<ReportOptions> options;
		public ReportStorageBlazorExtension(
			IObjectSpaceFactoryWrapper objectSpaceFactory,
			IReportStorage reportStorage,
			IReportDataSourceHelper reportDataSourceHelper,
			ReportViewerContainerDataHolder reportViewerContainerHolder,
			IOptions<ReportOptions> options) {
			this.objectSpaceFactory = objectSpaceFactory;
			this.reportStorage = reportStorage;
			this.reportDataSourceHelper = reportDataSourceHelper;
			this.reportViewerContainerHolder = reportViewerContainerHolder;
			this.options = options;
		}
		private void SetupReportParameters() {
			ReportViewerContainer reportViewerContainer = reportViewerContainerHolder.Container;
			if(reportViewerContainer != null) {
				EventHandler<BeforeShowPreviewEventArgs> onReportCreated = null;
				onReportCreated = (o, e) => {
					reportDataSourceHelper.SetupBeforePrint(e.Report, reportViewerContainer.ParametersObject,
						reportViewerContainer.Criteria, reportViewerContainer.CanApplyCriteria,
						reportViewerContainer.SortProperty, reportViewerContainer.CanApplySortProperty);
					reportStorage.ReportCreated -= onReportCreated;
					reportViewerContainerHolder.Container = null;
					EventHandler<EventArgs> dataSourceDemanded = null;
					dataSourceDemanded = (r, e) => {
						XtraReport report = (XtraReport)r;
						report.DataSourceDemanded -= dataSourceDemanded;
						if(reportViewerContainer.ParametersObject != null) {
							var param = report.Parameters[ReportDataSourceHelperBase.XafReportParametersObjectName];
							if(param != null && param.Value == null) {
								param.Value = reportViewerContainer.ParametersObject;
							}
						}
					};
					e.Report.DataSourceDemanded += dataSourceDemanded;
					var xafParameter = e.Report.Parameters[ReportDataSourceHelperBase.XafReportParametersObjectName];
					if(xafParameter != null) {
						xafParameter.Value = null;
					}
				};
				reportStorage.ReportCreated += onReportCreated;
			}
		}
		private static byte[] SaveLayoutToXml(XtraReport report) {
			using(MemoryStream stream = new MemoryStream()) {
				report.SaveLayoutToXml(stream);
				return stream.ToArray();
			}
		}
		public override bool CanSetData(string url) {
			return reportStorage.CanSetData(url);
		}
		public override byte[] GetData(string url) {
			if(IsValidUrl(url) && url != IsNewReportName) {
				SetupReportParameters();
				return reportStorage.GetData(url);
			}
			else if(NewReportParameters?.Report != null) {
				return SaveLayoutToXml(NewReportParameters.Report);
			}
			return null;
		}
		public virtual XtraReport GetReport(string url) {
			if(IsValidUrl(url) && url != IsNewReportName) {
				SetupReportParameters();
				return reportStorage.GetReport(url);
			}
			else if(NewReportParameters?.Report != null) {
				return NewReportParameters?.Report;
			}
			return null;
		}
		public override bool IsValidUrl(string url) {
			return reportStorage.IsValidUrl(url);
		}
		public override void SetData(XtraReport report, string url) {
			reportStorage.SetData(report, url);
		}
		public override void SetData(XtraReport report, Stream stream) {
			reportStorage.SetData(report, stream);
		}
		public override Dictionary<string, string> GetUrls() {
			Dictionary<string, string> urls = new Dictionary<string, string>();
			Type reportDataType = options.Value.ReportDataType;
			using(IObjectSpace objectSpace = objectSpaceFactory.CreateObjectSpace(reportDataType)) {
				CriteriaOperator criteriaNotPredefined = new NullOperator(ReportsModuleV2.FindPredefinedReportTypeMemberName(reportDataType));
				SortProperty sortbyName = new SortProperty(nameof(IReportDataV2.DisplayName), DevExpress.Xpo.DB.SortingDirection.Ascending);
				System.Collections.IList allReports = objectSpace.CreateCollection(reportDataType, criteriaNotPredefined, new SortProperty[] { sortbyName });
				foreach(object reportObject in allReports) {
					IReportDataV2 reportData = (IReportDataV2)reportObject;
					urls.Add(reportStorage.GetReportContainerHandle(reportData), reportData.DisplayName);
				}
			}
			return urls;
		}
		public override string SetNewData(XtraReport report, string defaultUrl) {
			string url = defaultUrl;
			if(!IsValidUrl(url)) {
				url = reportStorage.CreateNewReportHandle(options.Value.ReportDataType);
				report.Tag = NewReportParameters;
			}
			return reportStorage.SetNewData(report, url);
		}
		public INewReportWizardParameters NewReportParameters {
			get {
				return reportViewerContainerHolder.NewReportParameters;
			}
		}
	}
}
