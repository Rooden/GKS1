namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnterBox = new System.Windows.Forms.TextBox();
            this.OutBox = new System.Windows.Forms.TextBox();
            this.Xbox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.Ybox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EnterBox
            // 
            this.EnterBox.Location = new System.Drawing.Point(15, 147);
            this.EnterBox.Multiline = true;
            this.EnterBox.Name = "EnterBox";
            this.EnterBox.Size = new System.Drawing.Size(285, 183);
            this.EnterBox.TabIndex = 0;
            // 
            // OutBox
            // 
            this.OutBox.Location = new System.Drawing.Point(364, 61);
            this.OutBox.Multiline = true;
            this.OutBox.Name = "OutBox";
            this.OutBox.Size = new System.Drawing.Size(312, 269);
            this.OutBox.TabIndex = 1;
            // 
            // Xbox
            // 
            this.Xbox.Location = new System.Drawing.Point(448, 341);
            this.Xbox.Name = "Xbox";
            this.Xbox.Size = new System.Drawing.Size(100, 20);
            this.Xbox.TabIndex = 2;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(100, 341);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(125, 77);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Поехали!";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Ybox
            // 
            this.Ybox.Location = new System.Drawing.Point(448, 384);
            this.Ybox.Name = "Ybox";
            this.Ybox.Size = new System.Drawing.Size(100, 20);
            this.Ybox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(362, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Размер по Х";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Размер по У";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ввод ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(373, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Максимальная прыбиыль";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(710, 446);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ybox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Xbox);
            this.Controls.Add(this.OutBox);
            this.Controls.Add(this.EnterBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Лаба 1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EnterBox;
        private System.Windows.Forms.TextBox OutBox;
        private System.Windows.Forms.TextBox Xbox;
        public System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox Ybox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}

