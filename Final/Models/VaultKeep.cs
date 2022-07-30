using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class VaultKeep
    {
        public int Id { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set; }
        public string CreatorId { get; set; }
        
    }
}