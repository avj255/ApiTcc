using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc.Entidades
{
    public class Pedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pedidoID { get; set; }
        public double valor { get; set; }
        public string mesa { get; set; }
        [ForeignKey("Pratos")]
        public int prato { get; set; }
        [ForeignKey("Usuarios")]
        public int usuario { get; set; }
        public int quantidade { get; set; }
        public int situacao { get; set; }

        [NotMapped]
        public Pratos pratoObj { get; set; }
        [NotMapped]
        public string nomeUsuario { get; set; }
    }
}
