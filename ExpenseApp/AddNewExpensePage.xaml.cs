using ExpenseApp.Model;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseApp;

public partial class AddNewExpensePage : ContentPage
{
    public string ItemId
    {
        set { LoadExpenseItem(value); }
    }
   
    CategoryItem selectedCategory = new CategoryItem();
    string expenseFilename = Path.Combine(FileSystem.AppDataDirectory, "Expense.txt");
    public AddNewExpensePage()
    {
        InitializeComponent();
        var randomFileName = $"{Path.GetRandomFileName()}.expenseitem.txt";
        LoadExpenseItem(Path.Combine(FileSystem.AppDataDirectory, randomFileName));

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
        if (BindingContext is Model.ExpenseItem expenseitem)
        {
            Debug.WriteLine(Date.Date.ToShortDateString());
            Debug.WriteLine(Date.Date.ToString("yyyy-MM-dd"));
            string expenseData = $"{Name.Text},{Amount.Text},{Date.Date.ToString("yyyy-MM-dd")},{selectedCategory.CategoryName}";
            File.WriteAllText(expenseitem.Itemname, expenseData);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void categoryListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedCategory = e.SelectedItem as CategoryItem;

    }
    private void LoadExpenseItem(string itemName)
    {
        var expenseItemModel = new Model.ExpenseItem();
        expenseItemModel.Itemname = itemName;

        if (File.Exists(itemName))
        {
            expenseItemModel.Date = File.GetCreationTime(itemName);
            expenseItemModel.Itemname = File.ReadAllText(itemName);
            expenseItemModel.ItemFileName = itemName;
        }

        BindingContext = expenseItemModel;
    }
}