using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoDeCargas.Models.Helpers
{
    public class Security
    {
        private const String Domain = "AZULNATURAL\\";
        public static bool AllowManejoDeCargas(string user)
        {
            if ((user.CompareTo(Domain + "ADucca") == 0)
                || (user.CompareTo(Domain + "LMacedo") == 0)
                || (user.CompareTo(Domain + "IGodoy") == 0)
                || (user.CompareTo(Domain + "DJLongstaff") == 0)
                || (user.CompareTo(Domain + "ERocca") == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
