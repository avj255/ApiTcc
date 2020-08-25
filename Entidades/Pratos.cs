using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc.Entidades
{
    public class Pratos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pratoID { get; set; }
        public string nome { get; set; }
        public double valor { get; set; }
        public string modopreparo { get; set; }
        public byte[] video { get; set; }
        public byte[] foto { get; set; }
        public virtual ICollection<Pratos_Ingredientes> pratos_Ingredientes { get; set; }
        public List<Ingredientes> Ingredientes { get; set; }
    }
}
