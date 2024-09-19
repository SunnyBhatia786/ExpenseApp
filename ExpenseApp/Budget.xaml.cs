namespace ExpenseApp;

public partial class Budget : ContentPage

{
    string fileName = Path.Combine(FileSystem.AppDataDirectory, "Expense.txt");
	public Budget()
	{
		InitializeComponent();
     
            
        
	}

    private void Savebutton_Clicked(object sender, EventArgs e)
    {
        

    }
}