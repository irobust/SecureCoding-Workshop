using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Globomantics.ProductsApi.NETCORE2.Model;
using Microsoft.AspNetCore.Mvc;

namespace Gobomantis.ProductsApi.NETCORE2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            var imageSizes = new int[] { 640, 480, 300, 500 };
            var products = new Faker<ProductModel>()
                            .RuleFor(o => o.Id, f => Guid.NewGuid())
                            .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                            .RuleFor(o => o.Image, f => f.Image.PicsumUrl(f.PickRandomParam(imageSizes), f.PickRandomParam(imageSizes)))
                            .RuleFor(o => o.Price, f => f.Commerce.Price());

            return products.Generate(100);
        }
    }
}
