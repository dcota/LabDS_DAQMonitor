

namespace LabDS.View
{
    partial class Janela
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
       

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.com_box = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.iniciar = new System.Windows.Forms.Button();
            this.baud_box = new System.Windows.Forms.ComboBox();
            this.terminarDAQ = new System.Windows.Forms.Button();
            this.sensor_control = new System.Windows.Forms.GroupBox();
            this.setpoint = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPress = new System.Windows.Forms.TextBox();
            this.iniciarDAQ = new System.Windows.Forms.Button();
            this.textBoxTemp = new System.Windows.Forms.TextBox();
            this.tempGrph = new ZedGraph.ZedGraphControl();
            this.pressGrph = new ZedGraph.ZedGraphControl();
            this.alarmBox = new System.Windows.Forms.TextBox();
            this.reportBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.sensor_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.setpoint)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // com_box
            // 
            this.com_box.FormattingEnabled = true;
            this.com_box.Location = new System.Drawing.Point(69, 21);
            this.com_box.Name = "com_box";
            this.com_box.Size = new System.Drawing.Size(130, 21);
            this.com_box.TabIndex = 3;
            this.com_box.SelectedIndexChanged += new System.EventHandler(this.Com_box_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.iniciar);
            this.groupBox1.Controls.Add(this.baud_box);
            this.groupBox1.Controls.Add(this.com_box);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 127);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Baud Rate:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "COM port:";
            // 
            // iniciar
            // 
            this.iniciar.Location = new System.Drawing.Point(10, 87);
            this.iniciar.Name = "iniciar";
            this.iniciar.Size = new System.Drawing.Size(75, 23);
            this.iniciar.TabIndex = 4;
            this.iniciar.Text = "INICIAR";
            this.iniciar.UseVisualStyleBackColor = true;
            this.iniciar.Click += new System.EventHandler(this.Iniciar_Click);
            // 
            // baud_box
            // 
            this.baud_box.AutoCompleteCustomSource.AddRange(new string[] {
            "4800",
            "9600",
            "115200"});
            this.baud_box.FormattingEnabled = true;
            this.baud_box.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "115200"});
            this.baud_box.Location = new System.Drawing.Point(69, 48);
            this.baud_box.Name = "baud_box";
            this.baud_box.Size = new System.Drawing.Size(130, 21);
            this.baud_box.TabIndex = 0;
            this.baud_box.SelectedIndexChanged += new System.EventHandler(this.OnSelectBaudRateEvent);
            // 
            // terminarDAQ
            // 
            this.terminarDAQ.Location = new System.Drawing.Point(10, 69);
            this.terminarDAQ.Name = "terminarDAQ";
            this.terminarDAQ.Size = new System.Drawing.Size(75, 22);
            this.terminarDAQ.TabIndex = 5;
            this.terminarDAQ.Text = "TERMINAR";
            this.terminarDAQ.UseVisualStyleBackColor = true;
            this.terminarDAQ.Click += new System.EventHandler(this.TerminarDAQ_Click);
            // 
            // sensor_control
            // 
            this.sensor_control.Controls.Add(this.setpoint);
            this.sensor_control.Controls.Add(this.label5);
            this.sensor_control.Controls.Add(this.label4);
            this.sensor_control.Controls.Add(this.label3);
            this.sensor_control.Controls.Add(this.textBoxPress);
            this.sensor_control.Controls.Add(this.iniciarDAQ);
            this.sensor_control.Controls.Add(this.textBoxTemp);
            this.sensor_control.Controls.Add(this.terminarDAQ);
            this.sensor_control.Location = new System.Drawing.Point(22, 156);
            this.sensor_control.Name = "sensor_control";
            this.sensor_control.Size = new System.Drawing.Size(227, 220);
            this.sensor_control.TabIndex = 6;
            this.sensor_control.TabStop = false;
            this.sensor_control.Text = "Ligação ao DAQ";
            // 
            // setpoint
            // 
            this.setpoint.ForeColor = System.Drawing.SystemColors.WindowText;
            this.setpoint.Location = new System.Drawing.Point(122, 59);
            this.setpoint.Name = "setpoint";
            this.setpoint.Size = new System.Drawing.Size(77, 20);
            this.setpoint.TabIndex = 13;
            this.setpoint.Tag = "";
            this.setpoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.setpoint.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.setpoint.ValueChanged += new System.EventHandler(this.Setpoint_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "SetPoint Temperatura";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pressão do ar ( hPa )";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Temperatura ( Celsius )";
            // 
            // textBoxPress
            // 
            this.textBoxPress.Location = new System.Drawing.Point(124, 158);
            this.textBoxPress.Name = "textBoxPress";
            this.textBoxPress.Size = new System.Drawing.Size(75, 20);
            this.textBoxPress.TabIndex = 8;
            this.textBoxPress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // iniciarDAQ
            // 
            this.iniciarDAQ.Location = new System.Drawing.Point(9, 40);
            this.iniciarDAQ.Name = "iniciarDAQ";
            this.iniciarDAQ.Size = new System.Drawing.Size(75, 23);
            this.iniciarDAQ.TabIndex = 0;
            this.iniciarDAQ.Text = "INICIAR";
            this.iniciarDAQ.Click += new System.EventHandler(this.IniciarDAQ_Click);
            // 
            // textBoxTemp
            // 
            this.textBoxTemp.Location = new System.Drawing.Point(124, 114);
            this.textBoxTemp.Name = "textBoxTemp";
            this.textBoxTemp.Size = new System.Drawing.Size(75, 20);
            this.textBoxTemp.TabIndex = 7;
            this.textBoxTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tempGrph
            // 
            this.tempGrph.Location = new System.Drawing.Point(293, 19);
            this.tempGrph.Name = "tempGrph";
            this.tempGrph.ScrollGrace = 0D;
            this.tempGrph.ScrollMaxX = 0D;
            this.tempGrph.ScrollMaxY = 0D;
            this.tempGrph.ScrollMaxY2 = 0D;
            this.tempGrph.ScrollMinX = 0D;
            this.tempGrph.ScrollMinY = 0D;
            this.tempGrph.ScrollMinY2 = 0D;
            this.tempGrph.Size = new System.Drawing.Size(814, 357);
            this.tempGrph.TabIndex = 7;
            // 
            // pressGrph
            // 
            this.pressGrph.Location = new System.Drawing.Point(293, 420);
            this.pressGrph.Name = "pressGrph";
            this.pressGrph.ScrollGrace = 0D;
            this.pressGrph.ScrollMaxX = 0D;
            this.pressGrph.ScrollMaxY = 0D;
            this.pressGrph.ScrollMaxY2 = 0D;
            this.pressGrph.ScrollMinX = 0D;
            this.pressGrph.ScrollMinY = 0D;
            this.pressGrph.ScrollMinY2 = 0D;
            this.pressGrph.Size = new System.Drawing.Size(814, 347);
            this.pressGrph.TabIndex = 8;
            // 
            // alarmBox
            // 
            this.alarmBox.Location = new System.Drawing.Point(32, 420);
            this.alarmBox.Multiline = true;
            this.alarmBox.Name = "alarmBox";
            this.alarmBox.Size = new System.Drawing.Size(217, 91);
            this.alarmBox.TabIndex = 9;
            this.alarmBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // reportBox
            // 
            this.reportBox.Location = new System.Drawing.Point(10, 21);
            this.reportBox.Multiline = true;
            this.reportBox.Name = "reportBox";
            this.reportBox.Size = new System.Drawing.Size(214, 173);
            this.reportBox.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(100, 746);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "SAIR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Terminated_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reportBox);
            this.groupBox2.Location = new System.Drawing.Point(24, 534);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 206);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Relatório de sistema";
            // 
            // Janela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 797);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.alarmBox);
            this.Controls.Add(this.pressGrph);
            this.Controls.Add(this.tempGrph);
            this.Controls.Add(this.sensor_control);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Janela";
            this.Text = "DAQMonitor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.sensor_control.ResumeLayout(false);
            this.sensor_control.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.setpoint)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button iniciar;
        private System.Windows.Forms.Button terminarDAQ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox sensor_control;
        private System.Windows.Forms.Button iniciarDAQ;
        public System.Windows.Forms.ComboBox com_box;
        public System.Windows.Forms.ComboBox baud_box;
        public System.Windows.Forms.TextBox textBoxTemp;
        public System.Windows.Forms.TextBox textBoxPress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown setpoint;
        private System.ComponentModel.IContainer components;
        public ZedGraph.ZedGraphControl tempGrph;
        public ZedGraph.ZedGraphControl pressGrph;
        public System.Windows.Forms.TextBox alarmBox;
        public System.Windows.Forms.TextBox reportBox;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}