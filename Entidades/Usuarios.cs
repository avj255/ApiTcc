using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTcc
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userID { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public string cpf { get; set; }
        public int administrador { get; set; }
        [NotMapped]
        public string novaSenha { get; set; }
    }
}
