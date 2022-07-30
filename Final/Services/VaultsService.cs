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
        private Vault Validate(int id, string userId)
        {
            Vault found = _repo.Get(id);
         
            if (found.CreatorId != userId)
            {
                throw new Exception("You can only edit vaults that you created.");
            }
            return found;
        }

        internal List<Vault> Get(string userId)
        {
            List<Vault> vaults = _repo.Get();
            return vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userId);
        }
    

        internal Vault Get(int id, string userId)
        {
            Vault found = Validate(id, userId);
            _repo.Get(id);
            return found;
        }
      

        internal Vault Create(Vault newVault)
        {
            return _repo.Create(newVault);
        }

        internal Vault Edit( Vault vaultData)
        {
            Vault original = Validate(vaultData.Id, vaultData.CreatorId);
            if (original.CreatorId != vaultData.CreatorId)
            {
                throw new Exception("You can only edit vaults that you created.");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;
            _repo.Edit(original);
            return original;
        }

        internal void Delete(int id, string userId)
        {
            Vault original = Validate(id, userId);
         
            _repo.Delete(id);
        }
    
    }
    }
