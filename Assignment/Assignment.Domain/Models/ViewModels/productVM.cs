using Assignment.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Assignment.Domain.ViewModels
{
    public class productVM
    {

        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public float Price { get; set; }
        public float MinimumQuantity { get; set; }
        public float? DiscountRate { get; set; }
        public int Category_Id { get; set; }
    }
    public class GetProductVM
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public float MinimumQuantity { get; set; }
        public float? DiscountRate { get; set; }
        
        public Category Category { get; set; }

    }
}
public class CategoryVM
{
    public int Id { get; set; }
    public string Name { get; set; }
}
