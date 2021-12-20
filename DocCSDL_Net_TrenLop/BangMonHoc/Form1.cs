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

namespace BangMonHoc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        XuLy xl = new XuLy();
        private void button1_Click(object sender, EventArgs e)
        {
            
            MonHoc mh = new MonHoc();
            mh.MaMonHoc = txtMaMonHoc.Text;
            mh.TenMonHoc = txtTenMonHoc.Text;
            if (xl.KtraKC(mh) == 0)
            {
                MessageBox.Show("Trùng khóa chính");
            }
            if (xl.KtraKC(mh) == 2)
            {
                MessageBox.Show("Hàm lỗi");
            }
            if (xl.IsertMonHoc(mh)==true)
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
            MonHoc mh = new MonHoc();
            mh.MaMonHoc = txtMaMonHoc.Text;
            mh.TenMonHoc = txtTenMonHoc.Text;
            if (xl.KtraKN(mh) == 0)
            {
                MessageBox.Show("Trùng khóa ngoại ở bảng Diem");
            }
            if (xl.KtraKN(mh) == 2)
            {
                MessageBox.Show("Hàm lỗi");
            }
            if (xl.DeleteMonHoc(mh) == true)
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
            MonHoc mh = new MonHoc();
            mh.MaMonHoc = txtMaMonHoc.Text;
            mh.TenMonHoc = txtTenMonHoc.Text;
            if (xl.UpdateMonHoc(mh) == true)
            {
                MessageBox.Show("Sửa Thành Công");
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }
    }
}
