using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;
using TrackingVoucher_v02.Services;

namespace TrackingVoucher_v02.Controllers
{
    [Route("api/v1/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _ser;

        public StoresController(IStoreService ser)
        {
            _ser = ser;
        }

        #region Store APIs

        [HttpGet]
        public ActionResult<IEnumerable<Store>> GetStores(
            [FromQuery(Name = "brand-id")] int brandId, [FromQuery] string name,
            [FromQuery] string address, [FromQuery] string ids, [FromQuery(Name = "start-id")] int startId)
        {

            string query = ControllerContext.HttpContext.Request.QueryString.Value;
            IEnumerable<Store> list = _ser.GetAll(query, brandId, name, address, ids, startId);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("count")]
        public ActionResult<int> GetStoresCount()
        {
            return _ser.GetCount();
        }

        [HttpGet("{id}")]
        public ActionResult<Store> GetStore(int id)
        {
            var entity = _ser.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public ActionResult<Store> PostStore(StoreCreateRequest entity)
        {
            bool success = _ser.Create(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Create failed!");
        }

        [HttpPut("{id}")]
        public ActionResult PutStore(int id, StoreUpdateRequest entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            bool success = _ser.Update(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Update failed!");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStore(int id)
        {
            bool success = _ser.Delete(id);
            if (success)
            {
                return Ok("Delete successful!");
            }
            return Problem("Delete failed!");
        }
        #endregion
    }
}
