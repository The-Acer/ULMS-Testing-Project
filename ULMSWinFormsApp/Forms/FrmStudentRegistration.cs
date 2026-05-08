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
    public partial class FrmStudentRegistration : Form
    {
        public FrmStudentRegistration()
        {
            InitializeComponent();
        }


        private void btnSaveStudent_Click(object sender, EventArgs e)
        {
            
            // FIX: DEF-006 — Added empty field validation
            // Prevents crash when fields are blank
            if (string.IsNullOrWhiteSpace(txtStudentId.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(cmbProgramme.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error");
                return;
            }

            // FIX: DEF-006 — Replaced int.Parse with int.TryParse
            // Original: Age = int.Parse(txtAge.Text);  ← Crashed on empty/text input
            // Corrected: Uses TryParse to safely convert
            int age;
            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Please enter a valid numeric age.", "Invalid Input");
                return;
            }

            // FIX: DEF-006 — Added age range validation
            if (age < 16 || age > 120)
            {
                MessageBox.Show("Please enter a valid age between 16 and 120.", "Invalid Age");
                return;
            }

            Student student = new Student
            {
                StudentId = txtStudentId.Text,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Age = age,     // FIX: DEF-006 — Now uses validated 'age' variable instead of int.Parse
                Programme = cmbProgramme.Text
            };

            txtStudentOutput.Text =
                "Student saved successfully!" + Environment.NewLine +
                "Student ID: " + student.StudentId + Environment.NewLine +
                "Full Name: " + student.FullName + Environment.NewLine +
                "Email: " + student.Email + Environment.NewLine +
                "Age: " + student.Age + Environment.NewLine +
                "Programme: " + student.Programme;
        }

        private void btnClearStudent_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtAge.Clear();
            cmbProgramme.SelectedIndex = -1;
            txtStudentOutput.Clear();
            txtStudentId.Focus();
        }

        //Add Back button to return to dashboard
        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentRegistration_Load(object sender, EventArgs e)
        {

        }
    }
}