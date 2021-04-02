using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieNight.ViewModels
{
    public class PaginationInfoViewModel
    {
        public int TotalPages { get; set; }
        public int ItemsOnPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}