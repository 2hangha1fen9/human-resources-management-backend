using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository.Extensions
{
    public static class TransactionExtension
    {
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public static bool Transaction(this DbContext context,Func<bool> action)
        {
            bool flag = false;

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    flag = action();
                    if (flag)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
            return flag;
        }

    }
}
