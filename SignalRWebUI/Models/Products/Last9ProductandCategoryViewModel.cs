using SignalR.DtoLayer.ProductDto;
using SignalRWebUI.Dtos.CategoryDtos;

namespace SignalRWebUI.Models.Products
{
    public class Last9ProductandCategoryViewModel
    {
        public List<ResultGetLast9ProductsDto> Products { get; set; }
        public List<ResultCategoryDto> Categories { get; set; }
    }
}
