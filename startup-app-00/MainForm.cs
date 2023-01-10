namespace startup_app_00
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.Textbox1;
            textBox2.Text = Properties.Settings.Default.Textbox2;
        }
    }
}