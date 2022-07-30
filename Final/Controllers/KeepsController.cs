using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Final.Models;
using Final.Services;
using CodeWorks.Auth0Provider;

namespace Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpGet]
        public ActionResult<List<Keep>> Get()
        {
            try
            {
                return Ok(_ks.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]

        public ActionResult<Keep> Get(int id)
        {
            try
            {
                
                return Ok(_ks.Get(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newKeep.CreatorId = userInfo.Id;
                newKeep.Creator = userInfo;
                Keep created = _ks.Create(newKeep);
                return Ok(newKeep);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep original = _ks.Get(id);
                if (original.CreatorId != userInfo.Id)
                {
                    throw new Exception("You can only edit keeps that you created.");
                }
                keepData.Id = id;
                keepData.CreatorId = userInfo.Id;
                keepData.Creator = userInfo;
                Keep edited = _ks.Edit(keepData);
                return Ok(edited);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

                Keep deleted = _ks.Delete(id, userInfo.Id);
                if (deleted.CreatorId != userInfo.Id)
                {
                    throw new Exception("You can only delete keeps that you created.");
                }
                return Ok(deleted);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}