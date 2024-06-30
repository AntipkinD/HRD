using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorkSpace
{
    public partial class Form1 : Form
    {
        DB someDB = new DB();
        Application someApplication = new Application();
        Employee someEmployee = new Employee();
        bool change = false;
        public void RefreshApplication()
        {
            if (someApplication.passportApp != 0)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                someApplication.DrawApplication(someDB, comboBox1.Text);
                label1.Text = $"ФИО: {someApplication.FIOapp}";
                label2.Text = $"Дата рождения: {someApplication.dateofbApp.ToShortDateString()}";
                label3.Text = $"Пол: {someApplication.genderApp}";
                label4.Text = $"Серия и номер паспорта: {someApplication.passportApp}";
                label5.Text = $"ИНН: {someApplication.INNapp}";
                label6.Text = $"СНИЛС: {someApplication.SNILSapp}";
                label7.Text = $"Полис ОМС: {someApplication.OMSapp}";
                label8.Text = $"Стаж: {someApplication.experienceApp}";
                label9.Text = $"Отдел: {someApplication.deptApp}";
                label10.Text = $"Должность: {someApplication.jobtitleApp}";
                label11.Text = $"Образование: {someApplication.educApp}";
                label12.Text = $"Желаемая заработная плата: {someApplication.WPapp}";
                if (someApplication.mil_IDapp.ToString() == "True")
                    label13.Text = "Военный билет: Есть";
                else label13.Text = "Военный билет: Нет";
            }
            else comboBox1.Text = "Нет доступных заявок!";
        }
        internal void RefreshEmployee()
        {

            someEmployee.DrawEmployee(someDB, comboBox2.Text);
            if (someEmployee.passportEmployee != 0)
            {
                button4.Enabled = true;
                button3.Enabled = true;
                employeesBindingSource.ResetBindings(true);
                comboBox2.Update();
                textBox1.Text = $"{someEmployee.FIOemployee}";
                dateTimePicker1.Value = someEmployee.dateofbEmployee;
                textBox2.Text = $"{someEmployee.genderEmployee}";
                textBox3.Text = $"{someEmployee.passportEmployee}";
                textBox4.Text = $"{someEmployee.INNemployee}";
                textBox5.Text = $"{someEmployee.SNILSemployee}";
                textBox6.Text = $"{someEmployee.OMSemployee}";
                label21.Text = $"Стаж в компании: {DateTime.Today.Year - someEmployee.employmentDate.Year} лет";
                textBox7.Text = $"{someEmployee.deptEmployee}";
                textBox8.Text = $"{someEmployee.jobtitleEmployee}";
                textBox9.Text = $"{someEmployee.educEmployee}";
                textBox10.Text = $"{someEmployee.WPemployee}";
                label26.Text = $"Взыскания: {someEmployee.collsEmployee} раз";
                label27.Text = $"Поощрения: {someEmployee.encosEmployee} раз";
                dateTimePicker2.Value = someEmployee.employmentDate;
            }
            else comboBox2.Text = "Нет доступных работников!";
        }
        void RefuseApplication()
        {
            if (someApplication.passportApp != 0)
            {
                MessageBox.Show("В заявке отказано!");
                new SqlCommand($"delete from dbo.applications where passportApp = '{someApplication.passportApp}'", someDB.getConnect()).ExecuteNonQuery();
            }
            else MessageBox.Show("Нет доступных заявок!");
        }
        void AcceptApplication()
        {
            bool uniqueEmployee = true;
            if (someApplication.passportApp != 0)
            {
                someDB.openConnect();
                SqlDataReader dataReader = new SqlCommand($"select passportEmployee, INNemployee, SNILSemployee, OMSemployee from dbo.employees where passportEmployee = '{someApplication.passportApp}' or INNemployee = '{someApplication.INNapp}' or SNILSemployee = '{someApplication.SNILSapp}' or OMSemployee = '{someApplication.OMSapp}'", someDB.getConnect()).ExecuteReader();
                if (dataReader.HasRows)
                {
                    uniqueEmployee = false;
                }
                dataReader.Close();
                if (uniqueEmployee == true)
                {
                    new SqlCommand("insert into dbo.employees (passportEmployee, FIOemployee, dateofbEmployee, genderEmployee, INNemployee, SNILSemployee, OMSemployee, expierienceEmployee, deptEmployee, jobtitleEmployee, educEmployee, WPemployee, mil_IDemployee, collsEmployee, encosEmployee, employmentDate) values " +
                    $"('{someApplication.passportApp}', '{someApplication.FIOapp}', '{someApplication.dateofbApp}', '{someApplication.genderApp}', '{someApplication.INNapp}', '{someApplication.SNILSapp}', '{someApplication.OMSapp}', '{someApplication.experienceApp}', '{someApplication.deptApp}', '{someApplication.jobtitleApp}', '{someApplication.educApp}', '{someApplication.WPapp}', '{someApplication.mil_IDapp}', '{0}', '{0}', '{DateTime.Now}')", someDB.getConnect()).ExecuteNonQuery();
                    MessageBox.Show("Заявка успешно принята!");
                    new SqlCommand($"delete from dbo.applications where passportApp = '{someApplication.passportApp}'", someDB.getConnect()).ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Работник с такими данными уже присутствуют в списке работников! Заявка будет удалена!");
                    new SqlCommand($"delete from dbo.applications where passportApp = '{someApplication.passportApp}'", someDB.getConnect()).ExecuteNonQuery();
                }
                someDB.closeConnect();
            }
            else MessageBox.Show("Нет доступных заявок!");
        }
        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = new DateTime(DateTime.Today.Year - 18, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0, 0);
            dateTimePicker1.Value = new DateTime(DateTime.Today.Year - 18, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0, 0);
            dateTimePicker2.MaxDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "workSpaceDataSet1.employees". При необходимости она может быть перемещена или удалена.
            this.employeesTableAdapter.Fill(this.workSpaceDataSet1.employees);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "workSpaceDataSet.applications". При необходимости она может быть перемещена или удалена.
            this.applicationsTableAdapter.Fill(this.workSpaceDataSet.applications);
            RefreshApplication();
            RefreshEmployee();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AcceptApplication();
            RefreshApplication();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefuseApplication();
            RefreshApplication();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshEmployee();
            Form3 form3 = new Form3(someEmployee, this);
            this.Enabled = false;
            form3.Show();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshEmployee();
            Form2 form2 = new Form2(someEmployee, this);
            this.Enabled = false;
            form2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (change)
            {
                try
                {
                    if (textBox1.Text != $"{someEmployee.FIOemployee}" | dateTimePicker1.Value != someEmployee.dateofbEmployee | textBox2.Text != $"{someEmployee.genderEmployee}" |
                    textBox3.Text != $"{someEmployee.passportEmployee}" | textBox4.Text != $"{someEmployee.INNemployee}" | textBox5.Text != $"{someEmployee.SNILSemployee}" |
                    textBox6.Text != $"{someEmployee.OMSemployee}" | textBox7.Text != $"{someEmployee.deptEmployee}" | textBox8.Text != $"{someEmployee.jobtitleEmployee}" |
                    textBox9.Text != $"{someEmployee.educEmployee}" | textBox10.Text != $"{someEmployee.WPemployee}" | dateTimePicker2.Value != someEmployee.employmentDate)
                    {
                        someDB.openConnect();
                        SqlDataReader dataReader = new SqlCommand($"select passportEmployee, INNemployee, SNILSemployee, OMSemployee from dbo.employees where passportEmployee = '{textBox3.Text}' or INNemployee = '{textBox4.Text}' or SNILSemployee = '{textBox5.Text}' or OMSemployee = '{textBox6.Text}'", someDB.getConnect()).ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            MessageBox.Show("Ошибка при редактировании данных!");
                        }
                        else
                        {
                            new SqlCommand($"update dbo.employees set FIOemployee = '{textBox1.Text}', dateofbEmployee = '{dateTimePicker1.Value}'," +
                                $"genderEmployee = '{textBox2.Text}', passportEmployee = '{textBox3.Text}', INNemployee = '{textBox4.Text}'," +
                                $"SNILSemployee = '{textBox5.Text}', OMSemployee = '{textBox6.Text}', deptEmployee = '{textBox7.Text}'," +
                                $"jobtitleEmployee = '{textBox8.Text}', educEmployee = '{textBox9.Text}', WPemployee = '{textBox10.Text}'," +
                                $"employmentDate = '{dateTimePicker2.Value}' where passportEmployee = '{someEmployee.passportEmployee}'", someDB.getConnect()).ExecuteNonQuery();
                        }
                        someDB.closeConnect();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка при редактировании данных!");
                }
            }
            textBox1.ReadOnly = !textBox1.ReadOnly;
            textBox2.ReadOnly = !textBox2.ReadOnly;
            textBox3.ReadOnly = !textBox3.ReadOnly;
            textBox4.ReadOnly = !textBox4.ReadOnly;
            textBox5.ReadOnly = !textBox5.ReadOnly;
            textBox6.ReadOnly = !textBox6.ReadOnly;
            textBox7.ReadOnly = !textBox7.ReadOnly;
            textBox8.ReadOnly = !textBox8.ReadOnly;
            textBox9.ReadOnly = !textBox9.ReadOnly;
            textBox10.ReadOnly = !textBox10.ReadOnly;
            dateTimePicker2.Enabled = !dateTimePicker2.Enabled;
            dateTimePicker1.Enabled = !dateTimePicker1.Enabled;
            RefreshEmployee();
            change = !change;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEmployee();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ошибка при изменении данных!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshApplication();
        }
    }
}
