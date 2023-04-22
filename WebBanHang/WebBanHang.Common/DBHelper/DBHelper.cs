using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;
using WebBanHang.Common.Library;
using static Dapper.SqlMapper;

namespace WebBanHang.Common.DBHelper
{
    public class DBHelper : IDBHelper
    {
        private static IConfiguration _configuration;
        private IDbConnection _dbConnection;
        private string _connectionString = "";
        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MasterDB");
        }
        /// <summary>
        /// query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="dynamicParam"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public List<T> Query<T>(string query, DynamicParameters dynamicParam, CommandType? commandType = null)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                return _dbConnection.Query<T>(query, param: dynamicParam, commandType: commandType).ToList();
            }
        }
        /// <summary>
        /// query first or default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="dynamicParam"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string query, DynamicParameters dynamicParam, CommandType? commandType = null)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                return _dbConnection.QueryFirstOrDefault<T>(query, param: dynamicParam, commandType: commandType);
            }
        }
        /// <summary>
        /// xóa dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public bool Delete<T>(long entityID)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                string tableName = typeof(T).Name.ToLower();
                var sqlCommand = $"DELETE FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{tableName}Id", entityID);
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects > 0;
            }
        }

        /// <summary>
        /// Query multiple return two list
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="query"></param>
        /// <param name="dynamicParam"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public (List<T1>, List<T2>) QueryMultipleResult<T1, T2>(string query, DynamicParameters dynamicParam, CommandType? commandType = null)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var result = _dbConnection.QueryMultiple(query, dynamicParam, commandType: commandType);
                List<T1> listOne = result.Read<T1>().ToList();
                List<T2> listTwo = result.Read<T2>().ToList();
                return (listOne, listTwo);
            }

        }

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
        public (List<T1>, List<T2>, List<T3>) QueryMultipleResult<T1, T2, T3>(string query, DynamicParameters dynamicParam, CommandType? commandType = null)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var result = _dbConnection.QueryMultiple(query, dynamicParam, commandType: commandType);
                List<T1> listOne = result.Read<T1>().ToList();
                List<T2> listTwo = result.Read<T2>().ToList();
                List<T3> listThree = result.Read<T3>().ToList();
                return (listOne, listTwo, listThree);
            }
        }
        /// <summary>
        /// execute query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dynamicParam"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int Execute(string query, DynamicParameters dynamicParam, CommandType? commandType = null)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                return _dbConnection.Execute(query, dynamicParam, commandType: commandType);
            }
        }

        /// <summary>
        /// Thêm hàng loạt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listInsert"></param>
        /// <returns></returns>
        public bool InsertBulk<T>(IEnumerable<T> listInsert)
        {
            string tableName = typeof(T).Name.ToLower();
            // get the properties of the type T
            var properties = typeof(T).GetProperties();
            DynamicParameters parameters = new DynamicParameters();

            // build the parameterized query
            var query = new StringBuilder();
            query.Append($"INSERT INTO {tableName} (");
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].IsDefined(typeof(AttributeCustomId), false)) continue;
                if (properties[i].IsDefined(typeof(AttributeCustomNotMap), false)) continue;
                query.Append(properties[i].Name);
                if (i < properties.Length - 1)
                {
                    query.Append(",");
                }
            }
            if (query[query.Length - 1].ToString().Equals(",")){
                query = query.Remove(query.Length - 1, 1);
            }
            query.Append(") VALUES ");
            int rowCount = 0;
            foreach (var row in listInsert)
            {
                query.Append("(");
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].IsDefined(typeof(AttributeCustomId), false)) continue;
                    if (properties[i].IsDefined(typeof(AttributeCustomNotMap), false)) continue;
                    var paramName = $"@p_{rowCount}_{i}";
                    parameters.Add(paramName, properties[i].GetValue(row));
                    query.Append(paramName);
                    if (i < properties.Length - 1)
                    {
                        query.Append(",");
                    }
                }
                if(query[query.Length - 1].ToString().Equals(",")){
                    query = query.Remove(query.Length - 1, 1);
                }
                query.Append(")");
                rowCount++;
                if (rowCount < listInsert.Count())
                {
                    query.Append(", ");
                }
            }

            // execute the command
            using (var _dbConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    return _dbConnection.Execute(query.ToString(), parameters) > 0;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }

        /// <summary>
        /// Update hàng loạt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listUpdate"></param>
        /// <returns></returns>
        public bool UpdateBulk<T>(IEnumerable<T> listUpdate)
        {
            // create the connection
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            // create the transaction
            using MySqlTransaction transaction = connection.BeginTransaction();

            // create the command
            using MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            try
            {
                // execute each update query in the list
                foreach (var update in listUpdate)
                {
                    var properties = typeof(T).GetProperties();
                    string tableName = typeof(T).Name.ToLower();
                    string sql = "update {0} set {1} where {2}";
                    string setUpdate = "";
                    string v_where = "";
                    foreach (var property in properties)
                    {
                        var propName = property.Name;
                        var propValue = property.GetValue(update);
                        if (property.IsDefined(typeof(AttributeCustomId), false))
                        {
                            v_where = $"{propName} = {propValue}"; 
                            continue;
                        }
                        if (property.IsDefined(typeof(AttributeCustomNotMap), false)) continue;
                        
                        setUpdate += $"{propName} = @{propName},";
                        command.Parameters.AddWithValue($"@{propName}", propValue);
                    }
                    if (setUpdate.Length > 0)
                    {
                        setUpdate = setUpdate.Substring(0, setUpdate.Length - 1);
                    }
                    sql = String.Format(sql, tableName, setUpdate, v_where);
                    // set the update query
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                // commit the transaction to save the changes
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                // handle any exceptions that may occur during the updates
                transaction.Rollback();
                return false;
            }
        }
    }
}
