using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Models
{
   public class Equipo
    {
        public int Id { get; set; }
       
        public string Nombre { get; set; }

        public int ProyectoId { get; set; }
        public int UsuarioId { get; set; }

        public List<Usuario>? Miembros { get; set; }

        public Proyecto? Proyecto { get; set; }
    }
}
