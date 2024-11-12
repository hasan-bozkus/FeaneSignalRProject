using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;

namespace SignalRWebUI.Models.Products
{
    public class ProductandCategoryViewModel
    {
        public List<ResultProductWithCategoryDto> Products { get; set; }
        public List<ResultCategoryDto> Categories { get; set; }
    }
}
