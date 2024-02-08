using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovay_Lisy
{
    public partial class Form1 : Form
    {
        DB_Conector conector = new DB_Conector();

        public Form1()
        {
            InitializeComponent();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedTab = (sender as TabControl).SelectedTab;
            if (selectedTab == tabPage1)
            {
                ClerAbitur();
                qwestAbitur();
            }
            if (selectedTab == tabPage2)
            {
                textBox1.Text = conector.Qwest("SELECT [Name] FROM [University];")[0];
                textBox2.Text = conector.Qwest("SELECT [Chairman_Name] FROM [Commission]; ")[0];
                textBox4.Text = conector.Qwest("SELECT [Address] FROM [University];")[0];
                monthCalendar1.SetDate(DateTime.Parse(conector.Qwest("SELECT [Meeting_Date] FROM [Commission];")[0]));
                monthCalendar1.AnnuallyBoldedDates = new DateTime[] 
                {DateTime.Parse(conector.Qwest("SELECT [Meeting_Date] FROM [Commission];")[0])};
                conector.fill_dataView("SELECT [ID], [Name], [Seats] FROM [Specialty]; ", dataGridView1);

            }
        }



        #region Page1

        public Abiturient curent_abitur;

        private void button1_Click(object sender, EventArgs e)
        {
            Abiturient abitur = new Abiturient();
            abitur.Click += abiturient_Clic;
            flowLayoutPanel1.Controls.Add(abitur);
        }
        //функция выделение при клике
        private void abiturient_Clic(object sender, EventArgs e)
        {
            Abiturient clickedAbiturient = (Abiturient)sender;
            if (curent_abitur != null)
            {
                if(curent_abitur == clickedAbiturient)
                {
                    curent_abitur.BackColor = Color.White;
                    curent_abitur = null;
                    return;
                }
                curent_abitur.BackColor = Color.White;
                curent_abitur = clickedAbiturient;
                curent_abitur.BackColor = Color.Blue;
            }
            else
            {
                curent_abitur = clickedAbiturient;
                curent_abitur.BackColor = Color.Blue;
            }
        }
        // функция удаления выделенного итема
        private void button2_Click(object sender, EventArgs e)
        {

            if (curent_abitur != null)
            {
                curent_abitur.DELIT();
                flowLayoutPanel1.Controls.Remove(curent_abitur);
                curent_abitur = null;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            ClerAbitur();
            qwestAbitur();
        }

        public void ClerAbitur()
        {
            List<Control> controlsToRemove = new List<Control>();

            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if (item is Abiturient)
                {
                    controlsToRemove.Add(item);
                }
            }
            foreach (Control itemToRemove in controlsToRemove)
            {
                flowLayoutPanel1.Controls.Remove(itemToRemove);
            }
        }
        public void qwestAbitur()
        {
            string query = "Select ID From [Applicant];";
            foreach (var item in conector.Qwest(query))
            {
                Abiturient abitur = new Abiturient(int.Parse(item));
                abitur.Click += abiturient_Clic;
                flowLayoutPanel1.Controls.Add(abitur);
            }


        }

        #endregion

        #region Page2
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conector.Qwest($"UPDATE  [University]   SET [Name] = '{textBox1.Text}';");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conector.Qwest($"UPDATE  [University]   SET [Address]= '{textBox4.Text}';");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            conector.Qwest($"UPDATE  [Commission]  SET [Chairman_Name] = '{textBox2.Text}';");
        }


        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            conector.Updata();
            DataRow row = conector.ds.Tables[0].NewRow();
            conector.ds.Tables[0].Rows.Add(row);
            int rowI = dataGridView1.Rows.Count - 1;
            dataGridView1.Rows[rowI].Cells[0].Value = int.Parse(conector.Qwest("SELECT MAX(ID) FROM [Specialty];")[0]) + 1;
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conector.Updata();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Получение выбранной даты из monthCalendar1
            DateTime selectedDate = monthCalendar1.SelectionStart;

            // Преобразование выбранной даты в строку в нужном формате
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");

            // Сохранение выбранной даты в базу данных с помощью функции Qwest
            string query = $"UPDATE [Commission] SET Meeting_Date = '{formattedDate}'";
            conector.Qwest(query);
            monthCalendar1.SetDate(DateTime.Parse(conector.Qwest("SELECT [Meeting_Date] FROM [Commission];")[0]));
            monthCalendar1.AnnuallyBoldedDates = new DateTime[]
                {DateTime.Parse(conector.Qwest("SELECT [Meeting_Date] FROM [Commission];")[0])};
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ClerAbitur();
            string query = "Select ID From [Applicant];";
            foreach (var item in conector.Qwest(query))
            {
                Abiturient abitur = new Abiturient(int.Parse(item));
                abitur.Click += abiturient_Clic;
                if (abitur.FIO.ToLower().Contains(textBox5.Text.ToLower()))
                {
                    flowLayoutPanel1.Controls.Add(abitur);
                }
                
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var filter = $"Name LIKE '%{textBox3.Text}%'";
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = filter;
        }
    }
}
