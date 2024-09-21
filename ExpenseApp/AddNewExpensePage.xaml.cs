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
    string expenseFilename = Path.Combine(FileSystem.AppDataDirectory, "Expense.txt");
    public AddNewExpensePage()
	{
		InitializeComponent();
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