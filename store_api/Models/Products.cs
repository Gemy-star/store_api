using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace store_api.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        public string description { get; set; }

        public byte[] ImageData { get; set; }
        public bool status { get; set; }


    }
}
