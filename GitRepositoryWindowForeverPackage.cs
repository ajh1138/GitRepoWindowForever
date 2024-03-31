global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace Tostadasoft.GitRepositoryWindowForever
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
	[ProvideOptionPage(typeof(OptionsProvider.MyExtensionOptionsOptions), "GitRepositoryWindowForever", "MyExtensionOptions", 0, 0, true, SupportsProfiles = true)]
	[ProvideProfile(typeof(OptionsProvider.MyExtensionOptionsOptions), "GitRepositoryWindowForever", "MyExtensionOptions", 0, 0, true)]
	[Guid(PackageGuids.GitRepositoryWindowForeverString)]
	public sealed class GitRepositoryWindowForeverPackage : ToolkitPackage
	{
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);


			if (MyExtensionOptions.Instance != null)
			{
				if(MyExtensionOptions.Instance.ShowGitRepoWindowOnStartup)
				{
					await OpenGitWindowAsync();
				}

				if (MyExtensionOptions.Instance.ShowGitRepoWindowOnSolutionOpen)
				{
					Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterOpenSolution += this.OnAfterOpenSolution;
				}

				if (MyExtensionOptions.Instance.ShowGitRepoWindowOnDebugStart)
				{
					KnownUIContexts.DebuggingContext.WhenActivated(() =>
					{
						_ = OpenGitWindowAsync();
					});
				}
			}
		}

		public async Task OpenGitWindowAsync()
		{
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(default);

			DTE dte = AsyncPackage.GetGlobalService(typeof(DTE)) as DTE;

			dte.ExecuteCommand("View.GitRepositoryWindow");
		}

		public void OnAfterOpenSolution(object sender = null, EventArgs e = null)
		{
			_ = OpenGitWindowAsync();
		}
	}
}