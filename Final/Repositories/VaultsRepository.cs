using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using System.Data;
using Dapper;

namespace Final.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal Vault FindExisting(Vault newVault)
        {
            string sql = "SELECT * FROM vaults WHERE vaultId = @VaultId AND creatorId = @CreatorId";
            return _db.Query<Vault>(sql, newVault).FirstOrDefault();

        }

        internal List<Vault> Get()
        {
            string sql = @"
            SELECT 
            a.*,
            v.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId";

            return _db.Query<Account, Vault, Vault>(sql, (a, vault) =>
            {
                vault.Creator = a;
                return vault;
            }).ToList();
        }
        internal Vault Get(int id)
        {
            string sql = @"
            SELECT 
            a.*,
            v.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.id = @id";

            return _db.Query<Account, Vault, Vault>(sql, (a, vault) =>
            {
                vault.Creator = a;
                return vault;
            }, new { id }).FirstOrDefault();
        }


        internal Vault Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, creatorId, isPrivate)
            VALUES
            (@Name, @Description, @CreatorId, @IsPrivate);
            SELECT LAST_INSERT_ID()";

            newVault.Id = _db.ExecuteScalar<int>(sql, newVault);

            return newVault;
        }

        internal List<Vault> GetVaultsByUserId(string id)
        {
            string sql = @"
            SELECT 
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE a.id = @id";

            return _db.Query<Account, Vault, Vault>(sql, (a, vault) =>
            {
                vault.Creator = a;
                return vault;
            }).ToList();
        }

        internal void Edit(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET name = @Name, description = @Description, isPrivate = @IsPrivate
            WHERE id = @Id";
            _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM vaults
            WHERE id = @id";
            _db.Execute(sql, new { id });

        }

        internal List<VaultKeepModel> GetKeepsById(int id)
        {
            string sql = @"
            SELECT 
            a.*,
            v.*,
            k.id AS keepId,
            JOIN keeps k ON k.vaultId = v.id
            JOIN accounts a ON a.id = v.creatorId
            WHERE k.creatorId = @id";
            return _db.Query<Account, VaultKeepModel, VaultKeepModel>(sql, (a, vault) =>
            {
                vault.Creator = a;
                return vault;


            }, new { id }).ToList();
        }
    }
}