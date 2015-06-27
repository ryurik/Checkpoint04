using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class Sales
    {
        private Articles _articles;
        private Clients _clients;
        private FileLogs _fileLogs;

        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public int Article_Id { get; set; }
        public int Client_Id { get; set; }
        public int FileLog_Id { get; set; }

        public Articles Articles
        {
            get { return _articles; }
            set
            {
                _articles = value; 
                Article_Id = value.Id; 
            } 
        }

        public Clients Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                Client_Id = value.Id;
            }
        }

        public FileLogs FileLogs
        {
            get { return _fileLogs; }
            set
            {
                _fileLogs = value;
                FileLog_Id = value.Id;
            }
        }
    }
}
