using Datos;
using Datos.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class Users
    {
        private Usuario usuario = new Usuario();
        private Validaciones validaciones = new Validaciones();
        private InicioSesion inicioSesion = new InicioSesion();

        public string crearUsuario(string identificacion, string nombre, string apellidos, string password)
        {
            try
            {
                string resp = validaciones.ValidarUsuarioRegistro(identificacion, nombre, apellidos, password);
                if (resp.Equals("1"))
                {
                    if (!usuario.ExisteUsuario(identificacion))
                    {
                        int res = usuario.crearUsuario(new Usuarios()
                        {
                            Identificacion = identificacion,
                            Nombre = nombre,
                            Apellidos = apellidos,
                            pass = EncrytarPassword(password)
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
                        return "Usuario Existe";
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

        private string EncrytarPassword(string password)
        {
            try
            {
                string result = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
                result = Convert.ToBase64String(encryted);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string DesEncrytarPassword(string password)
        {
            try
            {
                string result = string.Empty;
                byte[] dencryted = Convert.FromBase64String(password);
                result = System.Text.Encoding.Unicode.GetString(dencryted);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidarInicioSesion(string identificacion, string password)
        {
            try
            {
                string resp = validaciones.ValidarUsuarioInicioSesion(identificacion, password);
                if (resp.Equals("1"))
                {

                    string clave = inicioSesion.ValidarInicioSesion(identificacion, password);
                    if (!clave.Equals("0"))
                    {
                        return clave;
                    }
                    else if (clave.Equals("0"))
                    {
                        return "Usuario y/o contraseña incorrectos.";
                    }
                    else 
                    {
                        return clave;
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
    }
}
