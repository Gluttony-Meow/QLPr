using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLPr.Models
{
    public class Project
    {
        [Key]
        public int ProjecttId { get; set; }
        public string ProjectName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProjectStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProjectEnd { get; set; }
        public string ProjectDescription { get; set;}

        public virtual ICollection<AssignTask> AssignTask { get; set; }
    }
}