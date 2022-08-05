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
            VaultKeep original = _repo.Create(newVaultKeep);
         
            return newVaultKeep;
        }
        private static void Owner(string creatorId, string userId)
        {
            if (creatorId != userId)
            {
                throw new Exception("no");
            }
        }

      internal List<VaultKeepModel> GetVaultKeeps(int vaultId)
        {
            List<VaultKeepModel> keeps = _repo.GetVaultKeeps(vaultId);
            return keeps;
        }
        private VaultKeep Get(int id)
        {
            VaultKeep found = _repo.Get(id);
            if (found == null)
            {
                throw new Exception("VaultkEEP not found.");
            }
            return found;
        }
        
     

        internal void Delete(string id, string userId)
        {
            VaultKeep found = _repo.GetById(id);
            if(found.CreatorId != userId)
            {
                throw new Exception("You can't delete this VaultKeep");
            }
            _repo.Delete(found.Id);
        }
        

        
        
    }
}