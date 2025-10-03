using System;
using System.Linq;

namespace Domain.Models.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum genericEnum)
        {
            var genericEnumType = genericEnum.GetType();
            var memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0]
                    .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute) attributes.ElementAt(0)).Description;
                }
            }

            return genericEnum.ToString();
        }
    }
}
