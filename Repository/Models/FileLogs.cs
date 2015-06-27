using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace Repository.Models
{
    public class FileLogs
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string FileName { get; set; }
        public int Manager_Id { get; set; }
    }
}
