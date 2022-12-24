namespace Freelancer_Companion_by_Dormammu
{
    partial class FreelancerCompanionDvurechensky
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
            this.Map = new System.Windows.Forms.PictureBox();
            this.comboBoxSystems = new System.Windows.Forms.ComboBox();
            this.labelSystemss = new System.Windows.Forms.Label();
            this.LoggerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanelNames = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxBases = new System.Windows.Forms.CheckBox();
            this.checkBoxContainers = new System.Windows.Forms.CheckBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.checkBoxHoll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Map.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Map.Location = new System.Drawing.Point(0, -2);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(1000, 1000);
            this.Map.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Map.TabIndex = 1;
            this.Map.TabStop = false;
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
            // 
            // comboBoxSystems
            // 
            this.comboBoxSystems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSystems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSystems.DisplayMember = "Add";
            this.comboBoxSystems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSystems.FormattingEnabled = true;
            this.comboBoxSystems.Location = new System.Drawing.Point(1006, 4);
            this.comboBoxSystems.Name = "comboBoxSystems";
            this.comboBoxSystems.Size = new System.Drawing.Size(426, 24);
            this.comboBoxSystems.TabIndex = 2;
            this.comboBoxSystems.SelectedIndexChanged += new System.EventHandler(this.comboBoxSystems_SelectedIndexChanged);
            this.comboBoxSystems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxSystems_KeyPress);
            // 
            // labelSystemss
            // 
            this.labelSystemss.AutoSize = true;
            this.labelSystemss.BackColor = System.Drawing.Color.DarkOrchid;
            this.labelSystemss.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSystemss.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSystemss.ForeColor = System.Drawing.Color.FloralWhite;
            this.labelSystemss.Location = new System.Drawing.Point(1438, 4);
            this.labelSystemss.Name = "labelSystemss";
            this.labelSystemss.Size = new System.Drawing.Size(44, 21);
            this.labelSystemss.TabIndex = 3;
            this.labelSystemss.Text = "999";
            this.labelSystemss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoggerRichTextBox
            // 
            this.LoggerRichTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoggerRichTextBox.Location = new System.Drawing.Point(1006, 436);
            this.LoggerRichTextBox.Name = "LoggerRichTextBox";
            this.LoggerRichTextBox.Size = new System.Drawing.Size(476, 550);
            this.LoggerRichTextBox.TabIndex = 4;
            this.LoggerRichTextBox.Text = "";
            // 
            // flowLayoutPanelNames
            // 
            this.flowLayoutPanelNames.AutoScroll = true;
            this.flowLayoutPanelNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flowLayoutPanelNames.Location = new System.Drawing.Point(1006, 92);
            this.flowLayoutPanelNames.Name = "flowLayoutPanelNames";
            this.flowLayoutPanelNames.Size = new System.Drawing.Size(476, 338);
            this.flowLayoutPanelNames.TabIndex = 5;
            // 
            // checkBoxBases
            // 
            this.checkBoxBases.AutoSize = true;
            this.checkBoxBases.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxBases.Location = new System.Drawing.Point(1006, 35);
            this.checkBoxBases.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxBases.Name = "checkBoxBases";
            this.checkBoxBases.Size = new System.Drawing.Size(128, 24);
            this.checkBoxBases.TabIndex = 6;
            this.checkBoxBases.Text = "Показать базы";
            this.checkBoxBases.UseVisualStyleBackColor = true;
            this.checkBoxBases.CheckedChanged += new System.EventHandler(this.checkBoxBases_CheckedChanged);
            // 
            // checkBoxContainers
            // 
            this.checkBoxContainers.AutoSize = true;
            this.checkBoxContainers.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxContainers.Location = new System.Drawing.Point(1006, 63);
            this.checkBoxContainers.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxContainers.Name = "checkBoxContainers";
            this.checkBoxContainers.Size = new System.Drawing.Size(175, 24);
            this.checkBoxContainers.TabIndex = 7;
            this.checkBoxContainers.Text = "Показать контейнеры";
            this.checkBoxContainers.UseVisualStyleBackColor = true;
            this.checkBoxContainers.CheckedChanged += new System.EventHandler(this.checkBoxContainers_CheckedChanged);
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAll.Location = new System.Drawing.Point(1211, 35);
            this.checkBoxAll.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(117, 24);
            this.checkBoxAll.TabIndex = 8;
            this.checkBoxAll.Text = "Показать всё";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxHoll
            // 
            this.checkBoxHoll.AutoSize = true;
            this.checkBoxHoll.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxHoll.Location = new System.Drawing.Point(1211, 63);
            this.checkBoxHoll.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxHoll.Name = "checkBoxHoll";
            this.checkBoxHoll.Size = new System.Drawing.Size(152, 24);
            this.checkBoxHoll.TabIndex = 9;
            this.checkBoxHoll.Text = "Показать порталы";
            this.checkBoxHoll.UseVisualStyleBackColor = true;
            this.checkBoxHoll.CheckedChanged += new System.EventHandler(this.checkBoxHoll_CheckedChanged);
            // 
            // FreelancerCompanionDvurechensky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1494, 994);
            this.Controls.Add(this.checkBoxHoll);
            this.Controls.Add(this.checkBoxAll);
            this.Controls.Add(this.checkBoxContainers);
            this.Controls.Add(this.checkBoxBases);
            this.Controls.Add(this.flowLayoutPanelNames);
            this.Controls.Add(this.LoggerRichTextBox);
            this.Controls.Add(this.labelSystemss);
            this.Controls.Add(this.comboBoxSystems);
            this.Controls.Add(this.Map);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1510, 1033);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1310, 839);
            this.Name = "FreelancerCompanionDvurechensky";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Freelancer Companion";
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Map;
        private System.Windows.Forms.ComboBox comboBoxSystems;
        private System.Windows.Forms.Label labelSystemss;
        private System.Windows.Forms.RichTextBox LoggerRichTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelNames;
        private System.Windows.Forms.CheckBox checkBoxBases;
        private System.Windows.Forms.CheckBox checkBoxContainers;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.CheckBox checkBoxHoll;
    }
}

