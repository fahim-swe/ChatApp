using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class PagedResponse<T> : Response<T>
    {
        public PagedResponse(T? data) : base(data)
        {

        }

        public PagedResponse(T? data, int pageNumber, int pageSize, int totalRecords) : base(data)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalRecords = totalRecords;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
            
            this.TotalPages = ((int)totalRecords / (int)pageSize);
            if(totalRecords % pageSize != 0){
                this.TotalPages++;
            }

        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
      
        public int TotalPages { get; set; } 
        public int TotalRecords { get; set; }
        
    }

}