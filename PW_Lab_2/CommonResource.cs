using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PW_Lab_2
{
    class CommonResource
    {
        public string[] stringTab;

        public CommonResource(int _parm)
        {
            this.stringTab = new string[_parm];
        }
    }
}
