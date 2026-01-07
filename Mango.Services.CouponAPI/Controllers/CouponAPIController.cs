using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
 
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper mapper;
        private ResponseDTO _response;

        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            this.mapper = mapper;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                _response.Result = mapper.Map<CouponDto>(obj);

            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }



        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
                if (obj == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = mapper.Map<CouponDto>(obj);

            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }



        [HttpPost]
      
        public ResponseDTO Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();
                
                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "usd",
                    Id = couponDto.CouponCode,
                   
                };
                
                var service = new Stripe.CouponService();
                service.Create(options);

                
                _response.Result = mapper.Map<CouponDto>(obj);

            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ResponseDTO put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(obj);
                _db.SaveChanges();
                _response.Result = mapper.Map<CouponDto>(obj);

            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public ResponseDTO Delete(int id)
        {

            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();


                var service = new Stripe.CouponService();
                service.Delete(obj.CouponCode);


            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }
    }
}
