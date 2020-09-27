using Microsoft.EntityFrameworkCore;

namespace MyFirstApi.Model
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public int Price { get; set; }
    }
}