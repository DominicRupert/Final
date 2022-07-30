using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }
        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            return _repo.Create(newVaultKeep);
        }

        internal VaultKeep Get(int id, string userId)
        {
            VaultKeep found = _repo.Get(id);
            if (found.CreatorId != userId)
            {
                throw new Exception("You can only edit vaults that you created.");
            }
            return found;
        }
     

        internal void Delete(int id, string userId)
        {
            VaultKeep found = _repo.Get(id);
            if (found == null)
            {
                throw new Exception("Vault not found.");
            }
            if (found.CreatorId != userId)
            {
                throw new Exception("You can only delete vaultkeeps that you created.");
            }
             _repo.Delete(id);
        }
        
    }
}