﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Shared.Entities
{
    //create a class Book with the members Id, Title Author, NoOfPages, Language, Category, Price and ImageUrl
    // Add data annotations for validation such as required, stringlength, min and max etc
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NoOfPages { get; set; }

        [Required]
        [StringLength(50)]
        public string Language { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(200)]
        public string ImageUrl { get; set; }
    }
}
