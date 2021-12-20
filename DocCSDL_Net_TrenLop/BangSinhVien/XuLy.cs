using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BangSinhVien
{
    class XuLy
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        DataSet dataset_Lop = new DataSet();
        DataSet dataset_SinhVien = new DataSet();
        public XuLy()
        {
            LoadDuLieuLop();
            LoadDuLieuSinhVien();
        }
        public void LoadDuLieuLop()
        {
            //B1: chuỗi câu lệnh
            string sql = "select * from Lop";
            //B2: 
            SqlDataAdapter sql_Lop = new SqlDataAdapter(sql, con);
            //B3: ánh xạ dữ liệu lên dataset
            sql_Lop.Fill(dataset_Lop, "Lop");
            //B4: tạo lại khóa chính cho bảng Khoa
            DataColumn[] kc_Lop = new DataColumn[1];
            kc_Lop[0] = dataset_Lop.Tables["Lop"].Columns[0];
            //Tạo Khoá chính cho bảng khoa trên dataset
            DataColumn[] key = new DataColumn[1];
            key[0] = dataset_Lop.Tables["Lop"].Columns[0];
            dataset_Lop.Tables["Lop"].PrimaryKey = key;//set khoá chính
        }

        public void LoadDuLieuSinhVien()
        {
            string sql = "select * from SinhVien";
            SqlDataAdapter sql_SinhVien = new SqlDataAdapter(sql,con);
            sql_SinhVien.Fill(dataset_SinhVien, "SinhVien");
            DataColumn[] key = new DataColumn[1];
            key[0] = dataset_SinhVien.Tables["SinhVien"].Columns[0];
            dataset_SinhVien.Tables["SinhVien"].PrimaryKey = key;
        }

        public DataTable GetLop()
        {
            return dataset_Lop.Tables["Lop"];
        }
        public DataTable GetSinhVien()
        {
            return dataset_SinhVien.Tables["SinhVien"];
        }
        //public bool ThucHienXuongSQL()
        //{
        //    try
        //    {
        //        //B1: chuỗi câu lệnh
        //        string sql = "select * from SinhVien";
        //        //B2: 
        //        SqlDataAdapter sql_Lop = new SqlDataAdapter(sql, con);
        //        //B3
        //        SqlCommandBuilder buil_Lop = new SqlCommandBuilder(sql_Lop);
        //        //B4
        //        sql_Lop.Update(dataset_SinhVien, "SinhVien");
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}





        public bool ThemSV(SinhVien sv)
        {
            try
            {
                DataRow dtr_SinhVien = dataset_SinhVien.Tables["SinhVien"].NewRow();
                dtr_SinhVien["MaSinhVien"] = sv.MaSV;
                dtr_SinhVien["HoTen"] = sv.TenSV;
                dtr_SinhVien["NgaySinh"] = sv.NgaySinh;
                dtr_SinhVien["MaLop"] = sv.MaLop;
                dataset_SinhVien.Tables["SinhVien"].Rows.Add(dtr_SinhVien);
                //B1: chuỗi câu lệnh
                string sql = "select * from SinhVien";
                //B2: 
                SqlDataAdapter sql_SinhVien = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_SinhVien = new SqlCommandBuilder(sql_SinhVien);
                //B4
                sql_SinhVien.Update(dataset_SinhVien, "SinhVien");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaSV(SinhVien sv)
        {
            try
            {
                DataRow dtr_SinhVien = dataset_SinhVien.Tables["SinhVien"].Rows.Find(sv.MaSV);
                dtr_SinhVien.Delete();
                //B1: chuỗi câu lệnh
                string sql = "select * from SinhVien";
                //B2: 
                SqlDataAdapter sql_SinhVien = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_SinhVien = new SqlCommandBuilder(sql_SinhVien);
                //B4
                sql_SinhVien.Update(dataset_SinhVien, "SinhVien");

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaSV(SinhVien sv)
        {
            try
            {
                DataRow dtr_SinhVien = dataset_SinhVien.Tables["SinhVien"].Rows.Find(sv.MaSV);
                dtr_SinhVien["HoTen"] = sv.TenSV;
                dtr_SinhVien["NgaySinh"] = sv.NgaySinh;
                dtr_SinhVien["MaLop"] = sv.MaLop;
                //B1: chuỗi câu lệnh
                string sql = "select * from SinhVien";
                //B2: 
                SqlDataAdapter sql_SinhVien = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_SinhVien = new SqlCommandBuilder(sql_SinhVien);
                //B4
                sql_SinhVien.Update(dataset_SinhVien, "SinhVien");
                return true;
            }
            catch
            {
                return false;
            }
        }








        //public List<string> shhowComBobox()
        //{
        //    List<string> ds_MaLop = new List<string>();
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        string sql = "select * from Lop";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while(rd.Read())
        //        {
        //            string malop = rd["MaLop"].ToString();
        //            ds_MaLop.Add(malop);
        //        }
        //        rd.Close();
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
                
        //        return ds_MaLop;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public int KtraKC(SinhVien sv)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from SinhVien where MaSinhVien = '" + sv.MaSV + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                int kq = (int)cmd.ExecuteScalar();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (kq >= 1)
                    return 0;
                else
                    return 1;
            }
            catch
            {
                return 2;
            }
        }
        public int KtraKN(SinhVien sv)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from Diem where MaSinhVien = '" + sv.MaSV + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                int kq = (int)cmd.ExecuteScalar();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (kq >= 1)
                    return 0;
                else
                    return 1;
            }
            catch
            {
                return 2;
            }
        }
        //public bool ThemSV(SinhVien sv)
        //{
        //    //B1: tao chuoi ket noi sql
        //    string sql = "insert into SinhVien values(@MaSV,@TenSV,@NgaySinh,@MaLop)";
        //    //B2: Lay chuoi ket noi cmd
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        //Mo ket noi
        //        con.Open();
        //        cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar).Value = sv.MaSV;
        //        cmd.Parameters.Add("@TenSV", SqlDbType.NVarChar).Value = sv.TenSV;
        //        cmd.Parameters.Add("@NgaySinh", SqlDbType.NVarChar).Value = sv.NgaySinh;
        //        cmd.Parameters.Add("@MaLop", SqlDbType.NVarChar).Value = sv.MaLop;
        //        cmd.ExecuteNonQuery();
        //        //Dong ket noi
        //        con.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //    return true;

        //}
        //public bool XoaSV(SinhVien sv)
        //{
        //    //B1: tao chuoi ket noi sql
        //    string sql = "delete SinhVien where MaSinhVien = @MaSV";
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        //Mo ket noi
        //        con.Open();
        //        cmd.Parameters.Add("@MaSV", SqlDbType.Int).Value = sv.MaSV;
        //        cmd.ExecuteNonQuery();
        //        //Dong ket noi
        //        con.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public bool SuaSV(SinhVien sv)
        //{
        //    //B1: tao chuoi ket noi sql
        //    string sql = "update MonHoc set HoTen = @TenSV, NgaySinh = @NgaySinh, MaLop = @MaLop where MaSinhVien = @MaSV";
        //    //B2: Lay chuoi ket noi cmd
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        //Mo ket noi
        //        con.Open();
        //        cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar).Value = sv.MaSV;
        //        cmd.Parameters.Add("@TenSV", SqlDbType.NVarChar).Value = sv.TenSV;
        //        cmd.Parameters.Add("@NgaySinh", SqlDbType.NVarChar).Value = sv.NgaySinh;
        //        cmd.Parameters.Add("@MaLop", SqlDbType.NVarChar).Value = sv.MaLop;
        //        cmd.ExecuteNonQuery();
        //        //Dong ket noi
        //        con.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
