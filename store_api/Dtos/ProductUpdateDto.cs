﻿using System;
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
    
        public string name { get; set; }

        public string description { get; set; }
        public bool status { get; set; }

    }
}
