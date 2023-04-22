using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.DBHelper
{
    public interface IDBHelper
    {
        public bool Delete<T>(long entityID);

        public List<T> Query<T>(string query, DynamicParameters dynamicParam, CommandType? commandType = null);

        public T QueryFirstOrDefault<T>(string query, DynamicParameters dynamicParam, CommandType? commandType = null);

        /// <summary>
        /// execute query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dynamicParam"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(string query, DynamicParameters dynamicParam, CommandType? commandType = null);

        /// <summary>
        /// query multiple
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="query"></param>
        /// <param name="dynamicParam"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public (List<T1>, List<T2>) QueryMultipleResult<T1,T2>(string query, DynamicParameters dynamicParam, CommandType? commandType = null);

        /// <summary>
        /// Query multiple return three result
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="query"></param>
        /// <param name="dynamicParam"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public (List<T1>, List<T2>, List<T3>) QueryMultipleResult<T1,T2,T3>(string query, DynamicParameters dynamicParam, CommandType? commandType = null);

        /// <summary>
        /// Thêm hàng loạt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listInsert"></param>
        /// <returns></returns>
        public bool InsertBulk<T>(IEnumerable<T> listInsert);

        /// <summary>
        /// Update hàng loạt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listInsert"></param>
        /// <returns></returns>
        public bool UpdateBulk<T>(IEnumerable<T> listUpdate);
    }
}
