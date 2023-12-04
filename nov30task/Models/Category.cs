﻿using System.ComponentModel.DataAnnotations;

namespace nov30task.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(16)]
        public string Name { get; set; }
    }
}
