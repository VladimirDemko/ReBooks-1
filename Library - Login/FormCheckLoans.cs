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
    public partial class FormCheckLoans : Form
    {
        int waitingReg;
        string AdminID;
        Connect2DB con = new Connect2DB();
        List<string> books = new List<string>();
        List<string> readers = new List<string>();
        List<string> lendings = new List<string>();
        List<string> returns = new List<string>();

        public FormCheckLoans(string UserID)
        {
            InitializeComponent(); this.StartPosition = FormStartPosition.CenterScreen;
            DatabaseInfo.Visible = false;
            AdminID = UserID;
            waitingReg = con.waitingRegistration();
            if (waitingReg > 0)
            {
                registrationReguestToolStripMenuItem.Text = "Registration Request (" + waitingReg + ")";
            }
            else if (waitingReg == 0)
            {
                registrationReguestToolStripMenuItem.Text = "Registration Request";
            }
            else if (waitingReg == -1)
            {
                registrationReguestToolStripMenuItem.Text = "Registration Request";
                DatabaseInfo.Text = "Cannot connect to database!";
                DatabaseInfo.Visible = true;
            }
            books = con.checkLentBookNames();
            readers = con.checkOwnersOfLentBooks();
            lendings = con.checkDatesLendings();
            returns = con.checkReturnsDates();

            for (int i = 0; i < books.Count; i++)
            {
                ListViewItem item = new ListViewItem(books[i]);
                item.SubItems.Add(readers[i]);
                item.SubItems.Add(lendings[i]);
                item.SubItems.Add(returns[i]);

                listView1.Items.Add(item);
            }
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdminInterface home = new FormAdminInterface(AdminID);
            home.Show();
            this.Close();
        }

        private void addBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddBooks form = new FormAddBooks();
            form.Show(); // or form.ShowDialog(this);
        }

        private void addCategoryBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddBookCategory form = new FormAddBookCategory();
            form.Show(); // or form.ShowDialog(this);
        }

        private void addBookLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddBookLanguage form = new FormAddBookLanguage();
            form.Show();
        }

        private void addLoansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddLoan loans = new FormAddLoan();
            loans.ShowDialog();
        }

        private void checkLoansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCheckLoans.ActiveForm.Refresh();
        }

        private void updateUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUpdateUser updateUser = new FormUpdateUser(AdminID);
            updateUser.Show();
            this.Close();
        }

        public void registrationReguestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormWaitingRegistrations admin = new FormWaitingRegistrations(AdminID);
            admin.Show();
            this.Close();
        }

        private void switchToUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUserInterface userForm = new FormUserInterface(AdminID);
            userForm.ShowDialog();
            this.Close();
            userForm.FormClosed += new FormClosedEventHandler(UserForm_FormClosed);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin.ActiveForm.Show();
            this.Close();
        }

        private void UserForm_FormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FormUserInterface>().Any())
            {
                FormLogin.ActiveForm.Hide();
            }
            else
            {
                FormLogin.ActiveForm.Show();
            }
        }
    }
}
