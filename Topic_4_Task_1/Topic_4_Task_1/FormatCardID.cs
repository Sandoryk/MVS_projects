using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic_4_Task_1
{
    class FormatCardID:IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg,IFormatProvider formatProvider) 
        {                       
            if (! this.Equals(formatProvider))
            {
                return null;
            }
            else
            {
                string customerString = arg.ToString();
                if (String.IsNullOrEmpty(format) || customerString.Length!=16 ) 
                    format = "G";
                
                format = format.ToUpper();
                switch (format)
                {
                    case"G":
                        return customerString;
                    case"CD":
                        return customerString.Substring(0, 4) + "-" + customerString.Substring(3, 4) + "-" + customerString.Substring(7, 4) + "-" + customerString.Substring(11, 4);
                    default: return customerString;
                    //throw new FormatException(String.Format("The '{0}' format specifier is not supported.", format));
                }
            }   
        }
    }
}
