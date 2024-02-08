using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovay_Lisy
{
    public class Abiturient: Panel
    {
        public string FIO;
        public string Telefon;
        public bool Dormitories = false;
        public bool   Statement   = false;
        public int ID;
        List<int> IDbal  = new List<int>();
        List<int> IDdoc  = new List<int>();
        List<int> IDspec = new List<int>();
        List<Bal> bal = new List<Bal>();
        List<Doc> doc = new List<Doc>();
        List<Spec> spec = new List<Spec>();
        DB_Conector conector = new DB_Conector();

        internal TextBox FIO_tb = new TextBox
        {
            Location = new System.Drawing.Point(10, 10),
            Text = "FIO",
            Size = new System.Drawing.Size(130, 25)
        };
        internal MaskedTextBox Telefon_tb = new MaskedTextBox
        {
            Location = new System.Drawing.Point(145, 10),
            Text = "Telefon",
            Mask = "+7(999) 000-0000", // Установите маску ввода для телефона
            RejectInputOnFirstFailure = true
        };
        internal CheckBox Dormitories_cb = new CheckBox
        {
            Location = new System.Drawing.Point(275, 10),
            Size = new System.Drawing.Size(30, 30)
          };
        internal CheckBox Statement_cb = new CheckBox
        {
            Location = new System.Drawing.Point(350, 10),
            Size = new System.Drawing.Size(30, 30)
        };
        internal Button btm1 = new Button
        {
            Text = "OK",
            Location = new System.Drawing.Point(650, 10),
            Size = new System.Drawing.Size(75, 25)
        };
        internal Button btm2 = new Button
        {
            Text = "Документ",
            Location = new System.Drawing.Point(390, 10),
            Size = new System.Drawing.Size(74, 25)
        };
        internal Button btm3 = new Button
        {
            Text = "Баллы",
            Location = new System.Drawing.Point(470, 10),
            Size = new System.Drawing.Size(73, 25)
        };
        internal Button btm4 = new Button
        {
            Text = "Специальности",
            Location = new System.Drawing.Point(548, 10),
            Size = new System.Drawing.Size(100, 25)
        };
        internal Panel separator_1 = new Panel
        {
            Location = new System.Drawing.Point(320, 0),
            Size = new System.Drawing.Size(1, 50),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = System.Drawing.Color.Gray
        };
        internal Panel separator_2 = new Panel
        {
            Location = new System.Drawing.Point(0, 0),
            Size = new System.Drawing.Size(1, 50),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = System.Drawing.Color.Gray
        };
        internal Panel separator_3 = new Panel
        {
            Location = new System.Drawing.Point(250, 0),
            Size = new System.Drawing.Size(1, 50),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = System.Drawing.Color.Gray
        };
        internal Panel separator_4 = new Panel
        {
            Location = new System.Drawing.Point(140, 0),
            Size = new System.Drawing.Size(1, 50),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = System.Drawing.Color.Gray
        };
        internal Panel separator_doun = new Panel
        {
            Location = new System.Drawing.Point(1, 50),
            Size = new System.Drawing.Size(460, 1),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = System.Drawing.Color.Black
        };

        public Abiturient()
        {
            InitializeAbiturient();
            ID = int.Parse(conector.Qwest("SELECT MAX(ID) FROM [Applicant];")[0]) + 1;
            conector.Qwest($"INSERT INTO [Applicant] (ID, Full_Name , Phone) VALUES ({ID}, 'ФИО', '0000000000');");
            conector.Qwest($"INSERT INTO Dormitory   (ID,Status, ID_Applicant) VALUES ({ID}, 0, {ID});");
            conector.Qwest($"INSERT INTO Application (ID,Status, ID_Applicant) VALUES ({ID}, 0, {ID});");
        }
        public Abiturient(int _ID)
        {
            InitializeAbiturient();

            ID = _ID;
            FIO = conector.Qwest($"SELECT [Full_Name] FROM [Applicant] WHERE ID = {ID};")[0];
            Telefon = conector.Qwest($"SELECT Phone FROM [Applicant] WHERE ID = {ID};")[0];
            Dormitories = conector.Qwest($"SELECT  [Status] FROM  [Dormitory]  WHERE [ID_Applicant] = {ID};")[0] == "0" ? false: true;
            Statement = conector.Qwest($"SELECT  [Status] FROM [Application] WHERE [ID_Applicant] = {ID};")[0] == "0" ? false : true; ;

            FIO_tb.Text = FIO;
            Telefon_tb.Clear();
            Telefon_tb.Text = Telefon;
            Dormitories_cb.Checked = Dormitories;
            Statement_cb.Checked = Statement;

        }

        public void DELIT()
        {
            conector.Qwest($"DELETE  FROM  [Applicant] WHERE ID = {ID};");
        }
        private void InitializeAbiturient()
        {
            this.Width = 730;
            this.Height = 41;
            this.Margin = new Padding(0, 0, 0, 0);

            this.Controls.Add(FIO_tb);
            this.Controls.Add(Telefon_tb);
            this.Controls.Add(Dormitories_cb);
            this.Controls.Add(Statement_cb);
            this.Controls.Add(btm1);
            this.Controls.Add(btm2);
            this.Controls.Add(btm3);
            this.Controls.Add(btm4);
            this.Controls.Add(separator_doun);
            this.Controls.Add(separator_1);
            this.Controls.Add(separator_2);
            this.Controls.Add(separator_3);
            this.Controls.Add(separator_4);

            btm1.Click += Btm1_Click;
            btm2.Click += Btm2_Click;
            btm3.Click += Btm3_Click;
            btm4.Click += Btm4_Click;
            Statement_cb.Click += Statement_cb_CheckedChanged;
        }

        private void Statement_cb_CheckedChanged(object sender, EventArgs e)
        {
            if(Statement_cb.Checked)
            {
                Form5 form5 = new Form5(conector, ID)
                {
                    StartPosition = FormStartPosition.Manual, // Разместите форму в указанном месте
                    Location = new System.Drawing.Point(100, 100) // Укажите координаты левого верхнего угла формы
                };
                form5.ShowDialog();
            }
        }

        //Кнопка сохранения 
        private void Btm1_Click(object sender, EventArgs e)
        {
            FIO = FIO_tb.Text;
            Telefon = Telefon_tb.Text;
            Dormitories = Dormitories_cb.Checked;
            Statement = Statement_cb.Checked;
            conector.Qwest($"UPDATE  [Applicant]   SET [Full_Name] = '{FIO}' WHERE ID = {ID};");
            conector.Qwest($"UPDATE  [Applicant]   SET [Phone] = '{Telefon}' WHERE ID = {ID};");
            conector.Qwest($"UPDATE  [Dormitory]   SET [Status] = {(Dormitories ? 1 : 0)}  WHERE [ID_Applicant] = {ID};");
            conector.Qwest($"UPDATE  [Application] SET [Status] = {(Statement ? 1 : 0)}    WHERE [ID_Applicant] = {ID};");

            // Добавьте код для сохранения данных и выхода из формы здесь
        }
        private void Btm2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(conector, ID)
            {
                StartPosition = FormStartPosition.Manual, // Разместите форму в указанном месте
                Location = new System.Drawing.Point(100, 100) // Укажите координаты левого верхнего угла формы
            };
            form2.ShowDialog();

            // Добавьте код для сохранения данных и выхода из формы здесь
        }
        private void Btm3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(conector, ID)
            {
                StartPosition = FormStartPosition.Manual, // Разместите форму в указанном месте
                Location = new System.Drawing.Point(100, 100) // Укажите координаты левого верхнего угла формы
            };
            form3.ShowDialog();

            // Добавьте код для сохранения данных и выхода из формы здесь
        }
        private void Btm4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(conector, ID)
            {
                StartPosition = FormStartPosition.Manual, // Разместите форму в указанном месте
                Location = new System.Drawing.Point(100, 100) // Укажите координаты левого верхнего угла формы
            };
            form4.ShowDialog();

            // Добавьте код для сохранения данных и выхода из формы здесь
        }







    }

    struct Bal
    {
        string name;
        int bal;
    }
    struct Doc 
    {
        string vid;
        int cod;
    }

    struct Spec
    {
        string name;
        int count_mest;
    }

}
