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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
namespace DevExpress.ExpressApp.ReportsV2.Blazor.Components.Models {
	public class DxReportDesignerCallbacksModel : ComponentModelBase {
		public string CustomizeElements {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeElements => HasPropertyValue(nameof(CustomizeElements));
		public string ExitDesigner {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ExitDesigner => HasPropertyValue(nameof(ExitDesigner));
		public string ReportSaving {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ReportSaving => HasPropertyValue(nameof(ReportSaving));
		public string ReportSaved {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ReportSaved => HasPropertyValue(nameof(ReportSaved));
		public string ReportOpened {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ReportOpened => HasPropertyValue(nameof(ReportOpened));
		public string ReportOpening {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ReportOpening => HasPropertyValue(nameof(ReportOpening));
		public string TabChanged {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_TabChanged => HasPropertyValue(nameof(TabChanged));
		public string ComponentAdded {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ComponentAdded => HasPropertyValue(nameof(ComponentAdded));
		public string CustomizeParameterEditors {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeParameterEditors => HasPropertyValue(nameof(CustomizeParameterEditors));
		public string CustomizeSaveDialog {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeSaveDialog => HasPropertyValue(nameof(CustomizeSaveDialog));
		public string CustomizeSaveAsDialog {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeSaveAsDialog => HasPropertyValue(nameof(CustomizeSaveAsDialog));
		public string CustomizeOpenDialog {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeOpenDialog => HasPropertyValue(nameof(CustomizeOpenDialog));
		public string CustomizeToolbox {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeToolbox => HasPropertyValue(nameof(CustomizeToolbox));
		public string CustomizeFieldListActions {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeFieldListActions => HasPropertyValue(nameof(CustomizeFieldListActions));
		public string CustomizeMenuActions {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeMenuActions => HasPropertyValue(nameof(CustomizeMenuActions));
		public string CustomizeLocalization {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeLocalization => HasPropertyValue(nameof(CustomizeLocalization));
		public string OnInitializing {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_OnInitializing => HasPropertyValue(nameof(OnInitializing));
		public string BeforeRender {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_BeforeRender => HasPropertyValue(nameof(BeforeRender));
		public string OnServerError {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_OnServerError => HasPropertyValue(nameof(OnServerError));
		public string CustomizeWizard {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeWizard => HasPropertyValue(nameof(CustomizeWizard));
		public string PreviewClick {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewClick => HasPropertyValue(nameof(PreviewClick));
		public string PreviewCustomizeElements {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewCustomizeElements => HasPropertyValue(nameof(PreviewCustomizeElements));
		public string PreviewCustomizeMenuActions {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewCustomizeMenuActions => HasPropertyValue(nameof(PreviewCustomizeMenuActions));
		public string PreviewEditingFieldChanged {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewEditingFieldChanged => HasPropertyValue(nameof(PreviewEditingFieldChanged));
		public string PreviewDocumentReady {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewDocumentReady => HasPropertyValue(nameof(PreviewDocumentReady));
		public string PreviewParametersReset {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewParametersReset => HasPropertyValue(nameof(PreviewParametersReset));
		public string PreviewCustomizeExportOptions {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewCustomizeExportOptions => HasPropertyValue(nameof(PreviewCustomizeExportOptions));
		public string PreviewParametersSubmitted {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewParametersSubmitted => HasPropertyValue(nameof(PreviewParametersSubmitted));
		public string PreviewOnExport {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewOnExport => HasPropertyValue(nameof(PreviewOnExport));
		public string PreviewParametersInitialized {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewParametersInitialized => HasPropertyValue(nameof(PreviewParametersInitialized));
	}
	namespace Renderers {
		public static class DxReportDesignerCallbacksRenderer {
			public static RenderFragment GetComponentContent(this DxReportDesignerCallbacksModel componentModel, Action<object> addComponentReferenceCapture = default) =>
				builder => builder.AddComponent<DxReportDesignerCallbacks>(0, componentModel, addComponentReferenceCapture);
			public static void AddComponent<TComponent>(this RenderTreeBuilder builder, int _, DxReportDesignerCallbacksModel componentModel, Action<object> addComponentReferenceCapture = default) where TComponent : DxReportDesignerCallbacks {
				if(builder is null) {
					throw new ArgumentNullException(nameof(builder));
				}
				if(componentModel is null) {
					throw new ArgumentNullException(nameof(componentModel));
				}
				builder.OpenComponent<TComponent>(0);
				builder.SetKey(componentModel);
				if(componentModel.Has_CustomizeElements) {
					builder.AddAttribute(1, nameof(DxReportDesignerCallbacks.CustomizeElements), componentModel.CustomizeElements);
				}
				if(componentModel.Has_ExitDesigner) {
					builder.AddAttribute(2, nameof(DxReportDesignerCallbacks.ExitDesigner), componentModel.ExitDesigner);
				}
				if(componentModel.Has_ReportSaving) {
					builder.AddAttribute(3, nameof(DxReportDesignerCallbacks.ReportSaving), componentModel.ReportSaving);
				}
				if(componentModel.Has_ReportSaved) {
					builder.AddAttribute(4, nameof(DxReportDesignerCallbacks.ReportSaved), componentModel.ReportSaved);
				}
				if(componentModel.Has_ReportOpened) {
					builder.AddAttribute(5, nameof(DxReportDesignerCallbacks.ReportOpened), componentModel.ReportOpened);
				}
				if(componentModel.Has_ReportOpening) {
					builder.AddAttribute(6, nameof(DxReportDesignerCallbacks.ReportOpening), componentModel.ReportOpening);
				}
				if(componentModel.Has_TabChanged) {
					builder.AddAttribute(7, nameof(DxReportDesignerCallbacks.TabChanged), componentModel.TabChanged);
				}
				if(componentModel.Has_ComponentAdded) {
					builder.AddAttribute(8, nameof(DxReportDesignerCallbacks.ComponentAdded), componentModel.ComponentAdded);
				}
				if(componentModel.Has_CustomizeParameterEditors) {
					builder.AddAttribute(9, nameof(DxReportDesignerCallbacks.CustomizeParameterEditors), componentModel.CustomizeParameterEditors);
				}
				if(componentModel.Has_CustomizeSaveDialog) {
					builder.AddAttribute(10, nameof(DxReportDesignerCallbacks.CustomizeSaveDialog), componentModel.CustomizeSaveDialog);
				}
				if(componentModel.Has_CustomizeSaveAsDialog) {
					builder.AddAttribute(11, nameof(DxReportDesignerCallbacks.CustomizeSaveAsDialog), componentModel.CustomizeSaveAsDialog);
				}
				if(componentModel.Has_CustomizeOpenDialog) {
					builder.AddAttribute(12, nameof(DxReportDesignerCallbacks.CustomizeOpenDialog), componentModel.CustomizeOpenDialog);
				}
				if(componentModel.Has_CustomizeToolbox) {
					builder.AddAttribute(13, nameof(DxReportDesignerCallbacks.CustomizeToolbox), componentModel.CustomizeToolbox);
				}
				if(componentModel.Has_CustomizeFieldListActions) {
					builder.AddAttribute(14, nameof(DxReportDesignerCallbacks.CustomizeFieldListActions), componentModel.CustomizeFieldListActions);
				}
				if(componentModel.Has_CustomizeMenuActions) {
					builder.AddAttribute(15, nameof(DxReportDesignerCallbacks.CustomizeMenuActions), componentModel.CustomizeMenuActions);
				}
				if(componentModel.Has_CustomizeLocalization) {
					builder.AddAttribute(16, nameof(DxReportDesignerCallbacks.CustomizeLocalization), componentModel.CustomizeLocalization);
				}
				if(componentModel.Has_OnInitializing) {
					builder.AddAttribute(17, nameof(DxReportDesignerCallbacks.OnInitializing), componentModel.OnInitializing);
				}
				if(componentModel.Has_BeforeRender) {
					builder.AddAttribute(18, nameof(DxReportDesignerCallbacks.BeforeRender), componentModel.BeforeRender);
				}
				if(componentModel.Has_OnServerError) {
					builder.AddAttribute(19, nameof(DxReportDesignerCallbacks.OnServerError), componentModel.OnServerError);
				}
				if(componentModel.Has_CustomizeWizard) {
					builder.AddAttribute(20, nameof(DxReportDesignerCallbacks.CustomizeWizard), componentModel.CustomizeWizard);
				}
				if(componentModel.Has_PreviewClick) {
					builder.AddAttribute(21, nameof(DxReportDesignerCallbacks.PreviewClick), componentModel.PreviewClick);
				}
				if(componentModel.Has_PreviewCustomizeElements) {
					builder.AddAttribute(22, nameof(DxReportDesignerCallbacks.PreviewCustomizeElements), componentModel.PreviewCustomizeElements);
				}
				if(componentModel.Has_PreviewCustomizeMenuActions) {
					builder.AddAttribute(23, nameof(DxReportDesignerCallbacks.PreviewCustomizeMenuActions), componentModel.PreviewCustomizeMenuActions);
				}
				if(componentModel.Has_PreviewEditingFieldChanged) {
					builder.AddAttribute(24, nameof(DxReportDesignerCallbacks.PreviewEditingFieldChanged), componentModel.PreviewEditingFieldChanged);
				}
				if(componentModel.Has_PreviewDocumentReady) {
					builder.AddAttribute(25, nameof(DxReportDesignerCallbacks.PreviewDocumentReady), componentModel.PreviewDocumentReady);
				}
				if(componentModel.Has_PreviewParametersReset) {
					builder.AddAttribute(26, nameof(DxReportDesignerCallbacks.PreviewParametersReset), componentModel.PreviewParametersReset);
				}
				if(componentModel.Has_PreviewCustomizeExportOptions) {
					builder.AddAttribute(27, nameof(DxReportDesignerCallbacks.PreviewCustomizeExportOptions), componentModel.PreviewCustomizeExportOptions);
				}
				if(componentModel.Has_PreviewParametersSubmitted) {
					builder.AddAttribute(28, nameof(DxReportDesignerCallbacks.PreviewParametersSubmitted), componentModel.PreviewParametersSubmitted);
				}
				if(componentModel.Has_PreviewOnExport) {
					builder.AddAttribute(29, nameof(DxReportDesignerCallbacks.PreviewOnExport), componentModel.PreviewOnExport);
				}
				if(componentModel.Has_PreviewParametersInitialized) {
					builder.AddAttribute(30, nameof(DxReportDesignerCallbacks.PreviewParametersInitialized), componentModel.PreviewParametersInitialized);
				}
				if(componentModel.Attributes.Count > 0) {
					builder.AddMultipleAttributes(31, componentModel.Attributes);
				}
				if(addComponentReferenceCapture != default) {
					builder.AddComponentReferenceCapture(32, addComponentReferenceCapture);
				}
				builder.CloseComponent();
			}
			public static int Offset => 33;
		}
	}
}
