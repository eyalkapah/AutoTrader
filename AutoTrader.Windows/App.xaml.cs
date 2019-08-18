using AutoTrader.Core;
using AutoTrader.Interfaces.Interfaces;
using MvvmCross;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AutoTrader.Windows
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
        {
            base.OnLaunched(activationArgs);

            InitApp();
        }

        private void InitApp()
        {
            try
            {
                TryInitContentFromCache();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void TryInitContentFromCache()
        {
            var appLifeCycleService = Mvx.IoCProvider.Resolve<IAppLifeCycleService>();
            appLifeCycleService.InitFromCache();
        }
    }

    public abstract class AutoTraderApp : MvxApplication<MvxWindowsSetup<CoreApp>, CoreApp>
    {
    }
}