using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class Store
    {
        private Almacen almacen;
        private Validaciones validaciones;

        public Store()
        {
            almacen = new Almacen();
            validaciones = new Validaciones();
        }

        public string crearAlmacen(string descripcion)
        {
            try
            {
                string resp = validaciones.ValidarAlmacen(descripcion);
                if (resp.Equals("1"))
                {
                    if (!almacen.ExisteAlmacen(descripcion))
                    {
                        int res = almacen.CrearAlmacen(new Almacenes()
                        {
                            Descripcion = descripcion,
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
                    else
                    {
                        return "Almacen Existe";
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

        public List<Almacenes> obtenerAlmacenes()
        {
            try
            {
                return almacen.ObtenerAlmacenes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
