namespace startup_app_00
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxLoginUsername.Text = Properties.Settings.Default.dbusername;
            textBoxLoginPassword.Text = Properties.Settings.Default.dbpassword;
            buttonLogin.Click += onClickLogin;
        }

        private void onClickLogin(object? sender, EventArgs e)
        {
            if(tryLoginWithCredentials())
            {
                Properties.Settings.Default.dbusername = textBoxLoginUsername.Text;
                Properties.Settings.Default.dbpassword = textBoxLoginUsername.Text;
                Properties.Settings.Default.Save();
                Text = $"Logged in as {Properties.Settings.Default.dbusername}";
            }
        }

        private bool tryLoginWithCredentials() => true; // Succeeded (for testing purposes).
    }
}