using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Datos;
using Negocios;

namespace ApiTarea.Controllers
{
    public class UsuariosController : ApiController
    {
        private Users db = new Users();

        // GET: api/Usuarios
        //public IQueryable<Usuarios> GetUsuarios()
        //{
        //    return db.Usuarios;
        //}

        // GET: api/Usuarios/5
        //[ResponseType(typeof(Usuarios))]
        //public IHttpActionResult GetUsuarios(int id)
        //{
        //    Usuarios usuarios = db.Usuarios.Find(id);
        //    if (usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(usuarios);
        //}

        // PUT: api/Usuarios/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUsuarios(int id, Usuarios usuarios)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != usuarios.idUsuario)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(usuarios).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsuariosExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Usuarios
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult PostUsuarios(Usuarios usuarios)
        {
            try
            {
                string resp = db.crearUsuario(usuarios.Identificacion, usuarios.Nombre, usuarios.Apellidos, usuarios.pass);
                if (resp.Equals("1"))
                {
                    return CreatedAtRoute("DefaultApi", new { id = usuarios.idUsuario }, usuarios);
                }
                else if (resp.Equals("Usuario Existe"))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception(resp);
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(Usuarios))]
        [Route("api/Usuarios/Login", Name = "InicioSesion")]
        public IHttpActionResult InicioSesion(Usuarios usuarios)
        {
            try
            {
                string resp = db.ValidarInicioSesion(usuarios.Identificacion, usuarios.pass);
                string[] data = resp.Split(','); 
                
                if (data[1].Equals("El usuario no existe"))
                {
                    return NotFound();
                }
                else if (data[1].Equals("Usuario y/o contraseña incorrectos."))
                {
                    throw new Exception(data[1]);
                }
                else if (data[0].Equals("1"))
                {
                    return Ok(data[1]);
                }
                else
                {
                    throw new Exception(data[1]);
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(Usuarios))]
        [Route("api/Usuarios/SingOut", Name = "CerrarSesion")]
        public IHttpActionResult CerrarSesion(Usuarios usuarios)
        {
            try
            {
                string resp = db.CerrarSesion(usuarios.Identificacion);

                if (resp.Equals("El usuario no existe"))
                {
                    return NotFound();
                }
                else if (resp.Equals("0"))
                {
                    throw new Exception("Error al momento de cerrar sesion");
                }
                else if (resp.Equals("1"))
                {
                    return Ok();
                }
                else
                {
                    throw new Exception(resp);
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Usuarios/5
        //[ResponseType(typeof(Usuarios))]
        //public IHttpActionResult DeleteUsuarios(int id)
        //{
        //    Usuarios usuarios = db.Usuarios.Find(id);
        //    if (usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Usuarios.Remove(usuarios);
        //    db.SaveChanges();

        //    return Ok(usuarios);
        //}

    }
}