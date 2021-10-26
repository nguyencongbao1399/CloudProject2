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
    [Route("api/v1/vouchers")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IVoucherService _voucherSer;
        private readonly IClaimedVoucherService _claimedSer;

        public VouchersController(IVoucherService voucherSer, IClaimedVoucherService claimedSer)
        {
            _voucherSer = voucherSer;
            _claimedSer = claimedSer;
        }

        #region Voucher APIs

        // GET: api/Vouchers
        [HttpGet]
        public ActionResult<IEnumerable<Voucher>> GetVouchers(
            [FromQuery(Name = "promotion-id")] int promotionId, [FromQuery(Name = "begin-date")] DateTime beginDate,
            [FromQuery(Name = "expired-date")] DateTime expiredDate, [FromQuery] string ids,
            [FromQuery(Name = "start-id")] int startId)
        {
            string query = ControllerContext.HttpContext.Request.QueryString.Value;
            IEnumerable<Voucher> list = _voucherSer.GetAll(query, promotionId, ids,
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
            return _voucherSer.GetCount();
        }

        [HttpGet("{id}")]
        public ActionResult<Voucher> GetVoucher(int id)
        {
            var entity = _voucherSer.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public ActionResult PutVoucher(int id, VoucherUpdateRequest entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            bool success = _voucherSer.Update(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Update failed!");
        }

        [HttpPost]
        public ActionResult<Voucher> PostVoucher(VoucherCreateRequest entity)
        {
            bool success = _voucherSer.Create(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Create failed!");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteVoucher(int id)
        {
            bool success = _voucherSer.Delete(id);
            if (success)
            {
                return Ok("Delete successful!");
            }
            return Problem("Delete failed!");
        }

        #endregion

        #region ClaimedVoucher APIs
        [HttpGet("claimed-vouchers")]
        public ActionResult<IEnumerable<ClaimedVoucher>> GetClaimedVouchers(
            [FromQuery] string ids, [FromQuery(Name = "start-id")] int startId,
            [FromQuery(Name = "user-id")] int userId,
            [FromQuery(Name = "voucher-id")] int voucherId, [FromQuery(Name = "claimed-date")] DateTime claimedDate,
            [FromQuery(Name = "expired-date")] DateTime expiredDate)
        {
            string query = ControllerContext.HttpContext.Request.QueryString.Value;
            IEnumerable<ClaimedVoucher> list = _claimedSer.GetAll(query, userId, voucherId, ids,
            startId, claimedDate, expiredDate);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("claimed-vouchers/count")]
        public ActionResult<int> GetClaimedVoucherCount()
        {
            return _claimedSer.GetCount();
        }

        [HttpGet("claimed-vouchers/{id}")]
        public ActionResult<ClaimedVoucher> GetClaimedVoucher(int id)
        {
            var entity = _claimedSer.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("claimed-vouchers/{id}")]
        public ActionResult PutClaimedVoucher(int id, ClaimedVoucherUpdateRequest entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            bool success = _claimedSer.Update(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Update failed!");
        }

        [HttpPost("claimed-vouchers")]
        public ActionResult<ClaimedVoucher> PostClaimedVoucher(ClaimedVoucherCreateRequest entity)
        {
            bool success = _claimedSer.Create(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Create failed!");
        }

        // DELETE: api/ClaimedVouchers/5
        [HttpDelete("claimed-vouchers/{id}")]
        public ActionResult DeleteClaimedVoucher(int id)
        {
            bool success = _claimedSer.Delete(id);
            if (success)
            {
                return Ok("Delete successful!");
            }
            return Problem("Delete failed!");
        }
        #endregion
    }
}
