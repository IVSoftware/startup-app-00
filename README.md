You show a registry key assignment that is potentially problematic.

First, it attempts to set the _default_ value of `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run` when it should be making named key for your startup app e.g. 

![regedit-annotated]()

Second, it looks like something that might get called in the startup app itself. Microsoft [documentation](https://learn.microsoft.com/en-us/windows/win32/setupapi/run-and-runonce-registry-keys) says don't do that.

>Run and RunOnce Registry Keys - 
A program that is run from any of these keys should not write to the key during its execution because this will interfere with the execution of other programs registered under the key. Applications should use the RunOnce key only for transient conditions, such as to complete application setup. An application must not continually recreate entries under RunOnce because this will interfere with Windows Setup.

Instead, we'll "do the right thing" by establishing this key in the installer project for the app:

![do-the-right-thing]()

Moving on...
***
**Settings**

Solving the _specific_ question **settings don't appear on windows startup** is impeded by the `string readable = EncryptionHelper.Decrypt(Properties.Settings.Default.dbpassword)` line. The points made by Panagiotis Kanavos are excellent, but notice that we're talking about _that_ now instead of the question you asked. I suggest you solve the main issue _first_ using a [Minimal Reproducible Example](https://stackoverflow.com/help/minimal-reproducible-example) that leaves out the authentication scheme.

Looking at the straightforward case of two startup settings:

![settings]()

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

![screenshot]()

_So here's the thing...
***

This goes beyond what your code shows, but this comes from my experience inb these things. It would "make sense" that if you have a default UID and Password then you "probably" have a mechanism for saving changes to these properties. Something like:



