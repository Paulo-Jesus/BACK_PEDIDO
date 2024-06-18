using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Responses
{
    public class Response
    {
        public ResponseType Code { get; set; }
        public string? Message { get; set; }
        public Object? data { get; set; }

        

        public string setMessage(String msj) {
            return Message;
        }

        public Object setData(Object data) {
            return data;
        }
    }
    public enum ResponseType
    {
        Error = 00,
        Success = 01
    }
}
