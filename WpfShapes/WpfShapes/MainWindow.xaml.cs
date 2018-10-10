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
        private void ProcessDrawingOfBrokenLine(object sender, MouseButtonEventArgs e)
        {
            if (_currentMode == Mode.Drawing)
            {
                if (_currentDrawingBrokenLine.Count < 6)
                {
                    var mousePos = e.GetPosition(DrawingPanel);
                    for (var i = _currentDrawingBrokenLine.Count - 2; i > 0; i--)
                    {
                        if (Util.AreSidesIntersected(
                            new System.Windows.Point(mousePos.X, mousePos.Y),
                            new System.Windows.Point(
                                _expectedBrokenLine.Points[_currentDrawingBrokenLine.Count - 1].X,
                                _expectedBrokenLine.Points[_currentDrawingBrokenLine.Count - 1].Y),
                            _expectedBrokenLine.Points[i],
                            _expectedBrokenLine.Points[i - 1]))
                        {
                            return;
                        }
                    }

                    for (var i = _currentDrawingBrokenLine.Count - 1; i >= 0; i--)
                    {
                        if (new System.Windows.Point(mousePos.X, mousePos.Y) == _expectedBrokenLine.Points[i])
                        {
                            return;
                        }
                    }

                    if (_currentDrawingBrokenLine.Count == 5)
                    {
                        for (var i = _currentDrawingBrokenLine.Count - 2; i > 1; i--)
                        {
                            if (Util.AreSidesIntersected(
                                new System.Windows.Point(mousePos.X, mousePos.Y),
                                new System.Windows.Point(
                                    _expectedBrokenLine.Points[0].X,
                                    _expectedBrokenLine.Points[0].Y),
                                _expectedBrokenLine.Points[i],
                                _expectedBrokenLine.Points[i - 1]))
                            {
                                return;
                            }
                        }
                    }

                    if (_currentDrawingBrokenLine.Count > 2 && Util.Orientation(
                            new System.Windows.Point(mousePos.X, mousePos.Y),
                            new System.Windows.Point(
                                _expectedBrokenLine.Points[_currentDrawingBrokenLine.Count - 1].X,
                                _expectedBrokenLine.Points[_currentDrawingBrokenLine.Count - 1].Y),
                            new System.Windows.Point(
                                _expectedBrokenLine.Points[_currentDrawingBrokenLine.Count - 1].X,
                                _expectedBrokenLine.Points[_currentDrawingBrokenLine.Count - 2].Y))
                        == 0)
                    {
                        return;
                    }

                    _currentDrawingBrokenLine.Add(new Point(mousePos.X, mousePos.Y));
                    if (_expectedBrokenLine == null)
                    {
                        _expectedBrokenLine = new Polyline
                        {
                            Stroke = _currentBorderColor,
                            Opacity = 1,
                            StrokeThickness = 2
                        };
                        DrawingPanel.Children.Add(_expectedBrokenLine);
                        _expectedLine = Util.GetLine(
                            new Point(_currentDrawingBrokenLine[0].X, _currentDrawingBrokenLine[0].Y),
                            new Point(mousePos.X, mousePos.Y),
                            _currentBorderColor);
                        _expectedLine.StrokeThickness = 2;
                        DrawingPanel.Children.Add(_expectedLine);
                    }

                    _expectedBrokenLine.Points.Add(new System.Windows.Point(mousePos.X, mousePos.Y));
                }

                if (_currentDrawingBrokenLine.Count == 6)
                {
                    var hexagon = new BrokenLine(
                        $"BrokenLine_{_currentChosenBrokenLineId + 1}",
                        _currentDrawingBrokenLine,
                        _currentFillColor,
                        _currentBorderColor).ToPolygon();
                    _currentChosenBrokenLineId++;
                    _pictureIsSaved = false;
                    hexagon.KeyDown += MoveBrokenLineWithKeys;
                    DrawingPanel.Children.Add(brokenLine);
                    Canvas.SetLeft(brokenLine, 0);
                    Canvas.SetTop(brokenLine, 0);
                    var newMenuItem = new MenuItem { Header = brokenLine.Name };
                    newMenuItem.Click += SetCurrentBrokenLineFromMenu;
                    ShapesMenu.Items.Add(newMenuItem);
                    ClearExpectedBrokenLinen();
                }
            }
        }
        private void MoveBrokenLineWithKeys(object sender, KeyEventArgs e)
        {
            try
            {
                if (Keyboard.IsKeyDown(Key.Escape))
                {
                    ClearExpectedBrokenLine();
                }
                else
                {
                    if (_currentMode == Mode.Moving && _currentChosenBrokenLineId > -1 && DrawingPanel.Children.Count > 0)
                    {
                        var newLoc = new System.Windows.Point(0, 0);
                        if (Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Right))
                        {
                            newLoc.Y -= 5;
                            newLoc.X += 5;
                        }
                        else if (Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Left))
                        {
                            newLoc.Y -= 5;
                            newLoc.X -= 5;
                        }
                        else if (Keyboard.IsKeyDown(Key.Down) && Keyboard.IsKeyDown(Key.Right))
                        {
                            newLoc.Y += 5;
                            newLoc.X += 5;
                        }
                        else if (Keyboard.IsKeyDown(Key.Down) && Keyboard.IsKeyDown(Key.Left))
                        {
                            newLoc.Y += 5;
                            newLoc.X -= 5;
                        }
                        else if (Keyboard.IsKeyDown(Key.Up))
                        {
                            newLoc.Y -= 5;
                        }
                        else if (Keyboard.IsKeyDown(Key.Right))
                        {
                            newLoc.X += 5;
                        }
                        else if (Keyboard.IsKeyDown(Key.Down))
                        {
                            newLoc.Y += 5;
                        }
                        else if (Keyboard.IsKeyDown(Key.Left))
                        {
                            newLoc.X -= 5;
                        }

                        if (!((DrawingPanel.Children[_currentChosenBrokenLineId] as Shape) is Polygon p))
                        {
                            throw new InvalidDataException("can't move null shape");
                        }

                        Canvas.SetLeft(p, Canvas.GetLeft(p) + newLoc.X);
                        Canvas.SetTop(p, Canvas.GetTop(p) + newLoc.Y);
                    }
                }
            }
            catch (Exception exc)
            {
                FormBl.MessageBoxFatal(exc.ToString());
            }
        }
        private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging && _currentMode == Mode.Moving)
            {
                if (!(_selectedPolygon is Polygon p))
                {
                    throw new InvalidDataException("selected shape is not Broken Line");
                }

                Canvas.SetLeft(p, e.GetPosition(DrawingPanel).X - _clickV.X);
                Canvas.SetTop(p, e.GetPosition(DrawingPanel).Y - _clickV.Y);
            }

            if (_currentMode == Mode.Drawing)
            {
                var point = e.GetPosition(this);
                _mouseLoc.X = point.X + 7;
                _mouseLoc.Y = point.Y - 25;
            }
        }
        private void MyPoly_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentChosenBrokenLineId > -1 && _currentMode == Mode.Moving)
            {
                for (var i = DrawingPanel.Children.Count - 1; i >= 0; i--)
                {
                    _selectedPolygon = DrawingPanel.Children[i] as Shape;
                    _clickV = e.GetPosition(_selectedPolygon);
                    if (Util.PointIsInBrokenLine(new Point(_clickV.X, _clickV.Y), _selectedPolygon as Polygon))
                    {
                        _currentChosenBrokenLineId = i;
                        _dragging = true;
                        return;
                    }
                }
            }
        }
        private void ClearExpectedBrokenLine()
        {
            _currentDrawingBrokenLine.Clear();
            DrawingPanel.Children.Remove(_expectedBrokenLine);
            DrawingPanel.Children.Remove(_expectedLine);
            _expectedBrokenLine = null;
            _expectedLine = null;
        }
    }
}
