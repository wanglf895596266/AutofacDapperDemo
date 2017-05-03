using AutofacDapperDemo.Core.Log;
using AutofacDapperDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDapperDemo.Service.Log
{
    public interface ILogService : IDependency
    {
        List<Logs> LogList();

        Logs GetLogById(int id);

        bool Insert(Logs log);

        bool Delete(Logs log);

        bool Update(Logs log);


    }
}
