using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace IV_sem___PwSG___lab3___WinFORMS
{
    public partial class Form1 : Form
    {
        private bool initialized;
        private int[] columnCounter = new int[4];
        private int[] rowCounter = new int[4];
        private bool[,] isActive = new bool[4, 4];
        int[,] tempActivityCounter;
        private int lifes;
        private int time;
        private int score;
        private int activeButtonsStroke;
        private int activeNumber;
        private bool editMode;
        Timer gameTimer;
        SettingsWindow settingsWindow;

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            settingsWindow = new SettingsWindow();
            initialized = false;
            this.score = 0;
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += timerTickHanlder;
            this.FormClosing += formClosingHandle;
            foreach (Button button in tableLayoutPanelButtons.Controls)
            {
                button.MouseDown += buttonClickedHandler;
                button.MouseEnter += mouseEnterHandler;
                button.MouseLeave += mouseLeaveHandler;
                initializeButton(button);
            }
            updateWindowSettingsData();
            updateStatusStrip();
            initializeStatusBar(toolStripProgressBarTime, this.time);
        }

        public void timerTickHanlder(object sender, EventArgs e)
        {
            if (!initialized) return;
            if (this.time == 0)
            {
                this.initialized = false;
                MessageBox.Show("Time elapsed", "Defeat!");
                this.gameTimer.Stop();
                return;
            }
            this.time--;
            toolStripProgressBarTime.Value--;
            updateMenuItems();
        }

        public void formClosingHandle(object obj, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit or no?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            this.DialogResult = DialogResult.Yes;
            //Application.Exit();
        }
        public void buttonClickedHandler(object obj, EventArgs args)
        {
            if (!initialized) return;
            Button sender = obj as Button;
            MouseEventArgs margs = (MouseEventArgs)args;
            if (!editMode)
            {
                switch (margs.Button)
                {
                    case MouseButtons.Left:
                        if (!initialized) return;
                        if (sender.BackColor == Color.Yellow || sender.BackColor == Color.RoyalBlue)
                        {
                            TableLayoutPanelCellPosition buttonPosition = tableLayoutPanelButtons.GetPositionFromControl(sender);
                            if (!isActive[buttonPosition.Row, buttonPosition.Column])
                            {
                                wrongClickActionAsync(sender);
                                lifes--;
                                if (lifes == 0)
                                {
                                    initialized = false;
                                    updateStatusStrip();
                                    MessageBox.Show("You lost!", "Defeat!");
                                }
                            }
                            else
                            {
                                sender.BackColor = Color.Black;
                                rowCounter[buttonPosition.Row]++;
                                columnCounter[buttonPosition.Column]++;
                                score += 50;
                                activeButtonsStroke++;
                                if (activeButtonsStroke == activeNumber)
                                {
                                    initialized = false;
                                    score += 500;
                                    updateStatusStrip();
                                    MessageBox.Show("You won! 500++", "Winner!");
                                    restartFlashcard();
                                    updateWindowSettingsData();
                                    initializeStatusBar(toolStripProgressBarTime, this.time);
                                    updateStatusStrip();
                                }
                            }
                        }
                        break;
                    case MouseButtons.Right:
                        if (sender.BackColor == Color.Black)
                        {
                            TableLayoutPanelCellPosition buttonPosition = tableLayoutPanelButtons.GetPositionFromControl(sender);
                            sender.BackColor = Color.White;
                            rowCounter[buttonPosition.Row]--;
                            columnCounter[buttonPosition.Column]--;
                        }
                        break;
                }
            }
            else // editMode == true
            {
                //bool[,] tempIsActive = new bool[4, 4];
                TableLayoutPanelCellPosition buttonPosition = tableLayoutPanelButtons.GetPositionFromControl(sender);
                switch (margs.Button)
                {
                    case MouseButtons.Left:
                        if (!initialized) return;
                        sender.BackColor = Color.Black;
                        isActive[buttonPosition.Row, buttonPosition.Column] = true;
                        tempActivityCounter[0, buttonPosition.Row]++;
                        tempActivityCounter[1, buttonPosition.Column]++;
                        activeNumber++;
                        break;
                    case MouseButtons.Right:
                        sender.BackColor = Color.White;
                        break;
                }
            }
            
            updateStatusStrip();
        }

        public void mouseEnterHandler(object obj, EventArgs args)
        {
            Button sender = obj as Button;
            if (sender.BackColor == Color.Black)
                return;
            sender.BackColor = Color.Yellow;
        }

        public void mouseLeaveHandler(object obj, EventArgs args)
        {
            Button sender = obj as Button;
            if (sender.BackColor == Color.Black || sender.BackColor == Color.Red)
                return;
            initializeButton(sender);
        }

        private void newGameToolStripMenuItem_Click(object obj, EventArgs e)
        {
            score = 0;
            restartFlashcard();
            updateWindowSettingsData();
            updateStatusStrip();
            initializeStatusBar(toolStripProgressBarTime, this.time);
            gameTimer.Start();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsWindow.ShowDialog();
            updateWindowSettingsData();
        }

        private void updateWindowSettingsData()
        {
            this.lifes = settingsWindow.lifes;
            this.time = settingsWindow.time;
        }

        private static void initializeButton(Button button)
        {
            button.BackColor = Color.RoyalBlue;
            button.Text = "?";
        }

        private static void wrongClickAction(Button sender)
        {
            Stopwatch myWatch = new Stopwatch();
            sender.BackColor = Color.Red;
            myWatch.Start();
            while (myWatch.ElapsedMilliseconds < 500) { }
            initializeButton(sender);
            myWatch.Stop();
        }

        private static async Task wrongClickActionAsync(Button sender)
        {
            await Task.Run(() => wrongClickAction(sender));
        }

        private static void initializeStatusBar(ToolStripProgressBar bar, int time)
        {
            bar.Minimum = 0;
            bar.Maximum = time;
            bar.Step = 1;
            bar.Value = time;
        }

        private void updateStatusStrip()
        {
            toolStripStatusLabelLife.Text = String.Format("life: {0}", lifes);
            toolStripStatusLabelScore.Text = String.Format("score: {0}", score);
        }
        
        private void restartFlashcard(bool flag = false)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int[,] tempActivityCounter = new int[2, 4];     // 1 wiersz - wiersze, 2 wiersz - kolumny
            TableLayoutPanelCellPosition buttonPosition;
            int activeCounter = 0;
            initialized = true;
            isActive = new bool[4, 4];
            menuStrip1.BackColor = Color.White;
            foreach (Button button in tableLayoutPanelButtons.Controls)
            {
                initializeButton(button);
                buttonPosition = tableLayoutPanelButtons.GetPositionFromControl(button);
                if (rand.Next() % 2 == 0)
                {
                    isActive[buttonPosition.Row, buttonPosition.Column] = true;
                    tempActivityCounter[0, buttonPosition.Row]++;
                    tempActivityCounter[1, buttonPosition.Column]++;
                    activeCounter++;
                }
            }
            foreach (Label label in tableLayoutPanelLabelColumns.Controls)
            {
                TableLayoutPanelCellPosition labelPosition = tableLayoutPanelLabelColumns.GetPositionFromControl(label);
                label.Text = tempActivityCounter[1, labelPosition.Column].ToString();
            }
            foreach (Label label in tableLayoutPanelLabelRows.Controls)
            {
                TableLayoutPanelCellPosition labelPosition = tableLayoutPanelLabelRows.GetPositionFromControl(label);
                label.Text = tempActivityCounter[0, labelPosition.Row].ToString();
            }
            for (int i = 0; i < 4; ++i)
            {
                columnCounter[i] = rowCounter[i] = 0;
            }
            //if (!flag)
            //    setActiveButtonsRandom();
            //else
            //    setActiveButtonsFromEditMode();
            activeNumber = activeCounter;
            activeButtonsStroke = 0;
            updateMenuItems();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Checked)
            {
                gameToolStripMenuItem.Checked = true;
                item.Checked = false;
                editMode = false;
                updateMenuItems();
            }
            else
            {
                initializeEditModeFlashcards();
            }
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Checked)
            {
                initializeEditModeFlashcards();
            }
            else
            {
                item.Checked = true;
                editToolStripMenuItem.Checked = false;
                editMode = false;
                updateMenuItems();
            }
        }

        private void updateMenuItems()
        {
            if (!editMode)
            {
                saveToolStripMenuItem.Enabled = false;
                newGameToolStripMenuItem.Enabled = true;
                settingsToolStripMenuItem.Enabled = true;
                openToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveToolStripMenuItem.Enabled = true;
                newGameToolStripMenuItem.Enabled = false;
                settingsToolStripMenuItem.Enabled = false;
                openToolStripMenuItem.Enabled = false;
            }
        }

        private void initializeEditModeFlashcards()
        {
            gameToolStripMenuItem.Checked = false;
            editToolStripMenuItem.Checked = true;
            editMode = true;
            updateMenuItems();
            tempActivityCounter = new int[2, 4];
            menuStrip1.BackColor = Color.Yellow;
            foreach (Button button in tableLayoutPanelButtons.Controls)
            {
                button.BackColor = Color.White;
            }
        }

        private void setActiveButtonsFromEditMode()
        {
            foreach (Button button in tableLayoutPanelButtons.Controls)
            {
                TableLayoutPanelCellPosition buttonPosition = tableLayoutPanelButtons.GetPositionFromControl(button);
                foreach (Label label in tableLayoutPanelLabelColumns.Controls)
                {
                    TableLayoutPanelCellPosition labelPosition = tableLayoutPanelLabelColumns.GetPositionFromControl(label);
                    label.Text = tempActivityCounter[1, labelPosition.Column].ToString();
                }
                foreach (Label label in tableLayoutPanelLabelRows.Controls)
                {
                    TableLayoutPanelCellPosition labelPosition = tableLayoutPanelLabelRows.GetPositionFromControl(label);
                    label.Text = tempActivityCounter[0, labelPosition.Row].ToString();
                }
                for (int i = 0; i < 4; ++i)
                {
                    columnCounter[i] = rowCounter[i] = 0;
                }
            }
        }

        private void setActiveButtonsRandom()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            TableLayoutPanelCellPosition buttonPosition;
            tempActivityCounter = new int[2, 4];     // 1 wiersz - wiersze, 2 wiersz - kolumny
            int activeCounter = 0;

            foreach (Button button in tableLayoutPanelButtons.Controls)
            {
                initializeButton(button);
                buttonPosition = tableLayoutPanelButtons.GetPositionFromControl(button);
                if (rand.Next() % 2 == 0)
                {
                    isActive[buttonPosition.Row, buttonPosition.Column] = true;
                    tempActivityCounter[0, buttonPosition.Row]++;
                    tempActivityCounter[1, buttonPosition.Column]++;
                    activeCounter++;
                }
            }
            foreach (Label label in tableLayoutPanelLabelColumns.Controls)
            {
                TableLayoutPanelCellPosition labelPosition = tableLayoutPanelLabelColumns.GetPositionFromControl(label);
                label.Text = tempActivityCounter[1, labelPosition.Column].ToString();
            }
            foreach (Label label in tableLayoutPanelLabelRows.Controls)
            {
                TableLayoutPanelCellPosition labelPosition = tableLayoutPanelLabelRows.GetPositionFromControl(label);
                label.Text = tempActivityCounter[0, labelPosition.Row].ToString();
            }
            for (int i = 0; i < 4; ++i)
            {
                columnCounter[i] = rowCounter[i] = 0;
            }
        }
    }

}
