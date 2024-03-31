using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Tostadasoft.GitRepositoryWindowForever
{
	internal partial class OptionsProvider
	{
		[ComVisible(true)]
		public class MyExtensionOptionsOptions : BaseOptionPage<MyExtensionOptions> { }
	}

	public class MyExtensionOptions : BaseOptionModel<MyExtensionOptions>
	{
		[Category("General")]
		[DisplayName("Always show on Visual Studio startup")]
		[Description("Note: Setting to False will not close the Git Repo window.")]
		[DefaultValue(true)]
		public bool ShowGitRepoWindowOnStartup { get; set; } = true;

		[Category("General")]
		[DisplayName("Show on Debug start")]
		[Description("Note: Setting to False will not close the Git Repo window on debug.")]
		[DefaultValue(true)]
		public bool ShowGitRepoWindowOnDebugStart { get; set; } = true;

		[Category("General")]
		[DisplayName("Always show after opening a Solution")]
		[Description("Note: Setting to False will not close the Git Repo window when a solution is loaded.")]
		[DefaultValue(true)]
		public bool ShowGitRepoWindowOnSolutionOpen { get; set; } = true;
	}
}
