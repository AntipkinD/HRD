using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkSpace
{
    internal class Application
    {
        protected internal long passportApp { get; set; } = 0;
        protected internal string FIOapp { get; set; }
        protected internal DateTime dateofbApp { get; set; }
        protected internal string genderApp { get; set; }
        protected internal long INNapp { get; set; }
        protected internal long SNILSapp { get; set; }
        protected internal long OMSapp { get; set; }
        protected internal string experienceApp { get; set; }
        protected internal string deptApp { get; set; }
        protected internal string jobtitleApp { get; set; }
        protected internal string educApp { get; set; }
        protected internal string WPapp { get; set; }
        protected internal bool mil_IDapp { get; set; }

        public void DrawApplication(DB db, string compare)
        {
            db.openConnect();
            FIOapp = compare;
            SqlDataReader dataReaderfirst = new SqlCommand($"select passportApp from dbo.applications where FIOapp = '{compare}'", db.getConnect()).ExecuteReader();
            if (dataReaderfirst.FieldCount != 0)
            {
                while (dataReaderfirst.Read())
                {
                    passportApp = Convert.ToInt64(dataReaderfirst.GetValue(0));
                }
                dataReaderfirst.Close();
                SqlDataReader dataReadersecond = new SqlCommand($"select * from dbo.applications where passportApp = '{passportApp}'", db.getConnect()).ExecuteReader();
                while (dataReadersecond.Read())
                {
                    FIOapp = dataReadersecond.GetString(1);
                    dateofbApp = Convert.ToDateTime(dataReadersecond.GetValue(2));
                    genderApp = dataReadersecond.GetString(3);
                    INNapp = Convert.ToInt64(dataReadersecond.GetValue(4));
                    SNILSapp = Convert.ToInt64(dataReadersecond.GetValue(5));
                    OMSapp = Convert.ToInt64(dataReadersecond.GetValue(6));
                    experienceApp = dataReadersecond.GetString(7);
                    deptApp = dataReadersecond.GetString(8);
                    jobtitleApp = dataReadersecond.GetString(9);
                    educApp = dataReadersecond.GetString(10);
                    WPapp = dataReadersecond.GetString(11);
                    mil_IDapp = dataReadersecond.GetBoolean(12);
                }
                db.closeConnect();
                dataReadersecond.Close();
            }
        }
    }

}
