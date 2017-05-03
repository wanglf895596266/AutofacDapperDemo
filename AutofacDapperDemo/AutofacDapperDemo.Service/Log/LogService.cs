using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacDapperDemo.Core.Log;
using AutofacDapperDemo.Repository;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AutofacDapperDemo.Service.Log
{
    public class LogService : ILogService
    {
        private readonly IDbConnection _dbConnnection;

        public LogService()
        {
            _dbConnnection = ConnectionFactory.GetDbConnection();
        }

        public Logs GetLogById(int id)
        {
            var log = _dbConnnection.Query<Logs>("select * from [Log] where Id=@Id ", new
            {
                Id = id
            }) as Logs;
            return log;
        }

        public List<Logs> LogList()
        {
            var logs = _dbConnnection.Query<Logs>("select * from  [Log]").ToList();
            return logs;
        }

        public bool Insert(Logs log)
        {
            try
            {
                var parems = new DynamicParameters();
                parems.Add("@Message", log.Message);
                parems.Add("@CreateDate", DateTime.Now);
                var tt= _dbConnnection.Execute("insert into [Log] values (@Message,@CreateDate)", parems);
                if (tt > 0)
                    return true;
                else return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(Logs log)
        {
           try
            {
                var tt = _dbConnnection.Execute(" delete [Log] where Id=@Id", new
                {
                    Id = log.Id
                });
                if (tt > 0)
                    return true;
                else return false;
            }
            catch(Exception ex)
            {
                return false;

            }
        }

        public bool Update(Logs log)
        {
            try
            {
                var parems = new DynamicParameters();
                parems.Add("@Message", log.Message);
                parems.Add("@CreateDate", DateTime.Now);
                parems.Add("@Id", log.Id);
                var tt = _dbConnnection.Execute("update  [Log] set Message=@Message,CreateDate=@CreateDate where Id=@Id)", parems);
                if (tt > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
