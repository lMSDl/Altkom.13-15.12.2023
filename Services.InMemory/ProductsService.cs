﻿using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    internal class ProductsService : GenericService<Product>, IProductsService
    {

        public ProductsService()
        {

            _entities = new List<Product>
                {
                    new Product("Kawa", 12, DateTime.Now.AddSeconds(3261272)),
                    new Product("Herbata", 45, DateTime.Now.AddSeconds(2938381)),
                    new Product("Lalka", 223),
                    new Product("Książka", 93),
                    new Product("Mleko", 12.2f, DateTime.Now.AddSeconds(3919919)),
                    new Product("Czekolada", 9.99f, DateTime.Now.AddSeconds(2501001)),
                    new Product("Telefon", 2000.50f),
                    new Product("Miś", 222),
                    new Product("Kiełbasa", 45, DateTime.Now.AddSeconds(1500000)),
                    new Product("Ser", 35, DateTime.Now.AddSeconds(2033092)),
                    new Product("Spodnie", 123),
                    new Product("Bluza", 53.12f),
                    new Product("Buty", 90.70f),
                    new Product("Woda", 20, DateTime.Now.AddSeconds(6038329)),
                    new Product("Czapka", 50),
                    new Product("Zegarek", 550),
                    new Product("Szalik", 123.22f),
                    new Product("Rękawiczki", 76),
                    new Product("Olej", 49, DateTime.Now.AddSeconds(3000000)),
                    new Product("Choinka", 300)
                };

        }

        public IEnumerable<Product> GetAboveAverage()
        {
            float average = _entities.Average(x => x.Price);
            return _entities.Where(x => x.Price > average).ToList();
        }

        public IEnumerable<Product> GetBelowPrice(float price)
        {
            return _entities.Where(x => x.Price < price).ToList();
        }

        public float GetMinimalPrice()
        {
            return _entities.Min(x => x.Price);
        }

        public float? GetPrice(string name)
        {
            return _entities.SingleOrDefault(x => x.Name == name)?.Price;
        }

        public float GetPriceSumFor5CharNames()
        {
            return _entities.Where(x => x.Name.Length < 5).Sum(x => x.Price);
        }

        public string GetProductsSummary()
        {
            return string.Join(";", _entities.Select(x => x.Summary).ToList());
        }
    }
}
