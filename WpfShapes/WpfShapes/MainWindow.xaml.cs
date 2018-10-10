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

namespace WpfShapes
{
    public partial class MainWindow : Window
    {
        private Brush _currentFillColor;
        
        private Brush _currentBorderColor;
        
        private readonly Point _mouseLoc;

        private readonly DispatcherTimer _dhsTimer = new DispatcherTimer();
        
        private Polyline _expectedBrokenLine;
        
        private Line _expectedLine;

        private readonly List<Point> _currentDrawingBrokenLine;

        private bool _pictureIsSaved;

        private int _currentChosenBrokenLineId;

        private Mode _currentMode;
        
        private bool _dragging;
        
        private System.Windows.Point _clickV;
        
        private static Shape _selectedPolygon;

        public static readonly RoutedCommand SetDrawingModeCommand = new RoutedCommand();
        public static readonly RoutedCommand SetMovingModeCommand = new RoutedCommand();
        public static readonly RoutedCommand SetFillColorCommand = new RoutedCommand();
        public static readonly RoutedCommand SetStrokeColorCommand = new RoutedCommand();
        public static readonly RoutedCommand NewDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand OpenDialogCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            ColorPickerFill.Fill = _currentFillColor;
            _currentBorderColor = new SolidColorBrush(Colors.Black);
            ColorPickerBorder.Fill = _currentBorderColor;
            StartDrawingTicker();
            _currentChosenBrokenLineId = 0;
            _currentDrawingBrokenLine = new List<Point>();
            _currentFillColor = new SolidColorBrush(Colors.Black); 
            _mouseLoc = new Point();
            _currentMode = Mode.Drawing;
            SetShortcuts();
        }
        public enum Mode
        {
            Drawing, Moving
        }
        private static void SetShortcuts()
        {
            SetDrawingModeCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            SetMovingModeCommand.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            SetFillColorCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            SetStrokeColorCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            NewDialogCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            SaveDialogCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            OpenDialogCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
        }
        private void DrawingPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _dragging = false;
        }
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_pictureIsSaved)
            {
                SaveButton_Click(sender, e);
            }
            DrawingPanel.Children.Clear();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrawingPanel.Children.Count > 0 && !_pictureIsSaved)
            {
                FormBl.SaveBrokenLine(ref DrawingPanel);
                _pictureIsSaved = true;
            }
        }
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var BrokenLines = FormBl.ReadBrokenLines();
                if (BrokenLines == null)
                {
                    return;
                }

                _currentChosenBrokenLineId = -1;
                DrawingPanel.Children.Clear();
                foreach (var brokenLine in BrokenLines)
                {
                    DrawingPanel.Children.Add(brokenLine.ToPolygon());
                    var newMenuItem = new MenuItem { Header = brokenLine.Name };
                    newMenuItem.Click += SetCurrentBrokenLineFromMenu;
                    ShapesMenu.Items.Add(newMenuItem);
                }

                _currentChosenBrokenLineId = DrawingPanel.Children.Count - 1;
                _pictureIsSaved = true;
            }
            catch (Exception exc)
            {
                FormBl.MessageBoxFatal(exc.Message);
            }
        }
        private void SetCurrentBrokenLineFromMenu(object sender, RoutedEventArgs e)
        {
            var menuItem = e.OriginalSource as MenuItem;
            try
            {
                if (menuItem != null)
                {
                    _currentChosenBrokenLineId = Util.GetBrokenLineIdByName(menuItem.Header.ToString(), DrawingPanel.Children);
                }
            }
            catch (InvalidDataException exc)
            {
                FormBl.MessageBoxFatal(exc.Message);
            }
        }
        private void SetFillColor(object sender, RoutedEventArgs e)
        {
            FormBl.SetColor(ref _currentFillColor, ref ColorPickerFill);
        }
        private void SetBorderColor(object sender, RoutedEventArgs e)
        {
            FormBl.SetColor(ref _currentBorderColor, ref ColorPickerBorder);
        }
        private void StartDrawingTicker()
        {
            _dhsTimer.Interval = TimeSpan.FromMilliseconds(10);
            _dhsTimer.Tick += DrawingHexagonSide;
            _dhsTimer.Start();
        }
        private void DrawingBrokenLineSide(object sender, EventArgs e)
        {
            if (_currentDrawingBrokenLine.Count > 0)
            {
                var lastPoint = _currentDrawingBrokenLine[_currentDrawingBrokenLine.Count - 1];
                _expectedLine.X1 = lastPoint.X;
                _expectedLine.Y1 = lastPoint.Y;
                _expectedLine.X2 = _mouseLoc.X;
                _expectedLine.Y2 = _mouseLoc.Y;
            }
        }
        private void SetDrawingMode(object sender, RoutedEventArgs e)
        {
            _currentMode = Mode.Drawing;
        }
        private void SetMovingMode(object sender, RoutedEventArgs e)
        {
            ClearExpectedBrokenLine();
            _currentMode = Mode.Moving;
        }

    }
}
