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
	public class DxDocumentViewerCallbacksModel : ComponentModelBase {
		public string CustomizeElements {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeElements => HasPropertyValue(nameof(CustomizeElements));
		public string PreviewClick {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_PreviewClick => HasPropertyValue(nameof(PreviewClick));
		public string EditingFieldChanged {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_EditingFieldChanged => HasPropertyValue(nameof(EditingFieldChanged));
		public string DocumentReady {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_DocumentReady => HasPropertyValue(nameof(DocumentReady));
		public string CustomizeExportOptions {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeExportOptions => HasPropertyValue(nameof(CustomizeExportOptions));
		public string CustomizeParameterEditors {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeParameterEditors => HasPropertyValue(nameof(CustomizeParameterEditors));
		public string CustomizeParameterLookUpSource {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CustomizeParameterLookUpSource => HasPropertyValue(nameof(CustomizeParameterLookUpSource));
		public string ParametersReset {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ParametersReset => HasPropertyValue(nameof(ParametersReset));
		public string ParametersSubmitted {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ParametersSubmitted => HasPropertyValue(nameof(ParametersSubmitted));
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
		public string OnExport {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_OnExport => HasPropertyValue(nameof(OnExport));
		public string ParametersInitialized {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ParametersInitialized => HasPropertyValue(nameof(ParametersInitialized));
	}
	namespace Renderers {
		public static class DxDocumentViewerCallbacksRenderer {
			public static RenderFragment GetComponentContent(this DxDocumentViewerCallbacksModel componentModel, Action<object> addComponentReferenceCapture = default) =>
				builder => builder.AddComponent<DxDocumentViewerCallbacks>(0, componentModel, addComponentReferenceCapture);
			public static void AddComponent<TComponent>(this RenderTreeBuilder builder, int _, DxDocumentViewerCallbacksModel componentModel, Action<object> addComponentReferenceCapture = default) where TComponent : DxDocumentViewerCallbacks {
				if(builder is null) {
					throw new ArgumentNullException(nameof(builder));
				}
				if(componentModel is null) {
					throw new ArgumentNullException(nameof(componentModel));
				}
				builder.OpenComponent<TComponent>(0);
				builder.SetKey(componentModel);
				if(componentModel.Has_CustomizeElements) {
					builder.AddAttribute(1, nameof(DxDocumentViewerCallbacks.CustomizeElements), componentModel.CustomizeElements);
				}
				if(componentModel.Has_PreviewClick) {
					builder.AddAttribute(2, nameof(DxDocumentViewerCallbacks.PreviewClick), componentModel.PreviewClick);
				}
				if(componentModel.Has_EditingFieldChanged) {
					builder.AddAttribute(3, nameof(DxDocumentViewerCallbacks.EditingFieldChanged), componentModel.EditingFieldChanged);
				}
				if(componentModel.Has_DocumentReady) {
					builder.AddAttribute(4, nameof(DxDocumentViewerCallbacks.DocumentReady), componentModel.DocumentReady);
				}
				if(componentModel.Has_CustomizeExportOptions) {
					builder.AddAttribute(5, nameof(DxDocumentViewerCallbacks.CustomizeExportOptions), componentModel.CustomizeExportOptions);
				}
				if(componentModel.Has_CustomizeParameterEditors) {
					builder.AddAttribute(6, nameof(DxDocumentViewerCallbacks.CustomizeParameterEditors), componentModel.CustomizeParameterEditors);
				}
				if(componentModel.Has_CustomizeParameterLookUpSource) {
					builder.AddAttribute(7, nameof(DxDocumentViewerCallbacks.CustomizeParameterLookUpSource), componentModel.CustomizeParameterLookUpSource);
				}
				if(componentModel.Has_ParametersReset) {
					builder.AddAttribute(8, nameof(DxDocumentViewerCallbacks.ParametersReset), componentModel.ParametersReset);
				}
				if(componentModel.Has_ParametersSubmitted) {
					builder.AddAttribute(9, nameof(DxDocumentViewerCallbacks.ParametersSubmitted), componentModel.ParametersSubmitted);
				}
				if(componentModel.Has_CustomizeMenuActions) {
					builder.AddAttribute(10, nameof(DxDocumentViewerCallbacks.CustomizeMenuActions), componentModel.CustomizeMenuActions);
				}
				if(componentModel.Has_CustomizeLocalization) {
					builder.AddAttribute(11, nameof(DxDocumentViewerCallbacks.CustomizeLocalization), componentModel.CustomizeLocalization);
				}
				if(componentModel.Has_OnInitializing) {
					builder.AddAttribute(12, nameof(DxDocumentViewerCallbacks.OnInitializing), componentModel.OnInitializing);
				}
				if(componentModel.Has_BeforeRender) {
					builder.AddAttribute(13, nameof(DxDocumentViewerCallbacks.BeforeRender), componentModel.BeforeRender);
				}
				if(componentModel.Has_OnServerError) {
					builder.AddAttribute(14, nameof(DxDocumentViewerCallbacks.OnServerError), componentModel.OnServerError);
				}
				if(componentModel.Has_OnExport) {
					builder.AddAttribute(15, nameof(DxDocumentViewerCallbacks.OnExport), componentModel.OnExport);
				}
				if(componentModel.Has_ParametersInitialized) {
					builder.AddAttribute(16, nameof(DxDocumentViewerCallbacks.ParametersInitialized), componentModel.ParametersInitialized);
				}
				if(componentModel.Attributes.Count > 0) {
					builder.AddMultipleAttributes(17, componentModel.Attributes);
				}
				if(addComponentReferenceCapture != default) {
					builder.AddComponentReferenceCapture(18, addComponentReferenceCapture);
				}
				builder.CloseComponent();
			}
			public static int Offset => 19;
		}
	}
}
