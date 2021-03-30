using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace store_api.Dtos
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string name { get; set; }

        [Required]
        public string description { get; set; }
        [Required]
        public bool status { get; set; }


        [Required]
        public IFormFile Image { get; set; }
    }
}
