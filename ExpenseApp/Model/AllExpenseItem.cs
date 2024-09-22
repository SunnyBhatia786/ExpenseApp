using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            //var expenseitems = Directory
            //    .EnumerateFiles(appDirpath, "*.expenseitem.txt")
            //    .Select(filename => new ExpenseItem()
            //        {
            //            Itemname = filename,
            //            Amount = File.ReadAllText(filename), //0.99:icecream
            //            Date = File.GetCreationTime(filename),
            //        })
            //        .OrderBy(expenseitem => expenseitem.Date);

            var expenseitems = Directory
                .EnumerateFiles(appDirpath, "*.expenseitem.txt")
                .Select(filename => {
                    string content = File.ReadAllText(filename); // 0.99
                    string[] contents = content.Split(','); // [0.99]

                    return new ExpenseItem()
                    {
                        Itemname = contents[0],
                        Amount = $"${contents[1]}", 
                        //add category 
                        Date = File.GetCreationTime(filename),
                    };
                })
                .OrderBy(expenseitem => expenseitem.Date);

            foreach (var expenseitem in expenseitems)
            {
                Expenses.Add(expenseitem);
            }
        }
    }
}
