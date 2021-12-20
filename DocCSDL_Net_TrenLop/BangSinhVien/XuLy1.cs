using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BangSinhVien
{
    class XuLy1
    {
        // Tao doi tuong ket noi co so du lieu
        SqlConnection cnn = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        // B1 Tao dataset
        DataSet ds_QLSV = new DataSet();
        SqlDataAdapter da;

        public XuLy1()
        {
            LoadKhoa();
        }

        public void LoadKhoa()
        {
            //1. Tao doi tuong dataadapter
            da = new SqlDataAdapter("select * from Khoa", cnn);
            //2. Dien du lieu vao dataset hoac goi là ánh xa bang khoa len dataset
            da.Fill(ds_QLSV, "Khoa");
            // Truoc khi them/xoa/sua can dat khoa chinh cho table Khoa
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLSV.Tables["Khoa"].Columns[0];

            //Set khoa chinh
            ds_QLSV.Tables["Khoa"].PrimaryKey = key;

            // Tra du lieu cho phuongthuc
           // return ds_QLSV.Tables["Khoa"];

        }
        public DataTable LoadLop()
        {
            //1. Tao doi tuong dataadapter
            da = new SqlDataAdapter("select * from Lop", cnn);
            //2. Dien du lieu vao dataset hoac goi là ánh xa bang khoa len dataset
            da.Fill(ds_QLSV, "Lop");
            // Truoc khi them/xoa/sua can dat khoa chinh cho table Khoa
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLSV.Tables["Lop"].Columns[0];

            //Set khoa chinh
            ds_QLSV.Tables["Lop"].PrimaryKey = key;

            // Tra du lieu cho phuongthuc
          return ds_QLSV.Tables["Lop"];

        }
        public DataTable LoadSinhVien()
        {
            //1. Tao doi tuong dataadapter
            da = new SqlDataAdapter("select * from SinhVien", cnn);
            //2. Dien du lieu vao dataset hoac goi là ánh xa bang khoa len dataset
            da.Fill(ds_QLSV, "SinhVien");
            // Truoc khi them/xoa/sua can dat khoa chinh cho table Khoa
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLSV.Tables["SinhVien"].Columns[0];

            //Set khoa chinh
            ds_QLSV.Tables["SinhVien"].PrimaryKey = key;

            // Tra du lieu cho phuongthuc
            return ds_QLSV.Tables["SinhVien"];

        }
        public DataTable LoadKhoaData()
        {
            return ds_QLSV.Tables["Khoa"];
        }

        public bool ThemKhoa(string pMaKhoa, string pTenKhoa)
        {
            try
            {
                //1. Tao 1 dong du lieu moi
                DataRow rowData = ds_QLSV.Tables["Khoa"].NewRow();
                //2. Gan gia tri vao row data
                rowData["MaKhoa"] = pMaKhoa;
                rowData["TenKhoa"] = pTenKhoa;
                //3. Chen vao bang Khoa tren dataset
                ds_QLSV.Tables["Khoa"].Rows.Add(rowData);
                //4. update vao database giup minh buld cau lenh them xoa sua vao bang khoa
                SqlCommandBuilder buid = new SqlCommandBuilder(da);
                //5. update vao DB
                da.Update(ds_QLSV, "Khoa");
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool XoaKhoa(string pMaKhoa)
        {
            try
            {
                //1. Tim dong du lieu can xoa Find chi co tac dung khi da co khóa chính
                DataRow rowData = ds_QLSV.Tables["Khoa"].Rows.Find(pMaKhoa);
                //2. Xóa dong khoi bang Khoa tren dataset
                rowData.Delete();// Giup danh dau trang thai du lieu da xoa
                /// ds_QLSV.Tables["Khoa"].Rows.Remove(rowData);
                //4. update vao database giup minh buld cau lenh them xoa sua vao bang khoa
                SqlCommandBuilder buid = new SqlCommandBuilder(da);//Giup danh dau trang thai du lieu da xoa
                //5. update vao DB
                da.Update(ds_QLSV, "Khoa");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaKhoa(string pMaKhoa, string pTenKhoa)
        {
            try
            {
                //1. Tim dong du lieu can xoa Find chi co tac dung khi da co khóa chính
                DataRow rowData = ds_QLSV.Tables["Khoa"].Rows.Find(pMaKhoa);
                //2. Xóa dong khoi bang Khoa tren dataset
                rowData["TenKhoa"] = pTenKhoa;
                /// ds_QLSV.Tables["Khoa"].Rows.Remove(rowData);
                //4. update vao database giup minh buld cau lenh them xoa sua vao bang khoa
                SqlCommandBuilder buid = new SqlCommandBuilder(da);//Giup danh dau trang thai du lieu da xoa
                //5. update vao DB
                da.Update(ds_QLSV, "Khoa");
                return true;
            }
            catch
            {
                return false;
            }
        }
        // Tu bo sung
        public bool KTKhoaNgoaiBangLop(string pMaKhoa)
        {
            return true;
        }

        public bool LuuSV()
        {
            try
            {
                //B1: chuỗi câu lệnh
                string sql = "select * from SinhVien";
                //B2: 
                SqlDataAdapter sql_SinhVien = new SqlDataAdapter(sql, cnn);
                //B3
                SqlCommandBuilder buil_SinhVien = new SqlCommandBuilder(sql_SinhVien);
                //5. update vao DB
                sql_SinhVien.Update(ds_QLSV, "SinhVien");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ThemSV(SinhVien sv)
        {
            try
            {
                DataRow dtr_SinhVien = ds_QLSV.Tables["SinhVien"].NewRow();
                dtr_SinhVien["MaSinhVien"] = sv.MaSV;
                dtr_SinhVien["HoTen"] = sv.TenSV;
                dtr_SinhVien["NgaySinh"] = sv.NgaySinh;
                dtr_SinhVien["MaLop"] = sv.MaLop;
                ds_QLSV.Tables["SinhVien"].Rows.Add(dtr_SinhVien);
                //B1: chuỗi câu lệnh
                string sql = "select * from SinhVien";
                //B2: 
                SqlDataAdapter sql_SinhVien = new SqlDataAdapter(sql, cnn);
                //B3
                SqlCommandBuilder buil_SinhVien = new SqlCommandBuilder(sql_SinhVien);
                //B4
                sql_SinhVien.Update(ds_QLSV, "SinhVien");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
