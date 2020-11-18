using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Dtos
{
    public class CatalogDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 30 символов")]
        public string Name { get; set; }
        public int? ParentCatalogId { get; set; }
        public string UserId { get; set; }   
    }
}
