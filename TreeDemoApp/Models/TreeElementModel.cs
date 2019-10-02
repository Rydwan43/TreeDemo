using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TreeDemoApp.Models
{
    public class TreeElementModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify name")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int Pos { get; set; }
    }
}