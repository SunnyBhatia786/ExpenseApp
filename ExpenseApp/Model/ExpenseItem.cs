using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace ExpenseApp.Model
{
    public class ExpenseItem
    {
        public string Itemname { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
    }
   public enum Category
    {
        Grocery,
        Rent,
        Restaurant,
        Gas,
        Miscellaneous
    }
}
