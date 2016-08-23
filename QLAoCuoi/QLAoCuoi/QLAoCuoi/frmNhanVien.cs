using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace QLAoCuoi
{
    public partial class frmNhanVien : Form
    {
        string cnstr;
        SqlConnection cn;
        DataSet ds;
        SqlDataAdapter da;
        DataTable dt;
        
        public frmNhanVien()
        {
            InitializeComponent();
        }
     private void frmNhanVien_Load(object sender, EventArgs e)
        {
            cnstr = @"server=.;database=aocuoi;integrated security=true";
            //cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new SqlConnection(cnstr);
            dgvEmployee.DataSource = GetNhanVien();
            //dgvKhachHang.DataSource = GetKhachHang();

            //cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            
            //string sql = "Select * From NHANVIEN";
            //SqlCommand cmd = new SqlCommand(sql, cn);
            /*SqlDataReader reder = new SqlDataReader(sql, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            txtID.Text = reader["MANV"].ToString();
            txtName.Text = reader["HOTENNV"].ToString();
            txtNumber.Text = reader["SDT"].ToString();
            txtAdress.Text = reader["DIACHI"].ToString();
            txtSex.Text = reader["GIOITINH"].ToString();
            reader.Close();*/
            
          /*  ds = new DataSet();
           string query = "select * from NHANVIEN";
            try
            {
                cn = new SqlConnection(cnstr);
               // da= new SqlDataAdapter(query,cn);

                //da.Fill(ds, "NHANVIEN");


            SqlDataAdapter da = new SqlDataAdapter(query,cn);
            //da.SelectCommand=cnstr;
            da.Fill(ds, "NHANVIEN");
            DataTable dt = ds.Tables["NHANVIEN"];
            dgvEmployee.DataSource = dt;
            

         */
            /* cnstr = "Data Source=.;Initial Catalog=aocuoi;Integrated Security=True";
            ds = new DataSet();
            string query = "select * from NHANVIEN";
            try
            {
                cn = new SqlConnection(cnstr);
                da = new SqlDataAdapter(query, cn);

                da.Fill(ds, "NHANVIEN");
                dgvEmployee.DataSource = ds.Tables["NHANVIEN"];
                //dgvKhachHang.DataSource = ds.Tables["KHACHHANG"];

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cn != null)
                {

                    cn.Close();
                }
            }

         */
        }
     private DataTable GetNhanVien()
     {
         string sql = "Select * from NHANVIEN";
         SqlDataAdapter da = new SqlDataAdapter(sql, cn);
         dt = new DataTable();
         da.Fill(dt);
         return dt;
     }
    /* private void Connect()
     {
         if (cn != null && cn.State == ConnectionState.Closed)
         {
             try
             {
                 cn.Open();
             }
             catch (Exception)//?
             {
                 MessageBox.Show("Loi:");
             }

         }
     }
     private void DisConnect()
     {
         if (cn != null && cn.State == ConnectionState.Open)
         {
             cn.Close();
         }
     }*/
     private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
     {
         DataGridViewRow row = new DataGridViewRow();

         row = dgvEmployee.Rows[e.RowIndex];
         txtID.Text = row.Cells[0].Value.ToString();
         txtName.Text = row.Cells[1].Value.ToString();
         txtNumber.Text = row.Cells[2].Value.ToString();
         txtAdress.Text = row.Cells[3].Value.ToString();
         txtSex.Text = row.Cells[4].Value.ToString();

     }

     

     private void btnIns_Click(object sender, EventArgs e)
     {
        
        /* string ins = "";
         ins = "INSERT INTO NHANVIEN(MANV,HOTENV,SDT,DIACHI, GIOITINH) VALUES('" + txtID.Text + "','" + txtName.Text + "','" + txtNumber.Text + "','" + txtAdress.Text + "','" + txtSex + "')";

         Connect();
         SqlCommand cmd = new SqlCommand(ins, cn);
         
          txtID.Enabled = true;
            
            cmd.CommandText = ins;
            cmd.ExecuteNonQuery();
            cn.Close();*/
         DataRow NewRow = dt.NewRow();
         NewRow["MANV"] = txtID.Text;
         NewRow["HOTENNV"] = txtName.Text;
         NewRow["DIACHI"] = txtAdress.Text;
         NewRow["SDT"] = txtNumber.Text;
         NewRow["GIOITINH"] = txtSex.Text;
         dt.Rows.Add(NewRow);

         string ins = "INSERT NHANVIEN(MANV,HOTENNV,DIACHI,SDT,GIOITINH) VALUES ( @ID, @Hoten,@diachi,@dienthoai,@gioitinh)";
         SqlCommand cmd = new SqlCommand(ins, cn);

         cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 10, "MANV");
         cmd.Parameters.Add("@Hoten", SqlDbType.NVarChar, 50, "HOTENNV");
         cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 100, "DIACHI");
         cmd.Parameters.Add("@dienthoai", SqlDbType.Char, 20, "SDT");
         cmd.Parameters.Add("@gioitinh", SqlDbType.NChar, 10, "GIOITINH");

         SqlDataAdapter da = new SqlDataAdapter();
         da.InsertCommand = cmd;
         da.Update(dt);
         
         /*string ins = "";
         ins = "INSERT INTO SINHVIEN(MANV,HOTENV,SDT,DIACHI, GIOITINH) VALUES('" + txtID.Text + "','" + txtName.Text + "','" + txtNumber.Text + "','" + txtAdress.Text + "','" + txtSex + "')";

        
         SqlCommand cmd = new SqlCommand(ins, cn);
         Connect();
          txtID.Enabled = true;
            
            cmd.CommandText = ins;
            cmd.ExecuteNonQuery();
            cn.Close();*/


         
         
     }

     private void btnUpdate_Click(object sender, EventArgs e)
     {
         cn.Open();
            if (txtID.Text != "")
            {
                string upd = "";
                upd = "UPDATE NHANVIEN SET MANV='" + txtID.Text + "', HOTENNV='" + txtName.Text + "',SDT='" + txtNumber.Text + "', DIACHI='"+txtAdress.Text +"',GIOITINH='"+txtSex.Text+"' WHERE MANVv='" + txtID.Text +"'";
                SqlCommand cmd = new SqlCommand (upd, cn);
                cmd.CommandText = upd;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập Nhật Thành Công");
                cn.Close();
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên...", "Thông báo");
            }
 
        }
     public void Load_DuLieu()
     {
         cn.Open();

         string sql = "Select * From NHANVIEN";
         SqlCommand cmd = new SqlCommand(sql, cn);
         SqlDataReader reader = cmd.ExecuteReader();
         reader.Read();
         txtID.Text = reader["MANV"].ToString();
         txtName.Text = reader["HOTENNV"].ToString();
         txtNumber.Text = reader["SDT"].ToString();
         txtAdress.Text = reader["DIACHI"].ToString();
         txtSex.Text = reader["GIOITINH"].ToString();
         reader.Close();
         //dua du lieu vao luoi

         SqlDataAdapter da = new SqlDataAdapter();
         da.SelectCommand = cmd;
         da.Fill(ds, "NHANVIEN");
         DataTable dtb = ds.Tables["NHANVIEN"];
         dgvEmployee.DataSource = dtb;
         
         
         cn.Close();
     }

     private void btnExit_Click(object sender, EventArgs e)
     {
         if (MessageBox.Show("Bạn chắc chắn muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Dispose();
            }

     }

   
 
     }
 }

