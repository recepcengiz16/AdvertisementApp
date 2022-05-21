﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Common
{
    public class Response<T>:Response, IResponse<T>
    {
        public Response(ResponseType responseType,string message):base(responseType,message)
        {

        }

        public Response(ResponseType responseType,T data):base(responseType)
        {
            Data = data;
        }
        public Response(T data,List<CustomValidationError> validationErrors):base(ResponseType.ValidationError)
        {
            Data= data;
            ValidationErrors = validationErrors;
        }
        public T Data { get; set; }
        public List<CustomValidationError> ValidationErrors { get; set; }
    }
}
