using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTarea.Models
{
    public class ModelProducto
    {
        public string descripcion { get; set; }

        public string codigo { get; set; }

        public string cantidad { get; set; }

        public string nombreAlmacen { get; set; }
    }
}