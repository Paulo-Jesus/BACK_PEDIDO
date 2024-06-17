using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Response
{
    public class Response
    {
        public ResponseType Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }


     
    }

    public enum ResponseType {
        Error=00,
        Success=01
    }


}


