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

            List<Order> init_orders = new List<Order>()
            {
                new Order(1, 100, "time 1", "university", "+12341", "not assigned"),
                new Order(5, 200, "time 2", "cafe", "+512341", "already assigned"),
                new Order(7, 7200, "time 7", "library", "+672341", "done"),
            };
            
            List<TaxiDriver> init_drivers = new List<TaxiDriver>()
            {
                new TaxiDriver("Petro",  null),
                new TaxiDriver("Vasyl",  new List<int>() {1, 5, 7})
            };
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
