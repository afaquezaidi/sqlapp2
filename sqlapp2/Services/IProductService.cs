using sqlapp.Models;
using System.Collections.Generic;

namespace sqlapp2.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}