namespace ExpenseApp;

public partial class Budget : ContentPage

{
    string fileName = Path.Combine(FileSystem.AppDataDirectory, "Expense.txt");

    public Budget(string s)
    {
        InitializeComponent();
        if (s != "0") {
            DecimalEntry.Text = s;
        }
        
    }
    private async void Savebutton_Clicked(object sender, EventArgs e)
    {   
        //bring the budget from Entry
        String input = DecimalEntry.Text;
        if (File.Exists(fileName))
        {
            File.WriteAllText(fileName, input) ;
            
            await Navigation.PopAsync();
        }
        else
        {
            File.WriteAllText(fileName, input);
            await Navigation.PopAsync();
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