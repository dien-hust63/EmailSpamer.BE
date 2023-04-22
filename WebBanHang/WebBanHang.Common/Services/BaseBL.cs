using Gather.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebBanHang.Common.Attributes;
using WebBanHang.Common.Entities;
using WebBanHang.Common.Interfaces.Base;
using static WebBanHang.Common.Enumeration.Enumeration;

namespace WebBanHang.Common.Services
{
    public class BaseBL<TEntity> : IBaseBL<TEntity>
    {
        #region DECLARE
        protected IBaseDL<TEntity> _baseDL;
        string _className;
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<TEntity> baseRepository)
        {
            _baseDL = baseRepository;

            _className = typeof(TEntity).Name.ToLower();
        }

        #endregion

        #region Method

        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public ServiceResult Delete(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            var rowEffects = _baseDL.Delete(entityId);
            serviceResult.Data = new
            {
                rowEffects = rowEffects,
                messages = Resources.ResourceVN.Success_Delete,
            };
            return serviceResult;
        }

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="entityIds">chuỗi chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public ServiceResult DeleteMultiple(string entityIds)
        {
            ServiceResult serviceResult = new ServiceResult();
            var rowEffects = _baseDL.DeleteMultiple(entityIds);
            serviceResult.Data = new
            {
                rowEffects = rowEffects,
                messages = Resources.ResourceVN.Success_Delete,
            };
            return serviceResult;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public ServiceResult GetAllEntities()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _baseDL.GetAllEntities();
            return serviceResult;
        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021) 
        public virtual ServiceResult GetEntityById(int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _baseDL.GetEntityById(entityId);
            return serviceResult;
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public ServiceResult Insert(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            //Validate dữ liệu
            //Validate chung
            ServiceResult validateData = ValidateData(entity);
            if (!validateData.Success)
            {
                return validateData;
            }
            //Validate riêng
            ServiceResult validateCustom = ValidateCustom(entity);
            if (!validateCustom.Success)
            {
                return validateCustom;
            }
            //Thêm dữ liệu
            var response = _baseDL.Insert(entity);
            if (response != null)
            {
                serviceResult.Data = response;
                AfterSave(EntityState.Add, entity);
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = null;
            }

            return serviceResult;
        }


        /// <summary>
        /// xử lý sau khi lưu
        /// </summary>
        /// <param name="response"></param>
        protected virtual void AfterSave(EntityState entityState, TEntity entity)
        {
            
        }

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <param name="entityId">Id </param>
        /// <returns>số bản ghi được sửa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public ServiceResult Update(TEntity entity, int entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            //Validate dữ liệu
            //Validate chung
            ServiceResult validateData = ValidateData(entity, entityId);
            if (!validateData.Success)
            {
                return validateData;
            }
            //Validate riêng
            ServiceResult validateCustom = ValidateCustom(entity);
            if (!validateCustom.Success)
            {
                return validateCustom;
            }
            //update dữ liệu
            var rowEffects = _baseDL.Update(entity, entityId);
            serviceResult.Data = new
            {
                rowEffects = rowEffects,
                messages = Resources.ResourceVN.Success_Update,
            };
            return serviceResult;
        }

        /// <summary>
        /// Ham lay paging
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual ServiceResult GetEntityPaging(BasePagingParam param)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Data = _baseDL.GetEntityPaging(param);
            return serviceResult;
        }
        #region Private Method


        /// <summary>
        /// Validate dữ liệu khi thêm mới
        /// </summary>
        /// <param name="entity">thông tin cần validate</param>
        /// <returns>error object nếu dữ liệu không thỏa mãn, null nếu thỏa mãn</returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// MoidifiedBy: nvdien(19/8/2021)
        private ServiceResult ValidateData(TEntity entity, int? entityId = null)
        {
            ServiceResult serviceResult = new ServiceResult();
            //Kiểm tra các trường bắt buộc nhập
            ServiceResult checkRequiredField = CheckRequiredField(entity);
            if (!checkRequiredField.Success)
            {
                return checkRequiredField;
            }
            //Kiểm tra các trường không được phép trùng
            ServiceResult checkUniqueField = CheckDuplication(entity, entityId);
            if (!checkUniqueField.Success)
            {
                return checkUniqueField;
            }

            //Kiểm tra email(nếu có) có đúng định dạng không
            ServiceResult checkEmailSyntax = CheckEmailSyntax(entity);
            if (!checkEmailSyntax.Success)
            {
                return checkEmailSyntax;
            }
            //Thỏa mãn hết
            return serviceResult;
        }


        /// <summary>
        /// Validate dữ liệu riêng
        /// </summary>
        /// <param name="entity">thông tin cần validate</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(20/8/2021)
        /// ModifiedBy: nvdien(20/8/2021)
        protected virtual ServiceResult ValidateCustom(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            return serviceResult;
        }

        /// <summary>
        /// Kiểm tra các trường bắt buộc nhập
        /// </summary>
        /// <param name="entity">Thông tin kiểm tra</param>
        /// <returns>object error nếu có trường để trống, null nếu thỏa mã</returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// MoidifiedBy: nvdien(19/8/2021)
        private ServiceResult CheckRequiredField(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            foreach (var property in entity.GetType().GetProperties())
            {
                var propGatherRequired = property.GetCustomAttributes(typeof(AttributeCustomRequired), true);
                var propGatherDislayName = property.GetCustomAttributes(typeof(AttributeCustomDisplayName), true);
                if (propGatherRequired.Length > 0)
                {
                    var fieldName = propGatherDislayName.Length > 0 ? (propGatherDislayName[0] as AttributeCustomDisplayName).FieldName : property.Name;
                    if (property.GetValue(entity) == null || string.IsNullOrEmpty(property.GetValue(entity).ToString()))
                    {
                        serviceResult.setError(string.Format(Resources.ResourceVN.Exception_Required, fieldName));
                        return serviceResult;
                    }
                }
            }
            return serviceResult;
        }

        /// <summary>
        /// Kiểm tra những trường không được phép trùng
        /// </summary>
        /// <param name="entity">Thông tin cần kiểm tra</param>
        /// <returns>error object nếu không thỏa mãn, null nếu thỏa mãn</returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// MoidifiedBy: nvdien(19/8/2021)
        private ServiceResult CheckDuplication(TEntity entity, int? entityId = null)
        {
            ServiceResult serviceResult = new ServiceResult();
            var properties = entity.GetType().GetProperties();
            //Kiểm tra xem entity có trường yêu cầu không được phép trùng không
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(AttributeCustomUnique), false))
                {
                    TEntity entityResult = _baseDL.GetEntityByProperty(property.Name, property.GetValue(entity));
                    if (entityResult != null)
                    {
                        var entityResultId = entityResult.GetType().GetProperty($"id{_className}").GetValue(entityResult).ToString();
                        var isSelf = entityResultId.Equals(entityId.ToString());
                        //entityId == null : trường hợp thêm mới
                        //entityId != null : trường hợp chỉnh sửa
                        if (entityId == null || (entityId != null && isSelf == false))
                        {
                            var propGatherDislayName = property.GetCustomAttributes(typeof(AttributeCustomDisplayName), true);
                            var fieldName = propGatherDislayName.Length > 0 ? (propGatherDislayName[0] as AttributeCustomDisplayName).FieldName : property.Name;
                            serviceResult.setError(string.Format(Resources.ResourceVN.Exception_Duplication, fieldName));
                            return serviceResult;
                        }
                    }
                }
            }
            return serviceResult;
        }

        /// <summary>
        /// Kiểm tra định dạng Email
        /// </summary>
        /// <param name="entity">thông tin cần kiểm tra</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// MoidifiedBy: nvdien(19/8/2021) 
        private ServiceResult CheckEmailSyntax(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            var propEmail = entity.GetType().GetProperty("Email");
            if (propEmail != null)
            {
                var email = propEmail.GetValue(entity);
                var regexEmail = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                var isValidEmail = Regex.IsMatch(email.ToString(), regexEmail);
                if (isValidEmail == false)
                {
                    serviceResult.setError(Resources.ResourceVN.Exception_EmployeeEmail);
                    return serviceResult;
                }
            }
            return serviceResult;
        }




        #endregion
        #endregion
    }
}
