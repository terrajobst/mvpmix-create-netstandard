using System;
using System.Data;

using NQuery;

namespace Northwind
{
    public class NorthwindDb
    {
        public static string GetData()
        {
            string result = "";
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"C:\demos\create-netstandard\northwind.xml");

            DataContext dataContext = new DataContext();
            dataContext.AddTablesAndRelations(dataSet);

            string sql = @"
                SELECT  FirstName + ' ' + LastName Name,
                        Birthdate.AddYears(65) RetirementDate
                FROM    Employees
                WHERE   Birthdate.AddYears(65) < GETDATE()
            ";

            Query query = new Query(sql, dataContext);
            DataTable resultsTable = query.ExecuteDataTable();

            foreach (DataRow row in resultsTable.Rows)
            {
                string name = Convert.ToString(row["Name"]);
                DateTime retirementDate = Convert.ToDateTime(row["RetirementDate"]); ;

                result += name + ": " + retirementDate + "\r\n";
            }

            return result;
        }
    }
}
