using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animated
{
    public class Animator
    {
        private Size cSize;
        private BufferedGraphics bg;
        private Graphics _g;
        private Thread? t;
        private Graphics g
        {
            get => _g;
            set
            {
                _g = value;
                bg = BufferedGraphicsManager.Current.Allocate(
                    g, Rectangle.Ceiling(g.VisibleClipBounds)
                );
                bg.Graphics.Clear(Color.White);
            }
        }

        private List<Circle> circs = new();
        public Animator(Size containerSize, Graphics g)
        {
            cSize = containerSize;
            this.g = g;
        }

        public void AddCircle()
        {
            var c = new Circle(cSize);
            c.Animate();
            circs.Add(c);
        }
        public void Start()
        {
            if (t == null || !t.IsAlive)
            {
                t = new Thread(() =>
                {
                    Graphics tg;
                    lock (bg)
                    {
                        tg = bg.Graphics;
                    }

                    do
                    {
                        tg.Clear(Color.White);
                        circs.RemoveAll( it => !it.IsAlive);
                        for (int i = 0; i < circs.Count; i++)
                        {
                            circs[i].Paint(tg);
                        }
                        bg.Render(g);
                        Thread.Sleep(30);
                    } while (true);
                });
                t.IsBackground = true;
                t.Start();
            }
        }
    }
}
