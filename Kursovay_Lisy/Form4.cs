using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kursovay_Lisy
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public DB_Conector conector;
        public int id;

        public Form4(DB_Conector conector, int id)
        {
            this.id = id;
            this.conector = conector;
            InitializeComponent();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                var r = row.Cells[0].Value;

                conector.Qwest($"DELETE FROM [Applicant_Speciality] " +
                    $"WHERE [ID_Specialty] = (SELECT ID FROM [Specialty] WHERE Name = '{r}') AND ID_Applicant = {id};");

                dataGridView1.Rows.Remove(row);
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conector.Qwest($"INSERT INTO [Applicant_Speciality] ([ID_Specialty]," +
                $" [ID_Applicant]) VALUES ((SELECT ID FROM [Specialty] WHERE Name = '{comboBox1.SelectedItem}'), {id});");

            conector.fill_dataView($"SELECT  [Specialty].[Name]  FROM  [Specialty] JOIN [Applicant_Speciality] ON" +
                $"[Specialty].ID = [Applicant_Speciality].ID_Specialty WHERE [ID_Applicant] = {id};", dataGridView1);

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            conector.fill_dataView($"SELECT  [Specialty].[Name]  FROM  [Specialty] JOIN [Applicant_Speciality] ON" +
                $"[Specialty].ID = [Applicant_Speciality].ID_Specialty WHERE [ID_Applicant] = {id};", dataGridView1);
       

            var str = conector.Qwest("SELECT [Name] FROM [Specialty]");
            comboBox1.Items.AddRange(str.ToArray());
            comboBox1.SelectedIndex = 0;

            //dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
        }


    }
}
