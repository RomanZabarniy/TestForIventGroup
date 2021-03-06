﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest_IG.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        //[MaxLength(50)]
        public string Adress { get; set; }
        public string Category { get; set; }
    }
}