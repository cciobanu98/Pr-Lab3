using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.IoC;
using Notepad.Core;

namespace Notepad.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, Notepad.App>
    {
        protected override IMvxIoCProvider InitializeIoC()
        {
            var provider = base.InitializeIoC();
            return SetupIoC.RegisterDependencies(provider);
        }
    }
}
