namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show("Действительно закрыть?", "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
        }
    }
}