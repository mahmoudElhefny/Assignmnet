using Assignment.Domain.Models.Entities;
using Assignment.Domain.ViewModels;
using Assignment.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Contracts.IServices
{
    public interface IProductService
    {
        public Task<Response<productVM>> Add(productVM productVM);
        public Task<Response<productVM>> update(int id,productVM productVM);
        public Task<Response<List<GetProductVM>>> GetAllProducts();
        public Task<Response<GetProductVM>> GetById(int id);
        public Task<Response<bool>> Remove(int id);
        
    }
}
