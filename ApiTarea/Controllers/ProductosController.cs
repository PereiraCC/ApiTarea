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
    public class ProductosController : ApiController
    {
        private Product db = new Product();

        // GET: api/Productos
        public List<VLIS_Articulos> GetProductos(string ticket, string id)
        {
            try
            {
                List<VLIS_Articulos> productos = new List<VLIS_Articulos>();

                if (db.ValidarTicket(ticket, id).Equals("1"))
                {
                    return db.obtenerTodosProducto();
                }
                else
                {
                    return productos;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult GetProductos(string ticket, string id, string codigo)
        {
            try
            {
                if (db.ValidarTicket(ticket, id).Equals("1"))
                {
                    VLIS_Articulos prod = db.obtenerUnProducto(codigo);
                    if (prod != null)
                    {
                        ModelProducto producto = new ModelProducto();
                        producto.codigo = prod.codigo;
                        producto.descripcion = prod.Descripcion;
                        producto.cantidad = prod.Stock.ToString();
                        producto.nombreAlmacen = prod.Almacen;
                        return Ok(producto);
                    }
                    else
                    {
                        return NotFound();
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

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        [Route("api/Productos/Modificar", Name = "PutProductos")]
        public IHttpActionResult PutProductos(string ticket, string id, ModelProducto productos)
        {
            try
            {
                if (db.ValidarTicket(ticket, id).Equals("1"))
                {
                    string resp = db.actualizarProducto(productos.codigo, productos.descripcion, productos.cantidad, productos.nombreAlmacen);
                    if (resp.Equals("1"))
                    {
                        return StatusCode(HttpStatusCode.NoContent);
                    }
                    else if (resp.Equals("Producto No existe"))
                    {
                        return NotFound();
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

        // POST: api/Productos
        [ResponseType(typeof(Productos))]
        public IHttpActionResult PostProductos(string ticket, string id, ModelProducto producto)
        {
            try
            {
                if (db.ValidarTicket(ticket, id).Equals("1"))
                {
                    string resp = db.crearProducto(producto.descripcion, producto.codigo, producto.cantidad, producto.nombreAlmacen);
                    if (resp.Equals("1"))
                    {
                        return CreatedAtRoute("DefaultApi", new { id = producto.codigo }, producto);
                    }
                    else if (resp.Equals("Producto Existe"))
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

        // DELETE: api/Productos/5
        [ResponseType(typeof(Productos))]
        //[Route("api/Productos/Eliminar", Name = "DeleteProductos")]
        public IHttpActionResult DeleteProductos(string data)
        {
            try
            {
                string[] info = data.Split(',');
                if (db.ValidarTicket(info[0], info[1]).Equals("1"))
                {
                    string resp = db.eliminarProducto(info[2]);
                    if (resp.Equals("1"))
                    {
                        return StatusCode(HttpStatusCode.NoContent);
                    }
                    else if (resp.Equals("Producto No existe"))
                    {
                        return NotFound();
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

    }
}