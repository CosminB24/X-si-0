using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_PCLP
{
    public partial class Form1 : Form
    {
        bool turn = true; // daca e true, atunci e randul lui X, daca e false, atunci e randul lui 0.
        int turn_count = 0;
        int scoreX, scoreO, scoreE = 0;
        int[] array = new int[9];
        int blockCnt = 0; //nr de celule blocate
        int blokedCell = -1;// care celule e blocata
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = 999;



        }

        private void instructiuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("X și O este un joc pentru doi jucători, X respectiv 0, care marchează pe rând câte o căsuță dintr-un tabel cu 3 linii și 3 coloane. Jucătorul care reușește primul să marcheze 3 căsute adiacente pe orizontală, verticală sau diagonală caștigă jocul.", "Instructiuni");
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
            {
                b.Text = "X";
                array[int.Parse(b.Tag.ToString())] = 1;
            }
            else
            {
                b.Text = "0";
                array[int.Parse(b.Tag.ToString())] = 0;
            }

            turn = !turn;
            b.Enabled = false; // se blocheaza casutele dupa apasarea acestora
            turn_count++;
           
            
            checkForWinner();
        }

        private void checkForWinner()
        {
            bool there_is_a_winner = false;
            String winner = "";
            int sum = 0;

            //Verfica linii
            for (int k = 0; k < 3; k++)
            {
                sum = 0;
                for (int j = 0+(k*3); j < 3+ (k*3); j++)
                {
                    sum += array[j];
                }

                if (sum == 0)
                {
                    there_is_a_winner = true;
                    winner = "0";
                    break;
                }
                if (sum == 3)
                {
                    there_is_a_winner = true;
                    winner = "X";
                    break;
                }
            }


            //Verfica coloane
            for (int k = 0; k < 3; k++)
            {
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    int elem = (j*3)+k;
                    sum += array[elem];
                }

                if (sum == 0)
                {
                    there_is_a_winner = true;
                    winner = "0";
                    break;
                }
                if (sum == 3)
                {
                    there_is_a_winner = true;
                    winner = "X";
                    break;
                }

            }


            //Verfica diag princip
            sum = 0;
            sum = array[0] + array[4] + array[8];
            if (sum == 0)
            {
                there_is_a_winner = true;
                winner = "0";
                
            }
            if (sum == 3)
            {
                there_is_a_winner = true;
                winner = "X";
              
            }

            //Verfica diag sec
            sum = 0;
            sum = array[2] + array[4] + array[6];
            if (sum == 0)
            {
                there_is_a_winner = true;
                winner = "0";
                
            }
            if (sum == 3)
            {
                there_is_a_winner = true;
                winner = "X";
                
            }


/*
            // verificarea pe orizontala (linie)
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled)) // pentru a recunoaste toata linia, trebuie sa fie blocate toate casutele, prin urmare apare !A/B/C1.Enabled
                there_is_a_winner = true;

            // verificarea pe verticala (coloana)
            if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled)) // pentru a recunoaste toata coloana, trebuie sa fie blocate toate casutele, prin urmare apare !A1/2/3.Enabled
                there_is_a_winner = true;

            // verificarea pe diagonala
            if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == B2.Text) && (B2.Text == A3.Text) && (!C1.Enabled)) // pentru a recunoaste toata diagonala, trebuie sa fie blocate toate casutele, prin urmare apare !A/C1.Enabled
                there_is_a_winner = true;              

 */
            if (there_is_a_winner)
            {
                disableButtons();
                
                if (turn)
                {
                    winner = "0";
                    scoreO++;
                    o_castiga.Text = scoreO.ToString();
                }
                else
                {
                    winner = "X";
                    scoreX++;
                    x_castiga.Text = scoreX.ToString();
                }
                MessageBox.Show(winner + " a câștigat!", "Victorie!");

            }// inchidem if-ul
            else
            {
                if (turn_count == 9- blockCnt)
                {
                    MessageBox.Show("Nimeni nu a caștigat!", "Egalitate!");
                    scoreE++;
                    egaluri.Text = scoreE.ToString(); ;
                }
                        
            }
            
        } //inchidem checkForWinner

        private void disableButtons()
        {
            try
            {
                foreach (Control c in panel1.Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false; // dezactiveaza toate butoanele in momentul in care cineva castiga
                }// inchidem foreach-ul
            }// inchidem try-ul
            catch { }

        }

        private void jocNouToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < array.Length; i++)
                array[i] = 999;

            turn = true;
            turn_count = 0;

            blockCnt = 1;
            Random rnd = new Random();
            blokedCell = rnd.Next(0, 9);

            try
            {
                foreach (Control c in panel1.Controls)
                {
                    Button b = (Button)c;
                    if (int.Parse(b.Tag.ToString()) != blokedCell)
                    {
                        b.BackColor = Color.LightGray;
                        b.Enabled = true; // se reactiveaza butoanele
                        b.Text = "";
                    }
                    else
                    {
                        b.BackColor = Color.Gray;
                        b.Enabled = false;
                        b.Text = "";
                    }
                }// inchidem foreach-ul
            }// inchidem try-ul
            catch { }
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
