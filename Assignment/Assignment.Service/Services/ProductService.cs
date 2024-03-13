using Assignment.Domain.Contracts.IRepositories;
using Assignment.Domain.Contracts.IServices;
using Assignment.Domain.Contracts.Repositories;
using Assignment.Domain.Models.Entities;
using Assignment.Domain.ViewModels;
using Assignment.Infrastructure.Entities;
using Assignment.Service.Validators;


using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Assignment.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
            IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<productVM>> Add(productVM productVM)
        {
            Response<productVM> response = new();
            response.Errors = ValidatorHandler.Validate(productVM, (AddProductValidator)Activator.CreateInstance(typeof(AddProductValidator)));
            if (response.Errors != null)
                throw new InvalidViewModelException(response.Errors);
            try
            {
                Product productmodel = _mapper.Map<Product>(productVM);
                productmodel.IsDeleted = false;
                productmodel.CreatedDate = DateTime.Now;
                var lastProduct = _productRepository.GetProducts().LastOrDefault();
                var newCode = lastProduct is not null ? int.Parse(lastProduct.ProductCode.Substring(1)) + 1 : 1;
                productmodel.ProductCode = $"P{newCode:D2}";
                 _productRepository.Add(productmodel);
                await _unitOfWork.SaveChanges();
                response.Errors = null;
                response.IsSucceded = true;
                response.Data = productVM;
                return response;
            }
            catch (Exception ex)
            {
                response.Errors = new List<Error> { new Error() { ErrorMessage = $"{ex.Message}" } };
                response.IsSucceded = false;
                response.Data = null;
                return response;
            }
        }
        public async Task<Response<bool>> Remove(int id)
        {
            Response<bool> response = new();
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product != null)
                {
                     _productRepository.Remove(product);
                    await _unitOfWork.SaveChanges();
                    response.IsSucceded = true;
                    response.Errors = null;
                    return response;
                }
                else
                {
                    response.IsSucceded = false;
                    response.Errors = new() { new Error() { ErrorMessage = $"Product Dosn't exit" } };
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.IsSucceded = false;
                response.Errors = new() { new Error() { ErrorMessage = $"{ex.Message}" } };
                return response;
            }
        }

        public async Task<Response<List<GetProductVM>>> GetAllProducts()
        {
            Response<List<GetProductVM>> response = new();
            try
            {
                var products = _productRepository.GetProducts();

                if (products == null)
                {
                    response.IsSucceded = false;
                    response.Data = null;
                    response.Errors = new() { new Error() { ErrorMessage = "Products Dosn't Exit" } };
                    return response;
                }
                response.IsSucceded = true;
                response.Data = _mapper.Map<List<GetProductVM>>(products);
                return response;

            }
            catch (Exception ex)
            {
                response.IsSucceded = false;
                response.Data = null;
                response.Errors = new() { new Error() { ErrorMessage = $"{ex.Message}" } };
                return response;
            }
        }

        public async Task<Response<GetProductVM>> GetById(int id)
        {
            Response<GetProductVM> response = new();
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product == null)
                {
                    response.IsSucceded = false;
                    response.Data = null;
                    response.Errors = new() { new Error() { ErrorMessage = "Product Dosn't Exit" } };
                    return response;
                }
                response.IsSucceded = true;
                response.Data = _mapper.Map<GetProductVM>(product);
                return response;

            }
            catch (Exception ex)
            {
                response.IsSucceded = false;
                response.Data = null;
                response.Errors = new() { new Error() { ErrorMessage = $"{ex.Message}" } };
                return response;
            }
        }
        public async Task<Response<productVM>> update(int id, productVM productVM)
        {
            Response<productVM> response = new();
            response.Errors = ValidatorHandler.Validate(productVM, (AddProductValidator)Activator.CreateInstance(typeof(AddProductValidator)));
            if (response.Errors != null)
                throw new InvalidViewModelException(response.Errors);
            Product oldProduct = await _productRepository.GetProduct(id);
            if (oldProduct != null)
            {
                try
                {
                    var prdCode = oldProduct.ProductCode;
                    oldProduct = _mapper.Map<Product>(productVM);
                    oldProduct.ProductCode = prdCode;
                    oldProduct.IsDeleted = false;
                    oldProduct.CreatedDate = DateTime.Now;
                     _productRepository.Update(oldProduct);
                    await _unitOfWork.SaveChanges();
                    response.Errors = null;
                    response.IsSucceded = true;
                    response.Data = productVM;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Errors = new List<Error> { new Error() { ErrorMessage = $"{ex.Message}" } };
                    response.IsSucceded = false;
                    response.Data = null;
                    return response;
                }
            }
            else
            {
                response.Errors = new List<Error> { new Error() { ErrorMessage = $"Product dosn't exit" } };
                response.IsSucceded = false;
                response.Data = null;
                return response;
            }

        }
    }
}
