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
using System.ComponentModel;
using DevExpress.Blazor.Reporting;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.XtraReports.Web.ReportDesigner.DataContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
namespace DevExpress.ExpressApp.ReportsV2.Blazor.Components.Models {
	public class DxReportDesignerWizardSettingsModel : ComponentModelBase {
		public bool UseFullscreenWizard {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_UseFullscreenWizard => HasPropertyValue(nameof(UseFullscreenWizard));
		public bool UseMasterDetailWizard {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_UseMasterDetailWizard => HasPropertyValue(nameof(UseMasterDetailWizard));
		public bool EnableJsonDataSource {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_EnableJsonDataSource => HasPropertyValue(nameof(EnableJsonDataSource));
		public bool EnableSqlDataSource {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_EnableSqlDataSource => HasPropertyValue(nameof(EnableSqlDataSource));
		public bool EnableObjectDataSource {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_EnableObjectDataSource => HasPropertyValue(nameof(EnableObjectDataSource));
		public bool EnableFederationDataSource {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_EnableFederationDataSource => HasPropertyValue(nameof(EnableFederationDataSource));
		public SearchBoxVisibility ReportWizardTemplatesSearchBoxVisibility {
			get => GetPropertyValue<SearchBoxVisibility>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ReportWizardTemplatesSearchBoxVisibility => HasPropertyValue(nameof(ReportWizardTemplatesSearchBoxVisibility));
	}
	namespace Renderers {
		public static class DxReportDesignerWizardSettingsRenderer {
			public static RenderFragment GetComponentContent(this DxReportDesignerWizardSettingsModel componentModel, Action<object> addComponentReferenceCapture = default) =>
				builder => builder.AddComponent<DxReportDesignerWizardSettings>(0, componentModel, addComponentReferenceCapture);
			public static void AddComponent<TComponent>(this RenderTreeBuilder builder, int _, DxReportDesignerWizardSettingsModel componentModel, Action<object> addComponentReferenceCapture = default) where TComponent : DxReportDesignerWizardSettings {
				if(builder is null) {
					throw new ArgumentNullException(nameof(builder));
				}
				if(componentModel is null) {
					throw new ArgumentNullException(nameof(componentModel));
				}
				builder.OpenComponent<TComponent>(0);
				builder.SetKey(componentModel);
				if(componentModel.Has_UseFullscreenWizard) {
					builder.AddAttribute(1, nameof(DxReportDesignerWizardSettings.UseFullscreenWizard), componentModel.UseFullscreenWizard);
				}
				if(componentModel.Has_UseMasterDetailWizard) {
					builder.AddAttribute(2, nameof(DxReportDesignerWizardSettings.UseMasterDetailWizard), componentModel.UseMasterDetailWizard);
				}
				if(componentModel.Has_EnableJsonDataSource) {
					builder.AddAttribute(3, nameof(DxReportDesignerWizardSettings.EnableJsonDataSource), componentModel.EnableJsonDataSource);
				}
				if(componentModel.Has_EnableSqlDataSource) {
					builder.AddAttribute(4, nameof(DxReportDesignerWizardSettings.EnableSqlDataSource), componentModel.EnableSqlDataSource);
				}
				if(componentModel.Has_EnableObjectDataSource) {
					builder.AddAttribute(5, nameof(DxReportDesignerWizardSettings.EnableObjectDataSource), componentModel.EnableObjectDataSource);
				}
				if(componentModel.Has_EnableFederationDataSource) {
					builder.AddAttribute(6, nameof(DxReportDesignerWizardSettings.EnableFederationDataSource), componentModel.EnableFederationDataSource);
				}
				if(componentModel.Has_ReportWizardTemplatesSearchBoxVisibility) {
					builder.AddAttribute(7, nameof(DxReportDesignerWizardSettings.ReportWizardTemplatesSearchBoxVisibility), componentModel.ReportWizardTemplatesSearchBoxVisibility);
				}
				if(componentModel.Attributes.Count > 0) {
					builder.AddMultipleAttributes(8, componentModel.Attributes);
				}
				if(addComponentReferenceCapture != default) {
					builder.AddComponentReferenceCapture(9, addComponentReferenceCapture);
				}
				builder.CloseComponent();
			}
			public static int Offset => 10;
		}
	}
}
