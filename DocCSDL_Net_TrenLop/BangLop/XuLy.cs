using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BangLop
{
    class XuLy
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        DataSet dataset_Lop = new DataSet();
        DataSet dataset_Khoa = new DataSet();
        public XuLy()
        {
            LoadDuLieuLop();
            LoadDuLieuKhoa();
            LoadDuLieuLopVaKhoa();
        }
        public void LoadDuLieuLopVaKhoa()
        {
            //B1: chuỗi câu lệnh
            string sql = "select MaLop,TenLop,Khoa.MaKhoa,TenKhoa from Khoa, Lop where Khoa.MaKhoa = Lop.MaKhoa";
            //B2: 
            SqlDataAdapter sql_Lop = new SqlDataAdapter(sql, con);
            //B3: ánh xạ dữ liệu lên dataset
            sql_Lop.Fill(dataset_Lop, "LopAndKhoa");
            //B4: tạo lại khóa chính cho bảng Khoa
            DataColumn[] kc_Lop = new DataColumn[1];
            kc_Lop[0] = dataset_Lop.Tables["LopAndKhoa"].Columns[0];
            //Tạo Khoá chính cho bảng khoa trên dataset
            DataColumn[] key = new DataColumn[1];
            key[0] = dataset_Lop.Tables["LopAndKhoa"].Columns[0];
            dataset_Lop.Tables["LopAndKhoa"].PrimaryKey = key;//set khoá chính
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
        public void LoadDuLieuKhoa()
        {
            //B1: chuỗi câu lệnh
            string sql = "select * from Khoa";
            //B2: 
            SqlDataAdapter sql_Khoa = new SqlDataAdapter(sql, con);
            //B3: ánh xạ dữ liệu lên dataset
            sql_Khoa.Fill(dataset_Khoa, "Khoa");
            //B4: tạo lại khóa chính cho bảng Khoa
            DataColumn[] kc_Khoa = new DataColumn[1];
            kc_Khoa[0] = dataset_Khoa.Tables["Khoa"].Columns[0];
            //Tạo Khoá chính cho bảng khoa trên dataset
            DataColumn[] key = new DataColumn[1];
            key[0] = dataset_Khoa.Tables["Khoa"].Columns[0];
            dataset_Khoa.Tables["Khoa"].PrimaryKey = key;//set khoá chính
        }

        public DataTable shhowComBobox()
        {
            return dataset_Khoa.Tables["Khoa"];
        }
        public DataTable GetLop()
        {
            return dataset_Lop.Tables["LopAndKhoa"];
        }



        public bool ThucHienXuongSQL()
        {
            try
            {
                //B1: chuỗi câu lệnh
                string sql = "select * from Lop";
                //B2: 
                SqlDataAdapter sql_Lop = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_Lop = new SqlCommandBuilder(sql_Lop);
                //B4
                sql_Lop.Update(dataset_Lop, "Lop");
                return true;
            }
            catch
            {
                return false;
            }
        }



        public bool ThemLop(Lop l)
        {
            try
            {
                DataRow dtr_Lop = dataset_Lop.Tables["Lop"].NewRow();
                dtr_Lop["MaLop"] = l.MaLop;
                dtr_Lop["TenLop"] = l.TenLop;
                dtr_Lop["MaKhoa"] = l.MaKhoa;
                dataset_Lop.Tables["Lop"].Rows.Add(dtr_Lop);
                return true;
            }
            catch
            {
                return false;
            }
        }





        public bool XoaLop(Lop l)
        {
            try
            {
                DataRow dtr_Lop = dataset_Lop.Tables["Lop"].Rows.Find(l.MaLop);
                dtr_Lop.Delete();
                LoadDuLieuLopVaKhoa();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaLop(Lop l)
        {
            try
            {
                DataRow dtr_Lop = dataset_Lop.Tables["Lop"].Rows.Find(l.MaLop);
                dtr_Lop["TenLop"] = l.TenLop;
                dtr_Lop["MaKhoa"] = l.MaKhoa;
                LoadDuLieuLopVaKhoa();
                return true;
            }
            catch
            {
                return false;
            }
        }



















        //public List<string> shhowComBobox()
        //{
        //    List<string> ds_MaKhoa = new List<string>();
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        string sql = "select * from Khoa";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            string makhoa = rd["MaKhoa"].ToString();
        //            ds_MaKhoa.Add(makhoa);
        //        }
        //        rd.Close();
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }

        //        return ds_MaKhoa;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public int KtraKC(Lop l)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from Lop where MaLop = '" + l.MaLop + "'";
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
        public int KtraKN(Lop l)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from SinhVien where MaLop = '" + l.MaLop + "'";
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
        //public bool ThemLop(Lop l)
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        string sql = "insert into Lop values ('" + l.MaLop + "','" + l.TenLop + "','" + l.MaKhoa + "')";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.ExecuteNonQuery();
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public bool XoaLop(Lop l)
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        string sql = "delete Lop where MaLop = '" + l.MaLop + "'";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.ExecuteNonQuery();
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public bool SuaLop(Lop l)
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        string sql = "update Lop set TenLop = '" + l.TenLop + "', MaKhoa ='" + l.MaKhoa + "' where MaLop = '" + l.MaLop + "'";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.ExecuteNonQuery();
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public List<Lop> loadDuLieu()
        //{
        //    List<Lop> ds_Lop = new List<Lop>();
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        string sql = "select * from Lop";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        SqlDataReader rdKhoa = cmd.ExecuteReader();
        //        int stt = 1;
        //        while(rdKhoa.Read())
        //        {
        //            Lop l = new Lop();
        //            l.iSTT = stt;
        //            l.MaLop = rdKhoa["MaLop"].ToString();
        //            l.TenLop = rdKhoa["TenLop"].ToString();
        //            l.MaKhoa = rdKhoa["MaKhoa"].ToString();
        //            ds_Lop.Add(l);
        //            stt++;
        //        }
        //        rdKhoa.Close();
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return ds_Lop;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}
