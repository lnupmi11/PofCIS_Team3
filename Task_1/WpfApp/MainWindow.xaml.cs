namespace WpfApp
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using Microsoft.Win32;
    using WpfApp.Models;
    using WpfApp.Utils;
    using WpfApp.UOW;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TaxiDriver> drivers = new List<TaxiDriver>();
        private List<Order> orders = new List<Order>();

        private TaxiDriver selectedTaxiDriver;

        public MainWindow()
        {
            this.InitializeComponent();

            Seeds.Execute(ref this.drivers);
            Seeds.Execute(ref this.orders);

            this.UpdateDriversUI();
            if (this.drivers.Count != 0)
            {
                this.selectedTaxiDriver = this.drivers.First();
                this.UpdateSelectedDriverUI();
            }

            this.UpdateOrdersUI();
        }

        /// <summary>
        /// Method to open file with drivers from file dialog
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs.</param>
        private void OnOpenDriversClick(object sender, RoutedEventArgs e)
        {
            var unit = new UnitOfWork();

            this.drivers = unit.Drivers.Get().ToList();
            this.UpdateDriversUI();
            if (this.drivers.Count != 0)
            {
                this.selectedTaxiDriver = this.drivers.First();
                this.UpdateSelectedDriverUI();
            }
            this.orders = unit.Orders.Get().ToList();
            this.UpdateOrdersUI();
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    this.drivers = (List<TaxiDriver>)Serialization.DeserializeDrivers(openFileDialog.FileName);
            //    this.UpdateDriversUI();
            //    if (this.drivers.Count != 0)
            //    {
            //        this.selectedTaxiDriver = this.drivers.First();
            //        this.UpdateSelectedDriverUI();
            //    }
            //}
        }

        /// <summary>
        /// Method to save file with drivers in file dialog menu.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs.</param>
        private void OnSaveDriversClick(object sender, RoutedEventArgs e)
        {
            var unit = new UnitOfWork();
            unit.Drivers.UpdateList(this.drivers);
            unit.Orders.UpdateList(this.orders);
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "XML file (*.xml) | *.xml";

            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    Serialization.Serialize(saveFileDialog.FileName, this.drivers);
            //}
        }

        /// <summary>
        /// Method to open file with orders from file dialog
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs.</param>
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
        /// <param name="e">RoutedEventArgs.</param>
        private void OnSaveOrdersClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml) | *.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                Serialization.Serialize(saveFileDialog.FileName, this.orders);
            }
        }

        /// <summary>
        /// Method to update all drivers UI.
        /// </summary>
        private void UpdateDriversUI()
        {
            this.allDrivers.ItemsSource = this.drivers;
            ICollectionView view = CollectionViewSource.GetDefaultView(this.allDrivers.ItemsSource);
            view.Refresh();
        }

        /// <summary>
        /// Method to update selected driver UI.
        /// </summary>
        private void UpdateSelectedDriverUI()
        {
            this.nameText.Text = this.selectedTaxiDriver.Name;
            this.ordersText.Text = this.selectedTaxiDriver.OrderIds;
        }

        /// <summary>
        /// Method to update orders UI.
        /// </summary>
        private void UpdateOrdersUI()
        {
            this.allOrders.ItemsSource = this.orders.Where(i => i.Status != "already assigned").ToList();
        }

        /// <summary>
        /// Method to assign order to selected driver.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs.</param>
        private void OnAsignOrderButtonClick(object sender, RoutedEventArgs e)
        {
            int selectedOrderIndex = this.allOrders.SelectedIndex;
            if (selectedOrderIndex != -1)
            {
                Order selectedOrder = this.orders.Where(i => i.Status != "already assigned").ToList()[selectedOrderIndex];
                if (selectedOrder.Status == "not assigned")
                {
                    this.selectedTaxiDriver.AssignOrder(selectedOrder);

                    selectedOrder.Status = "already assigned";

                    this.UpdateDriversUI();
                    this.UpdateSelectedDriverUI();
                    this.UpdateOrdersUI();
                }
            }
        }

        /// <summary>
        /// Method to select driver.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs.</param>
        private void OnSelectDriverButtonClick(object sender, RoutedEventArgs e)
        {
            int selectedTaxiDriverIndex = this.allDrivers.SelectedIndex;
            if (selectedTaxiDriverIndex != -1)
            {
                this.selectedTaxiDriver = this.drivers[selectedTaxiDriverIndex];
                this.UpdateSelectedDriverUI();
            }
        }

        /// <summary>
        /// Method to select not assigned order.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">RoutedEventArgs.</param>
        private void OnAsignRandomOrderButtonClick(object sender, RoutedEventArgs e)
        {
            BL.BL bl = new BL.BL();
            bl.TaxiDrivers = this.drivers;
            bl.Orders = this.orders;
            List<Order> freeOrders = bl.FindOrderByStatus("not assigned");
            if (freeOrders.Count != 0)
            {
                Order freeOrder = freeOrders[0];
                this.selectedTaxiDriver.AssignOrder(freeOrder);

                freeOrder.Status = "already assigned";

                this.UpdateDriversUI();
                this.UpdateSelectedDriverUI();
                this.UpdateOrdersUI();
            }
        }
    }
}
