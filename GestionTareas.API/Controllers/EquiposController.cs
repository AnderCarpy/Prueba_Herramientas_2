using Dapper;
using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            conexion = new MySqlConnector.MySqlConnection(connectionString);
            conexion.Open();    

        }
        // GET: api/<EquiposController>
        [HttpGet]
        public IEnumerable<Equipo> Get()
        {
           
            var equipo = conexion.Query<Equipo>("SELECT * FROM Equipo").ToList();
            return equipo;
        }

        // GET api/<EquiposController>/5
        [HttpGet("{id}")]
        public Equipo Get(int id)
        {

            var equipo = conexion.QuerySingle<Equipo>("SELECT * FROM Equipo WHERE Id = @Id", new { id = id });
            return equipo;
        }

        // POST api/<EquiposController>
        [HttpPost]
        public Equipo Post([FromBody]Equipo equipo)
        {
            conexion.Execute("INSERT INTO Equipo (Nombre, ProyectoId,UsuarioId) VALUES (@Nombre,@ProyectoId,@UsuarioId)",equipo);
            equipo.Id = conexion.QuerySingle<int>("SELECT LAST_INSERT_ID()");
            return equipo;
        }

        // PUT api/<EquiposController>/5
        [HttpPut("{id}")]
        public Equipo Put(int id, [FromBody]Equipo equipo)
        {
            conexion.Execute("UPDATE Equipo SET Nombre = @Nombre WHERE Id = @Id", new { Nombre = equipo, Id = id });
            return equipo;
        }

        // DELETE api/<EquiposController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Equipo WHERE Id = @Id", new { Id = id });
        }
    }
}
