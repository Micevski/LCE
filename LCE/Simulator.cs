using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LCE.Components;

namespace LCE
{
    public partial class Simulator : Form
    {
        public Component c { get; set; }
        public List<Component> Components { get; set; }
        public static int SnapSize = 5;
        public Point MoveStart { get; set; }

        public Simulator()
        {
            InitializeComponent();
            Components = new List<Component>();
            c = null;
            MoveStart = new Point();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Simulator_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Components.Add(new InputPin(GridSnap(e.Location), 50, 50));
                Invalidate(true);
            }
        }

        public static Point GridSnap (Point Location)
        {
            int x = (Location.X / SnapSize) * SnapSize;
            int y = (Location.Y / SnapSize) * SnapSize;
            return new Point(x, y);
        }

        private void Simulator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            foreach (Component c in Components)
            {
                c.Draw(e.Graphics);
            }
        }

        private void Simulator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (Component com in Components)
                {
                    if (com.Selected(e.Location))
                    {
                        c = com;
                        
                        MoveStart = c.TopLeft;
                        
                    }
                }
            }
        }

        private void Simulator_MouseMove(object sender, MouseEventArgs e)
        {
            if (c != null)
            {
                c.Move(e.Location.X - MoveStart.X, e.Location.Y - MoveStart.Y);
                MoveStart = c.TopLeft;
                Invalidate(true);
            }
        }

        private void Simulator_MouseUp(object sender, MouseEventArgs e)
        {
            c = null;
        }
    }
}
