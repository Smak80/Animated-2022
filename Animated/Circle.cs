using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animated
{
    public class Circle
    {
        private Point _pos;
        private int _diameter;
        private int _dx = 3;
        private Size _containerSize;
        public Color Color { get; set; }

        public Circle(Size containerSize)
        {
            _pos = new Point(0, 0);
            Color = Color.Blue;
            _diameter = 100;
            _containerSize = containerSize;
        }

        public void Paint(Graphics g)
        {
            var b = new SolidBrush(Color);
            g.FillEllipse(b, _pos.X, _pos.Y, _diameter, _diameter);
        }

        public bool Move()
        {
            if (_pos.X < _containerSize.Width - _diameter)
            {
                _pos.X += _dx;
                return true;
            }
            return false;
        }

        public void Animate()
        {
            new Thread(() =>
            {
                do
                {
                    Thread.Sleep(30);
                } while (Move());
            }).Start();
        }

        public void Clear(Graphics g)
        {
            var b = new SolidBrush(Color.White);
            g.FillEllipse(b, _pos.X, _pos.Y, _diameter, _diameter);
        }
    }
}
