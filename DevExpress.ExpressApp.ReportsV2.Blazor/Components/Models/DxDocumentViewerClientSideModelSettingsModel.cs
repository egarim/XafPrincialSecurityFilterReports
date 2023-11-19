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
	public class DxDocumentViewerClientSideModelSettingsModel : ComponentModelBase {
		public bool IncludeLocalization {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_IncludeLocalization => HasPropertyValue(nameof(IncludeLocalization));
		public bool IncludeCldrData {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_IncludeCldrData => HasPropertyValue(nameof(IncludeCldrData));
		public bool IncludeCldrSupplemental {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_IncludeCldrSupplemental => HasPropertyValue(nameof(IncludeCldrSupplemental));
	}
	namespace Renderers {
		public static class DxDocumentViewerClientSideModelSettingsRenderer {
			public static RenderFragment GetComponentContent(this DxDocumentViewerClientSideModelSettingsModel componentModel, Action<object> addComponentReferenceCapture = default) =>
				builder => builder.AddComponent<DxDocumentViewerClientSideModelSettings>(0, componentModel, addComponentReferenceCapture);
			public static void AddComponent<TComponent>(this RenderTreeBuilder builder, int _, DxDocumentViewerClientSideModelSettingsModel componentModel, Action<object> addComponentReferenceCapture = default) where TComponent : DxDocumentViewerClientSideModelSettings {
				if(builder is null) {
					throw new ArgumentNullException(nameof(builder));
				}
				if(componentModel is null) {
					throw new ArgumentNullException(nameof(componentModel));
				}
				builder.OpenComponent<TComponent>(0);
				builder.SetKey(componentModel);
				if(componentModel.Has_IncludeLocalization) {
					builder.AddAttribute(1, nameof(DxDocumentViewerClientSideModelSettings.IncludeLocalization), componentModel.IncludeLocalization);
				}
				if(componentModel.Has_IncludeCldrData) {
					builder.AddAttribute(2, nameof(DxDocumentViewerClientSideModelSettings.IncludeCldrData), componentModel.IncludeCldrData);
				}
				if(componentModel.Has_IncludeCldrSupplemental) {
					builder.AddAttribute(3, nameof(DxDocumentViewerClientSideModelSettings.IncludeCldrSupplemental), componentModel.IncludeCldrSupplemental);
				}
				if(componentModel.Attributes.Count > 0) {
					builder.AddMultipleAttributes(4, componentModel.Attributes);
				}
				if(addComponentReferenceCapture != default) {
					builder.AddComponentReferenceCapture(5, addComponentReferenceCapture);
				}
				builder.CloseComponent();
			}
			public static int Offset => 6;
		}
	}
}
