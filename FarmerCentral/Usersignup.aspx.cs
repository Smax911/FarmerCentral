using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FarmerCentral
{
    public partial class Usersignup : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script> alert('Successfully Registered');</script>");
            if (checkMemberExists())
            {

                Response.Write("<script>alert('Member Already Exist with this Member ID, try other ID');</script>");
            }
            else
            {
                signUpNewMember();
            }
        }

        // user defined method
        bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from member_master_table where member_id='" + txbUserID.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        void signUpNewMember()
        {
            //Response.Write("<script>alert('Testing');</script>");
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_table(full_name,dob,contact_no,email,province,city,pincode,full_address,member_id,password,account_status) values(@full_name,@dob,@contact_no,@email,@province,@city,@pincode,@full_address,@member_id,@password,@account_status)", con);
                cmd.Parameters.AddWithValue("@full_name", txbFullname.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TxbDOB.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", txbContuct.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txbEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@province", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", txbCity.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", txbPincode.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", txbFullAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", txbUserID.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txbPasswordL.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}