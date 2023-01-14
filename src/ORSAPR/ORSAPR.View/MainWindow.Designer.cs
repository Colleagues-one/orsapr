
namespace ORSAPR.View
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolTipInformation = new System.Windows.Forms.ToolTip(this.components);
            this.PanelWindow = new System.Windows.Forms.Panel();
            this.LabelInnerWidth = new System.Windows.Forms.Label();
            this.TextBoxInnerWidth = new System.Windows.Forms.TextBox();
            this.LabelParameter = new System.Windows.Forms.Label();
            this.TextBoxLength = new System.Windows.Forms.TextBox();
            this.ButtonBuild = new System.Windows.Forms.Button();
            this.TextBoxHeight = new System.Windows.Forms.TextBox();
            this.TextBoxInnerLength = new System.Windows.Forms.TextBox();
            this.TextBoxWidth = new System.Windows.Forms.TextBox();
            this.LabelBladeLength = new System.Windows.Forms.Label();
            this.TextBoxBladeLength = new System.Windows.Forms.TextBox();
            this.LabelInnerLength = new System.Windows.Forms.Label();
            this.LabelHeight = new System.Windows.Forms.Label();
            this.LabelLength = new System.Windows.Forms.Label();
            this.LabelWidth = new System.Windows.Forms.Label();
            this.PanelPictureChisel = new System.Windows.Forms.Panel();
            this.RadioButtonLocksmith = new System.Windows.Forms.RadioButton();
            this.RadioButtonPeak = new System.Windows.Forms.RadioButton();
            this.PanelWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelWindow
            // 
            this.PanelWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelWindow.Controls.Add(this.LabelInnerWidth);
            this.PanelWindow.Controls.Add(this.TextBoxInnerWidth);
            this.PanelWindow.Controls.Add(this.LabelParameter);
            this.PanelWindow.Controls.Add(this.TextBoxLength);
            this.PanelWindow.Controls.Add(this.ButtonBuild);
            this.PanelWindow.Controls.Add(this.TextBoxHeight);
            this.PanelWindow.Controls.Add(this.TextBoxInnerLength);
            this.PanelWindow.Controls.Add(this.TextBoxWidth);
            this.PanelWindow.Controls.Add(this.LabelBladeLength);
            this.PanelWindow.Controls.Add(this.TextBoxBladeLength);
            this.PanelWindow.Controls.Add(this.LabelInnerLength);
            this.PanelWindow.Controls.Add(this.LabelHeight);
            this.PanelWindow.Controls.Add(this.LabelLength);
            this.PanelWindow.Controls.Add(this.LabelWidth);
            this.PanelWindow.Location = new System.Drawing.Point(12, 256);
            this.PanelWindow.MaximumSize = new System.Drawing.Size(920, 1160);
            this.PanelWindow.Name = "PanelWindow";
            this.PanelWindow.Size = new System.Drawing.Size(448, 385);
            this.PanelWindow.TabIndex = 12;
            // 
            // LabelInnerWidth
            // 
            this.LabelInnerWidth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelInnerWidth.AutoSize = true;
            this.LabelInnerWidth.Location = new System.Drawing.Point(23, 269);
            this.LabelInnerWidth.Name = "LabelInnerWidth";
            this.LabelInnerWidth.Size = new System.Drawing.Size(155, 17);
            this.LabelInnerWidth.TabIndex = 19;
            this.LabelInnerWidth.Text = "w1 - Chisel cutout width";
            // 
            // TextBoxInnerWidth
            // 
            this.TextBoxInnerWidth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TextBoxInnerWidth.Location = new System.Drawing.Point(245, 266);
            this.TextBoxInnerWidth.Name = "TextBoxInnerWidth";
            this.TextBoxInnerWidth.Size = new System.Drawing.Size(180, 22);
            this.TextBoxInnerWidth.TabIndex = 5;
            this.TextBoxInnerWidth.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxInnerWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxInnerWidth_KeyPress);
            this.TextBoxInnerWidth.Leave += new System.EventHandler(this.TextBoxInnerWidth_Leave);
            this.TextBoxInnerWidth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseMove);
            // 
            // LabelParameter
            // 
            this.LabelParameter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LabelParameter.AutoSize = true;
            this.LabelParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LabelParameter.Location = new System.Drawing.Point(168, 18);
            this.LabelParameter.Name = "LabelParameter";
            this.LabelParameter.Size = new System.Drawing.Size(124, 25);
            this.LabelParameter.TabIndex = 12;
            this.LabelParameter.Text = "Tool Options";
            // 
            // TextBoxLength
            // 
            this.TextBoxLength.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TextBoxLength.Location = new System.Drawing.Point(245, 106);
            this.TextBoxLength.Name = "TextBoxLength";
            this.TextBoxLength.Size = new System.Drawing.Size(180, 22);
            this.TextBoxLength.TabIndex = 1;
            this.TextBoxLength.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxLenght_KeyPress);
            this.TextBoxLength.Leave += new System.EventHandler(this.TextBoxLenght_Leave);
            this.TextBoxLength.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseMove);
            // 
            // ButtonBuild
            // 
            this.ButtonBuild.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonBuild.Enabled = false;
            this.ButtonBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ButtonBuild.Location = new System.Drawing.Point(164, 313);
            this.ButtonBuild.Name = "ButtonBuild";
            this.ButtonBuild.Size = new System.Drawing.Size(120, 60);
            this.ButtonBuild.TabIndex = 6;
            this.ButtonBuild.Text = "Build";
            this.ButtonBuild.UseVisualStyleBackColor = true;
            this.ButtonBuild.Click += new System.EventHandler(this.ButtonBuild_Click);
            // 
            // TextBoxHeight
            // 
            this.TextBoxHeight.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TextBoxHeight.Location = new System.Drawing.Point(245, 146);
            this.TextBoxHeight.Name = "TextBoxHeight";
            this.TextBoxHeight.Size = new System.Drawing.Size(180, 22);
            this.TextBoxHeight.TabIndex = 2;
            this.TextBoxHeight.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxHeight_KeyPress);
            this.TextBoxHeight.Leave += new System.EventHandler(this.TextBoxHeight_Leave);
            this.TextBoxHeight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseMove);
            // 
            // TextBoxInnerLength
            // 
            this.TextBoxInnerLength.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TextBoxInnerLength.Location = new System.Drawing.Point(245, 226);
            this.TextBoxInnerLength.Name = "TextBoxInnerLength";
            this.TextBoxInnerLength.Size = new System.Drawing.Size(180, 22);
            this.TextBoxInnerLength.TabIndex = 4;
            this.TextBoxInnerLength.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxInnerLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxInnerLength_KeyPress);
            this.TextBoxInnerLength.Leave += new System.EventHandler(this.TextBoxInnerLength_Leave);
            this.TextBoxInnerLength.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseMove);
            // 
            // TextBoxWidth
            // 
            this.TextBoxWidth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TextBoxWidth.Location = new System.Drawing.Point(245, 66);
            this.TextBoxWidth.Name = "TextBoxWidth";
            this.TextBoxWidth.Size = new System.Drawing.Size(180, 22);
            this.TextBoxWidth.TabIndex = 0;
            this.TextBoxWidth.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxWidth_KeyPress);
            this.TextBoxWidth.Leave += new System.EventHandler(this.TextBoxWidth_Leave);
            this.TextBoxWidth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseMove);
            // 
            // LabelBladeLength
            // 
            this.LabelBladeLength.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelBladeLength.AutoSize = true;
            this.LabelBladeLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelBladeLength.Location = new System.Drawing.Point(23, 187);
            this.LabelBladeLength.Name = "LabelBladeLength";
            this.LabelBladeLength.Size = new System.Drawing.Size(154, 18);
            this.LabelBladeLength.TabIndex = 5;
            this.LabelBladeLength.Text = "l1 – Chisel blade length";
            // 
            // TextBoxBladeLength
            // 
            this.TextBoxBladeLength.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TextBoxBladeLength.Location = new System.Drawing.Point(245, 186);
            this.TextBoxBladeLength.Name = "TextBoxBladeLength";
            this.TextBoxBladeLength.Size = new System.Drawing.Size(180, 22);
            this.TextBoxBladeLength.TabIndex = 3;
            this.TextBoxBladeLength.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxBladeLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxBladeLength_KeyPress);
            this.TextBoxBladeLength.Leave += new System.EventHandler(this.TextBoxBladeLength_Leave);
            this.TextBoxBladeLength.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseMove);
            // 
            // LabelInnerLength
            // 
            this.LabelInnerLength.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelInnerLength.AutoSize = true;
            this.LabelInnerLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelInnerLength.Location = new System.Drawing.Point(23, 227);
            this.LabelInnerLength.Name = "LabelInnerLength";
            this.LabelInnerLength.Size = new System.Drawing.Size(160, 18);
            this.LabelInnerLength.TabIndex = 4;
            this.LabelInnerLength.Text = "l2 – Chisel cutout length";
            // 
            // LabelHeight
            // 
            this.LabelHeight.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelHeight.AutoSize = true;
            this.LabelHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelHeight.Location = new System.Drawing.Point(23, 147);
            this.LabelHeight.Name = "LabelHeight";
            this.LabelHeight.Size = new System.Drawing.Size(116, 18);
            this.LabelHeight.TabIndex = 3;
            this.LabelHeight.Text = "H - Chisel height";
            // 
            // LabelLength
            // 
            this.LabelLength.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelLength.AutoSize = true;
            this.LabelLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelLength.Location = new System.Drawing.Point(23, 107);
            this.LabelLength.Name = "LabelLength";
            this.LabelLength.Size = new System.Drawing.Size(113, 18);
            this.LabelLength.TabIndex = 2;
            this.LabelLength.Text = "L - Chisel length";
            // 
            // LabelWidth
            // 
            this.LabelWidth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelWidth.AutoSize = true;
            this.LabelWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LabelWidth.Location = new System.Drawing.Point(23, 67);
            this.LabelWidth.Name = "LabelWidth";
            this.LabelWidth.Size = new System.Drawing.Size(115, 18);
            this.LabelWidth.TabIndex = 1;
            this.LabelWidth.Text = "W - Chisel width";
            // 
            // PanelPictureChisel
            // 
            this.PanelPictureChisel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelPictureChisel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelPictureChisel.BackgroundImage")));
            this.PanelPictureChisel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelPictureChisel.Location = new System.Drawing.Point(12, 12);
            this.PanelPictureChisel.Name = "PanelPictureChisel";
            this.PanelPictureChisel.Size = new System.Drawing.Size(448, 212);
            this.PanelPictureChisel.TabIndex = 20;
            // 
            // RadioButtonLocksmith
            // 
            this.RadioButtonLocksmith.AutoSize = true;
            this.RadioButtonLocksmith.Checked = true;
            this.RadioButtonLocksmith.Location = new System.Drawing.Point(12, 230);
            this.RadioButtonLocksmith.Name = "RadioButtonLocksmith";
            this.RadioButtonLocksmith.Size = new System.Drawing.Size(134, 21);
            this.RadioButtonLocksmith.TabIndex = 20;
            this.RadioButtonLocksmith.TabStop = true;
            this.RadioButtonLocksmith.Text = "Locksmith Chisel";
            this.RadioButtonLocksmith.UseVisualStyleBackColor = true;
            this.RadioButtonLocksmith.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // RadioButtonPeak
            // 
            this.RadioButtonPeak.AutoSize = true;
            this.RadioButtonPeak.Location = new System.Drawing.Point(152, 230);
            this.RadioButtonPeak.Name = "RadioButtonPeak";
            this.RadioButtonPeak.Size = new System.Drawing.Size(103, 21);
            this.RadioButtonPeak.TabIndex = 21;
            this.RadioButtonPeak.Text = "Peak Chisel";
            this.RadioButtonPeak.UseVisualStyleBackColor = true;
            this.RadioButtonPeak.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(472, 653);
            this.Controls.Add(this.RadioButtonPeak);
            this.Controls.Add(this.PanelWindow);
            this.Controls.Add(this.RadioButtonLocksmith);
            this.Controls.Add(this.PanelPictureChisel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(491, 700);
            this.MinimumSize = new System.Drawing.Size(490, 700);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chisel Parameters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormParameters_FormClosing);
            this.PanelWindow.ResumeLayout(false);
            this.PanelWindow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTipInformation;
        private System.Windows.Forms.Panel PanelWindow;
        private System.Windows.Forms.Label LabelInnerWidth;
        private System.Windows.Forms.TextBox TextBoxInnerWidth;
        private System.Windows.Forms.Label LabelParameter;
        private System.Windows.Forms.TextBox TextBoxLength;
        private System.Windows.Forms.Button ButtonBuild;
        private System.Windows.Forms.TextBox TextBoxHeight;
        private System.Windows.Forms.TextBox TextBoxInnerLength;
        private System.Windows.Forms.Label LabelBladeLength;
        private System.Windows.Forms.TextBox TextBoxBladeLength;
        private System.Windows.Forms.Label LabelInnerLength;
        private System.Windows.Forms.Label LabelHeight;
        private System.Windows.Forms.Label LabelLength;
        private System.Windows.Forms.Label LabelWidth;
        private System.Windows.Forms.Panel PanelPictureChisel;
        private System.Windows.Forms.RadioButton RadioButtonLocksmith;
        private System.Windows.Forms.RadioButton RadioButtonPeak;
        private System.Windows.Forms.TextBox TextBoxWidth;
    }
}

