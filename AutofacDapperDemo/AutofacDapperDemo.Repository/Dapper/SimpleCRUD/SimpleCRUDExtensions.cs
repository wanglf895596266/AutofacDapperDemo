using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

namespace Dapper
{
    public partial class SimpleCRUD
    {
        #region Global condition extensions

        public int? TenantId { get; set; }
        public int[] DataPermissions { get; set; }

        public bool EnableOUFilter = true;

        private void BuildAttributeConditions(StringBuilder sb, object entity, DynamicParameters parameters = null)
        {
            var sourceProperties = GetScaffoldableProperties(entity).ToArray();
            for (var i = 0; i < sourceProperties.Count(); i++)
            {
                #region Tenant filter 

                if (sourceProperties.ElementAt(i).GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(MustHaveTenantAttribute).Name))
                {
                    if (TenantId.HasValue)
                    {
                        if (!parameters.ParameterNames.Any())
                        {
                            sb.AppendFormat(" where ");
                        }
                        else
                        {
                            sb.AppendFormat(" and ");
                        }
                        parameters.Add(sourceProperties.ElementAt(i).Name, TenantId);
                        sb.AppendFormat("{0} = @{1}", GetColumnName(sourceProperties.ElementAt(i)), sourceProperties.ElementAt(i).Name);
                    }
                }

                #endregion

                #region OU filter

                if (sourceProperties.ElementAt(i).GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(MustHaveOUAttribute).Name))
                {
                    if (EnableOUFilter)
                    {
                        if (DataPermissions != null)
                        {
                            if (!parameters.ParameterNames.Any())
                            {
                                sb.AppendFormat(" where ");
                            }
                            else
                            {
                                sb.AppendFormat(" and ");
                            }

                            int k = 0;
                            StringBuilder inClause = new StringBuilder();
                            foreach (var item in DataPermissions)
                            {
                                k++;
                                parameters.Add("OU" + k, item);
                                inClause.Append("@OU" + k + ",");
                            }
                            sb.AppendFormat("{0} in ({1})", GetColumnName(sourceProperties.ElementAt(i)), inClause.ToString().TrimEnd(','));
                        }
                    }
                }

                #endregion
            }
        }

        private DynamicParameters BuildParameters(object entity)
        {
            if (entity == null) entity = new { };
            var props = entity.GetType().GetProperties();

            var ps = new DynamicParameters();
            foreach (var prop in props)
            {
                ps.Add(prop.Name, prop.GetValue(entity, null));
            }
            return ps;
        }

        private void BuildDefaultAttributeValue(object entity)
        {
            var sourceProperties = GetScaffoldableProperties(entity).ToArray();
            for (var i = 0; i < sourceProperties.Count(); i++)
            {
                if (sourceProperties.ElementAt(i).GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(MustHaveTenantAttribute).Name))
                {
                    if (TenantId.HasValue)
                    {
                        sourceProperties.ElementAt(i).SetValue(entity, TenantId.ToString());
                    }
                }
            }
        }

        #endregion

        #region Operate extensions

        public T GetFirst<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetFirst<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QueryFirst<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public T GetFirstOrDefault<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetFirstOrDefault<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QueryFirstOrDefault<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public T GetSingle<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetSingle<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QuerySingle<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public T GetSingleOrDefault<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetSingleOrDefault<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QuerySingleOrDefault<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public Task<T> GetFirstAsync<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetFirstAsync<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QueryFirstAsync<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public Task<T> GetFirstOrDefaultAsync<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetFirstOrDefaultAsync<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QueryFirstOrDefaultAsync<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public Task<T> GetSingleAsync<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetSingle<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QuerySingleAsync<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public Task<T> GetSingleOrDefaultAsync<T>(IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties((T)Activator.CreateInstance(typeof(T))).ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere(sb, whereprops, (T)Activator.CreateInstance(typeof(T)), whereConditions);
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetSingleOrDefaultAsync<{0}>: {1}", currenttype, sb));

            #region Extension

            var parameters = BuildParameters(whereConditions);
            BuildAttributeConditions(sb, (T)Activator.CreateInstance(typeof(T)), parameters);

            #endregion

            return connection.QuerySingleOrDefaultAsync<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        #endregion
    }
}
