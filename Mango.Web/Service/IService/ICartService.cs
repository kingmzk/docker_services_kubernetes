using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string couponCode);
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int CartDetailsId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);

        Task<ResponseDto?> EmailCart(CartDto cartDto);
       
    }
}
