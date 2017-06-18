namespace April.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.customDataGridView1 = new April.UserControls.CustomDataGrid.CustomDataGridView();
            this.comboBoxUserActive = new System.Windows.Forms.ComboBox();
            this.btnSwitchVisible = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.DataSource = null;
            this.customDataGridView1.Location = new System.Drawing.Point(12, 42);
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.Size = new System.Drawing.Size(835, 487);
            this.customDataGridView1.TabIndex = 0;
            // 
            // comboBoxUserActive
            // 
            this.comboBoxUserActive.FormattingEnabled = true;
            this.comboBoxUserActive.Location = new System.Drawing.Point(616, 12);
            this.comboBoxUserActive.Name = "comboBoxUserActive";
            this.comboBoxUserActive.Size = new System.Drawing.Size(231, 21);
            this.comboBoxUserActive.TabIndex = 1;
            this.comboBoxUserActive.SelectedIndexChanged += new System.EventHandler(this.comboBoxUserActive_SelectedIndexChanged);
            // 
            // btnSwitchVisible
            // 
            this.btnSwitchVisible.Location = new System.Drawing.Point(445, 10);
            this.btnSwitchVisible.Name = "btnSwitchVisible";
            this.btnSwitchVisible.Size = new System.Drawing.Size(138, 23);
            this.btnSwitchVisible.TabIndex = 2;
            this.btnSwitchVisible.Text = "Switch visible column";
            this.btnSwitchVisible.UseVisualStyleBackColor = true;
            this.btnSwitchVisible.Click += new System.EventHandler(this.btnSwitchVisible_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 541);
            this.Controls.Add(this.btnSwitchVisible);
            this.Controls.Add(this.comboBoxUserActive);
            this.Controls.Add(this.customDataGridView1);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.CustomDataGrid.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.ComboBox comboBoxUserActive;
        private System.Windows.Forms.Button btnSwitchVisible;
    }
}

