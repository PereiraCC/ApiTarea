using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class Validaciones
    {
        public string ValidarUsuarioRegistro(string Identificacion, string Nombre, string Apellidos, string Contrasena)
        {
            try
            {
                if (!ValidarNulos(Identificacion) || !ValidarNulos(Nombre) || !ValidarNulos(Apellidos) || !ValidarNulos(Contrasena))
                {
                    if (ValidarNumero(Identificacion))
                    {
                        if (validarTexto(Nombre) && validarTexto(Apellidos))
                        {
                            if (!validarKeyWords(Identificacion) || !validarKeyWords(Nombre) || !validarKeyWords(Apellidos) || !validarKeyWords(Contrasena))
                            {
                                if (Contrasena.Length >= 6)
                                {
                                    return "1";
                                }
                                else
                                {
                                    return "La contraseña no contiene una cantidad de caracteres validos.";
                                }
                            }
                            else
                            {
                                return "Los datos contienen informacion invalida.";
                            }
                        }
                        else
                        {
                            return "El campo Nombre y Apellidos no deben contener numeros.";
                        }
                    }
                    else
                    {
                        return "El campo de Identificacion no debe contener letras.";
                    }
                }
                else
                {
                    return "Los datos son requeridos.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidarUsuarioInicioSesion(string Identificacion, string Contrasena)
        {
            try
            {
                if (!ValidarNulos(Identificacion) || !ValidarNulos(Contrasena))
                {
                    if (ValidarNumero(Identificacion))
                    {
                        if (!validarKeyWords(Identificacion) || !validarKeyWords(Contrasena))
                        {
                            if (Contrasena.Length >= 6)
                            {
                                return "1";
                            }
                            else
                            {
                                return "La contraseña no contiene una cantidad de caracteres validos.";
                            }
                        }
                        else
                        {
                            return "Los datos contienen informacion invalida.";
                        }
                    }
                    else
                    {
                        return "La identificacion no debe contener letras.";
                    }
                }
                else
                {
                    return "El campo de Identificacion y la contraseña es Obligatorio";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidarUsuarioInicioSesion(string Identificacion)
        {
            try
            {
                if (!ValidarNulos(Identificacion))
                {
                    if (ValidarNumero(Identificacion))
                    {
                        if (!validarKeyWords(Identificacion))
                        {
                            return "1";
                        }
                        else
                        {
                            return "Los datos contienen informacion invalida.";
                        }
                    }
                    else
                    {
                        return "La identificacion no debe contener letras.";
                    }
                }
                else
                {
                    return "El campo de Identificacion es Obligatorio";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidarAlmacen(string descripcion)
        {
            try
            {
                if (!ValidarNulos(descripcion))
                {
                    if (!validarKeyWords(descripcion))
                    {
                        return "1";
                    }
                    else
                    {
                        return "La descripcion del almacen es invalida.";
                    }
                }
                else
                {
                    return "La descripcion del almacen es obligatoria.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string validarDatosProducto(string descripcion, string codigo, string cantidad, string nombreAlmacen)
        {
            try
            {
                if (!ValidarNulos(descripcion) || !ValidarNulos(codigo) || !ValidarNulos(cantidad) || !ValidarNulos(nombreAlmacen))
                {
                    if (ValidarNumero(codigo) || ValidarNumero(cantidad))
                    {
                        if (validarTexto(descripcion))
                        {
                            if (!validarKeyWords(descripcion) || !validarKeyWords(codigo) || !validarKeyWords(cantidad) || !validarKeyWords(nombreAlmacen))
                            {
                                return "1";
                            }
                            else
                            {
                                return "Los datos contienen informacion invalida.";
                            }
                        }
                        else
                        {
                            return "El campo Descripcion deben contener solo letras.";
                        }
                    }
                    else
                    {
                        return "El campo Codigo y Cantidad deben contener solo numeros.";
                    }
                }
                else
                {
                    return "Los datos estan incompletos.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarCodigoP(string data)
        {
            try
            {
                if (ValidarNulos(data) == false && ValidarNumero(data))
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

        public bool validarTexto(string data)
        {
            try
            {
                data = data.Replace(" ", String.Empty);
                if (data.All(char.IsLetter))
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

        public bool ValidarNumero(string data)
        {
            try
            {
                data = data.Replace(" ", String.Empty);
                if (data.All(char.IsDigit))
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

        public bool ValidarNulos(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
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

        private bool validarKeyWords(string data)
        {
            try
            {
                data = data.Replace(" ", String.Empty);
                data = data.ToUpper();
                if ((data.Contains("SELECT") || data.Equals("SELECT")) || (data.Contains("UPDATE") || data.Equals("UPDATE")) || (data.Contains("INSERT") || data.Equals("INSERT")) || (data.Contains("DELETE")) || data.Equals("DELETE"))
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
