using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TedBank.Models
{
    public class TedBankContext : DbContext
    {
        public TedBankContext (DbContextOptions<TedBankContext> options)
            : base(options)
        {
        }

        public DbSet<TedBank.Models.TedItem> TedItem { get; set; }
    }
}
