using System;
using System.ComponentModel;
using System.Reflection;

namespace Homuai.App.Services
{
    public static class GetEnumDescription
    {
        public static string Description<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    var description = ((DescriptionAttribute)attrs[0]).Description;
                    return ResourceText.ResourceManager.GetString(description);
                }
            }

            return enumerationValue.ToString();
        }
    }
}
