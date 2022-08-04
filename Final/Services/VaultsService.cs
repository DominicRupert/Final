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
        // private Vault Validate(int id, string userId)
        // {
        //     Vault found = Get(id);

        //     if (found.CreatorId != userId && found.IsPrivate == true)
        //     {
        //         throw new Exception("Vault not found.");
        //     }
          
        //     return found;
        // }
        public List<Vault> GetByProfile(string id, string userId)
       
        {
            List<Vault> found = _repo.GetMyVaults(id);
           return found.FindAll(v=> v.IsPrivate==false || v.CreatorId==userId);
           
        }

        internal List<Vault> GetByUser(string userId)
        {
            List<Vault> vaults = _repo.GetByUser(userId);
            
            return vaults;

            

        }

        internal Vault GetById(int id)
        {
            Vault vaults = _repo.GetById(id);
            if (vaults == null)
            {
                throw new Exception("Vault not found.");
            }
            
         

            return vaults;
        }





        internal Vault Create(Vault newVault)
        {
            // Vault found = _repo.FindExisting(newVault);
            // if (found != null)
            // {
            //     throw new Exception("Vault already exists.");
            // }
            return _repo.Create(newVault);
        }

        internal List<Vault> GetVaultsByUserId(string userId)
        {
            List<Vault> vaults = _repo.GetVaultsByUserId(userId);
            return vaults;
        }

        internal Vault Edit(Vault vaultData)
        {
            Vault original = GetById(vaultData.Id);
            if (original.CreatorId != vaultData.CreatorId)
            {
                throw new Exception("You can only edit vaults that you created.");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            original.IsPrivate = vaultData.IsPrivate;
            _repo.Edit(original);
            return original;
        }

        // internal List<Vault> GetVaults(string id)
        // {
        //     List<Vault> vaults = _repo.Get();
        //     return vaults;
        // }

        internal void Delete(int id, string userId)
        {
            Vault original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You can only delete vaults that you created.");
            }

            _repo.Delete(id);
        }

        internal List<Vault> GetMyVaults(string userId)
        {
            return _repo.GetMyVaults(userId);
          
        }



    }
}
