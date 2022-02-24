﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLibrary.Response
{
    public class SharedResponse
    {
        public bool IsSuccess { get; set; }

        public int Status { get; set; }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Record { get; set; }

        public SharedResponse()
        {
        }

        public SharedResponse(bool isSuccess, int status = 200, string msg = "")
        {
            IsSuccess = isSuccess;
            Status = status;
            Message = msg;
        }

        public SharedResponse(bool isSuccess, int status = 200, string msg = "", object data = null)
        {
            IsSuccess = isSuccess;
            Status = status;
            Message = msg;
            Record = data;
        }
    }
}