using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities.Model;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.DL.BaseDL;
using static Gather.ApplicationCore.Constant.RoleProject;

namespace WebBanHang.DL.DL
{
    public class ReceiverDL : BaseDL<Receiver>, IReceiverDL   
    {
        public ReceiverDL(IConfiguration configuration, IDBHelper dbHelper) : base(configuration, dbHelper)
        { 
        }


        /// <summary>
        /// Thêm hàng loạt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listInsert"></param>
        /// <returns></returns>
        public bool importReceiver(List<ReceiverEmail> listInsert)
        {
            string tableName = typeof(Receiver).Name.ToLower();
            // get the properties of the type T
            var properties = typeof(Receiver).GetProperties();
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
            if (query[query.Length - 1].ToString().Equals(","))
            {
                query = query.Remove(query.Length - 1, 1);
            }
            query.Append(") VALUES ");
            int rowCount = 0;
            foreach (var receiver in listInsert)
            {
                query.Append("(");
                var paramName = $"@p_{rowCount}";
                parameters.Add(paramName, receiver.Email.Trim());
                query.Append(paramName);
                query.Append(",");
                if (query[query.Length - 1].ToString().Equals(","))
                {
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
    }
}
