namespace ExpenseApp;

public partial class Budget : ContentPage

{
    string fileName = Path.Combine(FileSystem.AppDataDirectory, "Expense.txt");
    public Budget()
    {
        InitializeComponent();
        if (File.Exists(fileName))
        {
            string fileContent = File.ReadAllText(fileName);
            if (double.TryParse(fileContent, out double number))
            {
                var newPage = new Budget();
                Navigation.PushAsync(newPage);
            }
            else
            {
                DisplayAlert("Invlid Data", "Invlid Data","OK");
            }
        }
    }
    private async void Savebutton_Clicked(object sender, EventArgs e)
    {   
        //bring the budget from Entry
        String input = DecimalEntry.Text;

        if (decimal.TryParse(input, out decimal budget))
        {
            MessagingCenter.Send(this, "budgetUpdated", budget);
            await Navigation.PopAsync(); //move previous page
        }
        else
        {
            await DisplayAlert("Invalid Input", "Please enter a valid budget amount.", "OK");
        }


    }

    //check if it number or not
    private void DecimalEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        string input = e.NewTextValue;
        if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^\d*\.?\d{0,2}$"))
        {
            DecimalEntry.TextColor = Colors.Black;
        }
        else
        {
            DecimalEntry.Text = e.OldTextValue;
            DecimalEntry.TextColor = Colors.Red;
        }
    }
}