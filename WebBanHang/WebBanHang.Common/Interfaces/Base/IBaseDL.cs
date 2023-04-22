using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities;

namespace WebBanHang.Common.Interfaces.Base
{
    public interface IBaseDL<TEntity>
    {

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        IEnumerable<TEntity> GetAllEntities();

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021) 
        TEntity GetEntityById(int entityId);

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        object Insert(TEntity entity);

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <param name="entityId">Id </param>
        /// <returns>số bản ghi được sửa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        int Update(TEntity entity, int entityId);

        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        int Delete(Guid entityId);

        /// <summary>
        /// Lấy thông tin theo property
        /// </summary>
        /// <param name="propName">Tên property</param>
        /// <param name="propValue">Gía trị property</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        TEntity GetEntityByProperty(string propName, object propValue);

        /// <summary>
        /// Lấy thông tin theo property
        /// </summary>
        /// <param name="propName">Tên property</param>
        /// <param name="propValue">Gía trị property</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        List<TEntity> GetListEntityByProperty(string propName, object propValue);

        /// <summary>
        /// Xóa nhiều 
        /// </summary>
        /// <param name="entityIds">chuỗi chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        int DeleteMultiple(string entityIds);

        /// <summary>
        /// Lay paging
        /// </summary>
        /// <param name="param">param lay paging</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        BasePagingResponse<TEntity> GetEntityPaging(BasePagingParam param);


    }
}
