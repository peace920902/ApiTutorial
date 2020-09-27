using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Model;
using Newtonsoft.Json;

namespace MyFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("GetProduct")]
        public List<Product> GetProducts()
        {
            using (ProductContext context = new ProductContext())
            {
                return context.Products.ToList();
            }
        }

        [HttpPost]
        [Route("CreateProduct")]
        public string CreateProduct(Product product)
        {
            using ProductContext context = new ProductContext();
            context.Products.Add(product);
            context.SaveChanges();
            return $"{JsonConvert.SerializeObject(product)}商品已加入清單";
        }

        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public ActionResult CreateProduct(string id,[FromBody]Product product)
        {
            using ProductContext context = new ProductContext();
            Product originProduct = context.Products.FirstOrDefault(x => x.Id == id);
            if (originProduct == null) return BadRequest("if not found");
            if (product.Id != id) return BadRequest("不可修改ID");

            originProduct.Name = product.Name;
            originProduct.Price = product.Price;
            originProduct.Origin = product.Origin;
            context.SaveChanges();
            return Ok($"{JsonConvert.SerializeObject(originProduct)}已被修改");
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")] 
        public ActionResult DeleteProduct(string id)
        {
            using ProductContext context = new ProductContext();
            Product originProduct = context.Products.FirstOrDefault(x => x.Id == id);
            if (originProduct == null) return BadRequest("if not found");

            context.Products.Remove(originProduct);
            context.SaveChanges();
            return Ok($"{JsonConvert.SerializeObject(originProduct)}已被刪除");
        }
    }
}


