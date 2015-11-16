using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xvs0
{
    public partial class X0window : Form
    {
        BackgroundWorker bgwker = new BackgroundWorker();
        X0Hnadler xooperations = new X0Hnadler();
        string crlf = char.ConvertFromUtf32(13) + char.ConvertFromUtf32(10);
        int move = 0;

        public X0window()
        {
            InitializeComponent();
            InitForm();
            bgwker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bgwker.DoWork += new DoWorkEventHandler(bw_DoWork);
            bgwker.WorkerReportsProgress = true;
            bgwker.RunWorkerAsync();
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (move)
            {
                case 0:
                    this.Bgwker.Text = "\\0/" + crlf + " ||" + crlf + " /\\" + crlf;
                    move = 1;
                    break;
                case 1:
                    this.Bgwker.Text = " \\0/" + crlf + " //" + crlf + "/\\" + crlf;
                    move = 2;
                    break;
                case 2:
                    this.Bgwker.Text = " _0_" + crlf + " //" + crlf + "/\\" + crlf;
                    move = 3;
                    break;
                case 3:
                    this.Bgwker.Text = "_0_" + crlf + " ||" + crlf + " /\\" + crlf;
                    move = 0;
                    break;
            } 
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(300);
                bgwker.ReportProgress(1);
            }
        }
 
        public void InitForm()
        {
            xooperations.InitGame();
            this.cell_1.Text = "";
            this.cell_2.Text = "";
            this.cell_3.Text = "";
            this.cell_4.Text = "";
            this.cell_5.Text = "";
            this.cell_6.Text = "";
            this.cell_7.Text = "";
            this.cell_8.Text = "";
            this.cell_9.Text = "";
            this.playerturn.Text = xooperations.GetPlayerName();

            return;
        }

        public void HandleChoice(int cellnum)
        {
            bool choiceacceptedf = false;

            if (xooperations.finishedgamef)
                goto LHandleChoice;
            
            switch (cellnum)
            {
                case 1:
                    if (String.IsNullOrEmpty(this.cell_1.Text))
                    {
                        if (xooperations.player==0)
                            this.cell_1.Text = "X";
                        else
                            this.cell_1.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 2:
                    if (String.IsNullOrEmpty(this.cell_2.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_2.Text = "X";
                        else
                            this.cell_2.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 3:
                    if (String.IsNullOrEmpty(this.cell_3.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_3.Text = "X";
                        else
                            this.cell_3.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 4:
                    if (String.IsNullOrEmpty(this.cell_4.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_4.Text = "X";
                        else
                            this.cell_4.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 5:
                    if (String.IsNullOrEmpty(this.cell_5.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_5.Text = "X";
                        else
                            this.cell_5.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 6:
                    if (String.IsNullOrEmpty(this.cell_6.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_6.Text = "X";
                        else
                            this.cell_6.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 7:
                    if (String.IsNullOrEmpty(this.cell_7.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_7.Text = "X";
                        else
                            this.cell_7.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 8:
                    if (String.IsNullOrEmpty(this.cell_8.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_8.Text = "X";
                        else
                            this.cell_8.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
                case 9:
                    if (String.IsNullOrEmpty(this.cell_9.Text))
                    {
                        if (xooperations.player == 0)
                            this.cell_9.Text = "X";
                        else
                            this.cell_9.Text = "0";
                        choiceacceptedf = true;
                    }
                    break;
            }
            if (choiceacceptedf)
            {
                xooperations.playerhist[xooperations.player] += cellnum;
                if (xooperations.CheckForVictory(xooperations.playerhist[xooperations.player]))
                {
                    MessageBox.Show(xooperations.GetPlayerName() + ", you have won.", "Congratulations");
                    //InitForm();
                }
                else
                {
                    xooperations.ChangePlayer();
                    this.playerturn.Text = xooperations.GetPlayerName();
                }
            }

            LHandleChoice:;
        }

        private void cell_1_Click(object sender, EventArgs e)
        {
            HandleChoice(1);
        }

        private void cell_2_Click(object sender, EventArgs e)
        {
            HandleChoice(2);
        }

        private void cell_3_Click(object sender, EventArgs e)
        {
            HandleChoice(3);
        }

        private void cell_4_Click(object sender, EventArgs e)
        {
            HandleChoice(4);
        }

        private void cell_5_Click(object sender, EventArgs e)
        {
            HandleChoice(5);
        }

        private void cell_6_Click(object sender, EventArgs e)
        {
            HandleChoice(6);
        }

        private void cell_7_Click(object sender, EventArgs e)
        {
            HandleChoice(7);
        }

        private void cell_8_Click(object sender, EventArgs e)
        {
            HandleChoice(8);
        }

        private void cell_9_Click(object sender, EventArgs e)
        {
            HandleChoice(9);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.InitForm();
        }
    }
}
