using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Reposatory;
using Talabat.Reposatory.Data;

namespace Talabat.Reposatory
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalbatDbContext talbatDb;

        public GenericRepository(TalbatDbContext talbatDb)
        {
            this.talbatDb = talbatDb;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Product))
            {
                return (IEnumerable<T>) await talbatDb.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType)
                    .ToListAsync();
            }
            else 
                  return await talbatDb.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
         
                return await talbatDb.Set<T>().FindAsync(id);
        }
    }
}
