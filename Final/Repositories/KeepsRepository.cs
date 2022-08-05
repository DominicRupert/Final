using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using System.Data;
using Dapper;

namespace Final.Repositories
{
    public class KeepsRepository
    {

        private readonly IDbConnection _db;
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }


        internal List<Keep> Get()
        {
            string sql = @"
            SELECT 
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId";

            return _db.Query<Account, Keep, Keep>(sql, (a, keep) =>
            {
                keep.Creator = a;
                return keep;
            }).ToList();
        }

        internal Keep Get(int id)
        {
            string sql = @"
               update keeps k
               set
               k.views = k.views + 1
               where k.id = @Id;
            SELECT 
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = @id";

            return _db.Query<Account, Keep, Keep>(sql, (a, keep) =>
            {
                keep.Creator = a;
                return keep;
            }, new { id }).FirstOrDefault();
        }

        internal Keep Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId)
            VALUES
            (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;
            return newKeep;

        }

        internal List<Keep> GetByCreatorId(string creatorId)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.creatorId = @creatorId";


            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }, new { creatorId }).ToList<Keep>();

        }

        internal void Edit(Keep keepData)
        {
            string sql = @"
            UPDATE keeps
            SET name = @Name, description = @Description, img = @Img, views = @Views, kept = @Kept
            
           WHERE id = @Id";
            _db.Execute(sql, keepData);

        }

        internal List<Keep> GetMyKeeps(string id)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE creatorId = @id";
            return _db.Query<Account, Keep, Keep>(sql, (a, keep) =>
            {
                keep.Creator = a;
                return keep;
            }).ToList();
        }

        internal List<Keep> GetKeepsByVaultId(int id)
        {
            string sql = @"
            select
            k.*,
            a.*
            from keeps k
            join accounts a on k.creatorId = a.id
            where k.id = @id";
            return _db.Query<Keep, Account, Keep>(sql, (keep, a) =>
            {
                keep.Creator = a;
                return keep;
            }).ToList();
        }

        // internal List<VaultKeepModel> GetMyVaults(string id)
        // {
        //     string sql = @"
        //     SELECT 
        //     a.*
        //     v.*,
        //     k.id AS keepId,
        //     FROM keeps k
        //     JOIN vaults v ON v.id = k.vaultId
        //     JOIN accounts a ON a.id = v.creatorId
        //     WHERE k.profileId = @id";
        //     return _db.Query<Account, VaultKeepModel, VaultKeepModel>(sql, (a, vkkmv) =>
        //     {
        //         vkkmv.Creator = a;
        //         return vkkmv;
        //     }, new { id }).ToList();


        // }
        internal List<Vault> GetMyVaults(string creatorId)
        {
            string sql = @"
            SELECT 
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE a.id = @creatorId";
            return _db.Query<Vault, Account, Vault>(sql, (vault, a) =>
            {
                vault.Creator = a;
                return vault;
            }, new { creatorId }).ToList();
        }

        // internal List<VaultKeepModel> GetByVaultId(int id)
        // {
        //     string sql = @"
        //     SELECT
        //     a.*,
        //     k.*

        //     FROM keeps k
        //     JOIN accounts a ON k.creatorId = a.id
        //     WHERE k.vaultId = @id";

        //     return _db.Query<VaultKeepModel>(sql, new { id }).ToList();
        // }

        public void Delete(int id)
        {
            string sql = @"
            DELETE FROM keeps
            WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}