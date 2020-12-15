using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Dtos
{
    public class ImageDto
    {
        public long Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 200 символов")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
    }
}
