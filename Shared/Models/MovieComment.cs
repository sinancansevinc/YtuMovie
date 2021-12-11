using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class MovieComment
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public string Comment { get; set; }
        public string CreateIp { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
