using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Final.Repositories;
using Microsoft.AspNetCore.Mvc;
using Final.Services;
using CodeWorks.Auth0Provider;

namespace Final.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProfilesController : ControllerBase

    
    {

        private readonly AccountService _as;
        private readonly VaultKeepsService _vks;
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;
        public ProfilesController(AccountService aserv, VaultKeepsService vks, VaultsService vs, KeepsService ks)
        {
            _as = aserv;
            _vks = vks;
            _vs = vs;
            _ks = ks;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> getById(string id)
        {
            try
            {
                Profile profile = _as.GetByProfileId(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> getKeepsById(string id)
        {
            try
            {
                _as.GetByProfileId(id);
                List<Keep> keeps = _ks.GetKeepsByUserId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> getVaults(string id)
        {
            try
            {
                List<Vault> vaults = _vs.GetVaultsByUserId(id);
                Account account = await HttpContext.GetUserInfoAsync<Account>();
                List <Vault> privVaults = vaults.Where((vault) =>
                {
                    if(account != null)
                    {
                        if(vault.CreatorId == account.Id)
                        {
                            return true;
                        }
                        return !vault.IsPrivate;
                    }
                    return !vault.IsPrivate;
                }
                ).ToList<Vault>();
                return Ok(privVaults);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        

        
        
    }
}