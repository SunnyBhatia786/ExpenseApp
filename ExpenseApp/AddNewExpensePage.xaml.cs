using ExpenseApp.Model;
using System.Diagnostics;

namespace ExpenseApp;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class AddNewExpensePage : ContentPage
{
    public string ItemId
    {
        set { LoadExpenseItem(value); }
    }
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
            String expenseData = $"{Name.Text},{Amount.Text}";
            File.WriteAllText(expenseitem.Itemname, expenseData);
        }

        await Shell.Current.GoToAsync("..");
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
    private void LoadExpenseItem(string itemName)
    {
        var expenseItemModel = new Model.ExpenseItem();
        expenseItemModel.Itemname = itemName;

        if (File.Exists(itemName))
        {
            expenseItemModel.Date = File.GetCreationTime(itemName);
            expenseItemModel.Itemname = File.ReadAllText(itemName);
        }

        BindingContext = expenseItemModel;
    }
}