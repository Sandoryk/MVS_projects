namespace Calculator
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
            this.a_param = new System.Windows.Forms.TextBox();
            this.b_param = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.result = new System.Windows.Forms.TextBox();
            this.add_but = new System.Windows.Forms.Button();
            this.subtract_but = new System.Windows.Forms.Button();
            this.multi_but = new System.Windows.Forms.Button();
            this.devide_but = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // a_param
            // 
            this.a_param.Location = new System.Drawing.Point(76, 13);
            this.a_param.Name = "a_param";
            this.a_param.Size = new System.Drawing.Size(100, 20);
            this.a_param.TabIndex = 0;
            this.a_param.Text = "1.5";
            // 
            // b_param
            // 
            this.b_param.Location = new System.Drawing.Point(76, 39);
            this.b_param.Name = "b_param";
            this.b_param.Size = new System.Drawing.Size(100, 20);
            this.b_param.TabIndex = 1;
            this.b_param.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "a";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "b";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "результат";
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(76, 65);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(380, 20);
            this.result.TabIndex = 4;
            // 
            // add_but
            // 
            this.add_but.Location = new System.Drawing.Point(206, 12);
            this.add_but.Name = "add_but";
            this.add_but.Size = new System.Drawing.Size(112, 20);
            this.add_but.TabIndex = 6;
            this.add_but.Text = "Сложить";
            this.add_but.UseVisualStyleBackColor = true;
            this.add_but.Click += new System.EventHandler(this.add_but_Click);
            // 
            // subtract_but
            // 
            this.subtract_but.Location = new System.Drawing.Point(206, 38);
            this.subtract_but.Name = "subtract_but";
            this.subtract_but.Size = new System.Drawing.Size(112, 20);
            this.subtract_but.TabIndex = 7;
            this.subtract_but.Text = "Отнять";
            this.subtract_but.UseVisualStyleBackColor = true;
            this.subtract_but.Click += new System.EventHandler(this.subtract_but_Click);
            // 
            // multi_but
            // 
            this.multi_but.Location = new System.Drawing.Point(344, 13);
            this.multi_but.Name = "multi_but";
            this.multi_but.Size = new System.Drawing.Size(112, 20);
            this.multi_but.TabIndex = 8;
            this.multi_but.Text = "Умножить";
            this.multi_but.UseVisualStyleBackColor = true;
            this.multi_but.Click += new System.EventHandler(this.multi_but_Click);
            // 
            // devide_but
            // 
            this.devide_but.Location = new System.Drawing.Point(344, 38);
            this.devide_but.Name = "devide_but";
            this.devide_but.Size = new System.Drawing.Size(112, 20);
            this.devide_but.TabIndex = 9;
            this.devide_but.Text = "Поделить";
            this.devide_but.UseVisualStyleBackColor = true;
            this.devide_but.Click += new System.EventHandler(this.devide_but_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 103);
            this.Controls.Add(this.devide_but);
            this.Controls.Add(this.multi_but);
            this.Controls.Add(this.subtract_but);
            this.Controls.Add(this.add_but);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.result);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_param);
            this.Controls.Add(this.a_param);
            this.Name = "Form1";
            this.Text = "Калькулятор";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox a_param;
        private System.Windows.Forms.TextBox b_param;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.Button add_but;
        private System.Windows.Forms.Button subtract_but;
        private System.Windows.Forms.Button multi_but;
        private System.Windows.Forms.Button devide_but;
    }
}

