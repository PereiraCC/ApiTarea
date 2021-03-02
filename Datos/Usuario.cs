using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Usuario
    {
        private TAREAEntities entities;

        public Usuario()
        {
            entities = new TAREAEntities();
        }

        public bool ExisteUsuario(string identificacion)
        {
            try
            {
                var query = from c in entities.Usuarios
                            where c.Identificacion == identificacion
                            select c;
                List<Usuarios> user = query.ToList<Usuarios>();
                if (user.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int crearUsuario(Usuarios u)
        {
            try
            {
                entities.Usuarios.Add(u);
                return entities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuarios> BuscarUsuario(string identificacion)
        {
            try
            {
                var query = from c in entities.Usuarios
                            where c.Identificacion == identificacion
                            select c;
                return query.ToList<Usuarios>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
