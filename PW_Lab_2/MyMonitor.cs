using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PW_Lab_2
{
    static class MyMonitor
    {
        static private ArrayList zasob1 = new ArrayList();
        static private ArrayList zasob2 = new ArrayList();
        static private ArrayList zasob3 = new ArrayList();
        static private ArrayList zasob4 = new ArrayList();
        static private ArrayList zasob5 = new ArrayList();

        static private bool czyZajety_1 = false;
        static private bool czyZajety_2 = false;
        static private bool czyZajety_3 = false;
        static private bool czyZajety_4 = false;
        static private bool czyZajety_5 = false;

        static private Stopwatch sw = new Stopwatch();

        static public void uzyskajDostepIWykonajOperacje(int _numerZasobu, int _threadId, int _numerGrupy, int _czasDostepu)
        {
            // wyzerowanie stop watcha
            sw.Reset();

            if(!sprawdzDostepnosc(_numerZasobu))
            {
                switch(_numerZasobu)
                {
                    case 1:
                        zablokujZasob(1);
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Enter(zasob1);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        try
                        {
                            wykonajOperacje(1, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                            zwolnijZasob(1);
                        }
                        finally
                        {
                            Monitor.Exit(zasob1);
                        }
                        break;
                    
                    case 2:
                        zablokujZasob(2);
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Enter(zasob2);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        try
                        {
                            wykonajOperacje(2, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                            zwolnijZasob(2);
                        }
                        finally
                        {
                            Monitor.Exit(zasob2);
                        }
                        break;

                    case 3:
                        zablokujZasob(3);
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Enter(zasob3);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        try
                        {
                            wykonajOperacje(3, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                            zwolnijZasob(3);
                        }
                        finally
                        {
                            Monitor.Exit(zasob3);
                        }
                        break;

                    case 4:
                        zablokujZasob(4);
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Enter(zasob4);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        try
                        {
                            wykonajOperacje(4, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                            zwolnijZasob(4);
                        }
                        finally
                        {
                            Monitor.Exit(zasob4);
                        }
                        break;

                    case 5:
                        zablokujZasob(5);
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Enter(zasob5);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        try
                        {
                            wykonajOperacje(5, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                            zwolnijZasob(5);
                        }
                        finally
                        {
                            Monitor.Exit(zasob5);
                        }
                        break;
                }
            }
            else
            {
                switch (_numerZasobu)
                {
                    case 1:
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Wait(zasob1);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        zablokujZasob(1);
                        wykonajOperacje(1, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                        zwolnijZasob(1);
                        break;

                    case 2:
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Wait(zasob2);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        zablokujZasob(2);
                        wykonajOperacje(2, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                        zwolnijZasob(2);
                        break;

                    case 3:
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Wait(zasob3);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        zablokujZasob(3);
                        wykonajOperacje(3, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                        zwolnijZasob(3);
                        break;

                    case 4:
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Wait(zasob4);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        zablokujZasob(4);
                        wykonajOperacje(4, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                        zwolnijZasob(4);
                        break;

                    case 5:
                        sw.Start(); // --> Start pomiaru czasu oczekiwania
                        Monitor.Wait(zasob5);
                        sw.Stop(); // <-- Stop pomiaru czasu oczekiwania
                        zablokujZasob(5);
                        wykonajOperacje(5, _threadId, _numerGrupy, _czasDostepu, sw.ElapsedMilliseconds);
                        zwolnijZasob(5);
                        break;
                }
            }
        }

        static public void zwolnijDostepWszystkim(int _numerZasobu)
        {
            switch (_numerZasobu)
            {
                case 1:
                    Monitor.Exit(zasob1);
                    odblokujZasob(1);
                    Monitor.PulseAll(zasob1);
                    break;

                case 2:
                    Monitor.Exit(zasob2);
                    odblokujZasob(2);
                    Monitor.PulseAll(zasob2);
                    break;

                case 3:
                    Monitor.Exit(zasob3);
                    odblokujZasob(3);
                    Monitor.PulseAll(zasob3);
                    break;

                case 4:
                    Monitor.Exit(zasob4);
                    odblokujZasob(4);
                    Monitor.PulseAll(zasob4);
                    break;

                case 5:
                    Monitor.Exit(zasob5);
                    odblokujZasob(5);
                    Monitor.PulseAll(zasob5);
                    break;
            }

        }

        static public void zwolnijZasob(int _numerZasobu)
        {
            switch (_numerZasobu)
            {
                case 1:
                    //Monitor.Exit(zasob1);
                    odblokujZasob(1);
                    lock(zasob1) Monitor.Pulse(zasob1);
                    break;

                case 2:
                    //Monitor.Exit(zasob2);
                    odblokujZasob(2);
                    lock(zasob2) Monitor.Pulse(zasob2);
                    break;

                case 3:
                    //Monitor.Exit(zasob3);
                    odblokujZasob(3);
                    lock(zasob3) Monitor.Pulse(zasob3);
                    break;

                case 4:
                    //Monitor.Exit(zasob4);
                    odblokujZasob(4);
                    lock(zasob4) Monitor.Pulse(zasob4);
                    break;

                case 5:
                    //Monitor.Exit(zasob5);
                    odblokujZasob(5);
                    lock(zasob5) Monitor.Pulse(zasob5);
                    break;
            }

        }

        static private bool sprawdzDostepnosc(int _numerZasobu)
        {
            switch(_numerZasobu)
            {
                case 1:
                    return czyZajety_1 ? true : false;

                case 2:
                    return czyZajety_2 ? true : false;

                case 3:
                    return czyZajety_3 ? true : false;
                
                case 4:
                    return czyZajety_4 ? true : false;

                case 5:
                    return czyZajety_5 ? true : false;

                default:
                    return false;
            }
        }
        
        static private void wykonajOperacje(int _numerZasobu, int _threadId, int _numerGrupy, int _czasDostepu, long _czasOczekiwania)
        {
            switch(_numerZasobu)
            {
                case 1:
                    zasob1.Add(String.Format("Wątek: {0} z puli {1}. Czas dostępu do zasobu {2}: {3}, Czas oczekiwania: {4}", 
                                              _threadId,
                                              _numerGrupy, 
                                              "1",
                                              _czasDostepu,
                                              _czasOczekiwania));
                    wstrzymajWatek(_czasDostepu);
                    break;

                case 2:
                    zasob2.Add(String.Format("Wątek: {0} z puli {1}. Czas dostępu do zasobu {2}: {3}, Czas oczekiwania: {4}",
                                              _threadId,
                                              _numerGrupy,
                                              "2",
                                              _czasDostepu,
                                              _czasOczekiwania));
                    wstrzymajWatek(_czasDostepu);
                    break;

                case 3:
                    zasob3.Add(String.Format("Wątek: {0} z puli {1}. Czas dostępu do zasobu {2}: {3}, Czas oczekiwania: {4}",
                                              _threadId,
                                              _numerGrupy,
                                              "3",
                                              _czasDostepu,
                                              _czasOczekiwania));
                    wstrzymajWatek(_czasDostepu);
                    break;

                case 4:
                    zasob4.Add(String.Format("Wątek: {0} z puli {1}. Czas dostępu do zasobu {2}: {3}, Czas oczekiwania: {4}",
                                              _threadId,
                                              _numerGrupy,
                                              "4",
                                              _czasDostepu,
                                              _czasOczekiwania));
                    wstrzymajWatek(_czasDostepu);
                    break;

                case 5:
                    zasob5.Add(String.Format("Wątek: {0} z puli {1}. Czas dostępu do zasobu {2}: {3}, Czas oczekiwania: {4}",
                                              _threadId,
                                              _numerGrupy,
                                              "5",
                                              _czasDostepu,
                                              _czasOczekiwania));
                    wstrzymajWatek(_czasDostepu);
                    break;

            }
        }

        static private void wstrzymajWatek(int _czasWstrzymania)
        {
            Thread.Sleep(_czasWstrzymania);
        }

        static private void zablokujZasob(int _numerZasobu)
        {
            switch(_numerZasobu)
            {
                case 1:
                    czyZajety_1 = true;
                    break;

                case 2:
                    czyZajety_2 = true;
                    break;

                case 3:
                    czyZajety_3 = true;
                    break;

                case 4:
                    czyZajety_4 = true;
                    break;

                case 5:
                    czyZajety_5 = true;
                    break; 
            }
        }

        static private void odblokujZasob(int _numerZasobu)
        {
            switch (_numerZasobu)
            {
                case 1:
                    czyZajety_1 = false;
                    break;

                case 2:
                    czyZajety_2 = false;
                    break;

                case 3:
                    czyZajety_3 = false;
                    break;

                case 4:
                    czyZajety_4 = false;
                    break;

                case 5:
                    czyZajety_5 = false;
                    break;
            }
        }

        static public ArrayList zwrocWyniki()
        {
            ArrayList tmpArrayList = new ArrayList();

            foreach (string s in zasob1)
                tmpArrayList.Add(s);

            foreach (string s in zasob2)
                tmpArrayList.Add(s);

            foreach (string s in zasob3)
                tmpArrayList.Add(s);

            foreach (string s in zasob4)
                tmpArrayList.Add(s);

            foreach (string s in zasob5)
                tmpArrayList.Add(s);

            return tmpArrayList;
        }
    }
}
