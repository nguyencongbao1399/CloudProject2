using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingVoucher_v02.Utils
{
    public class ValidateUtils
    {
        public bool ValidIntParam(string query, string paramName, int paramValue)
        {
            //validate input
            if(query==null || query.Trim().Length == 0
                || paramName==null || paramName.Trim().Length == 0)
            {
                return false;
            }

            //validate param
            if (query.Contains(paramName))
            {
                if (paramValue > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidStringParam(string query, string paramName, string paramValue)
        {
            //validate input
            if (query == null || query.Trim().Length == 0
                || paramName == null || paramName.Trim().Length == 0
                || paramValue == null || paramValue.Trim().Length == 0)
            {
                return false;
            }

            //validate param
            if (query.Contains(paramName))
            {
                return true;
            }
            return false;
        }
        public bool ValidIntParams(string query, string paramName, string paramValue)
        {
            //validate input
            if (query == null || query.Trim().Length == 0
                || paramName == null || paramName.Trim().Length == 0
                || paramValue == null || paramValue.Trim().Length == 0)
            {
                return false;
            }

            //validate param
            if (query.Contains(paramName))
            {
                string[] arrInt = paramValue.Split(",");
                foreach (string item in arrInt)
                {

                    try
                    {
                        int value = Int32.Parse(item);
                        if(value <= 0)
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Parse Exception");
                        return false;
                    }
                }
            }
            return false;
        }

        public bool ValidDateTimeParam(string query, string paramName, DateTime paramValue)
        {
            if (query.Contains(paramName))
            {
                if(paramValue.Millisecond > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidRangeLengthInput(string input, int minLength, int maxLength)
        {
            if (input == null)
            {
                return false;
            }
            return input.Trim().Length >= minLength && input.Trim().Length <= maxLength;
        }

        public bool ValidFixedLengthInput(string input, int fixedLength)
        {
            if (input == null)
            {
                return false;
            }
            return input.Trim().Length == fixedLength;
        }
    }
}
