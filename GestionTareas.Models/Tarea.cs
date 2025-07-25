using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Models
{
  public  class Tarea
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripción { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }
        public int EquipoId { get; set; }
        public List<Usuario>? UsuariosAsignados { get; set; }
        public Proyecto? Proyecto { get; set; }
        public Equipo? Equipo { get; set; }
    }
}
