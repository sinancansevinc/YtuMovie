using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
