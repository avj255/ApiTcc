using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc.Entidades
{
    public class Ingredientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ingredienteID { get; set; }
        public string nome { get; set; }
        public double peso { get; set; }
        public double calorias {get; set; }
        public virtual ICollection<Pratos_Ingredientes> pratos_Ingredientes { get; set; }
    }
}
