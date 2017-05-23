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
        // Static members
        public static int SNAP_SIZE = 5;

        public static Point GridSnap(Point Location)
        {
            int x = (Location.X / SNAP_SIZE) * SNAP_SIZE;
            int y = (Location.Y / SNAP_SIZE) * SNAP_SIZE;
            return new Point(x, y);
        }

        public enum MouseState { Default, Drag, Wire, Selecting }

        // Instance members
        private Scene Scene { get; set; }
        private MouseState CurrentMouseState { get; set; }

        

        public Simulator()
        {
            InitializeComponent();
            Scene = new Scene();
            CurrentMouseState = MouseState.Default;
        }
        

        private void Simulator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Scene.Draw(e.Graphics);
        }
    }
}
