using Assignment.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Contracts.IRepositories
{
    public interface IProductRepository:IBaseRepository<Product,int>
    {
         List<Product> GetProducts();
         Task<Product> GetProduct(int id);
    }
}
