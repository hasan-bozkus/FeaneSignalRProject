﻿using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<ResultProductWithCategoryDto> GetProductsWithCategories();
        int ProductCount();
        int ProductCountByNameHumburger();
        int ProductCountByNameDrink();
        decimal ProductPriceAvg();
        string ProductNameByMaxPrice();
        string ProductNameByMinPrice();
        decimal ProductAvgPriceByHamburger();
        decimal ProductPriceBySetakBurger();
        decimal TotalPriceByDrinkCategory();
        decimal TotalPriceBySalatCategory();

        List<ResultGetLast9ProductsDto> GetLast9Products();
    }
}
