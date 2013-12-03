namespace RealtimeAnalysis
{
    partial class FormRealtimeDiagram
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTasks = new System.Windows.Forms.DataGridView();
            this.buttonScheduleEDF = new System.Windows.Forms.Button();
            this.buttonDrawDiagram = new System.Windows.Forms.Button();
            this.buttonSchduleEDF = new System.Windows.Forms.Button();
            this.TaskNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExecutionTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoftDeadline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Probability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HardDeadline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTasks
            // 
            this.dataGridViewTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTasks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskNumber,
            this.Period,
            this.ExecutionTime,
            this.SoftDeadline,
            this.Probability,
            this.HardDeadline,
            this.Priority});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTasks.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTasks.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewTasks.Name = "dataGridViewTasks";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTasks.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTasks.RowTemplate.Height = 23;
            this.dataGridViewTasks.Size = new System.Drawing.Size(671, 278);
            this.dataGridViewTasks.TabIndex = 1;
            // 
            // buttonScheduleEDF
            // 
            this.buttonScheduleEDF.Location = new System.Drawing.Point(12, 296);
            this.buttonScheduleEDF.Name = "buttonScheduleEDF";
            this.buttonScheduleEDF.Size = new System.Drawing.Size(104, 23);
            this.buttonScheduleEDF.TabIndex = 2;
            this.buttonScheduleEDF.Text = "Schedule EDF";
            this.buttonScheduleEDF.UseVisualStyleBackColor = true;
            this.buttonScheduleEDF.Click += new System.EventHandler(this.buttonScheduleEDF_Click);
            // 
            // buttonDrawDiagram
            // 
            this.buttonDrawDiagram.Location = new System.Drawing.Point(122, 296);
            this.buttonDrawDiagram.Name = "buttonDrawDiagram";
            this.buttonDrawDiagram.Size = new System.Drawing.Size(75, 23);
            this.buttonDrawDiagram.TabIndex = 3;
            this.buttonDrawDiagram.Text = "Diagram";
            this.buttonDrawDiagram.UseVisualStyleBackColor = true;
            this.buttonDrawDiagram.Click += new System.EventHandler(this.buttonDrawDiagram_Click);
            // 
            // buttonSchduleEDF
            // 
            this.buttonSchduleEDF.Location = new System.Drawing.Point(203, 296);
            this.buttonSchduleEDF.Name = "buttonSchduleEDF";
            this.buttonSchduleEDF.Size = new System.Drawing.Size(109, 23);
            this.buttonSchduleEDF.TabIndex = 4;
            this.buttonSchduleEDF.Text = "ScheduleEDF";
            this.buttonSchduleEDF.UseVisualStyleBackColor = true;
            this.buttonSchduleEDF.Click += new System.EventHandler(this.buttonSchduleEDF_Click);
            // 
            // TaskNumber
            // 
            this.TaskNumber.DataPropertyName = "TaskNumber";
            this.TaskNumber.HeaderText = "TaskNumber";
            this.TaskNumber.Name = "TaskNumber";
            // 
            // Period
            // 
            this.Period.DataPropertyName = "Period";
            this.Period.FillWeight = 85F;
            this.Period.HeaderText = "Period";
            this.Period.Name = "Period";
            this.Period.Width = 85;
            // 
            // ExecutionTime
            // 
            this.ExecutionTime.DataPropertyName = "ExecutionTime";
            this.ExecutionTime.FillWeight = 80F;
            this.ExecutionTime.HeaderText = "Execution Time";
            this.ExecutionTime.Name = "ExecutionTime";
            this.ExecutionTime.Width = 80;
            // 
            // SoftDeadline
            // 
            this.SoftDeadline.DataPropertyName = "SoftDeadline";
            this.SoftDeadline.HeaderText = "Soft Deadline";
            this.SoftDeadline.Name = "SoftDeadline";
            // 
            // Probability
            // 
            this.Probability.DataPropertyName = "Probability";
            this.Probability.HeaderText = "Probability";
            this.Probability.Name = "Probability";
            // 
            // HardDeadline
            // 
            this.HardDeadline.DataPropertyName = "HardDeadline";
            this.HardDeadline.HeaderText = "Hard Deadline";
            this.HardDeadline.Name = "HardDeadline";
            // 
            // Priority
            // 
            this.Priority.DataPropertyName = "Priority";
            this.Priority.FillWeight = 60F;
            this.Priority.HeaderText = "Priority";
            this.Priority.Name = "Priority";
            this.Priority.Width = 60;
            // 
            // FormRealtimeDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 331);
            this.Controls.Add(this.buttonSchduleEDF);
            this.Controls.Add(this.buttonDrawDiagram);
            this.Controls.Add(this.buttonScheduleEDF);
            this.Controls.Add(this.dataGridViewTasks);
            this.Name = "FormRealtimeDiagram";
            this.Text = "Real-time Diagram";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTasks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTasks;
        private System.Windows.Forms.Button buttonScheduleEDF;
        private System.Windows.Forms.Button buttonDrawDiagram;
        private System.Windows.Forms.Button buttonSchduleEDF;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExecutionTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoftDeadline;
        private System.Windows.Forms.DataGridViewTextBoxColumn Probability;
        private System.Windows.Forms.DataGridViewTextBoxColumn HardDeadline;
        private System.Windows.Forms.DataGridViewTextBoxColumn Priority;
    }
}

