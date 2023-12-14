using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductsService
    {
        float GetMinimalPrice();
        float? GetPrice(string name);

        IEnumerable<Product> GetBelowPrice(float price);
        IEnumerable<Product> GetAboveAverage();
        string GetProductsSummary();
        float GetPriceSumFor5CharNames();
    }
}
