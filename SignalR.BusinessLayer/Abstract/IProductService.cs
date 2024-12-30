using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        List<ResultProductWithCategoryDto> TGetProductsWithCategories();
        int TProductCount();
        int TProductCountByNameHumburger();
        int TProductCountByNameDrink();
        decimal TProductPriceAvg();
        string TProductNameByMaxPrice();
        string TProductNameByMinPrice();
        decimal TProductAvgPriceByHamburger(); 
        decimal TProductPriceBySetakBurger();
        decimal TTotalPriceByDrinkCategory();
        decimal TTotalPriceBySalatCategory();
        List<ResultGetLast9ProductsDto> TGetLast9Products();
    }
}
