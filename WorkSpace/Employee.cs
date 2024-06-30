using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSpace
{
    internal class Employee
    {
        protected internal long passportEmployee { get; set; } = 0;
        protected internal string FIOemployee { get; set; }
        protected internal DateTime dateofbEmployee { get; set; }
        protected internal string genderEmployee { get; set; }
        protected internal long INNemployee { get; set; }
        protected internal long SNILSemployee { get; set; }
        protected internal long OMSemployee { get; set; }
        protected internal string expierienceEmployee { get; set; }
        protected internal string deptEmployee { get; set; }
        protected internal string jobtitleEmployee { get; set; }
        protected internal string educEmployee { get; set; }
        protected internal string WPemployee { get; set; }
        protected internal bool mil_IDemployee { get; set; }
        protected internal int collsEmployee { get; set; }
        protected internal int encosEmployee { get; set; }
        protected internal DateTime employmentDate { get; set; }

        public void DrawEmployee(DB db, string compare)
        {
            db.openConnect();
            FIOemployee = compare;
            SqlDataReader dataReaderfirst = new SqlCommand($"select passportEmployee from dbo.employees where FIOemployee = '{compare}'", db.getConnect()).ExecuteReader();
            if (dataReaderfirst.FieldCount != 0)
            {
                while (dataReaderfirst.Read())
                {
                    passportEmployee = Convert.ToInt64(dataReaderfirst.GetValue(0));
                }
                dataReaderfirst.Close();
                SqlDataReader dataReadersecond = new SqlCommand($"select * from dbo.employees where passportEmployee = '{passportEmployee}'", db.getConnect()).ExecuteReader();
                while (dataReadersecond.Read())
                {
                    FIOemployee = dataReadersecond.GetString(1);
                    dateofbEmployee = Convert.ToDateTime(dataReadersecond.GetValue(2));
                    genderEmployee = dataReadersecond.GetString(3);
                    INNemployee = Convert.ToInt64(dataReadersecond.GetValue(4));
                    SNILSemployee = Convert.ToInt64(dataReadersecond.GetValue(5));
                    OMSemployee = Convert.ToInt64(dataReadersecond.GetValue(6));
                    expierienceEmployee = dataReadersecond.GetString(7);
                    deptEmployee = dataReadersecond.GetString(8);
                    jobtitleEmployee = dataReadersecond.GetString(9);
                    educEmployee = dataReadersecond.GetString(10);
                    WPemployee = dataReadersecond.GetString(11);
                    mil_IDemployee = dataReadersecond.GetBoolean(12);
                    collsEmployee = Convert.ToInt32(dataReadersecond.GetValue(13));
                    encosEmployee = Convert.ToInt32(dataReadersecond.GetValue(14));
                    employmentDate = Convert.ToDateTime(dataReadersecond.GetValue(15));
                }
                db.closeConnect();
                dataReadersecond.Close();
            }
        }
    }
}
