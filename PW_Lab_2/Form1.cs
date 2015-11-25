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

        CommonResource commonResource;
        public Form1()
        {
            InitializeComponent();

            Form1.CheckForIllegalCrossThreadCalls = false;

            // Globalna zmienna, zapewni że wątki w jednej chwili otrzymają inną liczbę losową
            usingTime = new Random(DateTime.Now.Millisecond);
            startTime = new Random(DateTime.Now.Millisecond);

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

            commonResource = new CommonResource();

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
            int measureStart;

            Stopwatch sw = new Stopwatch();

            while (i < Convert.ToInt16(this.textBox3.Text))
            {
                /*
                 * Wątki pierwszej puli uzyskują częstszy dostęp : 1/10
                 * Wątki drugiej puli rzadszy : 1/100 
                 */ 
                localStartTime = (_isHighFrequency == true) ? startTime.Next(1, 100) : startTime.Next(1, 1000);
                i++;

                if (localStartTime <= 10)
                {
                    // Czas korzystania z zasobu przez wątek w ms
                    localUsingTimeRandom = (_isHighFrequency == true) ? usingTime.Next(100, 1000) : usingTime.Next(1, 100);

                    // Pomiar czasu od kiedy wątek próbuje uzyskać dostęp do momentu otrzymania -->
                    sw.Start();
                    if(checkBox1.Checked) Monitor.Enter(commonResource);
                    // <-- Otrzymano dostęp
                    sw.Stop();

                    try
                    {
                        // Po zablokowaniu zasobu pomiar czasu
                        measureStart = Environment.TickCount; 
                    
                        // Właściwa operacja
                        commonResource.stringArray.Add(
                                String.Format("Wątek: {0} z puli {1}. Czas dostępu do zasobu: {2}, Czas oczekiwania: {3}", 
                                    Thread.CurrentThread.ManagedThreadId,
                                    (_isHighFrequency == true ? "1" : "2"),     
                                    localUsingTimeRandom,
                                    sw.ElapsedMilliseconds));

                        // Stopuję wątek aż do wykorzystania wylosowanego czasu
                        while (Environment.TickCount < (localUsingTimeRandom + measureStart))
                        {
                            /*
                            commonResource.stringArray.Add(
                                String.Format("Wątek {0} zablokował zasob na: {1} ms",
                                    Thread.CurrentThread.ManagedThreadId,
                                        Math.Abs(Environment.TickCount - (localUsingTimeRandom + measureStart))));
                            */
                            // Usypiam na chwilę 
                            Thread.Sleep(10);
                        }
                    }
                    finally
                    {
                        // Zwalniam zasób przez wątek
                        if(checkBox1.Checked) Monitor.Exit(commonResource);
                    }
                }
            }
        }
     
        private void showResult()
        {
            foreach (string s in commonResource.stringArray)
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
