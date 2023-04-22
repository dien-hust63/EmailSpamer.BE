using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Interfaces.Base;

namespace WebBanHang.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {

        #region Declare
        IBaseBL<TEntity> _baseBL;
        #endregion

        #region Constructor
        public BaseEntityController(IBaseBL<TEntity> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpGet]
        public ServiceResult GetAllEntities()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                //Trả về kết quả cho client
                serviceResult = _baseBL.GetAllEntities();
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.ToString());
            }
            return serviceResult;


        }

        [HttpPost("paging")]
        public IActionResult GetEntityPaging(BasePagingParam param)
        {
            try
            {
                var serviceResult = _baseBL.GetEntityPaging(param);
                //4.Trả về kết quả cho client
                if (serviceResult.Data != null)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Common.Resources.ResourceVN.Exception_ErrorMsg,
                    errorCode = "Gather-001",
                    moreInfo = "https://openapi.Gather.com.vn/errorcode/Gather-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }


        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpGet("{entityId}")]
        public ServiceResult GetEntityById(int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                //Trả về kết quả cho client
                serviceResult = _baseBL.GetEntityById(entityId);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.ToString());
            }
            return serviceResult;
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Dữ liệu được thêm</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPost]
        public ServiceResult Insert(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                //Trả về kết quả cho client
                serviceResult = _baseBL.Insert(entity);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.ToString());
            }
            return serviceResult;

        }

        /// <summary>
        /// Chỉnh sửa theo Id
        /// </summary>
        /// <param name="entityId">Id</param>
        /// <param name="entity">Thông tin muốn thay đổi</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPut("{entityId}")]
        public ServiceResult Update(int entityId, TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                //Trả về kết quả cho client
                serviceResult = _baseBL.Update(entity, entityId);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.ToString());
            }
            return serviceResult;
        }


        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var serviceResult = _baseBL.Delete(entityId);
                return Ok(serviceResult.Data);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Common.Resources.ResourceVN.Exception_ErrorMsg,
                    errorCode = "Gather-001",
                    moreInfo = "https://openapi.Gather.com.vn/errorcode/Gather-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="entityIds">chuỗi chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: ndien(17/8/2021)
        [HttpPost("delete/multiple")]
        public ServiceResult DeleteMultiple([FromBody] ListIDObject entityIds)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                if(entityIds.ListID == null && entityIds.ListID.Length == 0)
                {
                    serviceResult.setError("Param không hợp lệ!");
                    return serviceResult;
                }
                //Trả về kết quả cho client
                serviceResult = _baseBL.DeleteMultiple(entityIds.ListID);
            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.ToString());
            }
            return serviceResult;
        }
        #endregion
    }
}
