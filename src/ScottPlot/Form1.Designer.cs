namespace ScottPlot
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_info = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_zoomIn = new System.Windows.Forms.Button();
            this.btn_zoomOut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_NE = new System.Windows.Forms.Button();
            this.btn_SW = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_draw = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.combo_routine = new System.Windows.Forms.ComboBox();
            this.cb_quality = new System.Windows.Forms.CheckBox();
            this.cb_animate = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel_plot = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_info.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel_plot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel_info, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_plot, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(708, 567);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel_info
            // 
            this.panel_info.Controls.Add(this.groupBox3);
            this.panel_info.Controls.Add(this.groupBox2);
            this.panel_info.Controls.Add(this.groupBox1);
            this.panel_info.Controls.Add(this.label2);
            this.panel_info.Controls.Add(this.label1);
            this.panel_info.Controls.Add(this.combo_routine);
            this.panel_info.Controls.Add(this.cb_quality);
            this.panel_info.Controls.Add(this.cb_animate);
            this.panel_info.Controls.Add(this.richTextBox1);
            this.panel_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_info.Location = new System.Drawing.Point(3, 3);
            this.panel_info.Name = "panel_info";
            this.panel_info.Size = new System.Drawing.Size(702, 94);
            this.panel_info.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flowLayoutPanel3);
            this.groupBox3.Location = new System.Drawing.Point(596, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(97, 85);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zoom";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btn_zoomIn);
            this.flowLayoutPanel3.Controls.Add(this.btn_zoomOut);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 18);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(91, 64);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // btn_zoomIn
            // 
            this.btn_zoomIn.Location = new System.Drawing.Point(3, 3);
            this.btn_zoomIn.Name = "btn_zoomIn";
            this.btn_zoomIn.Size = new System.Drawing.Size(75, 24);
            this.btn_zoomIn.TabIndex = 8;
            this.btn_zoomIn.Text = "IN";
            this.btn_zoomIn.UseVisualStyleBackColor = true;
            this.btn_zoomIn.Click += new System.EventHandler(this.btn_zoomIn_Click);
            // 
            // btn_zoomOut
            // 
            this.btn_zoomOut.Location = new System.Drawing.Point(3, 33);
            this.btn_zoomOut.Name = "btn_zoomOut";
            this.btn_zoomOut.Size = new System.Drawing.Size(75, 24);
            this.btn_zoomOut.TabIndex = 9;
            this.btn_zoomOut.Text = "OUT";
            this.btn_zoomOut.UseVisualStyleBackColor = true;
            this.btn_zoomOut.Click += new System.EventHandler(this.btn_zoomOut_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(496, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(97, 85);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pan";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btn_NE);
            this.flowLayoutPanel2.Controls.Add(this.btn_SW);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(91, 64);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // btn_NE
            // 
            this.btn_NE.Location = new System.Drawing.Point(3, 3);
            this.btn_NE.Name = "btn_NE";
            this.btn_NE.Size = new System.Drawing.Size(75, 23);
            this.btn_NE.TabIndex = 10;
            this.btn_NE.Text = "NE";
            this.btn_NE.UseVisualStyleBackColor = true;
            this.btn_NE.Click += new System.EventHandler(this.btn_NE_Click);
            // 
            // btn_SW
            // 
            this.btn_SW.Location = new System.Drawing.Point(3, 32);
            this.btn_SW.Name = "btn_SW";
            this.btn_SW.Size = new System.Drawing.Size(75, 23);
            this.btn_SW.TabIndex = 11;
            this.btn_SW.Text = "SW";
            this.btn_SW.UseVisualStyleBackColor = true;
            this.btn_SW.Click += new System.EventHandler(this.btn_SW_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(396, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(97, 85);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plot";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_draw);
            this.flowLayoutPanel1.Controls.Add(this.btn_clear);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(91, 64);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_draw
            // 
            this.btn_draw.Location = new System.Drawing.Point(3, 3);
            this.btn_draw.Name = "btn_draw";
            this.btn_draw.Size = new System.Drawing.Size(75, 24);
            this.btn_draw.TabIndex = 4;
            this.btn_draw.Text = "draw";
            this.btn_draw.UseVisualStyleBackColor = true;
            this.btn_draw.Click += new System.EventHandler(this.btn_draw_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(3, 33);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 24);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(174, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 35);
            this.label2.TabIndex = 7;
            this.label2.Text = "v3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 48);
            this.label1.TabIndex = 6;
            this.label1.Text = "ScottPlot";
            // 
            // combo_routine
            // 
            this.combo_routine.FormattingEnabled = true;
            this.combo_routine.Items.AddRange(new object[] {
            "confetti",
            "sine",
            "XY pairs",
            "1M points",
            "100 points"});
            this.combo_routine.Location = new System.Drawing.Point(269, 66);
            this.combo_routine.Name = "combo_routine";
            this.combo_routine.Size = new System.Drawing.Size(108, 24);
            this.combo_routine.TabIndex = 3;
            this.combo_routine.Text = "100 points";
            this.combo_routine.SelectedIndexChanged += new System.EventHandler(this.combo_routine_SelectedIndexChanged);
            // 
            // cb_quality
            // 
            this.cb_quality.AutoSize = true;
            this.cb_quality.Location = new System.Drawing.Point(269, 38);
            this.cb_quality.Name = "cb_quality";
            this.cb_quality.Size = new System.Drawing.Size(87, 21);
            this.cb_quality.TabIndex = 2;
            this.cb_quality.Text = "anti-alias";
            this.cb_quality.UseVisualStyleBackColor = true;
            this.cb_quality.CheckedChanged += new System.EventHandler(this.cb_quality_CheckedChanged);
            // 
            // cb_animate
            // 
            this.cb_animate.AutoSize = true;
            this.cb_animate.Location = new System.Drawing.Point(269, 9);
            this.cb_animate.Name = "cb_animate";
            this.cb_animate.Size = new System.Drawing.Size(80, 21);
            this.cb_animate.TabIndex = 1;
            this.cb_animate.Text = "animate";
            this.cb_animate.UseVisualStyleBackColor = true;
            this.cb_animate.CheckedChanged += new System.EventHandler(this.cb_animate_CheckedChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(3, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(260, 21);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "version 3 demo";
            // 
            // panel_plot
            // 
            this.panel_plot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_plot.Controls.Add(this.pictureBox1);
            this.panel_plot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_plot.Location = new System.Drawing.Point(3, 103);
            this.panel_plot.Name = "panel_plot";
            this.panel_plot.Size = new System.Drawing.Size(702, 461);
            this.panel_plot.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(700, 459);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 567);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_info.ResumeLayout(false);
            this.panel_info.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel_plot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_info;
        private System.Windows.Forms.Panel panel_plot;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox cb_quality;
        private System.Windows.Forms.CheckBox cb_animate;
        private System.Windows.Forms.ComboBox combo_routine;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_draw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_zoomOut;
        private System.Windows.Forms.Button btn_zoomIn;
        private System.Windows.Forms.Button btn_SW;
        private System.Windows.Forms.Button btn_NE;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

