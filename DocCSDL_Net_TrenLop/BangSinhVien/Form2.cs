using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BangSinhVien
{
    public partial class Form2 : Form
    {
        XuLy xl = new XuLy();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            txtMaSV.Enabled = txtHoTenSV.Enabled = false;
            comboBox1.Enabled = false;
            dtNgaySinh.Enabled = false;
            dataGridView1.DataSource = xl.GetSinhVien();
            comboBox1.DataSource = xl.GetLop();
            comboBox1.ValueMember = "TenLop";
            comboBox1.ValueMember = "MaKhoa";
            comboBox1.DisplayMember = "MaLop";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnSua.Enabled = btnXoa.Enabled = false;
            txtMaSV.Enabled = txtHoTenSV.Enabled = true;
            comboBox1.Enabled = true;
            dtNgaySinh.Enabled = true;
            txtMaSV.Focus();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = true;
            txtMaSV.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtHoTenSV.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dtNgaySinh.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            txtHoTenSV.Enabled = true;
            dtNgaySinh.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVien sv = new SinhVien();
                sv.MaSV = txtMaSV.Text;
                sv.TenSV = txtHoTenSV.Text;
                sv.NgaySinh = dtNgaySinh.Value;
                sv.MaLop = comboBox1.Text;
                if(txtMaSV.Enabled == true)
                {
                    if(xl.KtraKC(sv)==0)
                    {
                        MessageBox.Show("Trùng khóa chính!", "Thông báo");
                    }
                    else if (xl.KtraKC(sv) == 1)
                    {
                        if (xl.ThemSV(sv))
                        {
                            MessageBox.Show("Thêm thành công!", "Thông báo");
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại!", "Thông báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hàm kiểm tra khóa chính lỗi!", "Thông báo");
                    }
                }
                else
                {
                    if (xl.SuaSV(sv))
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
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                SinhVien sv = new SinhVien();
                sv.MaSV = txtMaSV.Text;
                sv.TenSV = txtHoTenSV.Text;
                sv.NgaySinh = dtNgaySinh.Value;
                sv.MaLop = comboBox1.Text;
                if (xl.KtraKN(sv) == 0)
                {
                    MessageBox.Show("Lỗi khóa ngoại", "Thông báo");
                }
                else if (xl.KtraKN(sv) == 1)
                {
                    if (xl.XoaSV(sv))
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
