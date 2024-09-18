namespace ExpenseApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void AddBudget_Clicked(object sender, EventArgs e)
        {

            var newExpenseDialog =  new AddNewExpensePage();
            await Navigation.PushModalAsync(newExpenseDialog);
        }
    }

}
