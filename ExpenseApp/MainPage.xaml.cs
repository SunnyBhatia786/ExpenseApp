using Android.Accounts;
using Microsoft.Maui.Storage;
using System.Diagnostics;
using static Java.Util.Jar.Attributes;

namespace ExpenseApp
{
    public partial class MainPage : ContentPage
    {
        string fileName = Path.Combine(FileSystem.AppDataDirectory, "Expense.txt");
        Double remainingBudget = 0;

        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = new Model.AllExpenseItem();
        }
        private void HandleViewWithBudget()
        {
            BudgetTitle.IsVisible = false;
            budgetLabel.IsVisible = true;
            Budgetbtn.IsVisible = false;
            EditBudget.IsVisible = true;
            AddNewExpense.IsVisible = true;
            DeleteButton.IsVisible = true;
        }
        private void HandleViewWithoutBudget()
        {
            BudgetTitle.IsVisible = true;
            budgetLabel.IsVisible = false;
            Budgetbtn.IsVisible = true;
            AddNewExpense.IsVisible = false;
            EditBudget.IsVisible = false;
            DeleteButton.IsVisible = false;
        }
        private void RecalculateRemainingBudget()
        {
            string fileContent = File.ReadAllText(fileName);
            double sum = 0;
            foreach (var item in ((Model.AllExpenseItem)BindingContext).Expenses)
            {
                string amt = item.Amount.Substring(1);
                sum = sum + Double.Parse(amt);
            }
            remainingBudget = double.Parse(fileContent) - sum;
        }
        protected override void OnAppearing()
        {
            ((Model.AllExpenseItem)BindingContext).LoadExpenseItem();
            if (File.Exists(fileName))
            {
                RecalculateRemainingBudget();

                string fileContent = File.ReadAllText(fileName);
                if (double.TryParse(fileContent, out double number))
                {
                    HandleViewWithBudget();
                    if (remainingBudget < 0.0)
                    {
                        budgetLabel.Text = $"You have exceeded your budget by {remainingBudget}";
                    }
                    else {
                        budgetLabel.Text = $"Your budget is: {remainingBudget}";
                    }
                    
                }
                else
                {
                    DisplayAlert("Invlid Data", "Invlid Data", "OK");
                }
            }
            else
            {
                HandleViewWithoutBudget();
            }
        }

        private async void AddBudget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Budget("0"));
        }

        private async void AddNewExpense_Clicked(object sender, EventArgs e)
        {
            var newExpenseDialog = new AddNewExpensePage();
            await Navigation.PushModalAsync(newExpenseDialog);
        }
        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            ((Model.AllExpenseItem)BindingContext).DeleteAllExpenses();
            RecalculateRemainingBudget();
            budgetLabel.Text = $"Your budget is: {remainingBudget}";
        }
        private async void EditBudget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Budget($"{remainingBudget}"));
        }
    }
}
