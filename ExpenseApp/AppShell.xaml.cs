namespace ExpenseApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddNewExpensePage), typeof(AddNewExpensePage));
        }
    }
}
