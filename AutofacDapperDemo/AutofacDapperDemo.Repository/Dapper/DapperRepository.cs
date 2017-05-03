using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace AutofacDapperDemo.Repository
{
    public partial class DapperRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private readonly SimpleCRUD _simpleCRUD;

        public DapperRepository(SimpleCRUD simpleCRUD)
        {
            _simpleCRUD = simpleCRUD;
        }

        #region Base

        /// <summary>
        /// By default queries the table matching the class name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(object id)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                T ret = _simpleCRUD.Get<T>(conn, id);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>()
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = _simpleCRUD.GetList<T>(conn);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(string conditions, object parameters = null)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = _simpleCRUD.GetList<T>(conn, conditions, parameters);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(object whereConditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = _simpleCRUD.GetList<T>(conn, whereConditions);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> GetListPaged<T>(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object parameters = null)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = _simpleCRUD.GetListPaged<T>(conn, pageNumber, rowsPerPage, conditions, orderby, parameters);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// Inserts a row into the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public TKey Insert<TKey>(object entityToInsert)
        {
            SetSimpleCRUD();

            TKey ret = default(TKey);
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = _simpleCRUD.Insert<TKey>(conn, entityToInsert);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Updates a record or records in the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public int Update<T>(object entityToUpdate)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = _simpleCRUD.Update(conn, entityToUpdate);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a record or records in the database that match the object passed in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToDelete"></param>
        /// <returns></returns>
        public int Delete<T>(T entityToDelete)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = _simpleCRUD.Delete<T>(conn, entityToDelete);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a record or records in the database by ID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete<T>(object id)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = _simpleCRUD.Delete<T>(conn, id);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a list of records in the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public int DeleteList<T>(object whereConditions)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = _simpleCRUD.DeleteList<T>(conn, whereConditions);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a list of records in the database.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int DeleteList<T>(string conditions, object parameters = null)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = _simpleCRUD.DeleteList<T>(conn, conditions, parameters);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int RecordCount<T>(string conditions = "", object parameters = null)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = _simpleCRUD.RecordCount<T>(conn, conditions, parameters);
                conn.Close();
            }
            return ret;
        }

        public T GetFirst<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = _simpleCRUD.GetFirst<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        public T GetFirstOrDefault<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = _simpleCRUD.GetFirstOrDefault<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        public T GetSingle<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = _simpleCRUD.GetSingle<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        public T GetSingleOrDefault<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = _simpleCRUD.GetSingleOrDefault<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        #endregion

        #region Async

        #region Extensions

        /// <summary>
        /// By default queries the table matching the class name asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(object id)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                T ret = await _simpleCRUD.GetAsync<T>(conn, id);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync<T>()
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = await _simpleCRUD.GetListAsync<T>(conn);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync<T>(string conditions, object parameters = null)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = await _simpleCRUD.GetListAsync<T>(conn, conditions, parameters);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync<T>(object whereConditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = await _simpleCRUD.GetListAsync<T>(conn, whereConditions);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListPagedAsync<T>(int pageNumber, int rowsPerPage, string conditions = null, string orderby = null, object parameters = null)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                IEnumerable<T> ret = await _simpleCRUD.GetListPagedAsync<T>(conn, pageNumber, rowsPerPage, conditions, orderby, parameters);
                conn.Close();
                return ret;
            }
        }

        /// <summary>
        /// Inserts a row into the database asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<TKey> InsertAsync<TKey>(object entityToInsert)
        {
            SetSimpleCRUD();

            TKey ret = default(TKey);
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await _simpleCRUD.InsertAsync<TKey>(conn, entityToInsert);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Updates a record or records in the database asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync<T>(object entityToUpdate)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await _simpleCRUD.UpdateAsync(conn, entityToUpdate);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a record or records in the database that match the object passed in asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToDelete"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(T entityToDelete)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await _simpleCRUD.DeleteAsync<T>(conn, entityToDelete);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a record or records in the database by ID asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(object id)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await _simpleCRUD.DeleteAsync<T>(conn, id);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a list of records in the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public async Task<int> DeleteListAsync<T>(object whereConditions)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await _simpleCRUD.DeleteListAsync<T>(conn, whereConditions);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Deletes a list of records in the database.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<int> DeleteListAsync<T>(string conditions, object parameters = null)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await _simpleCRUD.DeleteListAsync<T>(conn, conditions, parameters);
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// By default queries the table matching the class name.
        /// This uses your raw SQL so be careful to not create SQL injection holes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<int> RecordCountAsync<T>(string conditions = "", object parameters = null)
        {
            SetSimpleCRUD();

            int ret = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                ret = await _simpleCRUD.RecordCountAsync<T>(conn, conditions, parameters);
                conn.Close();
            }
            return ret;
        }

        public async Task<T> GetFirstAsync<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = await _simpleCRUD.GetFirstAsync<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        public async Task<T> GetFirstOrDefaultAsync<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = await _simpleCRUD.GetFirstOrDefaultAsync<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        public async Task<T> GetSingleAsync<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = await _simpleCRUD.GetSingleAsync<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        public async Task<T> GetSingleOrDefaultAsync<T>(object conditions)
        {
            SetSimpleCRUD();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var ret = await _simpleCRUD.GetSingleOrDefaultAsync<T>(conn, conditions);
                conn.Close();
                return ret;
            }
        }

        #endregion

        #endregion

        #region Custom

        public void EnableOUFilter()
        {
            _simpleCRUD.EnableOUFilter = true;
        }

        public void DisableOUFilter()
        {
            _simpleCRUD.EnableOUFilter = false;
        }

        public void SetSimpleCRUD()
        {
           
        }

        #endregion
    }
}
