using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoDeCargas.Models
{
    public class Pesada
    {
        public int PesadaId { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEntrada { get; set; }
        public string HoraEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string HoraSalida { get; set; }
        public string Transporte { get; set; }
        public string Patente { get; set; }
        public string Conductor { get; set; }

    }
}
