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

namespace DocCSDL_Net_TrenLop
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        XuLy xl = new XuLy();
        public Form1()
        {
            InitializeComponent();
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
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txtMaKhoa.Text == "" || txtTenKhoa.Text == "")
            {
                MessageBox.Show("Hai textbox không được rỗng");
                return;
            }
            Khoa k = new Khoa();
            k.MaKhoa = txtMaKhoa.Text;
            k.TenKhoa = txtTenKhoa.Text;
            if (xl.KtraKC(k) == 0)
            {
                MessageBox.Show("Trùng khóa chính");
            }
            if (xl.KtraKC(k) == 2)
            {
                MessageBox.Show("Lỗi hàm");
            }
            if(xl.ThemKhoa(k)==true)
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
            if (txtMaKhoa.Text == "")
            {
                MessageBox.Show("Mã Khoa không được rỗng");
                return;
            }
            Khoa k = new Khoa();
            k.MaKhoa = dataGridView1.CurrentRow.Cells[0].Value.ToString(); 
            k.TenKhoa = dataGridView1.CurrentRow.Cells[1].Value.ToString(); 
            if (xl.KtraKN(k) == 0)
            {
                MessageBox.Show("Trùng khóa ngoại ở bảng Lớp");
            }
            if (xl.KtraKN(k) == 2)
            {
                MessageBox.Show("Lỗi hàm");
            }
            if (xl.XoaKhoa(k) == true)
            {
                MessageBox.Show("Xóa Thành Công");
            }
            else
            {
                MessageBox.Show("Xóa Thất Bại");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMaKhoa.Text == "" || txtTenKhoa.Text == "")
            {
                MessageBox.Show("Hai textbox không được rỗng");
                return;
            }
            Khoa k = new Khoa();
            k.MaKhoa = txtMaKhoa.Text;
            k.TenKhoa = txtTenKhoa.Text;
            if (xl.SuaKhoa(k) == true)
            {
                MessageBox.Show("Sửa Thành Công");
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = xl.GetKhoa();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMaKhoa.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenKhoa.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
