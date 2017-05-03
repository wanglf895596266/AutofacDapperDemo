using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace AutofacDapperDemo.Repository
{
    public partial class DapperRepository
    {
        #region Base

        /// <summary>
        /// Executes a query, returning the data typed as per T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = conn.Query<T>(sql, param, null, true, null, commandType);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// Maps a query to objects
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id", object param = null, CommandType? commandType = default(CommandType))
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<TReturn> ret = conn.Query(sql, map, param, null, true, splitOn, null, commandType);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryMultiple<T>(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var multi = conn.QueryMultiple(sql, param, null, null, commandType);
                var ret = multi.Read<T>();
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null)
        {
            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = conn.Execute(sql, param);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Execute a query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = await conn.QueryAsync<T>(sql, param, null, null, commandType);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// Maps a query to objects
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id", object param = null, CommandType? commandType = default(CommandType))
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<TReturn> ret = await conn.QueryAsync(sql, map, param, null, true, splitOn, null, commandType);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// Execute a command asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await conn.ExecuteAsync(sql, param);
                conn.Close();
            }
            return ret;
        }

        #endregion

        #region generate id

        public string GeneratePrimaryKey<T>(string tableName, string fieldName, string paramRule, string tenantId, string tenantFieldName)
        {
            string keyId = "";
            var p = new DynamicParameters();
            p.Add("@TableName", tableName);
            p.Add("@FieldName", fieldName);
            p.Add("@ParamRule", string.IsNullOrWhiteSpace(paramRule) ? null : paramRule);
            p.Add("@TenantId", tenantId);
            p.Add("@TenantFieldName", tenantFieldName);
            p.Add("@KeyId",dbType:DbType.String, direction: ParameterDirection.Output,size:50);
            //p.Add("@p6", dbType: DbType.String, direction: ParameterDirection.ReturnValue, size: 50);

            var query = Query<T>("sp_generateprimarykey", p, CommandType.StoredProcedure);
            keyId = p.Get<string>("@KeyId");
            return keyId;
        }         
        #endregion

        #region Paging

        public IEnumerable<T> GetListPaged<T>(string sql, string orderby, int pageNumber, int rowsPerPage, out int rowsCount)
        {
            rowsCount = 0;
            var p = new DynamicParameters();
            p.Add("@sql", sql);
            p.Add("@orderby", orderby);
            p.Add("@page_number", pageNumber);
            p.Add("@page_size", rowsPerPage);
            p.Add("@page_count", DbType.Int32, direction: ParameterDirection.Output);

            var query = Query<T>("sp_pagination", p, CommandType.StoredProcedure);
            rowsCount = p.Get<int>("@page_count");

            return query;
        }

        public IEnumerable<TReturn> GetListPaged<TFirst, TSecond, TReturn>(string sql, string orderby, int pageNumber, int rowsPerPage, out int rowsCount,
            Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
        {
            rowsCount = 0;
            var p = new DynamicParameters();
            p.Add("@sql", sql);
            p.Add("@orderby", orderby);
            p.Add("@page_number", pageNumber);
            p.Add("@page_size", rowsPerPage);
            p.Add("@page_count", DbType.Int32, direction: ParameterDirection.Output);

            var query = Query("sp_pagination", map, splitOn, p, CommandType.StoredProcedure);
            rowsCount = p.Get<int>("@page_count");

            return query;
        }

        public Task<IEnumerable<T>> GetListPagedAsync<T>(string sql, string orderby, int pageNumber, int rowsPerPage, out int rowsCount)
        {
            rowsCount = 0;
            var p = new DynamicParameters();
            p.Add("@sql", sql);
            p.Add("@orderby", orderby);
            p.Add("@page_number", pageNumber);
            p.Add("@page_size", rowsPerPage);
            p.Add("@page_count", DbType.Int32, direction: ParameterDirection.Output);

            var query = QueryAsync<T>("sp_pagination", p, CommandType.StoredProcedure);
            rowsCount = p.Get<int>("@page_count");

            return query;
        }

        public Task<IEnumerable<TReturn>> GetListPagedAsync<TFirst, TSecond, TReturn>(string sql, string orderby, int pageNumber, int rowsPerPage, out int rowsCount,
            Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
        {
            rowsCount = 0;
            var p = new DynamicParameters();
            p.Add("@sql", sql);
            p.Add("@orderby", orderby);
            p.Add("@page_number", pageNumber);
            p.Add("@page_size", rowsPerPage);
            p.Add("@page_count", DbType.Int32, direction: ParameterDirection.Output);

            var query = QueryAsync("sp_pagination", map, splitOn, p, CommandType.StoredProcedure);
            rowsCount = p.Get<int>("@page_count");

            return query;
        }

        #endregion
    }
}
