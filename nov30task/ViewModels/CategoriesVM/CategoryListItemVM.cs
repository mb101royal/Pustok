﻿using nov30task.Models;
using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.CategoriesVM
{
    public class CategoryListItemVM
    {
        public int Id { get; set; }
        [MaxLength(16)]
        public string Name { get; set; }

    }
}
