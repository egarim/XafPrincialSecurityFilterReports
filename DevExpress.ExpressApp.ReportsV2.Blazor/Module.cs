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
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.ReportsV2.Blazor.Controllers;
using DevExpress.ExpressApp.Updating;
using DevExpress.Utils;
using DevExpress.XtraReports.Native;
namespace DevExpress.ExpressApp.ReportsV2.Blazor {
	[DXToolboxItem(true)]
	[ToolboxItemFilter("Xaf.Platform.Blazor")]
	[ToolboxTabName(XafAssemblyInfo.DXTabXafModules)]
	[Description("Extends the ReportsModuleV2 with Controllers that manage reports in Blazor applications.")]
	public sealed class ReportsBlazorModuleV2 : ModuleBase, IModelNodesGeneratorUpdater {
		public const string ReportViewerId = "ReportViewer";
		public const string ReportDesignerId = "ReportDesigner";
		public const string ReportViewerDetailViewName = "ReportViewer_DetailView";
		public const string ReportDesignerDetailViewName = "ReportDesigner_DetailView";
		private readonly DevExpress.ExpressApp.ReportsV2.Blazor.ReportsOptions options;
		public ReportsBlazorModuleV2() : this(new ReportsOptions(new ReportOptions())) {
		}
		public ReportsBlazorModuleV2(DevExpress.ExpressApp.ReportsV2.Blazor.ReportsOptions options) {
			this.options = options;
		}
		protected override IEnumerable<Type> GetRegularTypes() => new Type[] { typeof(IModelReportDesignerViewItem) };
		protected override IEnumerable<Type> GetDeclaredExportedTypes() => Type.EmptyTypes;
		protected override IEnumerable<Type> GetDeclaredControllerTypes() => new Type[] {
			typeof(ReportsController),
			typeof(BlazorReportServiceController),
			typeof(BlazorEditReportController),
			typeof(ReportProcessViewShortcutController),
			typeof(BlazorReportWizardDialogController),
			typeof(HideToolbarController)
		};
		protected override void RegisterEditorDescriptors(EditorDescriptorsFactory editorDescriptorsFactory) {
			editorDescriptorsFactory.RegisterViewItem(typeof(IModelReportDesignerViewItem), typeof(ReportDesignerViewItem), true);
		}
		protected override ModuleTypeList GetRequiredModuleTypesCore() {
			ModuleTypeList requiredModules = base.GetRequiredModuleTypesCore();
			requiredModules.Add(typeof(ReportsModuleV2));
			requiredModules.Add(typeof(SystemBlazorModule));
			return requiredModules;
		}
		public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) => ModuleUpdater.EmptyModuleUpdaters;
		public override void Setup(XafApplication application) {
			base.Setup(application);
			ApplicationReportObjectSpaceProvider.ContextApplication = application;
			PrintSelectionBaseController.ShowInReportActionEnableModeDefault = PrintSelectionBaseController.ActionEnabledMode.ModifiedChanged;
			application.SetupComplete += Application_SetupComplete;
			application.LoggedOn += Application_LoggedOn;
		}
		void IModelNodesGeneratorUpdater.UpdateNode(ModelNode node) {
			AddReportViewerModelView(node);
			AddReportDesignerModelView(node);
		}
		public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters) {
			base.AddGeneratorUpdaters(updaters);
			updaters.Add(this);
		}
		Type IModelNodesGeneratorUpdater.GeneratorType { get { return typeof(ModelViewsNodesGenerator); } }
		private void Application_SetupComplete(object sender, EventArgs e) {
			XafApplication application = (XafApplication)sender;
			application.SetupComplete -= Application_SetupComplete;
			SetReportViewerModelView(application);
		}
		private void Application_LoggedOn(object sender, LogonEventArgs e) {
			XafApplication application = (XafApplication)sender;
			application.LoggedOn -= Application_LoggedOn;
			ReportDesignExtensionManager.CreateReportExtension += CreateReportExtension;
			ReportDesignExtensionManager.CustomRegisterReportExtension += CustomRegisterReportExtension;
			ReportDesignExtensionManager.Initialize(application);
			ReportDesignExtensionManager.CreateReportExtension -= CreateReportExtension;
			ReportDesignExtensionManager.CustomRegisterReportExtension -= CustomRegisterReportExtension;
			void CreateReportExtension(object sender, CreateCustomReportExtensionEventArgs e) {
				e.ReportDesignExtension = new BlazorReportSerializer();
			}
			void CustomRegisterReportExtension(object sender, CustomizeReportExtensionEventArgs e) {
				if(e.ReportDesignExtension is BlazorReportSerializer reportSerializer) {
					SerializationService.RegisterSerializer(ReportsModuleV2.XtraReportContextName, reportSerializer);
					reportSerializer.FillParametersTypes(application);
				}
			}
		}
		private void AddReportViewerModelView(ModelNode node) {
			IModelDetailView modelView = FindReportModelView(node, ReportViewerDetailViewName);
			if(modelView == null) {
				AddReportModelView(node.Application, ReportViewerDetailViewName, ReportViewerId);
			}
		}
		private void AddReportDesignerModelView(ModelNode node) {
			IModelDetailView modelView = FindReportModelView(node, ReportDesignerDetailViewName);
			if(modelView == null) {
				modelView = AddReportModelView(node.Application, ReportDesignerDetailViewName, ReportDesignerId);
				if(modelView != null) {
					modelView.Items.AddNode<IModelReportDesignerViewItem>(ReportDesignerId);
				}
			}
		}
		private IModelDetailView FindReportModelView(ModelNode node, string modelId) {
			IModelApplication modelApplication = node.Application;
			return modelApplication.Views.GetNode(modelId) as IModelDetailView;
		}
		private IModelDetailView AddReportModelView(IModelApplication modelApplication, string modelId, string itemId) {
			IModelClass reportDataModelClass = GetReportDataModelClass(modelApplication);
			IModelDetailView modelView = null;
			if(reportDataModelClass != null) {
				modelView = modelApplication.Views.AddNode<IModelDetailView>(modelId);
				modelView.ModelClass = reportDataModelClass;
				IModelLayoutGroup modelLayoutGroup = (IModelLayoutGroup)modelView.Layout.GetNode("Main");
				if(modelLayoutGroup != null) {
					modelLayoutGroup.ClearNodes();
					IModelLayoutViewItem modelLayoutItem = modelLayoutGroup.AddNode<IModelLayoutViewItem>(itemId);
					modelLayoutItem.Index = 0;
					modelLayoutItem.ViewItem = null;
				}
			}
			return modelView;
		}
		private IModelClass GetReportDataModelClass(IModelApplication modelApplication) {
			IModelClass reportDataModelClass = null;
			if(Application != null) {
				ReportsModuleV2 reportsModuleV2 = Application.Modules.FindModule<ReportsModuleV2>();
				if(reportsModuleV2 != null) {
					reportDataModelClass = modelApplication.BOModel.GetClass(reportsModuleV2.ReportDataType);
				}
			}
			return reportDataModelClass;
		}
		private void SetReportViewerModelView(XafApplication application) {
			ReportsModuleV2 reportsModuleV2 = Application.Modules.FindModule<ReportsModuleV2>();
			if(reportsModuleV2 != null) {
				IModelDetailView modelView = application.Model.Views[ReportViewerDetailViewName] as IModelDetailView;
				if((modelView != null) && (modelView.ModelClass == null)) {
					modelView.ModelClass = application.Model.BOModel[reportsModuleV2.ReportDataType.FullName];
				}
			}
		}
		[Description("")]
		public DesignAndPreviewDisplayModes DesignAndPreviewDisplayMode {
			get { return options.DesignAndPreviewDisplayMode; }
			set { options.DesignAndPreviewDisplayMode = value; }
		}
	}
	public enum DesignAndPreviewDisplayModes {
		DetailView,
		Popup
	}
}
