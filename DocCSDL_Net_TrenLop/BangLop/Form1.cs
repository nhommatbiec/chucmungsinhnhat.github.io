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

namespace BangLop
{
    public partial class Form1 : Form
    {
        //SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        
        public Form1()
        {
            InitializeComponent();
        }
        //public void ShowCombobox()
        //{
        //    string sql = "select * from Khoa";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;

        //    DataTable table = new DataTable();
        //    da.Fill(table);
        //    comboBox1.DataSource = table;
        //    comboBox1.DisplayMember = "MaKhoa";
        //    comboBox1.ValueMember = "TenKhoa";
        //}
        XuLy xl = new XuLy();
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.GetLop();
            comboBox1.DataSource = xl.shhowComBobox();
            comboBox1.DisplayMember = "TenKhoa";
            comboBox1.ValueMember = "MaKhoa";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Lop l = new Lop();
            l.MaLop = txtMaLop.Text;
            l.TenLop = txtTenLop.Text;
            l.MaKhoa = comboBox1.SelectedValue.ToString();
            if (txtMaLop.Text == "" || txtTenLop.Text == "")
            {
                MessageBox.Show("Hai textbox không được rỗng");
                return;
            }
            if(xl.KtraKC(l)==0)
            {
                MessageBox.Show("Trùng khóa chính");
            }
            if (xl.KtraKC(l) == 2)
            {
                MessageBox.Show("Lỗi hàm");
            }
            if (xl.ThemLop(l)==true)
            {
                MessageBox.Show("Thêm Thành Công");
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMaLop.Text == "")
            {
                MessageBox.Show("Mã Khoa không được rỗng");
                return;
            }
            Lop l = new Lop();
            l.MaLop = txtMaLop.Text;
            l.TenLop = txtTenLop.Text;
            l.MaKhoa = comboBox1.SelectedIndex.ToString();
            if (xl.KtraKN(l) == 0)
            {
                MessageBox.Show("Trùng khóa ngoại bảng SinhVien");
            }
            if (xl.KtraKN(l) == 2)
            {
                MessageBox.Show("Lỗi hàm");
            }
            if (xl.XoaLop(l) == true)
            {
                xl.LoadDuLieuLopVaKhoa();
                MessageBox.Show("Xóa Thành Công");
            }
            else
            {
                MessageBox.Show("Xóa Thất Bại");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMaLop.Text == "" || txtTenLop.Text == "")
            {
                MessageBox.Show("Hai textbox không được rỗng");
                return;
            }
            Lop l = new Lop();
            l.MaLop = txtMaLop.Text;
            l.TenLop = txtTenLop.Text;
            l.MaKhoa = comboBox1.Text;
            if (xl.SuaLop(l) == true)
            {
                xl.LoadDuLieuLopVaKhoa();
                MessageBox.Show("Sửa Thành Công");
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(xl.ThucHienXuongSQL())
            {
                MessageBox.Show("Thực Hiện Thành Công");
            }
            else
            {
                MessageBox.Show("Thực Hiện Thất Bại");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //txtMaLop.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //txtTenLop.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
