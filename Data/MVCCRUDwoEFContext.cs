using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCCRUDwoEF.Models;

namespace MVCCRUDwoEF.Data
{
    public class MVCCRUDwoEFContext : DbContext
    {
        public MVCCRUDwoEFContext (DbContextOptions<MVCCRUDwoEFContext> options)
            : base(options)
        {
        }

        public DbSet<MVCCRUDwoEF.Models.PenViewModel> PenViewModel { get; set; } = default!;
    }
}
