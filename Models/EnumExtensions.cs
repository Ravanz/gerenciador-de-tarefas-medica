using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace vSaude.Models
{
    public static class EnumExtensions
    {
        public static string ObterDescricao(this Enum valor)
        {
            var memberInfo = valor.GetType().GetMember(valor.ToString()).FirstOrDefault();
            var displayAttribute = memberInfo?.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? valor.ToString();
        }
    }
}

