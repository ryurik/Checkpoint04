using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public int ArticleId { get; set; }
        public int ClientId { get; set; }
        public int FileLogId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Client Client { get; set; }
        public virtual FileLog FileLog { get; set; }
    }
}
