using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.ServiceCollection;

namespace WebBanHang.DL.BaseDL
{
    public class BaseDL<TEntity> : IBaseDL<TEntity>
    {
        #region DECLARE
        protected IConfiguration _configuration;
        protected IDBHelper _dbHelper;
        protected IDbConnection _dbConnection;
        protected string _connectionString = string.Empty;
        private string _className;

        #endregion

        #region CONSTRUCTOR
        public BaseDL(IConfiguration configuration, IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MasterDB");
            _className = typeof(TEntity).Name.ToLower();
           
        }


        #endregion

        #region METHOD
        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public int Delete(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"DELETE FROM {_className} WHERE {_className}Id = @{_className}Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_className}Id", entityId);
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects;
            }

        }

        /// <summary>
        /// Xóa nhiều 
        /// </summary>
        /// <param name="entityIds">chuỗi chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        public int DeleteMultiple(string entityIds)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var entityIdList = entityIds.Split(',');
            string tmp = "(";
            foreach (var entityId in entityIdList)
            {
                tmp += $"'{entityId}',";
            }
            tmp = tmp.Remove(tmp.Length - 1);
            tmp += ")";
            var sqlCommand = $"DELETE FROM {_className} WHERE id{_className} IN {tmp}";
            int rowEffects = _dbHelper.Execute(sqlCommand, dynamicParameters, commandType: CommandType.Text);
            return rowEffects;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public IEnumerable<TEntity> GetAllEntities()
        {
            var sqlCommand = $"SELECT * from {_className} T order by T.`id{_className}` desc;";
            var entities = _dbHelper.Query<TEntity>(sqlCommand, null);
            return entities;

        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021) 
        public TEntity GetEntityById(int entityId)
        {
            var sqlCommand = $"SELECT * from {_className} T where T.`id{_className}` = {entityId} order by T.`id{_className}` desc;";
            var entities = _dbHelper.QueryFirstOrDefault<TEntity>(sqlCommand, null);
            return entities;
        }

        /// <summary>
        /// Lấy thông tin theo property
        /// </summary>
        /// <param name="propName">Tên property</param>
        /// <param name="propValue">Gía trị property</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        public TEntity GetEntityByProperty(string propName, object propValue)
        {
            var sqlCommand = $"SELECT * from {_className} WHERE {propName} = @{propName}";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{propName}", propValue);
            var entity = _dbHelper.QueryFirstOrDefault<TEntity>(sqlCommand,  dynamicParameters);
            return entity;
        }

        /// <summary>
        /// Lấy thông tin theo property
        /// </summary>
        /// <param name="propName">Tên property</param>
        /// <param name="propValue">Gía trị property</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        public List<TEntity> GetListEntityByProperty(string propName, object propValue)
        {
            var sqlCommand = $"SELECT * from {_className} WHERE {propName} = @{propName}";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{propName}", propValue);
            var entity = _dbHelper.Query<TEntity>(sqlCommand, dynamicParameters);
            return entity;
        }

        /// <summary>
        /// Lay danh sach du lieu paging
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual BasePagingResponse<TEntity> GetEntityPaging(BasePagingParam param)
        {
            BasePagingResponse<TEntity> basePagingResponse = new BasePagingResponse<TEntity>();
            DynamicParameters dynamicParam = new DynamicParameters();
            string sql = buildCommandTextPaging(param);
            (List<TEntity> listResult, List<int> totalPage) = _dbHelper.QueryMultipleResult<TEntity, int>(sql, dynamicParam, System.Data.CommandType.Text);
            basePagingResponse.listPaging = listResult;
            basePagingResponse.Total = totalPage.FirstOrDefault();
            return basePagingResponse;
        }
        /// <summary>
        /// tạo câu sql paging
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string buildCommandTextPaging(BasePagingParam param)
        {
            string whereClause = BuildWhereFilter(param.ListFilter, param.FilterFormula);
            string orderByClause = BuildOrderByClause(param.ListOrderBy);
            string tableName = param.TableName.ToLower();
            int skip = (param.PageIndex - 1) * param.PageSize;
            string sql = $"select T.* from {tableName} T {whereClause} order by T.`id{tableName}` desc limit {skip},{param.PageSize}; select count(*) from {tableName} T {whereClause};";
            return sql;

        }

        /// <summary>
        /// tao câu order by
        /// </summary>
        /// <param name="listOderBy"></param>
        /// <returns></returns>
        private string BuildOrderByClause(List<OrderBy> listOderBy)
        {
            return "";
        }

        /// <summary>
        /// Tao cau lenh where
        /// </summary>
        /// <param name="listFilter"></param>
        /// <returns></returns>
        private string BuildWhereFilter(List<FilterObject> listFilter, string formula)
        {
            string baseWhere = "WHERE 1 = 1 ";
            if(listFilter.Count == 0 || formula.Length == 0)
            {
                return baseWhere;
            }
            string[] listFilterBuild = new string[listFilter.Count];
            int i = 0;
            foreach (var item in listFilter)
            {
                string filterBuild = "";
                switch (item.Operator)
                {
                    case Common.Enumeration.Enumeration.Operator.EQUAL:
                        filterBuild = $"T.`{item.FieldName}` = {item.FilterValue}";
                        break;
                    case Common.Enumeration.Enumeration.Operator.LIKE:
                        filterBuild = $"T.`{item.FieldName}` like '%{item.FilterValue}%'";
                        break;
                    default:
                        break;
                }

                listFilterBuild[i] = filterBuild;
                i++;
            }
            baseWhere += $"AND {String.Format(formula, listFilterBuild)}";
            return baseWhere;
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public object Insert(TEntity entity)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var properties = entity.GetType().GetProperties();
            string tableName = entity.GetType().Name.ToLower();
            string sql = "Insert into {0}({1}) values ({2})";
            string listProperty = "";
            string listValue = "";
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(AttributeCustomId), false)) continue;
                if (property.IsDefined(typeof(AttributeCustomNotMap), false)) continue;
                var propName = property.Name;
                var propValue = property.GetValue(entity);
                if(propName == "createddate")
                {
                    propValue = DateTime.Now;
                }
                listProperty += $"{propName},";
                listValue += $"@{propName},";
                dynamicParameters.Add($"@{propName}", propValue);
            }
            if(listProperty.Length > 0 && listValue.Length > 0)
            {
                listProperty = listProperty.Substring(0, listProperty.Length - 1);
                listValue = listValue.Substring(0, listValue.Length - 1);
            }
            sql = String.Format(sql, tableName, listProperty, listValue);
            var rowEffects = _dbHelper.Execute(sql, dynamicParameters, commandType: CommandType.Text);
            if (rowEffects > 0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <param name="entityId">Id </param>
        /// <returns>số bản ghi được sửa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public int Update(TEntity entity, int entityId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var properties = entity.GetType().GetProperties();
            string tableName = entity.GetType().Name.ToLower();
            string sql = "update {0} set {1} where {2}";
            string setUpdate = "";
            string v_where = $"id{tableName} = {entityId}";
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(AttributeCustomId), false)) continue;
                if (property.IsDefined(typeof(AttributeCustomNotMap), false)) continue;
                var propName = property.Name;
                var propValue = property.GetValue(entity);
                setUpdate += $"{propName} = @{propName},";
                dynamicParameters.Add($"@{propName}", propValue);
            }
            if (setUpdate.Length > 0 )
            {
                setUpdate = setUpdate.Substring(0, setUpdate.Length - 1);
            }
            sql = String.Format(sql, tableName, setUpdate, v_where);
            var rowEffects = _dbHelper.Execute(sql, dynamicParameters, commandType: CommandType.Text);
            return rowEffects;
        }

        #endregion
    }
}
