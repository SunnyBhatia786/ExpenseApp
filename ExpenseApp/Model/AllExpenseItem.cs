using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Model
{
    internal class AllExpenseItem
    {
        public ObservableCollection<ExpenseItem> Expenses {  get; set; } = new ObservableCollection<ExpenseItem>();

        public AllExpenseItem() => LoadExpenseItem();

        public void LoadExpenseItem()
        {
            Expenses.Clear();
            //Get the folder where the notes are saved
            var appDirpath = FileSystem.AppDataDirectory;

            var expenseitems = Directory
                .EnumerateFiles(appDirpath, "*.expenseitem.txt")
                .Select(filename => {
                    string content = File.ReadAllText(filename); // 0.99
                    string[] contents = content.Split(','); // [0.99]
                    try
                    {
                        return new ExpenseItem()
                        {
                            Itemname = contents[0],
                            Amount = $"${contents[1]}",
                            Date = DateTime.Parse(contents[2]),
                            DateString = contents[2],
                            Category = new CategoryItem()
                            {
                                CategoryName = contents[3],
                                IconFile = GetIconFile(contents[3])
                            },
                            ItemFileName = filename
                        };
                    }
                    catch (Exception ex)
                    {

                        File.Delete(filename);
                    }
                    return null;
                })
                .Where(item => item != null)
                .OrderBy(expenseitem => expenseitem.Date);

            foreach (var expenseitem in expenseitems)
            {
                Expenses.Add(expenseitem);
            }
        } 
        public void DeleteAllExpenses()
        {
            foreach (var item in Expenses)
            {
                File.Delete(item.ItemFileName);
            }
            Expenses.Clear();

        }
        private string GetIconFile(string categoryName)
        {
            switch (categoryName)
            {
                case "Restaurant":
                    return "restaurant.png";
                case "Grocery":
                    return "grocery.png";
                case "Gas":
                    return "fuel.png";
                case "Rent":
                    return "rent.png";
                case "Miscellaneous":
                    return "miscellaneous.png";
                default:
                    return "";
            }
        }
    }
}
