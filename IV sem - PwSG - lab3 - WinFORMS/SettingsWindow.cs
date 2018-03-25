using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IV_sem___PwSG___lab3___WinFORMS
{
    public partial class SettingsWindow : Form
    {
        public int lifes;
        public int time;

        public SettingsWindow()
        {
            InitializeComponent();
            this.AcceptButton = okButton;       //ok reaguje na ENTER
            this.CancelButton = cancelButton;   // cancel regauje na ESC
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            updateData();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            updateData();
            this.Hide();
        }

        private void updateData()
        {
            this.lifes = decimal.ToInt32(this.numericUpDownLifes.Value);
            this.time = decimal.ToInt32(this.numericUpDownTime.Value);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
