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
            Keep found = _repo.Get(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return _repo.Get(id);
        }

        internal Keep Create(Keep newKeep)
        {
            return _repo.Create(newKeep);
        }


        internal Keep Edit(Keep keepData)
        {
            Keep original = _repo.Get(keepData.Id);
            if (original.CreatorId != keepData.CreatorId)
            {
                throw new Exception("You can only edit keeps that you created.");
            }
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;

            _repo.Edit(original);
            return original;
        }
        internal Keep Delete(int id, string userId)
        {
            Keep original = Get(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You can only delete keeps that you created.");
            }
             _repo.Delete(id);
            return original;


        }


    }
}