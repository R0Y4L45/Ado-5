namespace Ado5
{
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
            this.cboxProvider = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.txtBoxSearch = new System.Windows.Forms.TextBox();
            this.cboxAuthors = new System.Windows.Forms.ComboBox();
            this.cboxCategories = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboxProvider
            // 
            this.cboxProvider.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.cboxProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxProvider.FormattingEnabled = true;
            this.cboxProvider.Location = new System.Drawing.Point(12, 15);
            this.cboxProvider.Name = "cboxProvider";
            this.cboxProvider.Size = new System.Drawing.Size(231, 23);
            this.cboxProvider.TabIndex = 3;
            this.cboxProvider.SelectedValueChanged += new System.EventHandler(this.cbox_SelectedIndexChanged);
            // 
            // listView1
            // 
            this.listView1.Enabled = false;
            this.listView1.Location = new System.Drawing.Point(276, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(798, 594);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // txtBoxSearch
            // 
            this.txtBoxSearch.Enabled = false;
            this.txtBoxSearch.Location = new System.Drawing.Point(12, 167);
            this.txtBoxSearch.Name = "txtBoxSearch";
            this.txtBoxSearch.Size = new System.Drawing.Size(231, 23);
            this.txtBoxSearch.TabIndex = 10;
            this.txtBoxSearch.Text = "Id or Name of Books";
            this.txtBoxSearch.Click += new System.EventHandler(this.txtBoxSearch_Click);
            // 
            // cboxAuthors
            // 
            this.cboxAuthors.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.cboxAuthors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxAuthors.Enabled = false;
            this.cboxAuthors.FormattingEnabled = true;
            this.cboxAuthors.Location = new System.Drawing.Point(12, 59);
            this.cboxAuthors.Name = "cboxAuthors";
            this.cboxAuthors.Size = new System.Drawing.Size(231, 23);
            this.cboxAuthors.TabIndex = 11;
            this.cboxAuthors.SelectedIndexChanged += new System.EventHandler(this.cbox_SelectedIndexChanged);
            // 
            // cboxCategories
            // 
            this.cboxCategories.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.cboxCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCategories.Enabled = false;
            this.cboxCategories.FormattingEnabled = true;
            this.cboxCategories.Location = new System.Drawing.Point(12, 109);
            this.cboxCategories.Name = "cboxCategories";
            this.cboxCategories.Size = new System.Drawing.Size(231, 23);
            this.cboxCategories.TabIndex = 12;
            this.cboxCategories.SelectedIndexChanged += new System.EventHandler(this.cbox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 40);
            this.button1.TabIndex = 13;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 612);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboxCategories);
            this.Controls.Add(this.cboxAuthors);
            this.Controls.Add(this.txtBoxSearch);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.cboxProvider);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComboBox cboxProvider;
        private ListView listView1;
        private TextBox txtBoxSearch;
        private ComboBox cboxAuthors;
        private ComboBox cboxCategories;
        private Button button1;
    }
}