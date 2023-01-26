using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Users
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public HistorialCargas HistorialCargas { get; set; }
    }
}
