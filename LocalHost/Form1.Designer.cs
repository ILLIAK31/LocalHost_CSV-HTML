namespace LocalHost
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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            port2 = new System.Windows.Forms.TextBox();
            path = new System.Windows.Forms.TextBox();
            button_port = new System.Windows.Forms.Button();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            button_folder = new System.Windows.Forms.Button();
            button_stop = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(39, 34);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 59);
            label1.TabIndex = 0;
            label1.Text = "Port";
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(39, 188);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(94, 59);
            label2.TabIndex = 1;
            label2.Text = "Path";
            // 
            // port2
            // 
            port2.Location = new System.Drawing.Point(39, 111);
            port2.Name = "port2";
            port2.Size = new System.Drawing.Size(308, 27);
            port2.TabIndex = 2;
            // 
            // path
            // 
            path.Location = new System.Drawing.Point(39, 296);
            path.Name = "path";
            path.Size = new System.Drawing.Size(308, 27);
            path.TabIndex = 3;
            // 
            // button_port
            // 
            button_port.Location = new System.Drawing.Point(441, 49);
            button_port.Name = "button_port";
            button_port.Size = new System.Drawing.Size(266, 63);
            button_port.TabIndex = 4;
            button_port.Text = "Set Port";
            button_port.UseVisualStyleBackColor = true;
            button_port.Click += button_port_Click;
            // 
            // button_folder
            // 
            button_folder.Location = new System.Drawing.Point(353, 295);
            button_folder.Name = "button_folder";
            button_folder.Size = new System.Drawing.Size(40, 29);
            button_folder.TabIndex = 5;
            button_folder.Text = "...";
            button_folder.UseVisualStyleBackColor = true;
            button_folder.Click += button_folder_Click;
            // 
            // button_stop
            // 
            button_stop.Location = new System.Drawing.Point(441, 159);
            button_stop.Name = "button_stop";
            button_stop.Size = new System.Drawing.Size(266, 63);
            button_stop.TabIndex = 6;
            button_stop.Text = "Stop";
            button_stop.UseVisualStyleBackColor = true;
            button_stop.Click += button_stop_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(button_stop);
            Controls.Add(button_folder);
            Controls.Add(button_port);
            Controls.Add(path);
            Controls.Add(port2);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Local Host";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port2;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button button_port;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_folder;
        private System.Windows.Forms.Button button_stop;
    }
}
