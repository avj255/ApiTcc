using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc.Entidades
{
    public class Pratos_DiaSemana
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idpratodia { get; set; }
        public int diasemana { get; set; }
        [ForeignKey("Prato")]
        public virtual Pratos prato { get; set; }
    }
}
