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
            await Navigation.PushAsync(new Budget());
        }
    }

}
