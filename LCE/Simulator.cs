using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LCE.Components;
using System.Diagnostics;

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
        private Point ClickedPosition { get; set; }
        private WireBuilder WireBuilder { get; set; }

        

        public Simulator()
        {
            InitializeComponent();
            Scene = new Scene();
            CurrentMouseState = MouseState.Default;
            WireBuilder = new WireBuilder();
        }
        

        private void Simulator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Scene.Draw(e.Graphics);
            WireBuilder.Draw(e.Graphics);
        }

        private void Simulator_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(CurrentMouseState == MouseState.Default)
                {
                    WireBuilder.Init(null, e.Location);
                    CurrentMouseState = MouseState.Wire;
                }else if(CurrentMouseState == MouseState.Wire)
                {
                    WireBuilder.AddPoint(e.Location);
                }
            }

            if (e.Button == MouseButtons.Right && CurrentMouseState == MouseState.Default)
            {
                cmsMainMenu.Show(Cursor.Position);
                ClickedPosition = e.Location;
            }
            Invalidate(true);
        }

        private void aND2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Scene.AddElement(new AndGate(ClickedPosition, 50, 50));
            Invalidate(true);
        }
    }
}
