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

namespace BangDiem
{
    public partial class Form1 : Form
    {
        //SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        XyLy xl = new XyLy();
        public Form1()
        {
            InitializeComponent();
        }
        //public void ShowCombobox()
        //{
        //    string sql = "select * from SinhVien";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;

        //    DataTable table = new DataTable();
        //    da.Fill(table);
        //    comboBox1.DataSource = table;
        //    comboBox1.DisplayMember = "MaSinhVien";
        //    comboBox1.ValueMember = "HoTen";
        //    comboBox1.ValueMember = "NgaySinh";
        //    comboBox1.ValueMember = "MaLop";
        //    con.Close();
        //}
        //public void ShowCombobox1()
        //{
        //    string sql = "select * from MonHoc";
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;

        //    DataTable table = new DataTable();
        //    da.Fill(table);
        //    comboBox2.DataSource = table;
        //    comboBox2.DisplayMember = "MaMonHoc";
        //    comboBox2.ValueMember = "TenMonHoc";
        //    con.Close();
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> dsMaSinhVien = xl.shhowComBobox1();
            foreach (string item1 in dsMaSinhVien)
            {
                comboBox1.Items.Add(item1);
            }
            List<string> dsMaMonHoc = xl.shhowComBobox2();
            foreach (string item2 in dsMaMonHoc)
            {
                comboBox2.Items.Add(item2);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtDiem.Text == "")
            {
                MessageBox.Show("Không được để trống điểm");
                return;
            }
            Diem d = new Diem();
            d.MaSV = comboBox1.Text;
            d.MaMH = comboBox2.Text;
            d.sDiem = float.Parse(txtDiem.Text);
            if(xl.ThemBangDiem(d)==true)
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
            Diem d = new Diem();
            d.MaSV = comboBox1.Text;
            d.MaMH = comboBox2.Text;
            
            if (xl.XoaBangDiem(d) == true)
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
            if (txtDiem.Text == "")
            {
                MessageBox.Show("Không được để trống điểm");
                return;
            }
            Diem d = new Diem();
            d.MaSV = comboBox1.Text;
            d.MaMH = comboBox2.Text;
            d.sDiem = float.Parse(txtDiem.Text);
            if (xl.SuaBangDiem(d) == true)
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
