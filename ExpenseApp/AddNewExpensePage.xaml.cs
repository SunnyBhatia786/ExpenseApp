namespace ExpenseApp;

public partial class AddNewExpensePage : ContentPage
{
	public AddNewExpensePage()
	{
		InitializeComponent();
	}

    private async void Save_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}