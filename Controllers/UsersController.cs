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
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _ser;

        public UsersController(IUserService ser)
        {
            _ser = ser;
        }

        #region User APIs

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers([FromQuery] string name, [FromQuery] string address, [FromQuery] string ids,
            [FromQuery(Name = "start-id")] int startId)
        {
            string query = ControllerContext.HttpContext.Request.QueryString.Value;
            IEnumerable<User> list = _ser.GetAll(query, name, address, ids, startId);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("count")]
        public ActionResult<int> GetUsersCount()
        {
            return _ser.GetCount();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var entity = _ser.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public ActionResult PutUser(int id, UserUpdateRequest entity)
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

        [HttpPost]
        public ActionResult<User> PostUser(UserCreateRequest entity)
        {
            bool success = _ser.Create(entity);
            if (success)
            {
                return Ok(entity);
            }
            return Problem("Create failed!");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
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
