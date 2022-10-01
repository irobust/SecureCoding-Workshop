using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Globomantics.ProductsApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Globomantics.ProductsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            var products = new Faker<ProductModel>()
                            .RuleFor(o => o.Id, f => Guid.NewGuid())
                            .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                            .RuleFor(o => o.Image, f => f.Image.PicsumUrl(200, 200))
                            .RuleFor(o => o.Price, f => f.Commerce.Price());

            return products.Generate(100);
        }
    }
}
