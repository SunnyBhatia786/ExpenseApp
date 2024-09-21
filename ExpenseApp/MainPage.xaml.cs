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
    }

}
