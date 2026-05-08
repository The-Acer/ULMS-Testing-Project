using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ULMSWinFormsApp.Models;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmMarksCapture : Form
    {
        public FrmMarksCapture()
        {
            InitializeComponent();
        }

        private void btnCalculateResults_Click(object sender, EventArgs e)
        {
           
            // FIX: DEF-005 — Added input validation for empty fields
            
            if (string.IsNullOrWhiteSpace(txtMarkStudentId.Text) ||
                string.IsNullOrWhiteSpace(txtMarkStudentName.Text))
            {
                MessageBox.Show("Please enter Student ID and Name.", "Validation Error");
                return;
            }

            
            // FIX: DEF-005 — Replaced Convert.ToDouble with TryParse
            // to prevent crashes on non-numeric input
            
            double sub1, sub2, sub3;

            if (!double.TryParse(txtSubject1.Text, out sub1) ||
                !double.TryParse(txtSubject2.Text, out sub2) ||
                !double.TryParse(txtSubject3.Text, out sub3))
            {
                MessageBox.Show("Please enter valid numeric marks.", "Invalid Input");
                return;
            }

            
            // FIX: DEF-005 — Added range validation (0–100)
            
            if (sub1 < 0 || sub1 > 100 || sub2 < 0 || sub2 > 100 || sub3 < 0 || sub3 > 100)
            {
                MessageBox.Show("Marks must be between 0 and 100.", "Invalid Range");
                return;
            }

            MarkRecord record = new MarkRecord();

            record.StudentId = txtMarkStudentId.Text;
            record.StudentName = txtMarkStudentName.Text;
            record.Subject1 = sub1;   // FIX: DEF-005 — Now uses validated variable instead of Convert.ToDouble
            record.Subject2 = sub2;   // FIX: DEF-005 — Now uses validated variable instead of Convert.ToDouble
            record.Subject3 = sub3;   // FIX: DEF-005 — Now uses validated variable instead of Convert.ToDouble

            
            // Corrected: (Subject1 + Subject2 + Subject3) / 3 (all three divided by 3)
            
            record.Average = (record.Subject1 + record.Subject2 + record.Subject3) / 3;

            if (record.Average >= 50)
            {
                record.ResultStatus = "PASS";
            }
            else
            {
                record.ResultStatus = "FAIL";
            }

            txtMarksOutput.Text =
                "Marks processed successfully!" + Environment.NewLine +
                "Student ID: " + record.StudentId + Environment.NewLine +
                "Student Name: " + record.StudentName + Environment.NewLine +
                "Subject 1: " + record.Subject1 + Environment.NewLine +
                "Subject 2: " + record.Subject2 + Environment.NewLine +
                "Subject 3: " + record.Subject3 + Environment.NewLine +
                "Average: " + record.Average.ToString("F2") + Environment.NewLine +   // FIX: Formatted to 2 decimal places
                "Final Result: " + record.ResultStatus;
        }

        private void btnClearMarks_Click(object sender, EventArgs e)
        {
            txtMarkStudentId.Clear();
            txtMarkStudentName.Clear();
            txtSubject1.Clear();
            txtSubject2.Clear();
            txtSubject3.Clear();
            txtMarksOutput.Clear();
            txtMarkStudentId.Focus();
        }

        private void btnBackMarks_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMarksCapture_Load(object sender, EventArgs e)
        {

        }
    }
}