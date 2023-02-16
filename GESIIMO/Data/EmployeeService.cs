using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Data
{
    public class EmployeeService
    {
        public DataTable GetEmployeeInfo()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Departament");
            dt.Columns.Add("Designation");
            dt.Columns.Add("JoinData");

            DataRow dr;

            for (int i=0; i<=50; i++)
            {
                dr = dt.NewRow();
                dr["EmpId"] = i;
                dr["EmpName"] ="nombre"+ i;
                dr["Departament"] = "Departament" + (i*2);
                dr["Designation"] = "Designation" + (i*4);
                dr["JoinData"] = DateTime.Now.AddYears(-5).AddDays(i);
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
