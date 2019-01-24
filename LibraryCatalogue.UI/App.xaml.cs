using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LibraryCatalogue.Helpers;
using Search;
using Unity;

namespace LibraryCatalogue.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISqlDbRepository, SqlDbRepository>();
            container.RegisterType<ILibrarianHelper, LibrarianHelper>();
            container.RegisterType<ISearchClass, SearchClass>();
            var window = container.Resolve<MainWindow>();
            container.Resolve<LibrarianHelper>();
            //var window = new MainWindow { DataContext = mainWindowViewModel };
            window.Show();
        }
    }
}
