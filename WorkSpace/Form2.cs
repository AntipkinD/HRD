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

namespace WorkSpace
{
    public partial class Form2 : Form
    {
        protected string FIOemployee;
        protected int encosEmployee;
        protected long passportEmployee;
        protected string deptEmployee;
        protected string jobtitleEmployee;
        private Form1 parentForm;
        DB someDB2 = new DB();
        void SetEncouragement()
        {
            someDB2.openConnect();
            new SqlCommand($"update dbo.employees set encosEmployee = '{this.encosEmployee + 1}' where passportEmployee = '{this.passportEmployee}'", someDB2.getConnect()).ExecuteNonQuery();
            someDB2.closeConnect();
        }
        internal Form2(Employee employee, Form1 form1)
        {
            InitializeComponent();
            this.FIOemployee = employee.FIOemployee;
            this.encosEmployee = employee.encosEmployee;
            this.passportEmployee = employee.passportEmployee;
            this.deptEmployee = employee.deptEmployee;
            this.jobtitleEmployee = employee.jobtitleEmployee;
            this.parentForm = form1;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (radioButton1.Checked)
                {
                    Form1 form1 = new Form1();
                    SetEncouragement();
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Уважаемый(ая) {this.FIOemployee}!\r\nХочется поблагодарить вас за Вашу деятельность в нашей компании. Благодаря Вам, мы вышли на качественно новый уровень и не падаем в грязь лицом в борьбе с конкурентами. Не остались незамеченными Ваши усилия и трудолюбие, которые Вы проявили в нашей организации. Благодарю Вас и желаю ещё большего развития Ваших навыков. Хочется верить, что наше сотрудничество продолжится ещё не один год.");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Поощрение применено!");
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                }
                else if (radioButton2.Checked)
                {
                    Form1 form1 = new Form1();
                    SetEncouragement();
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Согласно Положения о премировании и в связи с добросовестным исполнением обязанностей и перевыполнением плана ПРИКАЗЫВАЮ: Назначить премию сотруднику: {this.jobtitleEmployee.ToLower()} отдела {this.deptEmployee.ToLower()} {this.FIOemployee} в размере _____ рублей.");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Поощрение применено!");
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                }
                else if (radioButton3.Checked)
                {
                    Form1 form1 = new Form1();
                    SetEncouragement();
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Награждается {this.FIOemployee}. С пожеланиями дальнейшего процветания и стабильности в Вашей деятельности, сил и упорства в служении делу. Наш коллектив неизменно будет Вам надежным и дружественным партнером.");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Поощрение применено!");
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                }
                else if (radioButton4.Checked)
                {
                    Form1 form1 = new Form1();
                    SetEncouragement();
                    FileStream enco = new FileStream("D:\\VSWorks\\WorkSpace\\WorkSpace\\sys\\Поощрение.txt", FileMode.Create);
                    StreamWriter encotext = new StreamWriter(enco);
                    encotext.Write($"Ценным подарком награждается {this.FIOemployee}. С наилучшими пожеланиями. Надеемся на дальнейшее благоприятное сотрудничество и неизменную преданность общему делу компании. С уважением коллектив компании!");
                    encotext.Close();
                    enco.Close();
                    MessageBox.Show("Поощрение применено!");
                    this.Enabled = false;
                    this.Close();
                    this.parentForm.Enabled = true;
                    this.parentForm.Activate();
                }
            }
            else
            {
                SetEncouragement();
                this.Enabled = false;
                MessageBox.Show("Поощрение применено!");
                this.Close();
                this.parentForm.Enabled = true;
                this.parentForm.Activate();
                this.parentForm.RefreshEmployee();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = this.FIOemployee;
        }
    }
}
