using Assignment.Domain.Models.Entities;
using Assignment.Domain.ViewModels;
using Assignment.Infrastructure.Entities;
using Assignment.Service.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Mapper
{
    public class ProductMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<productVM, Product>().
                ForMember(dest => dest.Image, opt => opt.MapFrom
                (src => src.Image != null ? uploadImage(src.Image, "ProductImage").Result : src.ImageUrl)).
                ForMember(dest => dest.ProductCode, opt => opt.Ignore());
            mapperConfigs.CreateMap<Product, GetProductVM>();
        }
        public static async Task<string> uploadImage(IFormFile image, string filepath)//, Guid imageId)
        {
            //string NewName = Guid.NewGuid().ToString() + image.FileName;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);
            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            var imageUrl = "/images/" + fileName;
            return imageUrl;
        }
      

    }
}
