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

#nullable enable
using System;
using DevExpress.Blazor.Reporting;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.ReportsV2.Blazor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace DevExpress.ExpressApp.Blazor.ApplicationBuilder {
	public static class ReportsApplicationBuilderExtensions {
		public static IModuleBuilder<IBlazorApplicationBuilder> AddReports(this IModuleBuilder<IBlazorApplicationBuilder> builder) {
			return builder.AddReports(_ => { });
		}
		public static IModuleBuilder<IBlazorApplicationBuilder> AddReports(
				this IModuleBuilder<IBlazorApplicationBuilder> builder,
				Action<ReportsOptions> configureOptions) {
			ReportsOptions? reportsModuleOptions = null;
			builder.Context.ServerConfiguration.Services.AddXafReporting(o => {
				reportsModuleOptions = new ReportsOptions(o);
				configureOptions.Invoke(reportsModuleOptions);
			});
			builder.Context.ServerConfiguration.ConfigureApplicationBuilder(app => app.UseDevExpressBlazorReporting());
			builder.Add((serviceProvider) => {
				_ = serviceProvider.GetRequiredService<IOptions<ReportOptions>>().Value;
				ArgumentNullException.ThrowIfNull(reportsModuleOptions);
				return new ReportsModuleV2(
					serviceProvider.GetRequiredService<IReportDataSourceHelper>(),
					serviceProvider.GetRequiredService<IReportStorage>(),
					serviceProvider.GetRequiredService<IInplaceReportCacheHelper>(),
					reportsModuleOptions);
			});
			builder.Add((serviceProvider) => {
				_ = serviceProvider.GetRequiredService<IOptions<ReportOptions>>().Value;
				ArgumentNullException.ThrowIfNull(reportsModuleOptions);
				return new ReportsBlazorModuleV2(reportsModuleOptions);
			});
			return builder;
		}
	}
}
#nullable restore
