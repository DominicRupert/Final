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
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;
        public VaultKeepsController(VaultKeepsService vks, VaultsService vs, KeepsService ks)
        {
            _vks = vks;
            _vs = vs;
            _ks = ks;
        }

     

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetById(newVaultKeep.VaultId);
                if (vault.CreatorId != userInfo.Id)
                {
                    throw new Exception("You can only add keeps to vaults that you created.");
                }
                newVaultKeep.CreatorId = userInfo.Id;
                VaultKeep vaultKeep = _vks.Create(newVaultKeep);
                return Ok(newVaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Delete(string id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
               
                _vks.Delete(id, userInfo.Id);
                return Ok("deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        
    }
}