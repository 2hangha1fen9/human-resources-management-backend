using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Common
{
    /// <summary>
    /// 对象映射工具类，解决数据库实体和Dto之间的转换
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">原类型</param>
        /// <returns>目标类型</returns>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) //对象为空返回默认值
            {
                return default(T);
            }
            //配置映射关系
            var config = new MapperConfiguration(cfg => cfg.CreateMap(obj.GetType(), typeof(T)));
            //创建映射
            var mapper = config.CreateMapper();
            return mapper.Map<T>(obj);
        }

        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="TSource">原类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null)
            {
                return destination;
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TSource), typeof(TDestination)));
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 集合列表映射
        /// </summary>
        /// <typeparam name="TDestination">目标类型集合</typeparam>
        /// <param name="source">原类型集合</param>
        /// <returns>目标类型集合</returns>
        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            Type sourceType = source.GetType().GetGenericArguments()[0];  //获取枚举的成员类型
            var config = new MapperConfiguration(cfg => cfg.CreateMap(sourceType, typeof(TDestination)));
            var mapper = config.CreateMapper();

            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 集合列表映射(泛型集合映射转换)
        /// </summary>
        /// <typeparam name="TSource">目标类型集合</typeparam>
        /// <typeparam name="TDestination">原类型集合</typeparam>
        /// <param name="source">目标类型集合</param>
        /// <returns></returns>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TSource), typeof(TDestination)));
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }
    }
}
