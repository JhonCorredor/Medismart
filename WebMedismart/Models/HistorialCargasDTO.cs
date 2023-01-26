using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMedismart.Models
{
    public class HistorialCargasDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimento { get; set; }
    }
}