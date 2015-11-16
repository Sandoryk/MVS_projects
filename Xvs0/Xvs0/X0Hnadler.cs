using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xvs0
{
    class X0Hnadler
    {
        public int player = 0;
        public string[] playerhist = new string[2];
        public bool finishedgamef = false;

        public X0Hnadler()
        {
            this.InitGame();
        }

        public void InitGame()
        {
            player = 0;
            finishedgamef = false;
            for (int i = 0; i < playerhist.Length; i++)
            {
                playerhist[i] = "";
            }
        }

        public bool CheckForVictory(string hist)
        {
            bool res = false;

            if (hist.Length > 2)
            {
                if (hist.Contains("1") && hist.Contains("2") && hist.Contains("3"))
                    res = true;
                else if (hist.Contains("4") && hist.Contains("5") && hist.Contains("6"))
                    res = true;
                else if (hist.Contains("7") && hist.Contains("8") && hist.Contains("9"))
                    res = true;
                else if (hist.Contains("1") && hist.Contains("4") && hist.Contains("7"))
                    res = true;
                else if (hist.Contains("2") && hist.Contains("5") && hist.Contains("8"))
                    res = true;
                else if (hist.Contains("3") && hist.Contains("6") && hist.Contains("9"))
                    res = true;
                else if (hist.Contains("1") && hist.Contains("5") && hist.Contains("9"))
                    res = true;
                else if (hist.Contains("3") && hist.Contains("5") && hist.Contains("7"))
                    res = true;
            }
            if (res)
                finishedgamef = true;

            return res;
        }

        public void ChangePlayer()
        {
            if (player == 0)
                player = 1;
            else
                player = 0;
        }

        public string GetPlayerName()
        {
            string name;
            if (player == 0)
                name = "Player 1";
            else
                name = "Player 2";
            return name;
        }
    }
}
