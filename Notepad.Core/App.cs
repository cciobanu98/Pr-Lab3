using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterCustomAppStart<AppStart>();
        }
    }
}
