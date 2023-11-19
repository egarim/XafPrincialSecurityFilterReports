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
	public class DxDocumentViewerModel : ComponentModelBase {
		public string ReportName {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ReportName => HasPropertyValue(nameof(ReportName));
		public bool MobileMode {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_MobileMode => HasPropertyValue(nameof(MobileMode));
		public bool AccessibilityCompliant {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_AccessibilityCompliant => HasPropertyValue(nameof(AccessibilityCompliant));
		public bool RightToLeft {
			get => GetPropertyValue<bool>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_RightToLeft => HasPropertyValue(nameof(RightToLeft));
		public string Width {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_Width => HasPropertyValue(nameof(Width));
		public string Height {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_Height => HasPropertyValue(nameof(Height));
		public RenderFragment ChildContent {
			get => GetPropertyValue<RenderFragment>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_ChildContent => HasPropertyValue(nameof(ChildContent));
		public string CssClass {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_CssClass => HasPropertyValue(nameof(CssClass));
		public string Id {
			get => GetPropertyValue<string>();
			set => SetPropertyValue(value);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Has_Id => HasPropertyValue(nameof(Id));
	}
	namespace Renderers {
		public static class DxDocumentViewerRenderer {
			public static RenderFragment GetComponentContent(this DxDocumentViewerModel componentModel, Action<object> addComponentReferenceCapture = default) =>
				builder => builder.AddComponent<DxDocumentViewer>(0, componentModel, addComponentReferenceCapture);
			public static void AddComponent<TComponent>(this RenderTreeBuilder builder, int _, DxDocumentViewerModel componentModel, Action<object> addComponentReferenceCapture = default) where TComponent : DxDocumentViewer {
				if(builder is null) {
					throw new ArgumentNullException(nameof(builder));
				}
				if(componentModel is null) {
					throw new ArgumentNullException(nameof(componentModel));
				}
				builder.OpenComponent<TComponent>(0);
				builder.SetKey(componentModel);
				if(componentModel.Has_ReportName) {
					builder.AddAttribute(1, nameof(DxDocumentViewer.ReportName), componentModel.ReportName);
				}
				if(componentModel.Has_MobileMode) {
					builder.AddAttribute(2, nameof(DxDocumentViewer.MobileMode), componentModel.MobileMode);
				}
				if(componentModel.Has_AccessibilityCompliant) {
					builder.AddAttribute(3, nameof(DxDocumentViewer.AccessibilityCompliant), componentModel.AccessibilityCompliant);
				}
				if(componentModel.Has_RightToLeft) {
					builder.AddAttribute(4, nameof(DxDocumentViewer.RightToLeft), componentModel.RightToLeft);
				}
				if(componentModel.Has_Width) {
					builder.AddAttribute(5, nameof(DxDocumentViewer.Width), componentModel.Width);
				}
				if(componentModel.Has_Height) {
					builder.AddAttribute(6, nameof(DxDocumentViewer.Height), componentModel.Height);
				}
				if(componentModel.Has_ChildContent) {
					builder.AddAttribute(7, nameof(DxDocumentViewer.ChildContent), componentModel.ChildContent);
				}
				if(componentModel.Has_CssClass) {
					builder.AddAttribute(8, nameof(DxDocumentViewer.CssClass), componentModel.CssClass);
				}
				if(componentModel.Has_Id) {
					builder.AddAttribute(9, nameof(DxDocumentViewer.Id), componentModel.Id);
				}
				if(componentModel.Attributes.Count > 0) {
					builder.AddMultipleAttributes(10, componentModel.Attributes);
				}
				if(addComponentReferenceCapture != default) {
					builder.AddComponentReferenceCapture(11, addComponentReferenceCapture);
				}
				builder.CloseComponent();
			}
			public static int Offset => 12;
		}
	}
}
