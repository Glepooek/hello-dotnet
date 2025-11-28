using SharpDX.Mathematics.Interop;
using System.Windows;
using D2D = SharpDX.Direct2D1;

namespace Learning.SharpDX.ShowInD3DImage
{
    public class SsgnnnaTkmlo : SharpDXImage
    {
        private float _x;
        private float _y;
        private float _dx = 1;
        private float _dy = 1;

        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(SsgnnnaTkmlo), new PropertyMetadata(0.0));

        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(SsgnnnaTkmlo), new PropertyMetadata(0.0));

        protected override void OnRender(D2D.RenderTarget renderTarget)
        {
            var brush = new D2D.SolidColorBrush(renderTarget, new RawColor4(1, 0, 0, 1));

            renderTarget.Clear(null);

            renderTarget.DrawRectangle(new RawRectangleF(_x, _y, _x + 10, _y + 10), brush);

            _x += _dx;
            _y += _dy;
            if (_x >= Width - 10 || _x <= 0)
            {
                _dx = -_dx;
            }

            if (_y >= Height - 10 || _y <= 0)
            {
                _dy = -_dy;
            }
        }
    }
}
