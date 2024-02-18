using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos {
    public class CustomResponseDto<T> {
        public int StatusCode { get; set; }
        public IList<string>? Errors { get; set; }
        public T? Data { get; set; }


        public static CustomResponseDto<T> Success(T data, int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data};
        }

        public static CustomResponseDto<T> Failure(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Failure(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode};
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

    }
}
