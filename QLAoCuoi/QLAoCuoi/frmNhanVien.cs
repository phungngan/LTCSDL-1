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
        
        public frmNhanVien()
        {
            InitializeComponent();
        }
     private void frmNhanVien_Load(object sender, EventArgs e)
        {

            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new SqlConnection(cnstr);
            string sql = "Select * From NHANVIEN";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            txtID.Text = reader["MANV"].ToString();
            txtName.Text = reader["hotennv"].ToString();
            txtNumber.Text = reader["SODIENTHOAI"].ToString();
            txtAdress.Text = reader["DIACHI"].ToString();
            txtSex.Text = reader["GIOITINH"].ToString();
            reader.Close();
            

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds, "sinhvien");
            DataTable dt = ds.Tables["sinhvien"];
            dgvEmployee.DataSource = dt;
            


        }
     private void Connect()
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
     }
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
         string ins = "";
         ins = "INSERT INTO SINHVIEN(MANV,HOTENV,SODIENTHOAI,DIACHI, GIOITINH) VALUES('" + txtID.Text + "','" + txtName.Text + "','" + txtNumber.Text + "','" + txtAdress.Text + "','" + txtSex + "')";

        
         SqlCommand cmd = new SqlCommand(ins, cn);
         Connect();
          txtID.Enabled = true;
            
            cmd.CommandText = ins;
            cmd.ExecuteNonQuery();
            cn.Close();

         
         
     }

     private void btnUpdate_Click(object sender, EventArgs e)
     {
          Connect();
            if (txtID.Text != "")
            {
                string upd = "";
                upd = "UPDATE sinhvien SET MANV='" + txtID.Text + "', HOTENNV='" + txtName.Text + "',SODIENTHOAI='" + txtNumber.Text + "', DIACHI='"+txtAdress.Text +"',GIOITINH='"+txtSex.Text+"' WHERE MANVv='" + txtID.Text +"'";
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
         Connect();

         string sql = "Select * From sinhvien";
         SqlCommand cmd = new SqlCommand(sql, cn);
         SqlDataReader reader = cmd.ExecuteReader();
         reader.Read();
         txtID.Text = reader["MANV"].ToString();
         txtName.Text = reader["HOTENNV"].ToString();
         txtNumber.Text = reader["SODIENTHOAI"].ToString();
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

