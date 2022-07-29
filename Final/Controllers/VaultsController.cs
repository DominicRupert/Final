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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;

        private readonly KeepsService _ks;
        public VaultsController(VaultsService vs, KeepsService ks)
        {
            _vs = vs;
            _ks = ks;
        }
     
        [HttpGet]
        public async Task<ActionResult<List<Vault>>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> vaults = _vs.Get(userInfo?.Id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Vault> Get(int id)
        {
            try
            {
                Vault vault = _vs.Get(id);
              
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVault.CreatorId = userInfo.Id;
                newVault.Creator = userInfo;
                Vault vault = _vs.Create(newVault);
                return Ok(newVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                vaultData.Creator = userInfo;
                Vault vault = _vs.Edit(id, vaultData);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.Get(id);
                if (vault.CreatorId != userInfo.Id)
                {
                    return BadRequest("You do not have permission to delete this vault");
                }
                _vs.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}