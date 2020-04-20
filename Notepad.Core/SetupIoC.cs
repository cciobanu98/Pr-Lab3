using Acr.UserDialogs;
using MvvmCross.IoC;

namespace Notepad.Core
{
    public static class SetupIoC
    {
        public static IMvxIoCProvider RegisterDependencies(IMvxIoCProvider provider)
        {
            provider.RegisterSingleton(() => UserDialogs.Instance);
            provider.LazyConstructAndRegisterSingleton<IClient, Client>();
            return provider;
        }
    }
}
