﻿using System.ComponentModel.DataAnnotations;

namespace Forum.DB.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string? Content { get; set; }
    }
}
