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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_draw = new System.Windows.Forms.Button();
            this.combo_routine = new System.Windows.Forms.ComboBox();
            this.cb_quality = new System.Windows.Forms.CheckBox();
            this.cb_animate = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel_plot = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_info.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(709, 536);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel_info
            // 
            this.panel_info.Controls.Add(this.label2);
            this.panel_info.Controls.Add(this.label1);
            this.panel_info.Controls.Add(this.btn_clear);
            this.panel_info.Controls.Add(this.btn_draw);
            this.panel_info.Controls.Add(this.combo_routine);
            this.panel_info.Controls.Add(this.cb_quality);
            this.panel_info.Controls.Add(this.cb_animate);
            this.panel_info.Controls.Add(this.richTextBox1);
            this.panel_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_info.Location = new System.Drawing.Point(3, 3);
            this.panel_info.Name = "panel_info";
            this.panel_info.Size = new System.Drawing.Size(703, 114);
            this.panel_info.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(174, 15);
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
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(540, 37);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_draw
            // 
            this.btn_draw.Location = new System.Drawing.Point(540, 10);
            this.btn_draw.Name = "btn_draw";
            this.btn_draw.Size = new System.Drawing.Size(75, 23);
            this.btn_draw.TabIndex = 4;
            this.btn_draw.Text = "draw";
            this.btn_draw.UseVisualStyleBackColor = true;
            this.btn_draw.Click += new System.EventHandler(this.btn_draw_Click);
            // 
            // combo_routine
            // 
            this.combo_routine.FormattingEnabled = true;
            this.combo_routine.Items.AddRange(new object[] {
            "confetti",
            "sine"});
            this.combo_routine.Location = new System.Drawing.Point(394, 65);
            this.combo_routine.Name = "combo_routine";
            this.combo_routine.Size = new System.Drawing.Size(121, 24);
            this.combo_routine.TabIndex = 3;
            this.combo_routine.Text = "confetti";
            this.combo_routine.SelectedIndexChanged += new System.EventHandler(this.combo_routine_SelectedIndexChanged);
            // 
            // cb_quality
            // 
            this.cb_quality.AutoSize = true;
            this.cb_quality.Location = new System.Drawing.Point(394, 37);
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
            this.cb_animate.Location = new System.Drawing.Point(394, 9);
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
            this.panel_plot.Location = new System.Drawing.Point(3, 123);
            this.panel_plot.Name = "panel_plot";
            this.panel_plot.Size = new System.Drawing.Size(703, 410);
            this.panel_plot.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(701, 408);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
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
            this.ClientSize = new System.Drawing.Size(709, 536);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_info.ResumeLayout(false);
            this.panel_info.PerformLayout();
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
    }
}

