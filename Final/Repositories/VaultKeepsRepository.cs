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
            SELECT * FROM vaultkeeps
            WHERE id = @id";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal object Delete(int id)
        {
            string sql = @"
            DELETE FROM vaultkeeps
            WHERE id = @id";
            return _db.Execute(sql, new { id });

    }
}}