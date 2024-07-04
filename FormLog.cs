namespace SS_Rust_Win_Gui
{
    public partial class FormLog : Form
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public FormLog()
        {
            InitializeComponent();
        }

        private void FormLog_Load(object sender, EventArgs e)
        {
            Logger.Info("View Log");
            using (var fileStream = new FileStream(Program.LogFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Console.WriteLine(line);
                    richTextBox1.AppendText(string.Format("{0}\r", line));
                }
            }
            TextBoxWriter textBoxWriter = new(richTextBox1);
            Console.SetOut(textBoxWriter);
        }

        private void 清除日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo file = new(Program.LogFileName);
            file.Delete();
            richTextBox1.Text = "";

        }
    }

    public class TextBoxWriter(RichTextBox box) : TextWriter
    {
        readonly RichTextBox txtBox = box;
        delegate void VoidAction();

        public override void Write(char value)
        {
            VoidAction action = delegate
            {
                txtBox.AppendText(value.ToString());
                txtBox.Select(txtBox.TextLength, 0);
                txtBox.ScrollToCaret();
            };
            if (txtBox.IsHandleCreated) {
                txtBox.BeginInvoke(action);
            }
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
