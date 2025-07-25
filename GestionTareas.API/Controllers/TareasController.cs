using Dapper;
using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionTareas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private DbConnection conexion;

        public TareasController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            conexion = new SqlConnection(connectionString);
        }

        // GET: api/<TareasController>
        [HttpGet]
        public IEnumerable<Tarea> Get()
        {
            var tarea = conexion.Query<Tarea>("SELECT * FROM Tarea").ToList();
            return tarea;
        }

        // GET api/<TareasController>/5
        [HttpGet("{id}")]
        public Tarea Get(int id)
        {
            var tarea = conexion.QuerySingle<Tarea>("SELECT * FROM Tarea WHERE Id = @Id", new { Id = id });
            return tarea;
        }

        // POST api/<TareasController>
        [HttpPost]
        public Tarea Post([FromBody] Tarea tarea)
        {
            
            conexion.Execute(@"INSERT INTO Tarea (Nombre, Descripcion, FechaInicio, FechaFin, EquipoId, ProyectoId, UsuarioId) 
                              VALUES (@Nombre, @Descripcion, @FechaInicio, @FechaFin, @EquipoId, @ProyectoId, @UsuarioId)",
                              new
                              {
                                  tarea.Nombre,
                                  Descripcion = tarea.Descripcion,
                                  tarea.FechaInicio,
                                  tarea.FechaFin,
                                  tarea.EquipoId,
                                  tarea.ProyectoId,
                                  tarea.UsuarioId
                              });

            tarea.Id = conexion.QuerySingle<int>("SELECT SCOPE_IDENTITY()");
            return tarea;
        }

        // PUT api/<TareasController>/5
        [HttpPut("{id}")]
        public Tarea Put(int id, [FromBody] Tarea tarea)
        {
            
            conexion.Execute("UPDATE Tarea SET Nombre = @Nombre, Descripcion = @Descripcion WHERE Id = @Id",
                           new
                           {
                               Nombre = tarea.Nombre,
                               Descripcion = tarea.Descripcion,
                               Id = id
                           });
            return tarea;
        }

        // DELETE api/<TareasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Tarea WHERE Id = @Id", new { Id = id });
        }
    }
}