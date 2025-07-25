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
    public class ProyectosController : ControllerBase
    {
        private DbConnection conexion;

        public ProyectosController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            conexion = new SqlConnection(connectionString);
        }

        // GET: api/<ProyectosController>
        [HttpGet]
        public IEnumerable<Proyecto> Get()
        {
            var proyecto = conexion.Query<Proyecto>("Select * From Proyecto").ToList();
            return proyecto;
        }

        // GET api/<ProyectosController>/5
        [HttpGet("{id}")]
        public Proyecto Get(int id)
        {
            var proyecto = conexion.QuerySingle<Proyecto>("SELECT * FROM Proyecto WHERE Id = @Id", new { id = id });
            return proyecto;
        }

        // POST api/<ProyectosController>
        [HttpPost]
        public Proyecto Post([FromBody] Proyecto proyecto)
        {
            
            conexion.Execute(@"INSERT INTO Proyecto (Nombre, Descripcion, FechaInicio, FechaFin, EquipoId, UsuarioId) 
                              VALUES (@Nombre, @Descripcion, @FechaInicio, @FechaFin, @EquipoId, @UsuarioId)",
                              new
                              {
                                  proyecto.Nombre,
                                  Descripcion = proyecto.Descripcion,
                                  proyecto.FechaInicio,
                                  proyecto.FechaFin,
                                  proyecto.EquipoId,
                                  proyecto.UsuarioId
                              });

            proyecto.Id = conexion.QuerySingle<int>("SELECT SCOPE_IDENTITY()");
            return proyecto;
        }

        // PUT api/<ProyectosController>/5
        [HttpPut("{id}")]
        public Proyecto Put(int id, [FromBody] Proyecto proyecto)
        {
            
            conexion.Execute(@"UPDATE Proyecto SET 
                              Nombre = @Nombre, 
                              Descripcion = @Descripcion, 
                              FechaInicio = @FechaInicio, 
                              FechaFin = @FechaFin, 
                              EquipoId = @EquipoId, 
                              UsuarioId = @UsuarioId 
                              WHERE Id = @Id",
                           new
                           {
                               Nombre = proyecto.Nombre,
                               Descripcion = proyecto.Descripcion,
                               proyecto.FechaInicio,
                               proyecto.FechaFin,
                               proyecto.EquipoId,
                               proyecto.UsuarioId,
                               Id = id
                           });
            return proyecto;
        }

        // DELETE api/<ProyectosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Proyecto WHERE Id = @Id", new { Id = id });
        }
    }
}