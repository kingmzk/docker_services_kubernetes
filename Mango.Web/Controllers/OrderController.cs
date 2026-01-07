using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        public IActionResult OrderIndex()
        {
            return View();
        }

        /*
        [HttpGet]
        public  IActionResult GetAll()
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            if(!User.IsInRole(SD.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value;
            }
            ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<IEnumerable<OrderHeaderDto>>(Convert.ToString(response.Result));
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new { data = list });


        }
        
        */

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            try
            {
                if (!User.IsInRole(SD.RoleAdmin))
                {
                    userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
                }
                ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<IEnumerable<OrderHeaderDto>>(Convert.ToString(response.Result));
                }
                else
                {
                    list = new List<OrderHeaderDto>();
                }
                Console.WriteLine(list);
                return Json(new { data = list });
            }
            catch (Exception ex)
            {
             
                return StatusCode(500, ex.Message); // Internal Server Error
            }
        }
        

    }


}
