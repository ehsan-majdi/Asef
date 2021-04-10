using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asefian.Model.ViewModel.Account
{
   public class AssignGroupViewModel
    {
        public int id { get; set; }
        public int userId { get; set; }
        public List<int> groupId { get; set; }
    }
}
