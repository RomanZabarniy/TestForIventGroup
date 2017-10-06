using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest_IG.Models.HelperClasses
{
    public class JsonResponseInfo
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string BodyHtml { get; set; }

        internal JsonResponseInfo ConvertErrorListToJsonResp(HashSet<string> errorList)
        {
            //JsonResponseInfo res = new JsonResponseInfo();
            if (errorList.Count == 0)
            {
                Code = "200";
            }
            else
            {
                Code = "400";
                foreach (var item in errorList)
                {
                    Message += item + "\n";
                }
            }
            return this;
        }
    }
}