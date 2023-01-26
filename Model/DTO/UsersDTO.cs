using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public HistorialCargasDTO HistorialCargasDTO { get; set; }
    }
}
