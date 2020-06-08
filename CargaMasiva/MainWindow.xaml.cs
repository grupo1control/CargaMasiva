using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CargaMasiva
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string templatesDirectoryName = "Templates";
        public static string exeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string empresa = "Templates\\et1.ctl";
        public static string persona = "Templates\\et2.ctl";
        public static string log ="Templates\\sqllog.log";
        string control;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (openFileDlg.FileName.Contains(" "))
            {
                TxtBlk.Text = "El archivo no debe contener espacios en blanco";
            }
            else
            {
                FileNameTextBox.Text = openFileDlg.FileName;
            }
         
        }

        private void Cargar_Click(object sender, RoutedEventArgs e)
  
        {
            if (FileNameTextBox.Text != "Ruta")
            {
                System.Diagnostics.Process sysprocess = null;

                string myCommand = "/C sqlldr PFT8461/PFT8461@//34.95.129.4:1521/xe data =" + FileNameTextBox.Text + " control=" + control + " log="+log;

                try
                {

                    ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", myCommand);
                    startInfo.RedirectStandardOutput = false;
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardError = true;
                    System.Diagnostics.Process.Start(startInfo);

                    if ((startInfo.ErrorDialog == false))
                    {
                        TxtBlk.Text = myCommand;
                    }
                    else
                    {
                        TxtBlk.Text = sysprocess.StandardError.ReadToEnd().ToString();
                    }
                }
                catch (Exception ex)
                {
                    TxtBlk.Text = ex.Message.ToString();
                }
            }
            else
            {
                TxtBlk.Text = "Debes seleccionar un archivo";
            }

        }

        private void rbPersona_Checked(object sender, RoutedEventArgs e)
        {
            control = persona;
        }

        private void rbEmpresa_Checked(object sender, RoutedEventArgs e)
        {
            control = empresa;
        }

    }
}
