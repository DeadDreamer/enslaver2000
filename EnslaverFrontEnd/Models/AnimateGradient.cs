using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnslaverFrontEnd.Models
{
    public class AnimateGradient
    {
        private Timer _timer = new Timer();
        private Control _control;
        private float _angle;
        private Color _color1;
        private Color _color2;
        private Rectangle _clientRectangle;
        private Graphics _graphics;
        private LinearGradientBrush _linearGradientBrush { get; set; }

        public AnimateGradient(Control control, Color color1, Color color2, float gradientAngle, Control label)
        {
            _timer.Interval = 100;
            _timer.Tick += _timer_Tick;
            _control = control;
            _clientRectangle = control.ClientRectangle;
            _graphics = control.CreateGraphics();
            _angle = gradientAngle;
            _color1 = color1;
            _color2 = color2;
            _label = label;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            UpdateGradient();
        }

        public void StartAnimation()
        {
            _timer.Start();
        }

        public void StopAnimation()
        {
            _timer.Stop();
        }

        private void UpdateGradient()
        {
            int delta = 1;
            _clientRectangle = new Rectangle(_clientRectangle.X + delta, _clientRectangle.Y + delta, _clientRectangle.Width, _clientRectangle.Height);
            _linearGradientBrush = new LinearGradientBrush(_clientRectangle, _color1, _color2, _angle);
            _linearGradientBrush.WrapMode = WrapMode.TileFlipX;
            _graphics.FillRectangle((_linearGradientBrush), _control.ClientRectangle);
            if (_label != null)
                _label.Invalidate();
        }
        

        public Control _label { get; set; }
    }
}
