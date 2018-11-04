using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp.Models;
using WpfApp.Utils;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TaxiDriver> drivers = new List<TaxiDriver>();
        List<Order> orders = new List<Order>();

        public MainWindow()
        {
            InitializeComponent();

            Seeds.Execute(ref drivers);
            Seeds.Execute(ref orders);

        }

        private void OnOpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                drivers = (List<TaxiDriver>) Serialization.DeserializeDrivers(openFileDialog.FileName);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml) | *.xml";
            
            if (saveFileDialog.ShowDialog() == true)
                Serialization.Serialize(saveFileDialog.FileName, drivers);
        }
    }
}
