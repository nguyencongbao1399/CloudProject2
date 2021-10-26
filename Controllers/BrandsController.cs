using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;
using TrackingVoucher_v02.Services;

namespace TrackingVoucher_v02.Controllers
{
    [Route("api/v1/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandSer;
        private readonly IPromotionService _proSer;
        private readonly IStoreService _storeSer;
        private readonly IAppliedPromotionService _appliedSer;
        private readonly IClaimedVoucherService _claimedSer;

        public BrandsController(IBrandService brandSer, IPromotionService proSer, IStoreService storeSer, 
            IAppliedPromotionService aplliedSer, IClaimedVoucherService claimedSer)
        {
            _brandSer = brandSer;
            _proSer = proSer;
            _storeSer = storeSer;
            _appliedSer = aplliedSer;
            _claimedSer = claimedSer;
        }

        #region Brand APIs
        [HttpGet]
        public ActionResult<IEnumerable<Brand>> GetBrands([FromQuery] string name, [FromQuery] string address, [FromQuery] string ids,
            [FromQuery(Name = "start-id")] int startId)
        {
            string query = ControllerContext.HttpContext.Request.QueryString.Value;
            IEnumerable<Brand> list = _brandSer.GetAll(query, name, address, ids,startId);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("count")]
        public ActionResult<int> GetStoresCount()
        {
            return _brandSer.GetCount();
        }

        [HttpGet("{id}")]
        public ActionResult<Brand> GetBrand(int id)
        {
            var entity = _brandSer.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public ActionResult<Brand> PostBrand(BrandCreateRequest entity)
        {
            bool success = _brandSer.Create(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Create failed!");
        }

        [HttpPut("{id}")]
        public ActionResult PutBrand(int id, BrandUpdateRequest entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            bool success = _brandSer.Update(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Update failed!");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBrand(int id)
        {
            bool success = _brandSer.Delete(id);
            if (success)
            {
                return Ok("Delete successful!");
            }
            return Problem("Delete failed!");
        }
        #endregion
    }
}
