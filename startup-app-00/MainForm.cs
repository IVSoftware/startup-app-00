namespace startup_app_00
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxLoginUsername.Text = Properties.Settings.Default.dbusername;
            textBoxLoginPassword.Text = Properties.Settings.Default.dbpassword;

            // Commit changes
            textBoxLoginUsername.KeyDown += TextBoxLoginUsername_KeyDown;
            textBoxLoginPassword.KeyDown += TextBoxLoginPassword_KeyDown;
        }

        private void TextBoxLoginUsername_KeyDown(object? sender, KeyEventArgs e)
        {
            if(e.KeyData.Equals(Keys.Enter)) 
            {
                Properties.Settings.Default.dbusername = textBoxLoginUsername.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void TextBoxLoginPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                Properties.Settings.Default.dbpassword = textBoxLoginUsername.Text;
                Properties.Settings.Default.Save();
            }
        }
    }
}