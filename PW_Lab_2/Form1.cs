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
        Random usingTime;
        Random startTime;
        Random numerZasobuRandom;

        public Form1()
        {
            InitializeComponent();

            Form1.CheckForIllegalCrossThreadCalls = false;

            // Globalna zmienna, zapewni że wątki w jednej chwili otrzymają inną liczbę losową
            usingTime = new Random(DateTime.Now.Millisecond);
            startTime = new Random(DateTime.Now.Millisecond);
            numerZasobuRandom = new Random(DateTime.Now.Millisecond);

            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "100";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //todo : walidacja 
            if (textBox1.Text == String.Empty)
                throw new ArgumentException("Wpisz wartości");

            threadGroupOne = new Thread[Convert.ToInt32(this.textBox1.Text)];
            threadGroupTwo = new Thread[Convert.ToInt32(this.textBox2.Text)];

            this.startProcess();
        }

        private void startProcess()
        { 
            // Inicjalizacja wątków pierwszej grupy
            for(int j = 0; j<threadGroupOne.Length; j++)
            {
                threadGroupOne[j] = new Thread(this.getThreadStart(true));
            }
            
            // Inicjalizacja wątków drugiej grupy
            for (int j = 0; j < threadGroupTwo.Length; j++)
            {
                threadGroupTwo[j] = new Thread(this.getThreadStart(false));
            }
            
            // Uruchmienie wątków pierwszej grupy
            foreach(Thread t in threadGroupOne)
            {
                t.Start();
            }

            // Uruchmonie wątków drugiej grupy
            foreach(Thread t in threadGroupTwo)
            {
                t.Start();
            }
            
            foreach (Thread t in threadGroupOne)
            {
                t.Join();
            }

            foreach(Thread t in threadGroupTwo)
            {
                t.Join();
            }
            
            // Na koniec wypisz wyniki
            this.showResult();
        }
        
        private ThreadStart getThreadStart(bool _isHighFrequency)
        {
            ThreadStart ts = delegate()
            {
                this.processOneThread(_isHighFrequency);
            };

            return ts;
        }

        private void processOneThread(bool _isHighFrequency)
        {
            int i = 0;
            int localStartTime, localUsingTimeRandom;
            int numerZasobu;

            while (i < Convert.ToInt16(this.textBox3.Text))
            {
                i++;
                /*******************************************************
                 * Wątki pierwszej puli uzyskują częstszy dostęp : 1/10
                 * Wątki drugiej puli rzadszy : 1/100 
                 ******************************************************/ 
                localStartTime = (_isHighFrequency == true) ? startTime.Next(1, 100) : startTime.Next(1, 1000);

                if (localStartTime <= 10)
                {
                    // Losowanie numeru zasobu od 1 do 5
                    numerZasobu = numerZasobuRandom.Next(1, 6);

                    // Czas korzystania z zasobu przez wątek w ms
                    localUsingTimeRandom = (_isHighFrequency == true) ? usingTime.Next(100, 1000) : usingTime.Next(1, 100);
                    
                    //if (checkBox1.Checked)
                        MyMonitor.uzyskajDostepIWykonajOperacje(numerZasobu, 
                                                                Thread.CurrentThread.ManagedThreadId,
                                                                _isHighFrequency ? 1 : 2,
                                                                localUsingTimeRandom); 

                }
            }
        }
     
        private void showResult()
        {
            foreach (string s in MyMonitor.zwrocWyniki())
            {
                textBox4.Text += s + "\r\n";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox4.Text = "";
        }
    }
}
