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
    public partial class Form3 : Form
    {
        XuLy1 dt = new XuLy1();
        public Form3()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // 
            btnXoa.Enabled = btnSua.Enabled = true;
        }
        void Databingding(DataTable pDT)
        {
            comboBox1.DataBindings.Clear();
            txtHoTenSV.DataBindings.Clear();
            txtMaSV.DataBindings.Clear();
            dtNgaySinh.DataBindings.Clear();

            comboBox1.DataBindings.Add("Text", pDT, "MaLop");
            txtHoTenSV.DataBindings.Add("Text", pDT, "HoTen");
            txtMaSV.DataBindings.Add("Text", pDT, "MaSinhVien");
            dtNgaySinh.DataBindings.Add("Text", pDT, "NgaySinh");
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //-Formload:
            //+Combobox mă l?p: hi?n th? tên l?p trong b?ng lop
            comboBox1.DataSource = dt.LoadLop();
            comboBox1.DisplayMember = "MaLop";
            comboBox1.ValueMember = "TenLop";
            //+ Datagridview Sinh viên: hi?n th? t?t c? sinh viên trong b?ng SinhVien và có thu?c tính ch? d?c.
            dataGridView1.DataSource = dt.LoadSinhVien();

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            //+ T?t c? textbox, combobox b? vô hi?u hóa
            txtMaSV.Enabled = dtNgaySinh.Enabled = txtHoTenSV.Enabled = comboBox1.Enabled = false;
            //+Các button S?a, xóa, Luu b? vô hi?u hóa
            btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = false;
            //+Thi?t l?p liên k?t gi?a control nh?p li?u v?i ch?n thông tin trên datagridview
            Databingding(dt.LoadSinhVien());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //+Button Luu có hi?u l?c
            btnLuu.Enabled = true;
            btnSua.Enabled = true;
            dtNgaySinh.Enabled = true;
            comboBox1.Enabled = true;
            //+Cho phép thêm các ḍng ti?p theo trên datagridview
            dataGridView1.ReadOnly = false;
            // Dong du lieu thêm m?i nguoi dung se nhap
            dataGridView1.AllowUserToAddRows = true;
            // Khoa cac dong co san
            //Không du?c s?a các ḍng trên datagridview dă có d? li?u
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }
            // Khoa column   Khoá chính
            dataGridView1.Columns[0].ReadOnly = false;
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            //+Cho phép thêm các ḍng ti?p theo trên datagridview
            dataGridView1.ReadOnly = true;
            // Dong du lieu thêm m?i nguoi dung se nhap
            dataGridView1.AllowUserToAddRows = true;
            //SinhVien sv = new SinhVien();
            //sv.MaSV = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //sv.TenSV = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //sv.NgaySinh = dtNgaySinh.Value;
            //sv.MaLop = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (dt.LuuSV())
            {
                MessageBox.Show("Thành công");
            }
            else
            {
                MessageBox.Show("Th?t b?i");
            }
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            //-Ch?n button S?a
            //+Button Luu có hi?u l?c
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            //+Cho phép s?a các thông tin trên Datagrid
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = false;
            //Không du?c s?a các ḍng trên datagridview dă có d? li?u
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].ReadOnly = false;
            }
            // Khoa column   Khoá chính
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //giống bài 10
            while (dataGridView1.Rows[dataGridView1.Rows.Count - 1].ReadOnly == false)
            {
                SinhVien sv = new SinhVien();
                sv.MaSV = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sv.TenSV = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                sv.NgaySinh = dtNgaySinh.Value;
                dataGridView1.CurrentRow.Cells[2].Value = sv.NgaySinh;
                
                sv.MaLop = comboBox1.Text;

                
                
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].ReadOnly = true;
            }
        }

    }
}
