using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInfoCatalogue.Helper.BusinessClass
{
    public class ViewHelper
    {
        public void Hide(Grid containerCanvas)
        {
            foreach (Control ctl in containerCanvas.Children)
            {

                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Label))
                    ((Label)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(ListView))
                    ((ListView)ctl).Visibility = Visibility.Collapsed;
                if (ctl.GetType() == typeof(Button))
                    ((Button)ctl).Visibility = Visibility.Collapsed;
                if (ctl is PasswordBox box)
                    box.Visibility = Visibility.Collapsed;
            }
        }
        public void ClearTextBox(Grid containerCanvas)
        {
            foreach (Control ctl in containerCanvas.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
                //if (ctl.GetType() == typeof(ListView))
                //{
                //    ((ListView)ctl).Items.Clear();
                //}
            }
        }
    }
}
