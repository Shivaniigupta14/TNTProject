using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models
{
    public class ApiResult<T>
    {
        #region constructor
        public ApiResult(string rawResult, int code, T  result)
        {
            RawResult = rawResult;
            Code = code;
            Result = result;
        }
        #endregion

        #region Properties
        public string RawResult { get; private set; }
        public int Code { get; private set; }
        public T Result { get; private set; }
        public bool IsSuccessful
        {
            get { return Code >= 200 && Code < 300; }
        }
        #endregion 
    }
}


