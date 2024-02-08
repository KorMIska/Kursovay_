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
    public partial class Form2 : Form
    {

        public DB_Conector conector;
        public int id;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(DB_Conector conector, int id)
        {
            this.id = id;
            this.conector = conector;
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            conector.fill_dataView($"SELECT * FROM [Document] WHERE [ID_Applicant] = {id}", dataGridView1);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conector.Updata();
            DataRow row = conector.ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            conector.ds.Tables[0].Rows.Add(row);
            int rowI = dataGridView1.Rows.Count - 2;
            dataGridView1.Rows[rowI].Cells[0].Value = int.Parse(conector.Qwest("SELECT MAX(ID) FROM [Document];")[0]) + 1;
            dataGridView1.Rows[rowI].Cells[3].Value = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conector.Updata();
        }
    }
}
