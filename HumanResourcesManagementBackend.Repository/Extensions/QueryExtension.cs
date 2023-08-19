using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace HumanResourcesManagementBackend.Repository
{
    public static class QueryExtension
    {
        /// <summary>
        /// 分页查询扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<T> Pageing<T>(this IQueryable<T> query, PageRequest page) where T : class
        {
            //不分页
            if(page.Rows == 0)
            {
                return query.ToList();
            }
            //分页
            var fcount = query.Future();
            var takeRecord = (page.PageNum - 1) * page.Rows;
            QueryFutureEnumerable<T> futureQuery = query.Skip(takeRecord).Take(page.Rows).AsNoTracking().Future();
            page.RecordCount = fcount.Count();
            return futureQuery.ToList();
        }
    }
}
