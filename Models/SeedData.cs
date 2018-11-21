using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TedBank.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TedBankContext(
                serviceProvider.GetRequiredService<DbContextOptions<TedBankContext>>()))
            {
                // Look for any movies.
                if (context.TedItem.Count() > 0)
                {
                    return;   // DB has been seeded
                }

                context.TedItem.AddRange(
                    new TedItem
                    {
                        Title = "Is war between China and the US inevitable? | Graham Allison",
                        Url = "https://www.youtube.com/watch?v=XewnyUJgyA4",
                        Topic = "war",
                        Uploaded = "20-11-18",
                        Speaker = "Graham Allison"
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
