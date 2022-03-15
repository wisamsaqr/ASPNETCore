﻿namespace Products.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}