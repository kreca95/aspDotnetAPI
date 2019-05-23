using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace authAPI.Models
{
    public class Resource : IResource
    {

        public int Id { get; set; }
        public string Language { get; set; }
        public string Value { get; set; }

        [NotMapped]
        public IEnumerable<string> Tags { get; set; }
        public string TagsCompressed { get; set; }
    }
}