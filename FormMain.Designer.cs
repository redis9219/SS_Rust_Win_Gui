namespace SS_Rust_Win_Gui
{
    partial class FormMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            button_add = new Button();
            button_remove = new Button();
            button_copy = new Button();
            button_m_up = new Button();
            button_m_down = new Button();
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            s_server_timout = new TextBox();
            label3 = new Label();
            s_server_remark = new TextBox();
            s_server_plugin_opt = new TextBox();
            label4 = new Label();
            s_server_plugin = new TextBox();
            s_server_name = new TextBox();
            label7 = new Label();
            s_server_port = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label8 = new Label();
            s_server_method = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            button_switch_pwd = new Button();
            s_server_passwd = new TextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            s_local_port = new TextBox();
            label9 = new Label();
            tableLayoutPanel7 = new TableLayoutPanel();
            button_apply = new Button();
            button_cancel = new Button();
            label_save_msg = new Label();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewLinkColumn();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            服务器ToolStripMenuItem = new ToolStripMenuItem();
            无ToolStripMenuItem = new ToolStripMenuItem();
            剪贴板导入ss链接ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            打开配置界面ToolStripMenuItem = new ToolStripMenuItem();
            查看日志ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            开机启动ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            开启系统代理ToolStripMenuItem = new ToolStripMenuItem();
            允许局域网访问ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // button_add
            // 
            button_add.Dock = DockStyle.Fill;
            button_add.Location = new Point(2, 3);
            button_add.Margin = new Padding(2, 3, 2, 3);
            button_add.Name = "button_add";
            button_add.Size = new Size(171, 25);
            button_add.TabIndex = 1;
            button_add.Text = "添加";
            button_add.UseVisualStyleBackColor = true;
            button_add.Click += Button_add_Click;
            // 
            // button_remove
            // 
            button_remove.Dock = DockStyle.Fill;
            button_remove.Location = new Point(177, 3);
            button_remove.Margin = new Padding(2, 3, 2, 3);
            button_remove.Name = "button_remove";
            button_remove.Size = new Size(171, 25);
            button_remove.TabIndex = 1;
            button_remove.Text = "删除";
            button_remove.UseVisualStyleBackColor = true;
            button_remove.Click += Button_remove_Click;
            // 
            // button_copy
            // 
            button_copy.Dock = DockStyle.Fill;
            button_copy.Location = new Point(2, 34);
            button_copy.Margin = new Padding(2, 3, 2, 3);
            button_copy.Name = "button_copy";
            button_copy.Size = new Size(171, 25);
            button_copy.TabIndex = 1;
            button_copy.Text = "复制";
            button_copy.UseVisualStyleBackColor = true;
            button_copy.Click += Button_copy_Click;
            // 
            // button_m_up
            // 
            button_m_up.Dock = DockStyle.Fill;
            button_m_up.Location = new Point(2, 65);
            button_m_up.Margin = new Padding(2, 3, 2, 3);
            button_m_up.Name = "button_m_up";
            button_m_up.Size = new Size(171, 26);
            button_m_up.TabIndex = 1;
            button_m_up.Text = "上移";
            button_m_up.UseVisualStyleBackColor = true;
            button_m_up.Click += Button_m_up_Click;
            // 
            // button_m_down
            // 
            button_m_down.Dock = DockStyle.Fill;
            button_m_down.Location = new Point(177, 65);
            button_m_down.Margin = new Padding(2, 3, 2, 3);
            button_m_down.Name = "button_m_down";
            button_m_down.Size = new Size(171, 26);
            button_m_down.TabIndex = 1;
            button_m_down.Text = "下移";
            button_m_down.UseVisualStyleBackColor = true;
            button_m_down.Click += Button_m_down_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(358, 3);
            groupBox1.Margin = new Padding(2, 3, 2, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 3, 2, 3);
            groupBox1.Size = new Size(346, 283);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "服务器";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(label2, 0, 1);
            tableLayoutPanel3.Controls.Add(s_server_timout, 1, 7);
            tableLayoutPanel3.Controls.Add(label3, 0, 2);
            tableLayoutPanel3.Controls.Add(s_server_remark, 1, 6);
            tableLayoutPanel3.Controls.Add(s_server_plugin_opt, 1, 5);
            tableLayoutPanel3.Controls.Add(label4, 0, 3);
            tableLayoutPanel3.Controls.Add(s_server_plugin, 1, 4);
            tableLayoutPanel3.Controls.Add(s_server_name, 1, 0);
            tableLayoutPanel3.Controls.Add(label7, 0, 6);
            tableLayoutPanel3.Controls.Add(s_server_port, 1, 1);
            tableLayoutPanel3.Controls.Add(label6, 0, 5);
            tableLayoutPanel3.Controls.Add(label5, 0, 4);
            tableLayoutPanel3.Controls.Add(label8, 0, 7);
            tableLayoutPanel3.Controls.Add(s_server_method, 1, 3);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 1, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(2, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 9;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(342, 261);
            tableLayoutPanel3.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(2, 6);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "服务器地址";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(2, 36);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 0;
            label2.Text = "服务器端口";
            // 
            // s_server_timout
            // 
            s_server_timout.Dock = DockStyle.Fill;
            s_server_timout.Location = new Point(82, 213);
            s_server_timout.Margin = new Padding(2, 3, 2, 3);
            s_server_timout.Name = "s_server_timout";
            s_server_timout.Size = new Size(258, 23);
            s_server_timout.TabIndex = 1;
            s_server_timout.KeyPress += NumberBox_KeyPress;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(2, 66);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(32, 17);
            label3.TabIndex = 0;
            label3.Text = "密码";
            // 
            // s_server_remark
            // 
            s_server_remark.Dock = DockStyle.Fill;
            s_server_remark.Location = new Point(82, 183);
            s_server_remark.Margin = new Padding(2, 3, 2, 3);
            s_server_remark.Name = "s_server_remark";
            s_server_remark.Size = new Size(258, 23);
            s_server_remark.TabIndex = 1;
            // 
            // s_server_plugin_opt
            // 
            s_server_plugin_opt.Dock = DockStyle.Fill;
            s_server_plugin_opt.Location = new Point(82, 153);
            s_server_plugin_opt.Margin = new Padding(2, 3, 2, 3);
            s_server_plugin_opt.Name = "s_server_plugin_opt";
            s_server_plugin_opt.Size = new Size(258, 23);
            s_server_plugin_opt.TabIndex = 1;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(2, 96);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(32, 17);
            label4.TabIndex = 0;
            label4.Text = "加密";
            // 
            // s_server_plugin
            // 
            s_server_plugin.Dock = DockStyle.Fill;
            s_server_plugin.Location = new Point(82, 123);
            s_server_plugin.Margin = new Padding(2, 3, 2, 3);
            s_server_plugin.Name = "s_server_plugin";
            s_server_plugin.Size = new Size(258, 23);
            s_server_plugin.TabIndex = 1;
            // 
            // s_server_name
            // 
            s_server_name.Dock = DockStyle.Fill;
            s_server_name.Location = new Point(82, 3);
            s_server_name.Margin = new Padding(2, 3, 2, 3);
            s_server_name.Name = "s_server_name";
            s_server_name.Size = new Size(258, 23);
            s_server_name.TabIndex = 1;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(2, 186);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(32, 17);
            label7.TabIndex = 0;
            label7.Text = "备注";
            // 
            // s_server_port
            // 
            s_server_port.Dock = DockStyle.Fill;
            s_server_port.Location = new Point(82, 33);
            s_server_port.Margin = new Padding(2, 3, 2, 3);
            s_server_port.MaxLength = 5;
            s_server_port.Name = "s_server_port";
            s_server_port.Size = new Size(258, 23);
            s_server_port.TabIndex = 1;
            s_server_port.KeyPress += NumberBox_KeyPress;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(2, 156);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(56, 17);
            label6.TabIndex = 0;
            label6.Text = "插件选项";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(2, 126);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(56, 17);
            label5.TabIndex = 0;
            label5.Text = "插件程序";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new Point(2, 216);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(32, 17);
            label8.TabIndex = 0;
            label8.Text = "超时";
            // 
            // s_server_method
            // 
            s_server_method.Dock = DockStyle.Fill;
            s_server_method.FormattingEnabled = true;
            s_server_method.Location = new Point(82, 92);
            s_server_method.Margin = new Padding(2);
            s_server_method.Name = "s_server_method";
            s_server_method.Size = new Size(258, 25);
            s_server_method.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Controls.Add(button_switch_pwd, 1, 0);
            tableLayoutPanel1.Controls.Add(s_server_passwd, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(80, 60);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(262, 30);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // button_switch_pwd
            // 
            button_switch_pwd.Dock = DockStyle.Fill;
            button_switch_pwd.Image = Properties.Resources.no_eye;
            button_switch_pwd.Location = new Point(224, 2);
            button_switch_pwd.Margin = new Padding(2);
            button_switch_pwd.Name = "button_switch_pwd";
            button_switch_pwd.Size = new Size(36, 26);
            button_switch_pwd.TabIndex = 2;
            button_switch_pwd.UseVisualStyleBackColor = true;
            button_switch_pwd.Click += Button_switch_pwd_Click;
            // 
            // s_server_passwd
            // 
            s_server_passwd.Dock = DockStyle.Fill;
            s_server_passwd.Location = new Point(2, 3);
            s_server_passwd.Margin = new Padding(2, 3, 2, 3);
            s_server_passwd.Name = "s_server_passwd";
            s_server_passwd.PasswordChar = '*';
            s_server_passwd.Size = new Size(218, 23);
            s_server_passwd.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(button_add, 0, 0);
            tableLayoutPanel2.Controls.Add(button_m_down, 1, 2);
            tableLayoutPanel2.Controls.Add(button_m_up, 0, 2);
            tableLayoutPanel2.Controls.Add(button_copy, 0, 1);
            tableLayoutPanel2.Controls.Add(button_remove, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 292);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Size = new Size(350, 94);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel4.Controls.Add(groupBox1, 1, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 1, 1);
            tableLayoutPanel4.Controls.Add(panel1, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(10, 10);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel4.Size = new Size(706, 389);
            tableLayoutPanel4.TabIndex = 5;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel7, 0, 2);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(359, 292);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Size = new Size(344, 94);
            tableLayoutPanel5.TabIndex = 3;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(s_local_port, 1, 0);
            tableLayoutPanel6.Controls.Add(label9, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(344, 31);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // s_local_port
            // 
            s_local_port.Anchor = AnchorStyles.Left;
            s_local_port.Location = new Point(82, 4);
            s_local_port.Margin = new Padding(2, 3, 2, 3);
            s_local_port.MaxLength = 5;
            s_local_port.Name = "s_local_port";
            s_local_port.Size = new Size(121, 23);
            s_local_port.TabIndex = 1;
            s_local_port.KeyPress += NumberBox_KeyPress;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Location = new Point(2, 7);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(56, 17);
            label9.TabIndex = 0;
            label9.Text = "代理端口";
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 4;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel7.Controls.Add(button_apply, 3, 0);
            tableLayoutPanel7.Controls.Add(button_cancel, 2, 0);
            tableLayoutPanel7.Controls.Add(label_save_msg, 0, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(0, 62);
            tableLayoutPanel7.Margin = new Padding(0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel7.Size = new Size(344, 32);
            tableLayoutPanel7.TabIndex = 1;
            // 
            // button_apply
            // 
            button_apply.Dock = DockStyle.Fill;
            button_apply.Location = new Point(266, 3);
            button_apply.Margin = new Padding(2, 3, 2, 3);
            button_apply.Name = "button_apply";
            button_apply.Size = new Size(76, 26);
            button_apply.TabIndex = 3;
            button_apply.Text = "保存";
            button_apply.UseVisualStyleBackColor = true;
            button_apply.Click += Button_apply_Click;
            // 
            // button_cancel
            // 
            button_cancel.Dock = DockStyle.Fill;
            button_cancel.Location = new Point(186, 3);
            button_cancel.Margin = new Padding(2, 3, 2, 3);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(76, 26);
            button_cancel.TabIndex = 3;
            button_cancel.Text = "最小化";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += Button_cancel_Click;
            // 
            // label_save_msg
            // 
            label_save_msg.Anchor = AnchorStyles.Right;
            label_save_msg.AutoSize = true;
            label_save_msg.Location = new Point(101, 7);
            label_save_msg.Name = "label_save_msg";
            label_save_msg.Size = new Size(0, 17);
            label_save_msg.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(dataGridView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(2, 2);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 285);
            panel1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column3, Column2, Column4 });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(348, 281);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.DataPropertyName = "server";
            Column1.HeaderText = "地址";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "状态";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 60;
            // 
            // Column2
            // 
            Column2.HeaderText = "延迟";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 60;
            // 
            // Column4
            // 
            Column4.HeaderText = "操作";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Text = "连接";
            Column4.UseColumnTextForLinkValue = true;
            Column4.Width = 60;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "SS-Rust-Win-Gui";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += NotifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 服务器ToolStripMenuItem, 剪贴板导入ss链接ToolStripMenuItem, toolStripSeparator4, 打开配置界面ToolStripMenuItem, 查看日志ToolStripMenuItem, toolStripSeparator1, 开机启动ToolStripMenuItem, toolStripSeparator2, 开启系统代理ToolStripMenuItem, 允许局域网访问ToolStripMenuItem, toolStripSeparator3, 退出ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(186, 204);
            // 
            // 服务器ToolStripMenuItem
            // 
            服务器ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 无ToolStripMenuItem });
            服务器ToolStripMenuItem.Name = "服务器ToolStripMenuItem";
            服务器ToolStripMenuItem.Size = new Size(185, 22);
            服务器ToolStripMenuItem.Text = "服务器";
            // 
            // 无ToolStripMenuItem
            // 
            无ToolStripMenuItem.Name = "无ToolStripMenuItem";
            无ToolStripMenuItem.Size = new Size(88, 22);
            无ToolStripMenuItem.Text = "无";
            // 
            // 剪贴板导入ss链接ToolStripMenuItem
            // 
            剪贴板导入ss链接ToolStripMenuItem.Name = "剪贴板导入ss链接ToolStripMenuItem";
            剪贴板导入ss链接ToolStripMenuItem.Size = new Size(185, 22);
            剪贴板导入ss链接ToolStripMenuItem.Text = "剪贴板导入ss://链接";
            剪贴板导入ss链接ToolStripMenuItem.Click += 剪贴板导入ss链接ToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(182, 6);
            // 
            // 打开配置界面ToolStripMenuItem
            // 
            打开配置界面ToolStripMenuItem.Name = "打开配置界面ToolStripMenuItem";
            打开配置界面ToolStripMenuItem.Size = new Size(185, 22);
            打开配置界面ToolStripMenuItem.Text = "打开配置界面";
            打开配置界面ToolStripMenuItem.Click += 打开配置界面ToolStripMenuItem_Click;
            // 
            // 查看日志ToolStripMenuItem
            // 
            查看日志ToolStripMenuItem.Name = "查看日志ToolStripMenuItem";
            查看日志ToolStripMenuItem.Size = new Size(185, 22);
            查看日志ToolStripMenuItem.Text = "查看日志";
            查看日志ToolStripMenuItem.Click += 查看日志ToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(182, 6);
            // 
            // 开机启动ToolStripMenuItem
            // 
            开机启动ToolStripMenuItem.CheckOnClick = true;
            开机启动ToolStripMenuItem.Name = "开机启动ToolStripMenuItem";
            开机启动ToolStripMenuItem.Size = new Size(185, 22);
            开机启动ToolStripMenuItem.Text = "开机启动";
            开机启动ToolStripMenuItem.Click += 开机启动ToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(182, 6);
            // 
            // 开启系统代理ToolStripMenuItem
            // 
            开启系统代理ToolStripMenuItem.CheckOnClick = true;
            开启系统代理ToolStripMenuItem.Name = "开启系统代理ToolStripMenuItem";
            开启系统代理ToolStripMenuItem.Size = new Size(185, 22);
            开启系统代理ToolStripMenuItem.Text = "开启系统代理";
            开启系统代理ToolStripMenuItem.Click += 开启系统代理ToolStripMenuItem_Click;
            // 
            // 允许局域网访问ToolStripMenuItem
            // 
            允许局域网访问ToolStripMenuItem.CheckOnClick = true;
            允许局域网访问ToolStripMenuItem.Name = "允许局域网访问ToolStripMenuItem";
            允许局域网访问ToolStripMenuItem.Size = new Size(185, 22);
            允许局域网访问ToolStripMenuItem.Text = "允许局域网访问";
            允许局域网访问ToolStripMenuItem.Click += 允许局域网访问ToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(182, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(185, 22);
            退出ToolStripMenuItem.Text = "退出";
            退出ToolStripMenuItem.Click += 退出ToolStripMenuItem_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(726, 409);
            Controls.Add(tableLayoutPanel4);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            MinimumSize = new Size(675, 448);
            Name = "FormMain";
            Padding = new Padding(10);
            Text = "Main";
            FormClosing += Main_FormClosing;
            Load += Main_Load;
            groupBox1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button button_add;
        private Button button_remove;
        private Button button_copy;
        private Button button_m_up;
        private Button button_m_down;
        private GroupBox groupBox1;
        private TextBox s_server_remark;
        private Label label7;
        private TextBox s_server_plugin_opt;
        private Label label6;
        private Label label4;
        private TextBox s_server_plugin;
        private Label label5;
        private TextBox s_server_passwd;
        private Label label3;
        private TextBox s_server_port;
        private Label label2;
        private TextBox s_server_name;
        private Label label1;
        private TextBox s_server_timout;
        private Label label8;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 打开配置界面ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ComboBox s_server_method;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button_switch_pwd;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem 开机启动ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem 开启系统代理ToolStripMenuItem;
        private ToolStripMenuItem 允许局域网访问ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem 查看日志ToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem 服务器ToolStripMenuItem;
        private ToolStripMenuItem 无ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem 剪贴板导入ss链接ToolStripMenuItem;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewLinkColumn Column4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private TextBox s_local_port;
        private Label label9;
        private TableLayoutPanel tableLayoutPanel7;
        private Button button_apply;
        private Button button_cancel;
        private Label label_save_msg;
    }
}