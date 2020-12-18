﻿using Final_Lab_Assignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Lab_Assignment_code
{
    public partial class LoginForm : Form
    {
        DataAccess Da { get; set; }
        public LoginForm()
        {
            InitializeComponent();
            this.Da = new DataAccess();
        }

        private void btn_signin_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"select * from users where email = '" + this.email_txtBox.Text +
                               "'and password = '" + this.password_txtBox.Text + "';";
                DataSet ds = Da.ExecuteQuery(query);
                int serial = 0;
                while (serial <= (ds.Tables[0].Rows.Count - 1))
                {
                    if (ds.Tables[0].Rows[serial][2].ToString().Equals(this.email_txtBox.Text) && ds.Tables[0].Rows[serial][3].ToString().Equals(this.password_txtBox.Text))
                    {
                        this.Hide();
                        Dashboard dashboard = new Dashboard();
                        dashboard.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Log in failed...\n Password or user name did not match !!!", "ERROR 404", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    serial++;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "ERROR 404", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private bool CheckEmailCorrect(string mail)
        {
            return new EmailAddressAttribute().IsValid(mail);
        }

        private void password_OnClick(object sender, EventArgs e)
        {
            bool correct = CheckEmailCorrect(this.email_txtBox.Text);
            if (correct.Equals(false))
            {
                MessageBox.Show("Incorrect Mail Address !!!", "ERROR 404", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                this.email_txtBox.Clear();
            }
        }
    }
}