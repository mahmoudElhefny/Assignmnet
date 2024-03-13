using Assignment.Domain.Contracts.IRepositories;
using Assignment.Domain.Models.Entities;
using Assignment.Infrastructure.__AppContext;
using Assignment.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Reposatories
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Product> GetProduct(int id) =>
            await GetAll().Where(e => !(bool)e.IsDeleted)?.AsNoTracking()?
                .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public List<Product> GetProducts()
        {
            IQueryable<Product> query = GetAll().Where(e => !(bool)e.IsDeleted).AsNoTracking()
                .Include(p => p.Category);
            return query.ToList();
        }

    }
}
