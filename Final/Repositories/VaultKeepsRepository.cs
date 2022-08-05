using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Dapper;
using System.Data;

namespace Final.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            string sql = @"
            update keeps k
            set
            k.kept = k.kept + 1
            where k.id = @KeepId;
            INSERT INTO vaultkeeps
            (vaultId, keepId, creatorId)
            VALUES
            (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }

        internal VaultKeep Get(int id)
        {
            string sql = @"
            Select
            vk.*,
            v.*
            from vaultkeeps vk
            join vaults v on v.id = vk.vaultId
            where vk.id = @id";
            
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal object Delete(int id)
        {
            string sql = @"
            DELETE FROM vaultkeeps
            WHERE id = @id";
            return _db.Execute(sql, new { id });

    }

    internal VaultKeep GetById(string id)
    {
        string sql = @"
        SELECT
        *
        FROM vaultkeeps
        WHERE id = @id";
        return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
    internal List<VaultKeepModel> GetVaultKeeps(int vaultId)
    {
        string sql = @"
        SELECT 
        k.*,
        vk.id AS vaultKeepId,
        a.*
        FROM vaultkeeps vk
        JOIN keeps k ON vk.keepId = k.id
        JOIN accounts a ON k.creatorId = a.id 
        WHERE vk.vaultId = @vaultId";
        return _db.Query<VaultKeepModel, Account, VaultKeepModel>(sql, (vkool, a) =>
        {
            vkool.Creator = a;
           
            return vkool;
        }, new { vaultId }).ToList();
        
    }

}}