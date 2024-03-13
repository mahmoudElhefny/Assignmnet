using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment.Domain.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(58)]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Product> products { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
