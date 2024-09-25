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
        public CategoryItem Category { get; set; }
        public string ItemFileName {get; set;}
        public string DateString { get; set; }
    }
    public class CategoryItem
    {
        public string CategoryName { get; set; }
        public string IconFile { get; set; }
    }
   
}
