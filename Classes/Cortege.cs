using System;

namespace Checkpoint04
{
    public class Cortege : ICortege
    {
        public DateTime Date {get;set;}
        public ManagerName ManagerName { get; set; }
        public string Client { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string FileLog { get; set; }
    }
}