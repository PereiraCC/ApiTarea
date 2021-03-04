using Datos.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Almacen
    {
        private TAREAEntities entities;


        public Almacen()
        {
            entities = new TAREAEntities();
        }

        public bool ExisteAlmacen(string descripcion)
        {
            try
            {
                List<Almacenes> almacenes = ObtenerAlmacenes();

                foreach (Almacenes alm in almacenes)
                {
                    if (alm.Descripcion == descripcion)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ObtenerIDAlmacen(string descripcion)
        {
            try
            {
                List<Almacenes> almacenes = ObtenerAlmacenes();

                foreach (Almacenes alm in almacenes)
                {
                    if (alm.Descripcion == descripcion)
                    {
                        return alm.idAlmacen;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CrearAlmacen(Almacenes almacenes)
        {
            try
            {
                entities.Almacenes.Add(almacenes);
                return entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Almacenes> ObtenerAlmacenes()
        {
            try
            {
                return entities.Almacenes.ToList<Almacenes>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Almacenes ObtenerUnAlmacen(string nombre)
        {
            try
            {
                List<Almacenes> almacenes = entities.Almacenes.ToList<Almacenes>();

                foreach (Almacenes alm in almacenes)
                {
                    if (alm.Descripcion == nombre)
                    {
                        return alm;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
    }
}
