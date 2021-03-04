using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Datos.Clases
{
    public class Ticket
    {
        private TAREAEntities entities;

        public Ticket()
        {
            entities = new TAREAEntities();
        }

        private int CantidadMinutos()
        {
            try
            {
                var temp = from l in entities.ConfiguracionToken
                           select l;
                List<ConfiguracionToken> t = temp.ToList<ConfiguracionToken>();

                return t[0].Tiempo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RefrescarTiquete(int usuario)
        {
            try
            {
                Tickets ticket = obtenerTicket(usuario);

                Tickets nuevo = ticket;
                nuevo.HoraFinal = nuevo.HoraInicio.AddMinutes(CantidadMinutos());
                int n = entities.SaveChanges();
                if (n > 0)
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

        //public bool InactivarTiquete(int usuario, string identificacion, string pass)
        //{
        //    try
        //    {
        //        var temp = from l in entities.Tickets
        //                   where l.idUsuario == usuario && l.Estado == true
        //                   select l;

        //        List<Tickets> t = temp.ToList<Tickets>();

        //        if (t.Count > 0)
        //        {
        //            if (t[0].Fecha < DateTime.Now)
        //            {
        //                Tickets nuevo = t.First<Tickets>(x => x.idUsuario == usuario && x.Ticket == tiquet);
        //                nuevo.Estado = false;
        //                int n = entities.SaveChanges();
        //                //crearTicket(usuario);
        //                if (n > 0)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {

        //                int horaAactual = DateTime.Now.Hour;
        //                int horaInicio = t[0].HoraInicio.Hour;
        //                int MinutosActual = DateTime.Now.Minute;
        //                int MinutosInicio = t[0].HoraInicio.Minute;
        //                int tiempoEstablecido = CantidadMinutos();
        //                if ((horaAactual - horaInicio) == 0 && (MinutosInicio - MinutosActual) > tiempoEstablecido)
        //                {
        //                    Tickets nuevo = t.First<Tickets>(x => x.idUsuario == usuario && x.Ticket == tiquet);
        //                    nuevo.Estado = false;
        //                    int n = entities.SaveChanges();
        //                    //crearTicket(usuario);
        //                    if (n > 0)
        //                    {
        //                        return true;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }

        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }

        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public bool InactivarTiquete(int usuario)
        {
            try
            {
                Tickets ticket = obtenerTicket(usuario);

                if (ticket.Fecha < DateTime.Now)
                {
                    Tickets nuevo = ticket;
                    nuevo.Estado = false;
                    int n = entities.SaveChanges();
                    if (n > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    DateTime horaActual = DateTime.Now.ToLocalTime();
                    if (ticket.HoraInicio < horaActual && ticket.HoraFinal > horaActual)
                    {
                        Tickets nuevo = ticket;
                        nuevo.Estado = false;
                        int n = entities.SaveChanges();
                        if (n > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetMD5(string str)
        {
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = md5.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public string crearTicket(int idusuario, string iden, string nombre, string apellido)
        {
            try
            {
                string clave = GetMD5(idusuario.ToString() + iden + nombre + apellido+DateTime.Now.Date+DateTime.Now.Hour+DateTime.Now.Minute+DateTime.Now.Second);
                Tickets tiquete = new Tickets();
                tiquete.Estado = true;
                tiquete.Fecha = DateTime.Now.Date;
                tiquete.HoraInicio = DateTime.Now.ToLocalTime();
                tiquete.Ticket = clave;
                tiquete.idUsuario = idusuario;
                tiquete.HoraFinal = tiquete.HoraInicio.AddMinutes(CantidadMinutos());
                entities.Tickets.Add(tiquete);

                int res = entities.SaveChanges();
                if (res != 0)
                {
                    return clave;
                }
                else
                {
                    return "Error al generar el tiquete";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool InactivarTicketCierreSesion(int usuario)
        {
            try
            {
                Tickets ticket = obtenerTicket(usuario);

                Tickets nuevo = ticket;
                nuevo.Estado = false;
                int n = entities.SaveChanges();
                if (n > 0)
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

        public string validarTicket(int usuario)
        {
            try
            {
                DateTime horaActual = DateTime.Now.ToLocalTime();
                var temp = from l in entities.Tickets
                           where l.idUsuario == usuario && l.Estado == true
                           select l;

                List<Tickets> t = temp.ToList<Tickets>();

                if(t.Count > 0)
                {
                    if (t[0].HoraInicio < horaActual && t[0].HoraFinal > horaActual)
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
                    return "No tickets";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GetTicket(int usuario)
        {
            try
            {
                string validar = validarTicket(usuario);

                if (validar.Equals("1"))
                {
                    var temp = from l in entities.Tickets
                               where l.idUsuario == usuario && l.Estado == true
                               select l;

                    List<Tickets> t = temp.ToList<Tickets>();

                    return t[0].Ticket;
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

        public Tickets obtenerTicket(int usuario)
        {
            try
            {
                var temp = from l in entities.Tickets
                           where l.idUsuario == usuario && l.Estado == true
                           select l;

                List<Tickets> t = temp.ToList<Tickets>();

                return t[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
