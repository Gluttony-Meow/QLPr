using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLPr.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhonge { get; set; }
        public string ClientEmail { get; set; }
        public string ClientCompany { get; set; }
        public virtual ICollection<AssignTask> AssignTask { get; set; }
    }
}