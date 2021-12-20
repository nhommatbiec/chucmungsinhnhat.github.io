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

namespace BangSinhVien
{
    public partial class Form1 : Form
    {
        //SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        public Form1()
        {
            InitializeComponent();
        }
        //public void ShowComBoBox()
        //{
        //    string sql = "select * from Lop";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;

        //    DataTable table = new DataTable();
        //    da.Fill(table);
        //    comboBox1.DataSource = table;
        //    comboBox1.DisplayMember = "MaLop";
        //    comboBox1.ValueMember = "TenLop";
        //    comboBox1.ValueMember = "MaKhoa";
        //}
        XuLy xl = new XuLy();
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.GetSinhVien();
            comboBox1.DataSource = xl.GetLop();
            comboBox1.ValueMember = "MaLop";
            comboBox1.ValueMember = "MaKhoa";
            comboBox1.DisplayMember = "TenLop";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtMaSV.Text == "" || txtTenSV.Text == "" || txtNgaySinh.Text == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            SinhVien sv = new SinhVien();
            sv.MaSV = txtMaSV.Text;
            sv.TenSV = txtTenSV.Text;
            //sv.NgaySinh = txtNgaySinh.Text;
            sv.MaLop = comboBox1.SelectedValue.ToString();
            if(xl.KtraKC(sv)==0)
            {
                MessageBox.Show("Trùng khóa chính");
            }
            if (xl.KtraKC(sv) == 2)
            {
                MessageBox.Show("Hàm lỗi");
            }
            if(xl.ThemSV(sv)==true)
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Không được để trống mã sinh viên");
                return;
            }
            SinhVien sv = new SinhVien();
            sv.MaSV = txtMaSV.Text;
            sv.TenSV = txtTenSV.Text;
            //sv.NgaySinh = txtNgaySinh.Text;
            sv.MaLop = comboBox1.SelectedValue.ToString();
            if(xl.KtraKN(sv)==0)
            {
                MessageBox.Show("Trùng khóa ngoại ở bảng Diem");
            }
            if (xl.KtraKN(sv) == 2)
            {
                MessageBox.Show("Lỗi hàm");
            }
            if (xl.XoaSV(sv) == true)
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "" || txtTenSV.Text == "" || txtNgaySinh.Text == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            SinhVien sv = new SinhVien();
            sv.MaSV = txtMaSV.Text;
            sv.TenSV = txtTenSV.Text;
            //sv.NgaySinh = txtNgaySinh.Text;
            sv.MaLop = comboBox1.SelectedValue.ToString();
            if (xl.SuaSV(sv) == true)
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }
    }
}
