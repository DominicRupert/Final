using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Final.Services;
using CodeWorks.Auth0Provider;
using Final.Repositories;

namespace Final.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal List<Keep> Get()
        {
            return _repo.Get();
        }

        internal Keep Get(int id)
        {
            return _repo.Get(id);
        }

        internal Keep Create(Keep newKeep)
        {
            return _repo.Create(newKeep);
        }

        internal Keep Delete(int id, string userId)
        {
            Keep original = _repo.Get(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You can only delete keeps that you created.");
            }
            return _repo.Delete(id);


            }
       
        
    }
}