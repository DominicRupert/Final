using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Final.Repositories;   

namespace Final.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal List<Vault> Get(string userId)
        {
            List<Vault> vaults = _repo.Get();
            return vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userId);
        }
    

        internal Vault Get(int id)
        {
            return _repo.Get(id);
        }

        internal Vault Create(Vault newVault)
        {
            throw new NotImplementedException();
        }

        internal Vault Edit(int id, Vault vaultData)
        {
            throw new NotImplementedException();
        }

        internal void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
    }
