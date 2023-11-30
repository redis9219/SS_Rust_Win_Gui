namespace SS_Rust_Win_Gui
{
    partial class FormLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            操作ToolStripMenuItem = new ToolStripMenuItem();
            清除日志ToolStripMenuItem = new ToolStripMenuItem();
            richTextBox1 = new RichTextBox();
            panel1 = new Panel();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 操作ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            操作ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 清除日志ToolStripMenuItem });
            操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            操作ToolStripMenuItem.Size = new Size(53, 24);
            操作ToolStripMenuItem.Text = "操作";
            // 
            // 清除日志ToolStripMenuItem
            // 
            清除日志ToolStripMenuItem.Name = "清除日志ToolStripMenuItem";
            清除日志ToolStripMenuItem.Size = new Size(224, 26);
            清除日志ToolStripMenuItem.Text = "清除日志";
            清除日志ToolStripMenuItem.Click += 清除日志ToolStripMenuItem_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.Black;
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.ForeColor = Color.Lime;
            richTextBox1.Location = new Point(10, 10);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(780, 402);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(richTextBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 28);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(800, 422);
            panel1.TabIndex = 2;
            // 
            // FormLog
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormLog";
            Text = "FormLog";
            Load += FormLog_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 操作ToolStripMenuItem;
        private ToolStripMenuItem 清除日志ToolStripMenuItem;
        private RichTextBox richTextBox1;
        private Panel panel1;
    }
}