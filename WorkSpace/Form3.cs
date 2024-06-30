using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WorkSpace
{
    public partial class Form3 : Form
    {
        private Employee Employee;
        private Form1 parentForm;
        DB someDB3 = new DB();
        void SetCollection()
        {
            someDB3.openConnect();
            new SqlCommand($"update dbo.employees set collsEmployee = '{this.Employee.collsEmployee + 1}' where passportEmployee = '{this.Employee.passportEmployee}'", someDB3.getConnect()).ExecuteNonQuery();
            someDB3.closeConnect();
        }
        internal Form3(Employee employee, Form1 form1)

        {
            InitializeComponent();
            this.Employee = employee;
            this.parentForm = form1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                someDB3.openConnect();
                if (radioButton1.Checked)
                {
                    SetCollection();
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Взыскание.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Довожу до Вашего сведения, что сегодня, {DateTime.Today.ToShortDateString()}, {Employee.jobtitleEmployee.ToLower()} {Employee.FIOemployee} в {DateTime.Now.Hour} час. {DateTime.Now.Minute} мин. _________________(опишите обстоятельства, в соответствии с которыми работник совершил дисциплинарный проступок и его необходимо привлечь к дисциплинарной ответственности).");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Взыскание применено!");
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                }
                else if (radioButton2.Checked)
                {
                    SetCollection();
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Взыскание.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Об объявлении выговора\r\nВ связи с отсутствием {Employee.jobtitleEmployee.ToLower()} отдела {Employee.deptEmployee.ToLower()} {Employee.FIOemployee} {DateTime.Today.ToShortDateString()} на работе в период с __:__ (начала его рабочего дня) до __:__ и непредставлением им фактов, свидетельствующих об уважительности причин данного отсутствия, на основании положений ст. 192 и 193 ТК РФ приказываю:\r\nЗа нарушение трудовых обязанностей _________________(Причина) объявить работнику выговор.");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Взыскание применено!");
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                }
                else if (radioButton3.Checked)
                {
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Взыскание.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"ПРИКАЗ(распоряжение) \r\nо прекращении (расторжении) трудового \r\nдоговора с работником (увольнении) \r\nПрекратить действие трудового договора от {Employee.employmentDate.ToShortDateString()} , N___, \r\nуволить {Employee.employmentDate.ToShortDateString()} \r\n(ненужное зачеркнуть) \r\n{Employee.FIOemployee} \r\n_______________________\r\nструктурное подразделение \r\n__________________________________________________________________ \r\nоснование прекращения (расторжения) трудового договора (увольнения)");
                    encotext.Close();
                    enco.Close();
                    new SqlCommand($"delete from dbo.employees where passportEmployee = '{this.Employee.passportEmployee}'", someDB3.getConnect()).ExecuteNonQuery();
                    MessageBox.Show("Взыскание применено! Работник успешно уволен!");
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                }
                someDB3.closeConnect();
            }
            else
            {
                someDB3.openConnect();
                if (radioButton3.Checked)
                {
                    this.Enabled = false;
                    new SqlCommand($"delete from dbo.employees where passportEmployee = '{this.Employee.passportEmployee}'", someDB3.getConnect()).ExecuteNonQuery();
                    MessageBox.Show("Взыскание применено! Работник успешно уволен!");
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                }
                else
                {
                    SetCollection();
                    this.Enabled = false;
                    MessageBox.Show("Взыскание применено!");
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                }
                someDB3.closeConnect();
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = this.Employee.FIOemployee;
        }
    }
}
