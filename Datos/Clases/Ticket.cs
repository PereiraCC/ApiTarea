using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool RefrescarTiquete(int usuario, string tiquet)
        {
            try
            {
                return true;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool InactivarTiquete(int usuario, string tiquet, string identificacion, string pass)
        {
            try
            {
                var temp = from l in entities.Tickets
                           where l.idUsuario == usuario && l.Ticket.Equals(tiquet) && l.Estado == true
                           select l;

                List<Tickets> t = temp.ToList<Tickets>();

                if (t.Count > 0)
                {
                    if (t[0].Fecha < DateTime.Now)
                    {
                        Tickets nuevo = t.First<Tickets>(x => x.idUsuario == usuario && x.Ticket == tiquet);
                        nuevo.Estado = false;
                        int n = entities.SaveChanges();
                        crearTicket(usuario);
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

                        int horaAactual = DateTime.Now.Hour;
                        int horaInicio = t[0].HoraInicio.Hour;
                        int MinutosActual = DateTime.Now.Minute;
                        int MinutosInicio = t[0].HoraInicio.Minute;
                        int tiempoEstablecido = CantidadMinutos();
                        if ((horaAactual - horaInicio) == 0 && (MinutosInicio - MinutosActual) > tiempoEstablecido)
                        {
                            Tickets nuevo = t.First<Tickets>(x => x.idUsuario == usuario && x.Ticket == tiquet);
                            nuevo.Estado = false;
                            int n = entities.SaveChanges();
                            crearTicket(usuario);
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

        public string crearTicket(int idusuario)
        {
            try
            {
                string clave = idusuario.ToString() + "prueba";
                Tickets tiquete = new Tickets();
                tiquete.Estado = true;
                tiquete.Fecha = DateTime.Now.Date;
                tiquete.HoraInicio = DateTime.Now.ToLocalTime();
                tiquete.Ticket = clave;
                tiquete.idUsuario = idusuario;
                tiquete.HoraFinal = null;
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
    }
}
