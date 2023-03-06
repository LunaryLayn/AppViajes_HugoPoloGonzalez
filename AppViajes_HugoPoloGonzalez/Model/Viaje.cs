using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppViajes_HugoPoloGonzalez.Model
{
    public class Viaje
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Ciudad { get; set; }
        public string Detalle { get; set; }
        public string Foto { get; set; }

        public string Realizado { get; set; }
    }
}
