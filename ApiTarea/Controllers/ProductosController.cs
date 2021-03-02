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
        public List<VLIS_Articulos> GetProductos()
        {
            try
            {
                return db.obtenerTodosProducto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult GetProductos(string codigo)
        {
            try
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductos(string codigo, ModelProducto productos)
        {
            try
            {
                string resp = db.actualizarProducto(codigo, productos.descripcion, productos.cantidad, productos.nombreAlmacen);
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Productos
        [ResponseType(typeof(Productos))]
        public IHttpActionResult PostProductos(ModelProducto producto)
        {
            try
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult DeleteProductos(string codigo)
        {
            try
            {
                string resp = db.eliminarProducto(codigo);
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}