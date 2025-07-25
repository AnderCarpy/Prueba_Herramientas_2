using Dapper;
using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace GestionTareas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private DbConnection conexion;

        public EquiposController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            conexion = new SqlConnection(connectionString);
        }

        // GET: api/<EquiposController>
        [HttpGet]
        public IEnumerable<Equipo> Get()
        {
            var equipo = conexion.Query<Equipo>("SELECT * FROM Equipos").ToList();
            return equipo;
        }

        // GET api/<EquiposController>/5
        [HttpGet("{id}")]
        public Equipo Get(int id)
        {
            var equipo = conexion.QuerySingle<Equipo>("SELECT * FROM Equipos WHERE Id = @Id", new { Id = id });
            return equipo;
        }

        // POST api/<EquiposController>
        [HttpPost]
        public Equipo Post([FromBody] Equipo equipo)
        {
            conexion.Execute("INSERT INTO Equipos (Nombre, ProyectoId, UsuarioId) VALUES (@Nombre, @ProyectoId, @UsuarioId)", equipo);
            equipo.Id = conexion.QuerySingle<int>("SELECT SCOPE_IDENTITY()");
            return equipo;
        }

        // PUT api/<EquiposController>/5
        [HttpPut("{id}")]
        public Equipo Put(int id, [FromBody] Equipo equipo)
        {
            conexion.Execute("UPDATE Equipos SET Nombre = @Nombre WHERE Id = @Id", new { Nombre = equipo.Nombre, Id = id });
            return equipo;
        }

        // DELETE api/<EquiposController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Equipos WHERE Id = @Id", new { Id = id });
        }

      
    }
}