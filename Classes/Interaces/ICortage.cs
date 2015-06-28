using System;

namespace Checkpoint04
{
    public interface ICortege
    {
        DateTime Date { get; set; }
        ManagerName ManagerName { get; set; }
        string Client { get; set; }
        string Article { get; set; } 
        decimal Price { get; set; }
        string FileLog { get; set; }
    }
}