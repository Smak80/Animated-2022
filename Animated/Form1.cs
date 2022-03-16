namespace Animated
{
    public partial class Form1 : Form
    {
        private Animator a;
        public Form1()
        {
            InitializeComponent();
            a = new Animator(panel1.Size, panel1.CreateGraphics());
            a.Start();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            a.AddCircle();
        }
    }
}