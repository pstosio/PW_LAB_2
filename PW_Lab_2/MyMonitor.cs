using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PW_Lab_2
{
    static class MyMonitor
    {
        static private ArrayList zasob1 = new ArrayList();
        static private ArrayList zasob2 = new ArrayList();
        static private ArrayList zasob3 = new ArrayList();
        static private ArrayList zasob4 = new ArrayList();
        static private ArrayList zasob5 = new ArrayList();

        static private bool czyZajety_1 = true;
        static private bool czyZajety_2 = true;
        static private bool czyZajety_3 = true;
        static private bool czyZajety_4 = true;
        static private bool czyZajety_5 = true;

        static private void uzyskajDostep(int _numerZasobu)
        {
            if(sprawdzDostepnosc(_numerZasobu))
            {
                switch(_numerZasobu)
                {
                    case 1:
                        zablokujZasob(1);
                        Monitor.Enter(zasob1);
                        break;
                    
                    case 2:
                        zablokujZasob(2);
                        Monitor.Enter(zasob2);
                        break;

                    case 3:
                        zablokujZasob(3);
                        Monitor.Enter(zasob3);
                        break;

                    case 4:
                        zablokujZasob(4);
                        Monitor.Enter(zasob4);
                        break;

                    case 5:
                        zablokujZasob(5);
                        Monitor.Enter(zasob5);
                        break;
                }
            }
            else
            {
                switch (_numerZasobu)
                {
                    case 1:
                        Monitor.Wait(zasob1);
                        break;

                    case 2:
                        Monitor.Wait(zasob2);
                        break;

                    case 3:
                        Monitor.Wait(zasob3);
                        break;

                    case 4:
                        Monitor.Wait(zasob4);
                        break;

                    case 5:
                        Monitor.Wait(zasob5);
                        break;
                }
            }
        }

        static private void zwolnijDostepWszystkim(int _numerZasobu)
        {
            switch (_numerZasobu)
            {
                case 1:
                    Monitor.Exit(zasob1);
                    Monitor.PulseAll(zasob1);
                    break;

                case 2:
                    Monitor.Exit(zasob2);
                    Monitor.PulseAll(zasob2);
                    break;

                case 3:
                    Monitor.Exit(zasob3);
                    Monitor.PulseAll(zasob3);
                    break;

                case 4:
                    Monitor.Exit(zasob4);
                    Monitor.PulseAll(zasob4);
                    break;

                case 5:
                    Monitor.Exit(zasob5);
                    Monitor.PulseAll(zasob5);
                    break;
            }

        }

        static private void zwolnijDostep(int _numerZasobu)
        {
            switch (_numerZasobu)
            {
                case 1:
                    Monitor.Exit(zasob1);
                    Monitor.Pulse(zasob1);
                    break;

                case 2:
                    Monitor.Exit(zasob2);
                    Monitor.Pulse(zasob2);
                    break;

                case 3:
                    Monitor.Exit(zasob3);
                    Monitor.Pulse(zasob3);
                    break;

                case 4:
                    Monitor.Exit(zasob4);
                    Monitor.Pulse(zasob4);
                    break;

                case 5:
                    Monitor.Exit(zasob5);
                    Monitor.Pulse(zasob5);
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
    }
}
