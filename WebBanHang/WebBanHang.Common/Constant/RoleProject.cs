using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gather.ApplicationCore.Constant
{
    public static class RoleProject
    {
        public enum Role
        {
            [Display(Name ="Admin")]
            Admin = 1,
            [Display(Name = "Leader")]
            Leader = 2,
            [Display(Name = "Member")]
            Member = 3
        }

        public enum State
        {
            [Display(Name = "TODO")]
            TODO = 0,
            [Display(Name = "IN PROGRESS")]
            INPROGRESS = 1,
            [Display(Name = "IMPLEMENTED")]
            IMPLEMENTED = 2,
            [Display(Name = "DONE")]
            DONE = 3,

        }
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()?
                            .GetMember(enumValue.ToString())?
                            .First()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name;
        }
    }
}
