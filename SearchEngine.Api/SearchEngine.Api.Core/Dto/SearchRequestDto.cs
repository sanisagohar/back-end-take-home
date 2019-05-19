using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Api.Core.Dto
{
    public class SearchRequestDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
