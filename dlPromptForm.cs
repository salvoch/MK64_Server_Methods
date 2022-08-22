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
        private Button nolapButton;
        private Label label1;

        public Win32Form1()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.flapButton = new System.Windows.Forms.Button();
            this.tlapButton = new System.Windows.Forms.Button();
            this.nolapButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flapButton
            // 
            this.flapButton.Location = new System.Drawing.Point(10, 50);
            this.flapButton.Name = "flapButton";
            this.flapButton.Size = new System.Drawing.Size(100, 24);
            this.flapButton.TabIndex = 1;
            this.flapButton.Text = "Fast Lap Ghost";
            this.flapButton.Click += new System.EventHandler(this.flapButton_Click);
            // 
            // tlapButton
            // 
            this.tlapButton.Location = new System.Drawing.Point(120, 50);
            this.tlapButton.Name = "tlapButton";
            this.tlapButton.Size = new System.Drawing.Size(100, 24);
            this.tlapButton.TabIndex = 2;
            this.tlapButton.Text = "3-Lap Ghost";
            this.tlapButton.Click += new System.EventHandler(this.threeLapButton_Click);
            // 
            // nolapButton
            // 
            this.nolapButton.Location = new System.Drawing.Point(65, 85);
            this.nolapButton.Name = "nolapButton";
            this.nolapButton.Size = new System.Drawing.Size(100, 24);
            this.nolapButton.TabIndex = 3;
            this.nolapButton.Text = "None";
            this.nolapButton.Click += new System.EventHandler(this.noLapButton_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select which type of ghost you would like to load";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Win32Form1
            // 
            this.ClientSize = new System.Drawing.Size(230, 120);
            this.Controls.Add(this.flapButton);
            this.Controls.Add(this.tlapButton);
            this.Controls.Add(this.nolapButton);
            this.Controls.Add(this.label1);
            this.Name = "Win32Form1";
            this.Text = "Ghost type?";
            this.ResumeLayout(false);

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
        private void noLapButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
