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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            ErrorMessage.Visible = false;
            Username.Text = "Please enter your email";
            Password.Text = "Password";
            Username.ForeColor = Color.Gray;
            Password.ForeColor = Color.Gray;
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        // after clicking button "Sign up", there will be opened new method with registration form
        private void Register_Click(object sender, EventArgs e)
        {
            FormRegistrationUser reg = new FormRegistrationUser();
            reg.Show();
            this.Hide();
            reg.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }

        //primary amdin login, that will be changed later
        private void Login_Click(object sender, EventArgs e)
        {
            Connect2DB con = new Connect2DB();
            if (con.isUserRegistered(Username.Text, Password.Text))
            {
                if (con.isUserBlocked(Username.Text))
                {
                    ErrorMessage.Text = "Your account is blocked.\nPlease contact your library.";
                    ErrorMessage.Visible = true;
                }
                else
                {
                    if (con.isUserAdmin(Username.Text) == "admin")
                    {
                        FormAdminInterface admin = new FormAdminInterface(Username.Text, Password.Text);
                        admin.Show();
                        this.Hide();
                        admin.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
                    }
                    else if (con.isUserAdmin(Username.Text) == "user")
                    {
                        FormUserInterface userForm = new FormUserInterface();
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
            }
            else
            {
                ErrorMessage.Text = "Invalid email or password!";
                ErrorMessage.Visible = true;
            }
        }

        //set the looks of char at password textbox as *
        private void Password_TextChanged(object sender, EventArgs e)
        {
            Password.ForeColor = Color.Black;
           // Password.PasswordChar = '*';
        }

        //if some opened formular will be closed, login form closed as well
        private void Form1_FormClosed(object sender, EventArgs e)
        {
                this.Hide();

                Username.Text = "Please enter your email";
                Password.Text = "Password";
                Username.ForeColor = Color.Gray;
                Password.ForeColor = Color.Gray;
                ErrorMessage.Visible = false;
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void Username_TextChanged(object sender, EventArgs e)
        {
            Username.ForeColor = Color.Black;
        }

        private void Username_MouseClick(object sender, MouseEventArgs e)
        {
            Username.Text = "";
            if (Password.Text == "")
            {
                Password.Text = "Password";
                Password.ForeColor = Color.Gray;
            }
        }

        private void Username_Click(object sender, EventArgs e)
        {
            Username.Text = "";
            if (Password.Text == "")
            {
                Password.Text = "Password";
                Password.ForeColor = Color.Gray;
            }
        }

        private void Password_MouseClick(object sender, MouseEventArgs e)
        {
            Password.Text = "";
            if (Username.Text == "")
            {
                Username.Text = "Please enter your email";
                Username.ForeColor = Color.Gray;
            }
        }

        private void Password_Click(object sender, EventArgs e)
        {
            Password.Text = "";
            if (Username.Text == "")
            {
                Username.Text = "Please enter your email";
                Username.ForeColor = Color.Gray;
            }
        }

        private void Password_Enter(object sender, EventArgs e)
        {
            Password.PasswordChar = '*';
        }

        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_Click(Username, null);
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_Click(Username, null);
            }
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
    }
}
