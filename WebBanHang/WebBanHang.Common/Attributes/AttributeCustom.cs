using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Common.Attributes
{
    /// <summary>
    /// Attribute: Trường bắt buộc nhập
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class AttributeCustomRequired : Attribute
    {
    }

    /// <summary>
    /// Attribute: Trường Id
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class AttributeCustomId : Attribute
    {
    }

    /// <summary>
    /// Attribute: hiển thị tên trường
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class AttributeCustomDisplayName : Attribute
    {
        public AttributeCustomDisplayName(string name)
        {
            FieldName = name;
        }
        public string FieldName { get; set; }
    }

    /// <summary>
    /// Attribute: những Property của đối tượng không map với cơ sở dữ liệu
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class AttributeCustomNotMap : Attribute
    {

    }

    /// <summary>
    /// Attribute: Những trường không được phép trùng
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class AttributeCustomUnique : Attribute
    {

    }
}
