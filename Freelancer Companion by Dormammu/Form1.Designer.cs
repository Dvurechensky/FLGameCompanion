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
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Map.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Map.Location = new System.Drawing.Point(0, -2);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(1000, 1000);
            this.Map.TabIndex = 1;
            this.Map.TabStop = false;
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
            // 
            // comboBoxSystems
            // 
            this.comboBoxSystems.DisplayMember = "Add";
            this.comboBoxSystems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSystems.FormattingEnabled = true;
            this.comboBoxSystems.Location = new System.Drawing.Point(1006, 4);
            this.comboBoxSystems.Name = "comboBoxSystems";
            this.comboBoxSystems.Size = new System.Drawing.Size(336, 24);
            this.comboBoxSystems.TabIndex = 2;
            this.comboBoxSystems.SelectedIndexChanged += new System.EventHandler(this.comboBoxSystems_SelectedIndexChanged);
            // 
            // labelSystemss
            // 
            this.labelSystemss.AutoSize = true;
            this.labelSystemss.BackColor = System.Drawing.Color.DarkOrchid;
            this.labelSystemss.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSystemss.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSystemss.ForeColor = System.Drawing.Color.FloralWhite;
            this.labelSystemss.Location = new System.Drawing.Point(1348, 4);
            this.labelSystemss.Name = "labelSystemss";
            this.labelSystemss.Size = new System.Drawing.Size(44, 21);
            this.labelSystemss.TabIndex = 3;
            this.labelSystemss.Text = "999";
            this.labelSystemss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoggerRichTextBox
            // 
            this.LoggerRichTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoggerRichTextBox.Location = new System.Drawing.Point(1006, 432);
            this.LoggerRichTextBox.Name = "LoggerRichTextBox";
            this.LoggerRichTextBox.Size = new System.Drawing.Size(386, 566);
            this.LoggerRichTextBox.TabIndex = 4;
            this.LoggerRichTextBox.Text = "";
            // 
            // flowLayoutPanelNames
            // 
            this.flowLayoutPanelNames.AutoScroll = true;
            this.flowLayoutPanelNames.Location = new System.Drawing.Point(1184, 35);
            this.flowLayoutPanelNames.Name = "flowLayoutPanelNames";
            this.flowLayoutPanelNames.Size = new System.Drawing.Size(208, 391);
            this.flowLayoutPanelNames.TabIndex = 5;
            // 
            // FreelancerCompanionDvurechensky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1394, 996);
            this.Controls.Add(this.flowLayoutPanelNames);
            this.Controls.Add(this.LoggerRichTextBox);
            this.Controls.Add(this.labelSystemss);
            this.Controls.Add(this.comboBoxSystems);
            this.Controls.Add(this.Map);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1410, 1035);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1310, 1035);
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
    }
}

