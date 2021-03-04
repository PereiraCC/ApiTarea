using Datos;
using Datos.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class Product
    {
        private Producto producto;
        private Almacen almacen;
        private Validaciones validaciones;
        private InicioSesion inicioSesion;

        public Product()
        {
            producto = new Producto();
            almacen = new Almacen();
            validaciones = new Validaciones();
            inicioSesion = new InicioSesion();
        }

        public string crearProducto(string descripcion, string codigo, string cantidad, string nombreAlmacen)
        {
            try
            {
                string resp = validaciones.validarDatosProducto(descripcion, codigo, cantidad, nombreAlmacen);
                if (resp.Equals("1"))
                {
                    if (!producto.ExisteProducto(descripcion))
                    {
                        int res = producto.crearProducto(new Productos()
                        {
                            Descripcion = descripcion,
                            codigo = codigo,
                        });

                        if (res == 1)
                        {
                            resp = crearStock(cantidad, nombreAlmacen, descripcion);
                            return resp;
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        return "Producto Existe";
                    }
                }
                else
                {
                    return resp;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string crearStock(string cantidad, string nombreAlmacen, string descripcion)
        {
            try
            {
                int idAlmacen = almacen.ObtenerIDAlmacen(nombreAlmacen);
                int idProducto = producto.ObtenerUnProducto2(descripcion);
                int res = producto.CrearStock(new Stock()
                {
                    Stock1 = Int32.Parse(cantidad),
                    idAlmacen = idAlmacen,
                    idProducto = idProducto
                });

                if (res == 1)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VLIS_Articulos> obtenerTodosProducto()
        {
            try
            {
                return producto.ObtenerTodos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VLIS_Articulos obtenerUnProducto(string codigo)
        {
            try
            {
                VLIS_Articulos produc;
                if (validaciones.validarCodigoP(codigo))
                {
                    produc = producto.ObtenerUnProducto(codigo);
                }
                else
                {
                    produc = null;
                }
                return produc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string actualizarProducto(string codigo, string descripcion, string cantidad, string nombreAlmacen)
        {
            try
            {
                string resp = validaciones.validarDatosProducto(descripcion, codigo, cantidad, nombreAlmacen);
                if (resp.Equals("1"))
                {
                    if (producto.ExisteProducto(codigo))
                    {
                        int res = producto.ActualizarProducto(codigo, descripcion, cantidad, nombreAlmacen);
                        if (res == 1)
                        {
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        return "Producto No existe";
                    }
                }
                else
                {
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string eliminarProducto(string codigo)
        {
            try
            {
                if (validaciones.validarCodigoP(codigo))
                {
                    if (producto.ExisteProducto(codigo))
                    {
                        int resp = producto.BorrarProducto(codigo);
                        if (resp == 2)
                        {
                            return "1";
                        }
                        else if (resp == 1)
                        {
                            return "0";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        return "Producto No existe";
                    }
                }
                else
                {
                    return "El codigo del producto es invalido.";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidarTicket(string tiquete, string id)
        {
            try
            {
                return inicioSesion.ValidarTicket(tiquete, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
