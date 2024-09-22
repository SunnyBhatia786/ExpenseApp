using Android.Accounts;
using static Java.Util.Jar.Attributes;

namespace ExpenseApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<Budget, decimal>(this, "budgetUpdated", (sender, budget) =>
            {
                budgetLabel.Text = $"Your budget is: {budget:C2}";
            });

            BindingContext = new Model.AllExpenseItem();
        }

        protected override void OnAppearing()
        {
            ((Model.AllExpenseItem)BindingContext).LoadExpenseItem();
        }

        private async void AddBudget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Budget());
        }

        private async void AddNewExpense_Clicked(object sender, EventArgs e)
        {
            var newExpenseDialog = new AddNewExpensePage();
            await Navigation.PushModalAsync(newExpenseDialog);
        }

        private async void expenseitemCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 0)
            {
                var expenseitem = (Model.ExpenseItem)e.CurrentSelection[0];
                await Shell.Current.GoToAsync($"{nameof(AddNewExpensePage)} ?{nameof(AddNewExpensePage.ItemId)}={expenseitem.Itemname}");
                expenseitemCollection.SelectedItem = null;
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var ListofExpense = ((Model.AllExpenseItem)BindingContext).Expenses;
            if (ListofExpense != null)
            {
                foreach (var expense in ListofExpense)
                {
                    File.Delete(expense.Itemname);
                }
            }

            await Shell.Current.GoToAsync(".."); 
        }
    }
}
