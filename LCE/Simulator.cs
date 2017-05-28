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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace LCE
{
    [Serializable]
    public partial class Simulator : Form
    {
        // Static members
        public static Image PLAY_ICON;
        public static Image PAUSE_ICON;
        public static int SNAP_SIZE = 5;

        public string fileName { get; set; }

        public static Point GridSnap(Point Location)
        {
            int x = (Location.X / SNAP_SIZE) * SNAP_SIZE;
            int y = (Location.Y / SNAP_SIZE) * SNAP_SIZE;
            return new Point(x, y);
        }

        static Simulator()
        {
            PLAY_ICON = Properties.Resources.PLAY_ICON;
            PAUSE_ICON = Properties.Resources.PAUSE_ICON; 
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
            fileName = null;
        }
        

        private void Simulator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Scene.Draw(e.Graphics);
            WireBuilder.Draw(e.Graphics);
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
                //SelectedElement = null;
            }
            Invalidate(true);
        }

        private void Simulator_MouseClick(object sender, MouseEventArgs e)
        {
            if (Simulating)
            {
                InputPin ip = Scene.InputClicked(e.Location);
                if(ip != null)
                {
                    ip.Toggle();
                }
            }


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
            if (Simulating)
            {
                btnSimulate.Image = PAUSE_ICON;
            }
            else
            {
                btnSimulate.Image = PLAY_ICON;
            }
        }

        private void nOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene.AddElement(new NotGate(ClickedPosition, 60, 60));
            Invalidate(true);
        }

        private void aND2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Scene.AddElement(new AndGate(ClickedPosition, 60, 60));
            Invalidate(true);
        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene.AddInput(new InputPin(ClickedPosition, 60, 60));
            Invalidate(true);
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene.AddElement(new OutputPin(ClickedPosition, 60, 60));
            Invalidate(true);
        }

        private void oR2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene.AddElement(new OrGate(ClickedPosition, 60, 60));
            Invalidate(true);
        }

        private void xOR2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene.AddElement(new XorGate(ClickedPosition, 60, 60));
            Invalidate(true);
        }

        private void Simulator_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (SelectedElement != null)
                {
                    Scene.RemoveElement(SelectedElement);
                }
            }
        }


        // Serialization

        public void saveFile()
        {
           if(fileName == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Save LCE file (*.lce)|*.lce";
                sfd.Title = "Save LCE file";
                sfd.FileName = fileName;
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    fileName = sfd.FileName;
                }
            }
           if (fileName != null)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    IFormatter formater = new BinaryFormatter();
                    formater.Serialize(fs, Scene);
                }
            }
        }

        public void openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Open LCE (*.lce)|*.lce";
            ofd.Title = "Open LCE";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileName;
                try
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        Scene = (Scene)formatter.Deserialize(fs);
                    }
                }
                catch
                {
                    MessageBox.Show("Could not read file: " + fileName);
                    fileName = null;
                    return;
                }
                Invalidate(true);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileName = null;
            saveFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private DialogResult savePrompt()
        {
            return MessageBox.Show("Would you like to save this file?", "Save file?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        } 

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Simulator_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = savePrompt();
            if (dr == DialogResult.Yes)
            {
                saveFile();
            }
        }
    }
}
