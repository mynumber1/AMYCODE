using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lclass.common.lib.Utils
{
    public class LambdaFields<T> where T : class,new()
    {
        private List<String> fieldsList;
        public LambdaFields()
        {
            fieldsList = new List<String>();
        }

        /// <summary>
        /// 
        /// </summary>
        public String Fields
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                fieldsList.ForEach(x =>
                {
                    sb.AppendFormat("{0},", x);
                });
                return sb.ToString().TrimEnd(',');
            }
        }

        internal void Add(string fieldname)
        {
            if (!fieldsList.Any(x => x == fieldname))
                fieldsList.Add(fieldname);
        }
    }
}
