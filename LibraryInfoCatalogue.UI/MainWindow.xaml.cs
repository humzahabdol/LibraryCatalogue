using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryOrganizerDB;

namespace LibraryInfoCatalogue.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDB();
            Menu menu = new Menu();
            frame_mainwindow.Content = menu;
        }

        private void LoadDB()
        {
            Database.SetInitializer<LibraryContext>(new LibraryInitializer());
        }
    }
}
