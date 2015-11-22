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
using System.Threading;

namespace PW_Lab_2
{
    public partial class Form1 : Form
    {
        // Thread groups
        Thread[] threadGroupOne;
        Thread[] threadGroupTwo;

        // Randoms
        Random usingTime1;
        Random usingTime2;

        CommonResource commonResource;
        public Form1()
        {
            InitializeComponent();

            Form1.CheckForIllegalCrossThreadCalls = false;  

            usingTime1 = new Random();
            usingTime2 = new Random();

            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "1000";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //todo : walidacja 
            if (textBox1.Text == String.Empty)
                throw new ArgumentException("Wpisz wartości");

            threadGroupOne = new Thread[Convert.ToInt32(this.textBox1.Text)];
            threadGroupTwo = new Thread[Convert.ToInt32(this.textBox2.Text)];

            commonResource = new CommonResource(Convert.ToInt32(this.textBox3.Text));

            this.startProcess();
        }
        /// <summary>
        /// Funkcja startProcess będzie realizować przebieg zadania.
        /// </summary>
        private void startProcess()
        {
            int i = 0;
            int rand1, rand2;
            
            /* 1. Pierwsza grupa 10 razy częściej i dłużej używa zasobu
             * Np losować liczbę z przedziału i dla danych wartości blokować zasób
             * 
             * 
             */

            ThreadStart threadStart1 = () =>
                {
                    Monitor.Enter(commonResource);
                    try
                    {
                        // sekcja krytyczna 
                        while(true) // Czas pracy
                        {
                            rand1 = usingTime1.Next(1, 100);
 
                            if(rand1 <=10)
                            {
                                                       
                            }
                        }
                    }
                    finally
                    {
                        Monitor.Exit(commonResource);
                    }
                };

            ThreadStart threadStart2 = () =>
                {
                    Monitor.Enter(commonResource);
                    try
                    {
                        // sekcja krytyczna 
                    }
                    finally
                    {
                        Monitor.Exit(commonResource);
                    }
                };    
        }        
    }
}
