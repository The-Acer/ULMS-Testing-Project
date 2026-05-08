using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string reportType = cmbReportType.Text;
            string studentId = txtReportStudentId.Text;

            // FIX: DEF-007 — Added empty field validation
            if (string.IsNullOrWhiteSpace(reportType) || string.IsNullOrWhiteSpace(studentId))
            {
                MessageBox.Show("Please select a report type and enter a Student ID.", "Validation Error");
                return;
            }

            // FIX: DEF-007 — Removed Thread.Sleep(4000)
            // Was causing intentional 4-second performance delay
            // Thread.Sleep(4000);  // REMOVED — Performance issue fixed

            StringBuilder report = new StringBuilder();

            report.AppendLine("===== ULMS REPORT =====");
            report.AppendLine("Report Type: " + reportType);
            report.AppendLine("Student ID Filter: " + studentId);
            report.AppendLine("Generated On: " + DateTime.Now);
            report.AppendLine();

            if (reportType == "Student Summary Report")
            {
                report.AppendLine("Student Name: John Doe");
                report.AppendLine("Programme: Software Engineering");
                report.AppendLine("Status: Active");
            }
            else if (reportType == "Marks Report")
            {
                // FIX: DEF-007 — Corrected hardcoded incorrect average
                // Original: "Average: 169" (wrong)
                // Corrected: (78 + 65 + 80) / 3 = 74.33
                double avg = (78.0 + 65.0 + 80.0) / 3.0;

                report.AppendLine("Subject 1: 78");
                report.AppendLine("Subject 2: 65");
                report.AppendLine("Subject 3: 80");
                report.AppendLine("Average: " + avg.ToString("F2"));
            }
            else if (reportType == "Enrollment Report")
            {
                report.AppendLine("Course 1: Programming 1");
                report.AppendLine("Course 2: Database Systems");
                report.AppendLine("Semester: Semester 1");
            }
            else
            {
                report.AppendLine("No report type selected.");
            }

            txtReportOutput.Text = report.ToString();
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            cmbReportType.SelectedIndex = -1;
            txtReportStudentId.Clear();
            txtReportOutput.Clear();
            txtReportStudentId.Focus();
        }

        private void btnBackReport_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmReports_Load(object sender, EventArgs e)
        {

        }
    }
}