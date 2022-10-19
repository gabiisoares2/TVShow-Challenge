using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.ViewModel
{
    public class BaseEntityViewModel
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; } = "";
        public bool OrderDesc { get; set; } = true;
        public string OrderColumn { get; set; } = "";
        public bool OrderDirection { get; set; } = true;
    }
}
