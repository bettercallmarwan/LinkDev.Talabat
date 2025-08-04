using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {

        //mine
        public required ICollection<KeyValuePair<string, IEnumerable<string>>> Errors { get; set; }

        //ahmed nasr

        //public required IEnumerable<string> Errors { get; set; }

        // Key => parameter name , Value => Errors
        public ApiValidationErrorResponse(string? message = null) 
            : base(400, message)
        {
            
        }   
    }
}
