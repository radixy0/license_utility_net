using System;
using System.Collections.Generic;
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
using Microsoft.Win32;

namespace License_Utility2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const String REGKEY  = @"Software\Siemens_PLM_Software\Common_Licensing";
        const String REGVALUE = @"SE_SERVER";

        public MainWindow()
        {
            InitializeComponent();
        }


        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTextBox();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            if (delete_RB.IsChecked == true)
            {
                deleteVar();
            }
            else if (LF_RB.IsChecked == true)
            {
                setVar(LF_TextBox.Text);
            }
            else if (LS_RB.IsChecked == true)
            {
                setVar(License_Server_name_Box.Text);
            }
            UpdateTextBox();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Help(object sender, RoutedEventArgs e)
        {
            //Display help from file
            MessageBox.Show("Help!");
        }

        private void Button_Click_Browse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odf = new OpenFileDialog();
            odf.Filter = "License files (*.lic, *.txt, *.dat)|*.LIC;*.TXT;*.DAT|All files (*.*)|*.*";
            if (odf.ShowDialog() == true)
            {
                LF_TextBox.Text = odf.FileName;
                LF_RB.IsChecked = true;
            }
        }

        private void UpdateTextBox()
        {
            TextBox_value.Text = getVar();
        }

        //logic

        private String getVar()
        {
            RegistryKey subkey = Registry.CurrentUser.OpenSubKey(REGKEY);
            Object result = subkey.GetValue(REGVALUE);
            if(result == null)
            {
                return "Value not found";
            }
            else
            {
                return (String)result;
            }
        }
        private void setVar(String newVar)
        {
            RegistryKey subkey = Registry.CurrentUser.OpenSubKey(REGKEY, true);
            subkey.SetValue(REGVALUE, newVar);
        }
        private void deleteVar()
        {
            RegistryKey subkey = Registry.CurrentUser.OpenSubKey(REGKEY, true);
            if(subkey.GetValue(REGVALUE) != null)
            {
                subkey.DeleteValue(REGVALUE);
            }
        }
    }

}
