using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtimeAnalysis
{
    public partial class FormTimeDiagram : Form
    {
        public FormTimeDiagram()
        {
            InitializeComponent();            
        }


        internal void SetTask(List<List<JobEvent>> listList, int startTime, long endTime)
        {
            this.tableLayoutPanel1.RowCount = listList.Count;

            foreach (List<JobEvent> list in listList)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / listList.Count));

                TimeDiagram diagram = new TimeDiagram();
                diagram.BackColor = System.Drawing.Color.White;
                diagram.Location = new System.Drawing.Point(3, 3);
                diagram.MinimumSize = new System.Drawing.Size(100, 100);
                diagram.Size = new System.Drawing.Size(582, 100);
                diagram.Dock = DockStyle.Fill;

                diagram.SetTask(list, startTime, endTime);

                tableLayoutPanel1.Controls.Add(diagram);
            }
        }

        internal void SetTask(PeriodicTask periodicTask, int startTime, long endTime)
        {
            TimeDiagram diagram = new TimeDiagram();
            diagram.BackColor = System.Drawing.Color.White;
            diagram.Location = new System.Drawing.Point(3, 3);
            diagram.MinimumSize = new System.Drawing.Size(100, 100);
            diagram.Size = new System.Drawing.Size(582, 100);
            diagram.Dock = DockStyle.Fill;

            diagram.SetTask(periodicTask, startTime, endTime);

            tableLayoutPanel1.Controls.Add(diagram);            
        }
    }
}
