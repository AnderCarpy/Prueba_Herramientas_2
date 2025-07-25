namespace GestionTareas.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int TareaId { get; set; }
        
        public int EquipoId { get; set; }

        public List<Tarea>? TareasAsignadas { get; set; }
        public List<Equipo>? Equipos { get; set; } 
        


    }
}
