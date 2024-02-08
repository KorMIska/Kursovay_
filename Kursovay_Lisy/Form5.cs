using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovay_Lisy
{
    public partial class Form5 : Form
    {
        public DB_Conector conector;
        public int id;

        public Form5(DB_Conector conector, int id)
        {
            this.id = id;
            this.conector = conector;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conector.Qwest($"UPDATE  [Application]   SET [ID_Specialty] = (SELECT ID FROM [Specialty] " +
                $"WHERE Name = '{comboBox1.SelectedItem}') WHERE ID_Applicant = {id};");
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            var str = conector.Qwest("SELECT [Specialty].[Name]  FROM  [Specialty] JOIN [Applicant_Speciality]" +
                $" ON [Specialty].ID = [Applicant_Speciality].ID_Specialty WHERE [ID_Applicant] = {id}");

            comboBox1.Items.AddRange(str.ToArray());
            comboBox1.SelectedIndex = 0;
        }
    }
}
