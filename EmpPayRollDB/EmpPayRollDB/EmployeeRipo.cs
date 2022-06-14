﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpPayRollDB
{
    public class EmployeeRipo
    {
        private SqlConnection con;
        private void connection()
        {
            string connectingString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AddressBook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(connectingString);
        }
        public string AddEmp(EmpModel obj)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spAddNewEmpPerson", con);
                com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("@Id", obj.Id);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@Salary", obj.Salary);
                com.Parameters.AddWithValue("@StartDate", obj.StartDate);
                com.Parameters.AddWithValue("@Gender", obj.Gender);
                com.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
                com.Parameters.AddWithValue("@Address", obj.Address);
                com.Parameters.AddWithValue("@Pay", obj.Pay);
                com.Parameters.AddWithValue("@Deduction", obj.Deduction);
                com.Parameters.AddWithValue("@TexablePay", obj.TexablePay);
                
                com.Parameters.AddWithValue("@IncomeTax", obj.IncomeTax);
                com.Parameters.AddWithValue("@NetPay", obj.NetPay);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i == 1)
                {
                    return "data Added";
                }
                else
                {
                    return "data not added";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.con.Close();
            }
        }

        public List<EmpModel> GetAllEmployees()
        {
            connection();
            List<EmpModel> EmpList = new List<EmpModel>();
            SqlCommand com = new SqlCommand("spViewDetailPerson", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new EmpModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Salary = Convert.ToDecimal(dr["Salary"]),
                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        ContactNumber = Convert.ToString(dr["ContactNumber"]),
                        Address = Convert.ToString(dr["Address"]),
                        Pay = Convert.ToDecimal(dr["Pay"]),
                        Deduction = Convert.ToDecimal(dr["Deduction"]),
                        TexablePay = Convert.ToDecimal(dr["TexablePay"]),
                       
                        IncomeTax = Convert.ToDecimal(dr["IncomeTax"]),
                        NetPay = Convert.ToDecimal(dr["NetPay"])
                    }
                    );
            }
            return EmpList;
        }
        //To Update Emp data   
        public bool UpdateEmp(EmpModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("SPUpdateEmpDetails", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Salary", obj.Salary);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteEmployee(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("spDeletePersonById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RetrieveByDate(DateTime date)
        {
            connection();
            SqlCommand com = new SqlCommand("spViewSameDateJoinPerson", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@StartDate", date);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


