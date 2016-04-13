﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library___Login
{
    public partial class Form1 : Form
    {
        private string adminLogin;
        private string adminPassword;
        public Form1()
        {
            InitializeComponent();
            adminLogin = "admin";
            adminPassword = "Jablko";
            ErrorMessage.Visible = false;
            Username.Text = "Enter e-mail";
            Password.Text = "Enter password";
            Username.ForeColor = Color.Gray;
            Password.ForeColor = Color.Gray;
        }

        // after clicking label, there will be opened new method with registration form
        private void Registration_Click(object sender, EventArgs e)
        {
            RegistrationForm reg = new RegistrationForm();
            reg.Show();
            this.Hide();
            reg.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }

        //primary amdin login, that will be changed later
        private void Login_Click(object sender, EventArgs e)
        {
            if (Username.Text.Equals(adminLogin) && Password.Text.Equals(adminPassword))
            {
                Admin adminForm = new Admin();
                adminForm.Show();
                this.Hide();
                adminForm.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            }
            else
            {
                Connect2DB con = new Connect2DB();
                if (con.isUserRegistered(Username.Text, Password.Text))
                {
                    if (con.isUserAdmin(Username.Text) == "a")
                    {
                        Admin admin = new Admin();
                        admin.Show();
                        this.Hide();
                        admin.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
                    }
                    else if (con.isUserAdmin(Username.Text) == "u")
                    {
                        Users userForm = new Users(Username.Text, Password.Text);
                        userForm.Show();
                        this.Hide();
                        userForm.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
                    }
                    else
                    {
                        ErrorMessage.Text = "Your registration request is treated!";
                        ErrorMessage.Visible = true;
                    }
                }
                else
                {
                    ErrorMessage.Text = "Invalid email or password!";
                    ErrorMessage.Visible = true;
                }
            }
        }

        //set the looks of char at password textbox as *
        private void Password_TextChanged(object sender, EventArgs e)
        {
            Password.ForeColor = Color.Black;
            Password.PasswordChar = '*';
        }

        //if some opened formular will be closed, login form closed as well
        private void Form1_FormClosed(object sender, EventArgs e)
        {
            Username.Text = "";
            Password.Text = "";
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            RegistrationForm reg = new RegistrationForm();
            reg.Show();
            this.Hide();
            reg.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            Username.ForeColor = Color.Black;
        }
    }
}
