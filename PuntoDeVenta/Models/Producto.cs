using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
 
        public string Nombre { get; set; }
       
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
       
        public int Stock { get; set; }
         public String Categoria { get; set; }
    }
            

    
}
