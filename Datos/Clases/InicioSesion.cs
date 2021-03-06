﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Clases
{
    public class InicioSesion
    {
        private TAREAEntities entities;
        private Ticket ticket;

        public InicioSesion()
        {
            entities = new TAREAEntities();
            ticket = new Ticket();
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

        public int IdUsuario(string identificacion)
        {
            try
            {
                var query = from c in entities.Usuarios
                            where c.Identificacion == identificacion
                            select c;
                List<Usuarios> user = query.ToList<Usuarios>();
                if (user.Count > 0)
                {
                    return user[0].idUsuario;
                }
                else
                {
                    return 0;
                }
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

        public string ValidarInicioSesion(string identificacion, string password)
        {
            try
            {
                List<Usuarios> usuarios = BuscarUsuario(identificacion);

                if (usuarios.Count > 0)
                {
                    foreach (Usuarios user in usuarios)
                    {
                        if (user.Identificacion == identificacion && DesEncrytarPassword(user.pass) == password)
                        {
                            string resp = ticket.validarTicket(IdUsuario(identificacion));
                            if (resp.Equals("2") || resp.Equals("1") || resp.Equals("0"))
                            {
                                if (ticket.InactivarTiquete(IdUsuario(identificacion)))
                                {
                                    string clave = ticket.crearTicket(IdUsuario(identificacion), user.Identificacion, user.Nombre, user.Apellidos);
                                    if (clave.Equals("Error al generar el tiquete"))
                                    {
                                        return "0";
                                    }
                                    else
                                    {
                                        return clave;
                                    }
                                }
                                else
                                {
                                    return "Error al generar el tiquete";
                                }
                            }
                            else if (resp.Equals("No tickets"))
                            {
                                string clave = ticket.crearTicket(IdUsuario(identificacion), user.Identificacion, user.Nombre, user.Apellidos);
                                if (clave.Equals("Error al generar el tiquete"))
                                {
                                    return "0";
                                }
                                else
                                {
                                    return clave;
                                }
                            }
                        }
                    }
                    return "0";
                }
                else
                {
                    return "El usuario no existe";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CerrarSesion(string identificacion)
        {
            try
            {
                List<Usuarios> usuarios = BuscarUsuario(identificacion);

                if (usuarios.Count > 0)
                {

                    if (ticket.InactivarTicketCierreSesion(IdUsuario(identificacion)))
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
                    return "El usuario no existe";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidarTicket(string tiquete, string iden)
        {
            try
            {
                string resp = ticket.GetTicket(IdUsuario(iden));
                if (!resp.Equals("0"))
                {
                    tiquete = tiquete.Replace('"', ' ');
                    tiquete = tiquete.Trim();
                    if (resp.Equals(tiquete))
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
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool RefrescarTicket(string iden)
        {
            try
            {
                if (ticket.RefrescarTiquete(IdUsuario(iden)))
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
    }
}
