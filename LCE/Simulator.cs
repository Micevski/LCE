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

        public enum MouseState { Default, Drag, Wire }

        // Instance members
        private Scene Scene { get; set; }
        private MouseState CurrentMouseState { get; set; }
        private Point ClickedPosition { get; set; }
        private WireBuilder WireBuilder { get; set; }
        private Component SelectedElement { get; set; }
        private Point SelectionOffset { get; set; }

        private bool Simulating { get; set; }

        public Simulator()
        {
            InitializeComponent();
            Scene = new Scene();
            CurrentMouseState = MouseState.Default;
            WireBuilder = new WireBuilder();
            SelectedElement = null;
            Simulating = false;
        }
        

        private void Simulator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Scene.Draw(e.Graphics);
            WireBuilder.Draw(e.Graphics);
        }



        private void aND2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Scene.AddElement(new AndGate(ClickedPosition, 50, 50));
            Invalidate(true);
        }

        private void Simulator_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(CurrentMouseState == MouseState.Default)
                {
                    WireHandle wh = Scene.ClickedOutHandle(e.Location);
                    Component selected = Scene.ClickSelect(e.Location);
                    if (wh != null)
                    {
                        WireBuilder.Init(wh.Source, wh);
                        CurrentMouseState = MouseState.Wire;
                    }else if(selected != null)
                    {
                        SelectedElement = selected;
                        CurrentMouseState = MouseState.Drag;
                        ClickedPosition = e.Location;
                        SelectionOffset = GeometryUtil.GetOffset(selected.TopLeft, e.Location);
                    }
                }else if(CurrentMouseState == MouseState.Wire)
                {
                    WireHandle wh = Scene.ClickedInHandle(e.Location);
                    if (wh != null)
                    {
                        Wire w = WireBuilder.Finalize(wh);
                        Scene.AddElement(w);
                        CurrentMouseState = MouseState.Default;
                    }
                    else
                    {
                        WireBuilder.AddPoint(e.Location);
                    }
                }
            }
            Invalidate(true);
        }

        private void Simulator_MouseMove(object sender, MouseEventArgs e)
        {
            if(CurrentMouseState == MouseState.Wire)
            {
                WireBuilder.EndPosition = e.Location;
            }
            else if(CurrentMouseState == MouseState.Drag)
            {
                Point off = GeometryUtil.GetOffset(e.Location, ClickedPosition);
                SelectedElement.Move(off);
                ClickedPosition = e.Location; 
            }
            Invalidate(true);
        }

        private void Simulator_MouseUp(object sender, MouseEventArgs e)
        {
            if(CurrentMouseState == MouseState.Drag)
            {
                CurrentMouseState = MouseState.Default;
            }
            Invalidate(true);
        }

        private void Simulator_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if(CurrentMouseState == MouseState.Default)
                {
                    ClickedPosition = e.Location;
                    cmsMainMenu.Show(Cursor.Position);
                }
                else if(CurrentMouseState == MouseState.Wire)
                {
                    WireBuilder.Dismiss();
                    CurrentMouseState = MouseState.Default;
                }
            }
            Invalidate(true);
        }

        private void statusLabel_Paint(object sender, PaintEventArgs e)
        {
            statusLabel.Text = CurrentMouseState.ToString();
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            Simulating = !Simulating;
        }
    }
}
