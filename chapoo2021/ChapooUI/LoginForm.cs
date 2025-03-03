﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChapooModel;
using ChapooLogic;

namespace ChapooUI
{
    public partial class LoginForm : Form
    {
        private Employee_Service employee_service = new Employee_Service();
        public LoginForm()
        {
            InitializeComponent();
        }

        // Made by Jelle de Vries
        private void CheckCredentials()
        {
            ChapooLogic.Employee_Service service = new ChapooLogic.Employee_Service();
            ChapooLogic.Function_Service functieService = new ChapooLogic.Function_Service();
            // check if input is is valid input
            if (string.IsNullOrEmpty(txt_User.Text) || string.IsNullOrEmpty(txt_Pass.Text) || !int.TryParse(txt_User.Text, out int id))
            {
                lbl_Error.Text = "Incorrect Username/Password";
            }
            //assign values to var
            string password = txt_Pass.Text;
            Employee huidigGebruiker = new Employee();
            huidigGebruiker.username = txt_User.Text;
            // get salt with username
            string salt = service.GetSalt(huidigGebruiker);
            HashwithSalt retrieve = new HashwithSalt();
            // generate hash with input password and the retrieved salt
            string hash = retrieve.GenerateHash(password, salt);

            // get credentials and check if they are valid
            /*            Employee employee = employee_service.GetCredentials(int.Parse(huidigGebruiker.username), hash);*/
            huidigGebruiker.validlogin = 1;

            // show welcome box if login is valid
            if (huidigGebruiker.validlogin == 1)
            {
                int function = functieService.GetFunctie(huidigGebruiker);
                MessageBox.Show($"Welkom {huidigGebruiker.username.ToUpper()}\n\nU bent ingelogd met functie: {(Employee.FunctieNaam)function}");

                this.Hide();
                //  open correct form according to function
                if (function == 1)
                {
                    Boolean keuken = false;
                    BarKeukenForm barOverzicht = new BarKeukenForm(keuken);
                    barOverzicht.Show();
                    
                }
                else if (function == 2)
                {
                    AdminForm adminform = new AdminForm();
                    adminform.Show();
                }
                else if (function == 3)
                {
                    Boolean keuken = true;
                    BarKeukenForm keukenOverzicht = new BarKeukenForm(keuken);
                    keukenOverzicht.Show();
                }
                else if (function == 4)
                {
                    BedieningForm bedieningForm = new BedieningForm();
                    bedieningForm.Show();
                }
                else
                {
                    MessageBox.Show("Geen functie gevonden.");
                }
            }
        }

        // action on clock of login button
        private void lbl_WachtVer_Click(object sender, EventArgs e)
        {
            WachtwoordVergeten wachtwoordVergeten = new WachtwoordVergeten();
            wachtwoordVergeten.Show();
            this.Close();
        }

        // textbox behavior on entering focus and leaving focus

        private void txt_User_Enter_1(object sender, EventArgs e)
        {
            if (txt_User.Text == "Username")
            {
                txt_User.Text = "";
            }
        }

        private void txt_User_Leave_1(object sender, EventArgs e)
        {
            if (txt_User.Text.Length == 0)
            {
                txt_User.Text = "Username";
            }
        }

        private void txt_Pass_Enter_1(object sender, EventArgs e)
        {
            if (txt_Pass.Text == "Password")
            {
                txt_Pass.Text = "";
                txt_Pass.UseSystemPasswordChar = true;
            }
        }

        private void txt_Pass_Leave_1(object sender, EventArgs e)
        {
            if (txt_Pass.Text.Length == 0)
            {
                txt_Pass.Text = "Password";
                txt_Pass.UseSystemPasswordChar = false;
            }
        }

        // behavior on closing of application
        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLoginAsAdmin_Click(object sender, EventArgs e)
        {
            AdminForm adminform = new AdminForm();
            adminform.Show();
            this.Hide();
        }

        private void btnLoginAsBarman_Click(object sender, EventArgs e)
        {
            Boolean keuken = false;
            BarKeukenForm barkeuken = new BarKeukenForm(keuken);
            barkeuken.Show();
            this.Hide();
        }

        private void btnLoginAsKeuken_Click(object sender, EventArgs e)
        {
            Boolean keuken = true;
            BarKeukenForm barkeuken = new BarKeukenForm(keuken);
            barkeuken.Show();
            this.Hide();
        }

        private void btnLoginAsBediening_Click(object sender, EventArgs e)
        {
            BedieningForm bform = new BedieningForm();
            bform.Show();
            this.Hide();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {            
            CheckCredentials();
        }

        private void lbl_WachtVer_Click_1(object sender, EventArgs e)
        {
            WachtwoordVergeten wachtwoordvergeten = new WachtwoordVergeten();
            wachtwoordvergeten.Show();
            this.Hide();
        }


    }
}
