using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class FirstLetterCapitalizeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return char.IsUpper(value.ToString()[0]);
            
        }
    }
}
