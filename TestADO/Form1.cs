using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestADO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connect;
        string chuoikt = @"Data Source=DESKTOP-0C8O8RG\SQLEXPRESS01;Initial Catalog=QLHANG;Integrated Security=True";
        private void Form1_Load(object sender, EventArgs e)
        {
            getDataGV();
            getdata_SX();
            txtMaSP.Focus();
        }
        private void getDataGV()
        {
            connect = new SqlConnection(chuoikt);
            connect.Open();
            string query = "select MaSP,TenSP,MauSac,Soluong,Dongia,DiaChi,HangSX.TenHang,Hangsx.Mahang from HangSX,sanpham " +
                "where HangSX.mahang=sanpham.mahang";
            SqlCommand cmd = new SqlCommand(query,connect);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            connect.Close();
            dataGridView1.DataSource = dt;
        }
        private void getdata_SX()
        {
            connect = new SqlConnection(chuoikt);
            connect.Open();
            string query = "select MaHang,TenHang from HangSX";
            SqlCommand cmd = new SqlCommand(query, connect);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            connect.Close();
            cboTenHang.DataSource = dt;
            cboTenHang.DisplayMember = "TenHang";
            cboTenHang.ValueMember = "MaHang";
        }

        private void btnThem_Click(object sender, EventArgs e)//ADD
        {
            if(check_key(txtMaSP.Text)==false)
            {
                if (txtMaSP.Text == "")
                {
                    MessageBox.Show("Bạn nhập thiếu thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    add();
                    getDataGV();
                }
            }
            else
            {
                MessageBox.Show("Mã sản phầm này đã tồn tại!", "Thông báo",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
               
        }
        private void add()
        {
            try
            {
                connect = new SqlConnection(chuoikt);
                connect.Open();
                string query = "insert into sanpham values('" + txtMaSP.Text + "','" + txtTenSP.Text + "','";
                query += txtMauSac.Text + "','" + txtSL.Text + "','" + txtGia.Text + "','" + txtDC.Text + "','";
                query += cboTenHang.SelectedValue + "')";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
            }catch
            {
                MessageBox.Show("Có lỗi nhập liệu, vui lòng nhập lại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Remove()
        {
            try
            {
                connect = new SqlConnection(chuoikt);
                connect.Open();
                string query = "delete from sanpham where masp='"+txtMaSP.Text+"'";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Có lỗi nhập liệu, vui lòng nhập lại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtMaSP.Text=="")
            {
                MessageBox.Show("Vui lòng nhập mã vào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (check_key(txtMaSP.Text) == false)
                {
                    MessageBox.Show("Không có mã sản phầm này!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Remove();
                    getDataGV();
                }
            } 
        }
        private bool check_key(string key)
        {
            connect = new SqlConnection(chuoikt);
            connect.Open();
            string query = "select*from sanpham where MaSP='" + key + "'";
            SqlCommand cmd = new SqlCommand(query, connect);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            connect.Close();
            if(dt.Rows.Count==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (check_key(txtMaSP.Text) == false)
            {
                MessageBox.Show("Không có mã sản phầm này!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtMaSP.Text == "")
                {
                    MessageBox.Show("Bạn nhập thiếu thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    edit();
                    getDataGV();
                }
            }
        }
        private void edit()
        {
            try
            {
                connect = new SqlConnection(chuoikt);
                connect.Open();
                string query = "UPDATE SanPham SET TenSP='"+txtTenSP.Text+"',MauSac='"+txtMauSac.Text
                    +"',Soluong='"+txtSL.Text+"',Dongia='"+txtGia.Text+"',Diachi='"+txtDC.Text+"',MaHang='"+cboTenHang.SelectedValue+"'WHERE MaSP='"+txtMaSP.Text+"'";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Có lỗi nhập liệu, vui lòng nhập lại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Clear();//
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn có muốn thoát không", "Thông báo",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (a == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int numrow;
                numrow = e.RowIndex;
                txtMaSP.Text = dataGridView1.Rows[numrow].Cells[0].Value.ToString();
                txtTenSP.Text = dataGridView1.Rows[numrow].Cells[1].Value.ToString();
                txtMauSac.Text = dataGridView1.Rows[numrow].Cells[2].Value.ToString();
                txtSL.Text = dataGridView1.Rows[numrow].Cells[3].Value.ToString();
                txtGia.Text = dataGridView1.Rows[numrow].Cells[4].Value.ToString();
                txtDC.Text = dataGridView1.Rows[numrow].Cells[5].Value.ToString();
                //cboTenHang.Text = TenHang(dataGridView1.Rows[numrow].Cells[6].Value.ToString());
                cboTenHang.Text = dataGridView1.Rows[numrow].Cells[6].Value.ToString();
            }catch
            {}
        }
        private string TenHang(string MaHang)
        {
            string tensanpham = "";
            //Tạo chuỗi kết nối
            connect = new SqlConnection(chuoikt);
            //Mở kết nối
            connect.Open();
            //Câu lệnh lấy dữ liệu
            string sql = "select TenHang from HangSX where MaHang = '" + MaHang + "'";
            SqlCommand com = new SqlCommand(sql, connect); //bat dau truy
            com.CommandType = CommandType.Text;//sử dụng DataReader để đọc từng dòng dữ liệu
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                tensanpham = dr["TenHang"].ToString();
            }
            dr.Close();
            connect.Close();
            return tensanpham;
        }
    }
}
