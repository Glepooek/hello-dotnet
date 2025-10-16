using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace Test.InkCanvasDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DrawingAttributes _drawingAttributes;
        private Point _startPoint = new Point(-1, 0);
        private Point _endPoint = new Point(-1, 0);
        private Stroke _stroke;
        private bool _isMouseMove;

        public MainWindow()
        {
            InitializeComponent();
            //inkCanvas.AddHandler(InkCanvas.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.InkCanvas_MouseLeftButtonDown), true);
            _drawingAttributes = new DrawingAttributes()
            {
                Color = Colors.Red,
                Width = 10,
                Height = 10,
            };
        }

        private void inkCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            this.inkCanvas.EditingMode = InkCanvasEditingMode.None;

            this.inkCanvas.UseCustomCursor = true;
            this.inkCanvas.Cursor = Cursors.Pen;
        }

        private void InkCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = Mouse.GetPosition(this.inkCanvas);
            _isMouseMove = true;
        }

        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseMove || _startPoint.X < 0)
            {
                return;
            }

            _endPoint = Mouse.GetPosition(this.inkCanvas);

            GenerateStroke(_isMouseMove, _startPoint, _endPoint);
        }

        private void InkCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isMouseMove || _startPoint.X < 0 || _endPoint.X < 0)
            {
                return;
            }

            _isMouseMove = false;

            GenerateStroke(_isMouseMove, _startPoint, _endPoint);

            Dispose();
        }

        /// <summary>
        /// 生成直线笔画
        /// </summary>
        /// <param name="isMouseMove"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        private void GenerateStroke(bool isMouseMove, Point startPoint, Point endPoint)
        {
            StylusPoint stylusPoint1 = new StylusPoint(startPoint.X, startPoint.Y);
            StylusPoint stylusPoint2 = new StylusPoint(endPoint.X, endPoint.Y);

            var points = new StylusPointCollection(new StylusPoint[] { stylusPoint1, stylusPoint2 });
            Stroke newStroke = new Stroke(points, _drawingAttributes);

            if (isMouseMove)
            {
                if (_stroke != null)
                {
                    this.inkCanvas.Strokes.Remove(_stroke);
                }
                _stroke = newStroke;
            }

            inkCanvas.Strokes.Add(newStroke);
        }

        /// <summary>
        /// 释放、重置资源
        /// </summary>
        private void Dispose()
        {
            _startPoint = new Point(-1, 0);
            _endPoint = new Point(-1, 0);
            _stroke = null;
        }
    }
}
