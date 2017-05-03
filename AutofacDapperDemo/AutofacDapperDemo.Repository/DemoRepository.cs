using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace AutofacDapperDemo.Repository
{
    public class DemoRepository<TEntity, TKey> : IDemoRepository<TEntity, TKey>
    {
        private readonly DapperRepository _dapperRepository;

        public DemoRepository(DapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public TEntity Get(TKey id)
        {
            return _dapperRepository.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetList()
        {
            return _dapperRepository.GetList<TEntity>();
        }

        public IEnumerable<TEntity> GetList(object conditions)
        {
            return _dapperRepository.GetList<TEntity>(conditions);
        }

        public IEnumerable<TEntity> GetList(string conditions, object parameters = null)
        {
            return _dapperRepository.GetList<TEntity>(conditions, parameters);
        }

        public IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object parameters = null)
        {
            return _dapperRepository.GetListPaged<TEntity>(pageNumber, rowsPerPage, conditions, orderby, parameters);
        }

        public TKey Insert(TEntity entity)
        {
            return _dapperRepository.Insert<TKey>(entity);
        }

        public int Update(TEntity entity)
        {
            return _dapperRepository.Update<TEntity>(entity);
        }

        public int Delete(TKey id)
        {
            return _dapperRepository.Delete<TEntity>(id);
        }

        public int DeleteList(object conditions)
        {
            return _dapperRepository.DeleteList<TEntity>(conditions);
        }

        public int DeleteList(string conditions, object parameters = null)
        {
            return _dapperRepository.DeleteList<TEntity>(conditions, parameters);
        }

        public int RecordCount(string conditions = "", object parameters = null)
        {
            return _dapperRepository.RecordCount<TEntity>(conditions, parameters);
        }

        public TEntity GetFirst(object conditions)
        {
            return _dapperRepository.GetFirst<TEntity>(conditions);
        }

        public TEntity GetFirstOrDefault(object conditions)
        {
            return _dapperRepository.GetFirstOrDefault<TEntity>(conditions);
        }

        public TEntity GetSingle(object conditions)
        {
            return _dapperRepository.GetSingle<TEntity>(conditions);
        }

        public TEntity GetSingleOrDefault(object conditions)
        {
            return _dapperRepository.GetSingleOrDefault<TEntity>(conditions);
        }

        public IEnumerable<TEntity> GetRecursiveList(string tableName, string parentId, string idKey, string parentKey, string conditions = null, string orderby = null, string classId = null, string classKey = null, string levelId = null, string levelKey = null, bool isReverse = false)
        {
            var p = new DynamicParameters();
            p.Add("@TableName", tableName);
            p.Add("@ClassId", classId);
            p.Add("@ClassKey", classKey);
            p.Add("@LevelId", levelId);
            p.Add("@LevelKey", levelKey);
            p.Add("@ParentId", parentId);
            p.Add("@IdKey", idKey);
            p.Add("@ParentKey", parentKey);
            p.Add("@Conditions", conditions);
            p.Add("@Orderby", orderby);
            p.Add("@IsReverse", isReverse);

            var query = _dapperRepository.Query<TEntity>("sp_table_recursive", p, CommandType.StoredProcedure);

            return query;
        }

        public Task<IEnumerable<TEntity>> GetRecursiveListAsync(string tableName, string parentId, string idKey, string parentKey, string conditions = null, string orderby = null, string classId = null, string classKey = null, string levelId = null, string levelKey = null, bool isReverse = false)
        {
            var p = new DynamicParameters();
            p.Add("@TableName", tableName);
            p.Add("@ClassId", classId);
            p.Add("@ClassKey", classKey);
            p.Add("@LevelId", levelId);
            p.Add("@LevelKey", levelKey);
            p.Add("@ParentId", parentId);
            p.Add("@IdKey", idKey);
            p.Add("@ParentKey", parentKey);
            p.Add("@Conditions", conditions);
            p.Add("@Orderby", orderby);
            p.Add("@IsReverse", isReverse);

            var query = _dapperRepository.QueryAsync<TEntity>("sp_table_recursive", p, CommandType.StoredProcedure);

            return query;
        }


        public String GeneratePrimaryKey(string tableName, string fieldName, string paramRule, string tenantId, string tenantFieldName)
        {
            string query = _dapperRepository.GeneratePrimaryKey<string>(tableName, fieldName, paramRule, tenantId, tenantFieldName);
            return query;
        }

        public Task<TEntity> GetAsync(TKey id)
        {
            return _dapperRepository.GetAsync<TEntity>(id);
        }

        public Task<IEnumerable<TEntity>> GetListAsync()
        {
            return _dapperRepository.GetListAsync<TEntity>();
        }

        public Task<IEnumerable<TEntity>> GetListAsync(object conditions)
        {
            return _dapperRepository.GetListAsync<TEntity>(conditions);
        }

        public Task<IEnumerable<TEntity>> GetListAsync(string conditions, object parameters = null)
        {
            return _dapperRepository.GetListAsync<TEntity>(conditions, parameters);
        }

        public Task<IEnumerable<TEntity>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object parameters = null)
        {
            return _dapperRepository.GetListPagedAsync<TEntity>(pageNumber, rowsPerPage, conditions, orderby, parameters);
        }

        public Task<TKey> InsertAsync(TEntity entity)
        {
            return _dapperRepository.InsertAsync<TKey>(entity);
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            return _dapperRepository.UpdateAsync<TEntity>(entity);
        }

        public Task<int> DeleteAsync(TKey id)
        {
            return _dapperRepository.DeleteAsync<TEntity>(id);
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            return _dapperRepository.DeleteAsync<TEntity>(entity);
        }

        public Task<int> DeleteListAsync(object conditions)
        {
            return _dapperRepository.DeleteListAsync<TEntity>(conditions);
        }

        public Task<int> DeleteListAsync(string conditions, object parameters = null)
        {
            return _dapperRepository.DeleteListAsync<TEntity>(conditions, parameters);
        }

        public Task<int> RecordCountAsync(string conditions = "", object parameters = null)
        {
            return _dapperRepository.RecordCountAsync<TEntity>(conditions, parameters);
        }

        public Task<TEntity> GetFirstAsync(object conditions)
        {
            return _dapperRepository.GetFirstAsync<TEntity>(conditions);
        }

        public Task<TEntity> GetFirstOrDefaultAsync(object conditions)
        {
            return _dapperRepository.GetFirstOrDefaultAsync<TEntity>(conditions);
        }

        public Task<TEntity> GetSingleAsync(object conditions)
        {
            return _dapperRepository.GetSingleAsync<TEntity>(conditions);
        }

        public Task<TEntity> GetSingleOrDefaultAsync(object conditions)
        {
            return _dapperRepository.GetSingleOrDefaultAsync<TEntity>(conditions);
        }

        public void EnableOUFilter()
        {
            _dapperRepository.EnableOUFilter();
        }

        public void DisableOUFilter()
        {
            _dapperRepository.DisableOUFilter();
        }
    }
}
