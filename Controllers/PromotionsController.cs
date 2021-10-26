using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;
using TrackingVoucher_v02.Services;

namespace TrackingVoucher_v02.Controllers
{
    [Route("api/v1/promotions")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _proSer;
        private readonly IAppliedPromotionService _applySer;
        private readonly IClaimedVoucherService _claimedSer;
        private readonly IVoucherService _vouSer;

        public PromotionsController(IPromotionService proSer, IAppliedPromotionService applySer,
            IClaimedVoucherService claimedSer, IVoucherService vouSer)
        {
            _proSer = proSer;
            _applySer = applySer;
            _claimedSer = claimedSer;
            _vouSer = vouSer;
        }
        #region Promotion APIs
        // GET: api/Promotions
        [HttpGet]
        public ActionResult<IEnumerable<Promotion>> GetPromotions(
            [FromQuery(Name = "brand-id")] int brandId, [FromQuery(Name = "begin-date")] DateTime beginDate,
            [FromQuery(Name = "expired-date")] DateTime expiredDate, [FromQuery] string ids,
            [FromQuery(Name = "start-id")] int startId)
        {
            string query = ControllerContext.HttpContext.Request.QueryString.Value;
            IEnumerable<Promotion> list = _proSer.GetAll(query, brandId, ids,
            startId, beginDate, expiredDate);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("count")]
        public ActionResult<int> GetStoresCount()
        {
            return _proSer.GetCount();
        }

        [HttpGet("{id}")]
        public ActionResult<Promotion> GetPromotion(int id)
        {
            var entity = _proSer.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public ActionResult<Promotion> PostPromotion(PromotionCreateRequest entity)
        {
            bool success = _proSer.Create(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Create failed!");
        }

        [HttpPut("{id}")]
        public ActionResult PutPromotion(int id, PromotionUpdateRequest entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            bool success = _proSer.Update(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Update failed!");
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePromotion(int id)
        {
            bool success = _applySer.DeleteByPromotionId(id);
            if (!success)
            {
                return Problem("Delete failed!");
            }
            success = _vouSer.DeleteByPromotionId(id);
            if (!success)
            {
                return Problem("Delete failed!");
            }
            success = _proSer.Delete(id);
            if (!success)
            {
                return Problem("Delete failed!");
            }
            return Ok("Delete successful!");
            
        }
        #endregion

        #region AppliedPromotions APIs
        // GET: api/AppliedPromotions
        [HttpGet("applied-promotions")]
        public ActionResult<IEnumerable<AppliedPromotion>> GetAppliedPromotions(
            [FromQuery(Name = "store-id")] int storeId, [FromQuery(Name = "promotion-id")] int promotionId,
            [FromQuery] string ids, [FromQuery(Name = "start-id")] int startId)
        {
            string query = ControllerContext.HttpContext.Request.QueryString.Value;
            IEnumerable<AppliedPromotion> list = _applySer.GetAll(query, storeId, promotionId, ids, startId);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("applied-promotions/count")]
        public ActionResult<int> GetAppliedPromotionCount()
        {
            return _applySer.GetCount();
        }

        [HttpGet("applied-promotions/{id}")]
        public ActionResult<AppliedPromotion> GetAppliedPromotion(int id)
        {
            var entity = _applySer.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost("applied-promotions")]
        public ActionResult<AppliedPromotion> PostAppliedPromotion(AppliedPromotionCreateRequest entity)
        {
            bool success = _applySer.Create(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Create failed!");
        }

        [HttpDelete("applied-promotions/{id}")]
        public ActionResult DeleteAppliedPromotion(int id)
        {
            bool success = _applySer.Delete(id);
            if (success)
            {
                return Ok("Delete successful!");
            }
            return Problem("Delete failed!");
        }
        #endregion
    }
}

