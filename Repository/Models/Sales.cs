using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public int Article_Id { get; set; }
        public int Client_Id { get; set; }
        public int FileLog_Id { get; set; }
    }
}
