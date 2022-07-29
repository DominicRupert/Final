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

        internal Keep Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}