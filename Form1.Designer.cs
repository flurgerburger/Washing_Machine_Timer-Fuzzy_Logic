namespace Washing_Machine_Timer_Fuzzy_Logic;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        numLoad = new NumericUpDown();
        numSoiling = new NumericUpDown();
        numDetergent = new NumericUpDown();
        cmbMethod = new ComboBox();
        btnCalculate = new Button();
        lblResult = new Label();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        label6 = new Label();
        ((System.ComponentModel.ISupportInitialize)numLoad).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSoiling).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numDetergent).BeginInit();
        SuspendLayout();
        // 
        // numLoad
        // 
        numLoad.Location = new Point(22, 55);
        numLoad.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        numLoad.Name = "numLoad";
        numLoad.Size = new Size(48, 23);
        numLoad.TabIndex = 0;
        numLoad.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // numSoiling
        // 
        numSoiling.Location = new Point(22, 117);
        numSoiling.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        numSoiling.Name = "numSoiling";
        numSoiling.Size = new Size(48, 23);
        numSoiling.TabIndex = 1;
        numSoiling.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // numDetergent
        // 
        numDetergent.Location = new Point(22, 183);
        numDetergent.Name = "numDetergent";
        numDetergent.Size = new Size(48, 23);
        numDetergent.TabIndex = 2;
        numDetergent.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // cmbMethod
        // 
        cmbMethod.FormattingEnabled = true;
        cmbMethod.Location = new Point(204, 33);
        cmbMethod.Name = "cmbMethod";
        cmbMethod.Size = new Size(121, 23);
        cmbMethod.TabIndex = 3;
        // 
        // btnCalculate
        // 
        btnCalculate.Font = new Font("Segoe UI", 10F);
        btnCalculate.Location = new Point(230, 163);
        btnCalculate.Name = "btnCalculate";
        btnCalculate.Size = new Size(86, 43);
        btnCalculate.TabIndex = 4;
        btnCalculate.Text = "Wash";
        btnCalculate.UseVisualStyleBackColor = true;
        btnCalculate.Click += btnCalculate_Click;
        // 
        // lblResult
        // 
        lblResult.AutoSize = true;
        lblResult.Font = new Font("Segoe UI", 20F);
        lblResult.Location = new Point(22, 240);
        lblResult.Name = "lblResult";
        lblResult.Size = new Size(237, 37);
        lblResult.TabIndex = 5;
        lblResult.Text = "Please Input stuff...";
        lblResult.Click += lblResult_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 10F);
        label1.Location = new Point(76, 58);
        label1.Name = "label1";
        label1.Size = new Size(26, 19);
        label1.TabIndex = 6;
        label1.Text = "KG";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 10F);
        label2.Location = new Point(76, 117);
        label2.Name = "label2";
        label2.Size = new Size(39, 19);
        label2.TabIndex = 7;
        label2.Text = "Scale";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Segoe UI", 10F);
        label3.Location = new Point(74, 187);
        label3.Name = "label3";
        label3.Size = new Size(28, 19);
        label3.TabIndex = 8;
        label3.Text = "mL";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI", 10F);
        label4.Location = new Point(22, 161);
        label4.Name = "label4";
        label4.Size = new Size(125, 19);
        label4.TabIndex = 9;
        label4.Text = "Detergent Amount";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI", 10F);
        label5.Location = new Point(22, 95);
        label5.Name = "label5";
        label5.Size = new Size(84, 19);
        label5.TabIndex = 10;
        label5.Text = "Soiling Level";
        label5.Click += label5_Click;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 10F);
        label6.Location = new Point(22, 33);
        label6.Name = "label6";
        label6.Size = new Size(66, 19);
        label6.TabIndex = 11;
        label6.Text = "Load Size";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(label6);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(lblResult);
        Controls.Add(btnCalculate);
        Controls.Add(cmbMethod);
        Controls.Add(numDetergent);
        Controls.Add(numSoiling);
        Controls.Add(numLoad);
        Name = "Form1";
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)numLoad).EndInit();
        ((System.ComponentModel.ISupportInitialize)numSoiling).EndInit();
        ((System.ComponentModel.ISupportInitialize)numDetergent).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private NumericUpDown numLoad;
    private NumericUpDown numSoiling;
    private NumericUpDown numDetergent;
    private ComboBox cmbMethod;
    private Button btnCalculate;
    private Label lblResult;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
}
