using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Interfaces;


namespace Final.Models
{
    public class Keep : IRepoItem<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }

        public int Views { get; set; }
        public int Kept { get; set; }

        public Account Creator { get; set; }
        public string CreatorId { get; set; }

        
    }

    public class VaultKeepModel : Keep
    {
        public int VaultKeepId { get; set; }
    }
}