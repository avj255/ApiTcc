using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc.Entidades
{
    public class Pratos_Ingredientes
    {
        [Key, Column(Order = 0), ForeignKey("Pratos")]
        public int pratoID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Ingredientes")]
        public int ingredienteID { get; set; }
    }
}
