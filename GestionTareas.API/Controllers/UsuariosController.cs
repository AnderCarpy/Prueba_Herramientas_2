using Dapper;
using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

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
            var usuarios = conexion.Query<Usuario>("SELECT * FROM Usuario").ToList();
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
        public Usuario Post([FromBody] Usuario usuario)
        {
            // Generar Id manualmente - buscar el máximo Id actual y sumar 1
            var maxId = conexion.QuerySingleOrDefault<int?>("SELECT MAX(Id) FROM Usuario") ?? 0;
            usuario.Id = maxId + 1;

            // Insertar incluyendo el Id que generamos
            conexion.Execute(@"
                INSERT INTO Usuario (Id, Nombre, Apellido, Email, Password, EquipoId, TareaId) 
                VALUES (@Id, @Nombre, @Apellido, @Email, @Password, @EquipoId, @TareaId)",
                new
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Password = usuario.Password,
                    EquipoId = usuario.EquipoId == 0 ? (int?)null : usuario.EquipoId,
                    TareaId = usuario.TareaId == 0 ? (int?)null : usuario.TareaId
                });

            return usuario;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public Usuario Put(int id, [FromBody] Usuario usuario)
        {
            conexion.Execute(@"
                UPDATE Usuario 
                SET Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    Email = @Email, 
                    Password = @Password, 
                    EquipoId = @EquipoId, 
                    TareaId = @TareaId 
                WHERE Id = @Id",
                new
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Password = usuario.Password,
                    EquipoId = usuario.EquipoId == 0 ? (int?)null : usuario.EquipoId,
                    TareaId = usuario.TareaId == 0 ? (int?)null : usuario.TareaId,
                    Id = id
                });
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