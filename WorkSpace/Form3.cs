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
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Уважаемая {this.Employee.FIOemployee}!\r\nХочется поблагодарить вас за Вашу деятельность в нашей компании. Благодаря Вам, мы вышли на качественно новый уровень и не падаем в грязь лицом в борьбе с конкурентами. Не остались незамеченными Ваши усилия и трудолюбие, которые Вы проявили в нашей организации. Благодарю Вас и желаю ещё большего развития Ваших навыков. Хочется верить, что наше сотрудничество продолжится ещё не один год.");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Взыскание применено!");
                    File.Open("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Open);
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                }
                else if (radioButton2.Checked)
                {
                    SetCollection();
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Согласно Положения о премировании и в связи с добросовестным исполнением обязанностей и перевыполнением плана ПРИКАЗЫВАЮ: Назначить премию сотруднику: {this.Employee.jobtitleEmployee} отдела {this.Employee.deptEmployee} {this.Employee.FIOemployee} в размере _____ рублей.");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Взыскание применено!");
                    File.Open("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Open);
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                }
                else if (radioButton3.Checked)
                {
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Награждается {this.Employee.FIOemployee}. С пожеланиями дальнейшего процветания и стабильности в Вашей деятельности, сил и упорства в служении делу. Наш коллектив неизменно будет Вам надежным и дружественным партнером.");
                    encotext.Close();
                    enco.Close();
                    new SqlCommand($"delete from dbo.employees where passportEmployee = '{this.Employee.passportEmployee}'", someDB3.getConnect()).ExecuteNonQuery();
                    MessageBox.Show("Взыскание применено! Работник успешно уволен!");
                    File.Open("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Open);
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
                    MessageBox.Show("Работник успешно уволен!");
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                    this.parentForm.RefreshEmployee();
                    someDB3.closeConnect();
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
