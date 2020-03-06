/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：接口实现                                                    
*│　作    者：sinvie                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-03-06 14:57:12                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Sinvie.Cms.Repository.SqlServer                                  
*│　类    名： T_AdminUserRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Sinvie.Cms.Core.DbHelper;
using Sinvie.Cms.Core.Options;
using Sinvie.Cms.Core.Repository;
using Sinvie.Cms.IRepository;
using Sinvie.Cms.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Sinvie.Cms.Repository.SqlServer
{
    public class T_AdminUserRepository:BaseRepository<T_AdminUser,Int32>, IT_AdminUserRepository
    {
        public T_AdminUserRepository(IOptionsSnapshot<DbOption> options)
        {
            _dbOption =options.Get("CzarCms");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

		public int DeleteLogical(int[] ids)
        {
            string sql = "update T_AdminUser set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update T_AdminUser set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

    }
}