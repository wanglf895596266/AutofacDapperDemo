using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutofacDapperDemo.Repository
{
    public interface IDemoRepository<TEntity, TKey> 
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetList();
        IEnumerable<TEntity> GetList(object conditions);
        IEnumerable<TEntity> GetList(string conditions, object parameters = null);
        IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object parameters = null);
        TKey Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(TKey id);
        int DeleteList(object conditions);
        int DeleteList(string conditions, object parameters = null);
        int RecordCount(string conditions = "", object parameters = null);
        TEntity GetFirst(object conditions);
        TEntity GetFirstOrDefault(object conditions);
        TEntity GetSingle(object conditions);
        TEntity GetSingleOrDefault(object conditions);
        Task<IEnumerable<TEntity>> GetRecursiveListAsync(string tableName, string parentId, string idKey, string parentKey, string conditions = null, string orderby = null, string classId = null, string classKey = null, string levelId = null, string levelKey = null, bool isReverse = false);
        IEnumerable<TEntity> GetRecursiveList(string tableName, string parentId, string idKey, string parentKey, string conditions = null, string orderby = null, string classId = null, string classKey = null, string levelId = null, string levelKey = null, bool isReverse = false);
        string GeneratePrimaryKey(string tableName, string fieldName, string paramRule, string tenantId, string tenantFieldName);
        Task<TEntity> GetAsync(TKey id);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<IEnumerable<TEntity>> GetListAsync(object conditions);
        Task<IEnumerable<TEntity>> GetListAsync(string conditions, object parameters = null);
        Task<IEnumerable<TEntity>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object parameters = null);
        Task<TKey> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TKey id);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteListAsync(object conditions);
        Task<int> DeleteListAsync(string conditions, object parameters = null);
        Task<int> RecordCountAsync(string conditions = "", object parameters = null);
        Task<TEntity> GetFirstAsync(object conditions);
        Task<TEntity> GetFirstOrDefaultAsync(object conditions);
        Task<TEntity> GetSingleAsync(object conditions);
        Task<TEntity> GetSingleOrDefaultAsync(object conditions);

        void EnableOUFilter();
        void DisableOUFilter();
    }
}
