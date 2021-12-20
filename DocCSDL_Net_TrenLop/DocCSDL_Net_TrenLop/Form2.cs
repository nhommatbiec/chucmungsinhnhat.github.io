using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocCSDL_Net_TrenLop
{
    public partial class Form2 : Form
    {
        XuLy XL = new XuLy();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = XL.GetKhoa();
            btnLuu.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            txtMaKhoa.Enabled = txtTenKhoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaKhoa.Enabled = txtTenKhoa.Enabled = true;
            btnLuu.Enabled = true;
            txtMaKhoa.Focus();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtMaKhoa.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenKhoa.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtMaKhoa.Enabled = false;
            txtTenKhoa.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtMaKhoa.Text == "" || txtTenKhoa.Text == "")
            {
                MessageBox.Show("Thông báo", "Mã Khoa và Tên Khoa không được trống!");
                return;
            }
            try
            {
                Khoa k = new Khoa();
                k.MaKhoa = txtMaKhoa.Text;
                k.TenKhoa = txtTenKhoa.Text;
                if(txtMaKhoa.Enabled == true)
                {
                    if(XL.KtraKC(k)==1)
                    {
                        if (XL.ThemKhoa(k))
                        {
                            MessageBox.Show("Thêm thành công!", "Thông báo");
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại!", "Thông báo");
                        }
                    }
                    else if(XL.KtraKC(k)==0)
                    {
                        MessageBox.Show("Trùng khóa chính!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Hàm thêm lỗi!", "Thông báo");
                    }
                }
                else
                {
                    if(XL.SuaKhoa(k))
                    {
                        MessageBox.Show("Sửa thành công!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại!", "Thông báo");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hàm lỗi!", "Thông báo");
                return;
            }
            btnLuu.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo",MessageBoxButtons. YesNo, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) ==System.Windows.Forms.DialogResult.Yes)
            {
                Khoa k = new Khoa();
                k.MaKhoa = txtMaKhoa.Text;
                k.TenKhoa = txtTenKhoa.Text;
                if(XL.KtraKN(k) == 0)
                {
                    MessageBox.Show("Lỗi khóa ngoại", "Thông báo");
                }
                else if (XL.KtraKN(k) == 1)
                {
                    if(XL.XoaKhoa(k))
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Hàm xóa bị lỗi", "Thông báo");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
