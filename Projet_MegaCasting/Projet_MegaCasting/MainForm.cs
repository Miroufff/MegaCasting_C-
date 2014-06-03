using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_MegaCasting
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CompanyForm companyForm = new CompanyForm();
            companyForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdsForm adsForm = new AdsForm();
            adsForm.ShowDialog();
        }
    }
}
