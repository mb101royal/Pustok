﻿using nov30task.Models;
using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.BlogsVM
{
    public class BlogCreateVM
    {
        [Required, MaxLength(128)]
        public string? Title { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        public int AuthorId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public Author? Author { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
