using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Final.Models;
using Final.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public AccountController(AccountService accountService, VaultKeepsService vaultKeepsService, VaultsService vaultsService, KeepsService keepsService)
        {
            _accountService = accountService;
            _vaultKeepsService = vaultKeepsService;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }
      
        

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("vaults")]
        [Authorize]
        public async Task<ActionResult<List<Vault>>> GetAccountVaults()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> vaults = _keepsService.GetMyVaults(userInfo.Id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}