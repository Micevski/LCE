﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LCE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Simulator sim = new Simulator();
            sim.MdiParent = this;
            sim.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}