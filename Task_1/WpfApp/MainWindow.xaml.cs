namespace WpfApp
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Microsoft.Win32;
    using WpfApp.Models;
    using WpfApp.Utils;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TaxiDriver> drivers = new List<TaxiDriver>();
        private List<Order> orders = new List<Order>();

        public MainWindow()
        {
            this.InitializeComponent();

            Seeds.Execute(ref this.drivers);
            Seeds.Execute(ref this.orders);
            this.UpdateDriversUI();
            this.UpdateOrdersUI();
        }

        /// <summary>
        /// Method to open file with drivers from file dialog
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs</param>
        private void OnOpenDriversClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.drivers = (List<TaxiDriver>)Serialization.DeserializeDrivers(openFileDialog.FileName);
                this.UpdateDriversUI();
            }
        }

        /// <summary>
        /// Method to save file with drivers in file dialog menu.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs</param>
        private void OnSaveDriversClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml) | *.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                Serialization.Serialize(saveFileDialog.FileName, this.drivers);
            }
        }

        /// <summary>
        /// Method to open file with orders from file dialog
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs</param>
        private void OnOpenOrdersClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.orders = (List<Order>)Serialization.DeserializeOrders(openFileDialog.FileName);
                this.UpdateOrdersUI();
            }
        }

        /// <summary>
        /// Method to save file with orders in file dialog menu.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs</param>
        private void OnSaveOrdersClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml) | *.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                Serialization.Serialize(saveFileDialog.FileName, this.orders);
            }
        }

        private void UpdateDriversUI()
        {
            this.allDrivers.ItemsSource = this.drivers;
            if (this.drivers.Count != 0)
            {
                this.nameText.Text = this.drivers.First().Name;
                this.ordersText.Text = this.drivers.First().OrderIds;
            }
        }

        private void UpdateOrdersUI()
        {
            this.allOrders.ItemsSource = this.orders.Where(i => i.Status != "already assigned").ToList();
        }
    }
}
