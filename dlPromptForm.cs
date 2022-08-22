using System;
using System.Windows.Forms;

/* Creates a form to prompt user for 3lap or flap ghost loading
 * For the example code to this, see here:
 * https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.combobox?view=windowsdesktop-6.0
 * 
 * If using .Net Core 6.0 and you cannot use System.Windows.Forms, see this:
 * https://stackoverflow.com/a/70466224
*/

namespace Win32Form1Namespace
{

    public class Win32Form1 : System.Windows.Forms.Form
    {
        public string type_result = "NA";
        private Button flapButton;
        private Button tlapButton;
        private Label label1;

        public Win32Form1()
        {
            this.InitializeComponent();
        }

        [System.STAThreadAttribute()]
        public static void tMain()
        {
            System.Windows.Forms.Application.Run(new Win32Form1());
        }

        private void InitializeComponent()
        {
            //Fast lap button
            this.flapButton = new Button();
            this.flapButton.Location = new System.Drawing.Point(248, 32);
            this.flapButton.Size = new System.Drawing.Size(400, 24);
            this.flapButton.TabIndex = 1;
            this.flapButton.Text = "Load Fast Lap Ghost";
            this.flapButton.Click += new System.EventHandler(this.flapButton_Click);

            //three lap button
            this.tlapButton = new Button();
            this.tlapButton.Location = new System.Drawing.Point(248, 64);
            this.tlapButton.Size = new System.Drawing.Size(400, 24);
            this.tlapButton.TabIndex = 2;
            this.tlapButton.Text = "Load 3-Lap Ghost";
            this.tlapButton.Click += new System.EventHandler(this.threeLapButton_Click);

            //Label
            this.label1 = new Label();
            this.label1.Location = new System.Drawing.Point(8, 224);
            this.label1.Size = new System.Drawing.Size(144, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test Label";

            //Others?
            

            //Combine all
            this.ClientSize = new System.Drawing.Size(800, 273);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                        this.flapButton,
                        this.tlapButton,
                        this.label1,
                        });
            this.Text = "Parent text?";

        }

        private void flapButton_Click(object sender, System.EventArgs e)
        {
            type_result = "flap";
            this.Close();
        }
        private void threeLapButton_Click(object sender, System.EventArgs e)
        {
            type_result = "3lap";
            this.Close();
        }
    }
}
