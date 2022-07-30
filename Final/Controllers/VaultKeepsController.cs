using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Final.Services;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;

namespace Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;

        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVaultKeep.CreatorId = userInfo.Id;
                return Ok(_vks.Create(newVaultKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _vks.Delete(id, userInfo.Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        
    }
}