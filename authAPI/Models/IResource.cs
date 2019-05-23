using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace authAPI.Models
{
    interface IResource
    {
        int Id { get; set; }
        string Language { get; set; }
        string Value { get; set; }
        string TagsCompressed { get; set; }
        IEnumerable<string> Tags { get; set; }
    }
}
