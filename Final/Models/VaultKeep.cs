using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Interfaces;

namespace Final.Models
{
    public class VaultKeep : IRepoItem<int>
    {
        public int Id { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set; }
        public string CreatorId { get; set; }

    }

}