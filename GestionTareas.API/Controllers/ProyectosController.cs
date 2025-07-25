using Dapper;
using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
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
            conexion = new MySqlConnector.MySqlConnection(connectionString);
            conexion.Open();

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
        public Proyecto Post([FromBody]Proyecto proyecto)
        {
            conexion.Execute("INSERT INTO Proycto (Nombre, ProyectoId,UsuarioId) VALUES (@Nombre,@ProyectoId,@UsuarioId)", proyecto);
            proyecto.Id = conexion.QuerySingle<int>("SELECT LAST_INSERT_ID()");
            return proyecto;
        }


        // PUT api/<ProyectosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<ProyectosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
