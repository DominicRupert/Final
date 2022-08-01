using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Interfaces;

namespace Final.Models
{
    public class Vault : IRepoItem <int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CreatorId { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }


        public Account Creator { get; set; }
        
    }
}