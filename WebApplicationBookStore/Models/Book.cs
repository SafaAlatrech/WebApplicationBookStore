﻿namespace WebApplicationBookStore.Models
{
    public class Book
    { 
        public int Id { get; set; } 

        public string? Title { get; set; }
        public string? Description  { get; set; } 
        public string? ImageURL  { get; set; } 
        public Author? Author { get; set; }
    }
}
