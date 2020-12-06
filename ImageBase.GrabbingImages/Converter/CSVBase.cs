using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.GrabbingImages.Converter
{
    public abstract class CSVBase
    {
        public virtual string ToCsv()
        {
            string output = "";

            var properties = GetType().GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                output += properties[i].GetValue(this).ToString();
                if (i != properties.Length - 1)
                {
                    output += ",";
                }
            }

            return output;
        }
    }
}
