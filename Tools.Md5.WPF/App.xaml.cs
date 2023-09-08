using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Tools.Md5.Core;
using Tools.Md5.WPF.Services;

namespace Tools.Md5.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var view = new MainWindow()
            {
                
            };

            view.Loaded += (s, e) =>
            {
                if (s is Window w) w.DataContext = new Md5ViewModel(new DialogService());
            };

            view.Show();
            view.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1f1f1f"));
        }
    }
}
