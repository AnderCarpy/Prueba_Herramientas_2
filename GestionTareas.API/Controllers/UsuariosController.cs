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
    public class UsuariosController : ControllerBase
    {
        private DbConnection conexion;

        public UsuariosController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            conexion = new SqlConnection(connectionString);
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
           var usuarios = conexion.Query<Usuario>("SELECT Nombre FROM Usuario").ToList();
            return usuarios;
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            var usuario = conexion.QuerySingle<Usuario>("SELECT * FROM Usuario WHERE Id = @Id", new { Id = id });
            return usuario;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public Usuario Post([FromBody]Usuario usuario)
        {
            conexion.Execute("INSERT INTO Usuario (Nombre,Apellido,Email,Password, TareaId, EquipoId) VALUES (@Nombre,@Apellido,@Email,@Password @TareaId, @EquipoId)", usuario);
            usuario.Id = conexion.QuerySingle<int>("SELECT SCOPE_IDENTITY()");
            return usuario;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public Usuario Put(int id, [FromBody]Usuario usuario)
        {
            conexion.Execute("UPDATE Usuario SET Nombre = @Nombre WHERE Id = @Id", new { Nombre = usuario.Nombre, Id = id });
            return usuario;
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM Usuario WHERE Id = @Id", new { Id = id });
        }
    }
}
