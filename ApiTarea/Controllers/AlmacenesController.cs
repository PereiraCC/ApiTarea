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
using ApiTarea.Models;
using Datos;
using Negocios;

namespace ApiTarea.Controllers
{
    public class AlmacenesController : ApiController
    {
        private Store db = new Store();

        // GET: api/Almacenes
        public List<ModelAlmacen> GetAlmacenes(string ticket, string id)
        {
            try
            {
                List<ModelAlmacen> almacenes = new List<ModelAlmacen>();

                if (db.ValidarTicket(ticket, id).Equals("1"))
                {
                    List<Almacenes> almac = db.obtenerAlmacenes();
                    foreach (Almacenes al in almac)
                    {
                        ModelAlmacen temp = new ModelAlmacen();
                        temp.idAlmacen = al.idAlmacen;
                        temp.Descripcion = al.Descripcion;

                        almacenes.Add(temp);
                    }

                    return almacenes;
                }
                else
                {
                    return almacenes;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Almacenes/5
        //[ResponseType(typeof(Almacenes))]
        //public IHttpActionResult GetAlmacenes(int id)
        //{
        //    Almacenes almacenes = db.Almacenes.Find(id);
        //    if (almacenes == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(almacenes);
        //}

        // PUT: api/Almacenes/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAlmacenes(int id, Almacenes almacenes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != almacenes.idAlmacen)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(almacenes).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AlmacenesExists(id))
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

        // POST: api/Almacenes

        [ResponseType(typeof(Almacenes))]
        public IHttpActionResult PostAlmacenes(string ticket, string id, Almacenes almacenes)
        {
            try
            {
                if (db.ValidarTicket(ticket, id).Equals("1"))
                {
                    string resp = db.crearAlmacen(almacenes.Descripcion);
                    if (resp.Equals("1"))
                    {
                        return CreatedAtRoute("DefaultApi", new { id = almacenes.idAlmacen }, almacenes);
                    }
                    else if (resp.Equals("Almacen Existe"))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw new Exception(resp);
                    }
                }
                else
                {
                    throw new Exception("El ticket no existe o es invalido.");
                }
                    
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Almacenes/5
        //[ResponseType(typeof(Almacenes))]
        //public IHttpActionResult DeleteAlmacenes(int id)
        //{
        //    Almacenes almacenes = db.Almacenes.Find(id);
        //    if (almacenes == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Almacenes.Remove(almacenes);
        //    db.SaveChanges();

        //    return Ok(almacenes);
        //}

    }
}