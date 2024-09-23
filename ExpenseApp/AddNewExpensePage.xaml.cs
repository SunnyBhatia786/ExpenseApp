using ExpenseApp.Model;
using System.Diagnostics;

namespace ExpenseApp;

public partial class AddNewExpensePage : ContentPage
{
    public class CategoryItem
    {
        public string CategoryName { get; set; }
        public string IconFile { get; set; }
    }
    CategoryItem selectedCategory = new CategoryItem();
    string expenseFilename = Path.Combine(FileSystem.AppDataDirectory, "ExpenseList.txt");
    ExpenseItem[] expenseItems = new ExpenseItem[1];
    public AddNewExpensePage()
	{
		InitializeComponent();
        LoadAllExpenseItems( ref expenseItems);
        var categories = new List<CategoryItem>
        {
            new CategoryItem
            {
                CategoryName = "Restaurant",
                IconFile = "restaurant.png"
            },
            new CategoryItem {
                CategoryName = "Grocery",
                IconFile = "grocery.png"
            },
            new CategoryItem {
                CategoryName = "Gas",
                IconFile = "fuel.png"
            },
            new CategoryItem {
                CategoryName = "Rent",
                IconFile = "rent.png"
            },
             new CategoryItem {
                CategoryName = "Miscellaneous",
                IconFile = "miscellaneous.png"
            }

        };

        categoryListview.ItemsSource = categories;
        

    }

    private void LoadAllExpenseItems(ref ExpenseItem[] expenseItemsObj)
    {
        StreamReader reader = new StreamReader(expenseFilename);
        if (reader.Peek() >= 0)
        {
            int size = Convert.ToInt32(reader.ReadLine());
            expenseItemsObj = new ExpenseItem[size];
            for (int index = 0; index<expenseItemsObj.Length;  index++)
            {
                expenseItemsObj[index] = new ExpenseItem();
                expenseItemsObj[index].Name = reader.ReadLine();
                expenseItemsObj[index].Amount = reader.ReadLine();
                expenseItemsObj[index].DateOfExpense = reader.ReadLine();
                expenseItemsObj[index].Category = (Category)Enum.Parse(typeof(Category), reader.ReadLine());
                    
            }
        }
        reader.Close();
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        var item = new ExpenseItem();
        item.Name = Name.Text;
        item.Amount = Amount.Text;
        item.DateOfExpense = Date.Date.ToString("d");
        item.Category = (Category)Enum.Parse(typeof(Category), selectedCategory.CategoryName);
		await Navigation.PopModalAsync();
        saveExpenseToFile(item);
    }

    private void saveExpenseToFile(ExpenseItem item)
    {
        if (File.Exists(expenseFilename))
        {
            StreamWriter writer = new StreamWriter(expenseFilename);
            writer.WriteLine(expenseItems.Length + 1);

        }
        else
        {

        }
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void categoryListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
       selectedCategory = e.SelectedItem as CategoryItem;
        
    }
}