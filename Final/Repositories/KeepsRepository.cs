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

            return _db.Query<Profile, Keep, Keep>(sql, (prof, keep)=>
            {
                keep.Creator = prof;
                return keep;
            }).ToList();
        }

        internal Keep Get(int id)
        {
               string sql = @"
               update keeps k
                set views = views + 1
                where k.id = @id
            SELECT 
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = @id";

            return _db.Query<Profile, Keep, Keep>(sql, (prof, keep)=>
            {
                keep.Creator = prof;
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

        internal List<Keep> GetKeepsByUserId(string id)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE creatorId = @id";
            

            return _db.Query<Profile, Keep, Keep>(sql, (prof, keep)=>
            {
                keep.Creator = prof;
                return keep;
            }).ToList();

        }

        internal void Edit(Keep keepData)
        {
            string sql = @"
            UPDATE keeps
            SET name = @Name, description = @Description, img = @Img, views = @Views, kept = @Kept
            
           WHERE id = @Id";
            _db.Execute(sql, keepData);
        
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
            return _db.Query<Keep, Profile, Keep>(sql, (keep, prof)=>
            {
                keep.Creator = prof;
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
        internal List <Vault> GetMyVaults(string id)
        {
            string sql = @"
            SELECT 
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE a.id = @id";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, prof)=>
            {
                vault.Creator = prof;
                return vault;
            }).ToList();
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