Your code shows a registry key assignment that is potentially problematic.

First, it attempts to set the _default_ ("") value of
    `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`

It _should_ be making named key for your startup app e.g. 

[![regedit-annotated][1]][1]

Second, is it being set in the startup app itself? This is contrary to Microsoft [documentation](https://learn.microsoft.com/en-us/windows/win32/setupapi/run-and-runonce-registry-keys).

>Run and RunOnce Registry Keys - 
A program that is run from any of these keys should not write to the key during its execution because this will interfere with the execution of other programs registered under the key. Applications should use the RunOnce key only for transient conditions, such as to complete application setup. An application must not continually recreate entries under RunOnce because this will interfere with Windows Setup.

Instead, we'll "do the right thing" by establishing this key in the installer project for the app:

[![do-the-right-thing][2]][2]

Moving on...
***
**Settings**

Showing a "mystery line" like:

`string readable = EncryptionHelper.Decrypt(Properties.Settings.Default.dbpassword)`

makes it more difficult to diagnose the question that you _actually asked_. The points made by Panagiotis Kanavos are excellent, but notice that we're talking about _that_ now instead of your original ask. I suggest you solve the main issue _first_ using a [Minimal Reproducible Example](https://stackoverflow.com/help/minimal-reproducible-example) that leaves out the authentication scheme. Then we can look at the straightforward case of two startup settings:

[![settings][3]][3]

The textboxes are initialized here:

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxLoginUsername.Text = Properties.Settings.Default.dbusername;
            textBoxLoginPassword.Text = Properties.Settings.Default.dbpassword;
        }
    }

And after _installing_ the app and restarting the PC we see that things are "so far so good".

[![restart][4]][4]

***
**Things to check**

Without seeing more code, I can only make general suggestions.

1. Consider writing a startup log file to your app's AppData folder tracing values at various execution points.
2. Look for uncaught or "swallowed" exceptions that might skip your textbox initialization code.
3. Check for race condition where `Properties.Settings.Default.Save()` might be being called before textboxes are initialized.
4. Since there's "probably" a mechanism for saving login changes, make sure any event handlers are attached _after_ the `InitializeComponents` has run. For example:

*Initializing a Persist scheme*

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


  [1]: https://i.stack.imgur.com/wT7Rz.png
  [2]: https://i.stack.imgur.com/fQCG3.png
  [3]: https://i.stack.imgur.com/vQBQG.png
  [4]: https://i.stack.imgur.com/nTrnM.png