using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiTcc;

namespace ApiTcc.Data
{
    public class ApiTccContext : DbContext
    {
        public ApiTccContext (DbContextOptions<ApiTccContext> options)
            : base(options)
        {
        }

        public DbSet<ApiTcc.Usuarios> Usuarios { get; set; }
    }
}
