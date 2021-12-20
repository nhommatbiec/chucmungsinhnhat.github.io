using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DocCSDL_Net_TrenLop
{
    class XuLy
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");

        DataSet dataset_Khoa = new DataSet();
        public XuLy()
        {
            LoadDuLieuKhoa();
        }


        public void LoadDuLieuKhoa()
        {
            //B1: chuỗi câu lệnh
            string sql = "select * from Khoa";
            //B2: 
            SqlDataAdapter sql_Khoa = new SqlDataAdapter(sql,con);
            //B3: ánh xạ dữ liệu lên dataset
            sql_Khoa.Fill(dataset_Khoa, "KhoaNe");
            //B4: tạo lại khóa chính cho bảng Khoa
            DataColumn[] kc_Khoa = new DataColumn[1];
            kc_Khoa[0] = dataset_Khoa.Tables["KhoaNe"].Columns[0];
            //Tạo Khoá chính cho bảng khoa trên dataset
            DataColumn[] key = new DataColumn[1];
            key[0] = dataset_Khoa.Tables["KhoaNe"].Columns[0];
            dataset_Khoa.Tables["KhoaNe"].PrimaryKey = key;//set khoá chính

        }
        public DataTable GetKhoa()
        {
            return dataset_Khoa.Tables["KhoaNe"];
        }
        public bool ThemKhoa(Khoa k)
        {
            try
            {
                //B1
                DataRow dtr_Khoa = dataset_Khoa.Tables["KhoaNe"].NewRow();
                //B2
                dtr_Khoa["MaKhoa"] = k.MaKhoa;
                dtr_Khoa["TenKhoa"] = k.TenKhoa;
                //B3 thêm vào dataset
                dataset_Khoa.Tables["KhoaNe"].Rows.Add(dtr_Khoa);
                //B1: chuỗi câu lệnh
                string sql = "select * from Khoa";
                //B2: 
                SqlDataAdapter sql_Khoa = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_Khoa = new SqlCommandBuilder(sql_Khoa);
                //B4
                sql_Khoa.Update(dataset_Khoa, "KhoaNe");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ThucHienXuongSQL()
        {
            try
            {
                //B1: chuỗi câu lệnh
                string sql = "select * from Khoa";
                //B2: 
                SqlDataAdapter sql_Khoa = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_Khoa = new SqlCommandBuilder(sql_Khoa);
                //B4
                sql_Khoa.Update(dataset_Khoa, "KhoaNe");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaKhoa(Khoa k)
        {
            try
            {
                if(dataset_Khoa.Tables["KhoaNe"].Rows.Count == 0)
                {
                    return false;
                }
                //B1
                DataRow dtr_Khoa = dataset_Khoa.Tables["KhoaNe"].Rows.Find(k.MaKhoa);
                //B2
                dtr_Khoa.Delete();
                //B1: chuỗi câu lệnh
                string sql = "select * from Khoa";
                //B2: 
                SqlDataAdapter sql_Khoa = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_Khoa = new SqlCommandBuilder(sql_Khoa);
                //B4
                sql_Khoa.Update(dataset_Khoa, "KhoaNe");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaKhoa(Khoa k)
        {
            try
            {
                if (dataset_Khoa.Tables["KhoaNe"].Rows.Count == 0)
                {
                    return false;
                }
                //B1
                DataRow dtr_Khoa = dataset_Khoa.Tables["KhoaNe"].Rows.Find(k.MaKhoa);
                //B2
                dtr_Khoa["TenKhoa"] = k.TenKhoa;
                //B1: chuỗi câu lệnh
                string sql = "select * from Khoa";
                //B2: 
                SqlDataAdapter sql_Khoa = new SqlDataAdapter(sql, con);
                //B3
                SqlCommandBuilder buil_Khoa = new SqlCommandBuilder(sql_Khoa);
                //B4
                sql_Khoa.Update(dataset_Khoa, "KhoaNe");
                return true;
            }
            catch
            {
                return false;
            }
        }

















        public int KtraKC(Khoa k)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from Khoa where MaKhoa = '" + k.MaKhoa + "'";
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
        public int KtraKN(Khoa k)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from Lop where MaKhoa = '" + k.MaKhoa + "'";
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
        //public bool ThemKhoa(Khoa k)
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        String sql = "insert into khoa values('" + k.MaKhoa + "','" + k.TenKhoa + "')";
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
        //public bool XoaKhoa(Khoa k)
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }

        //        String sql = "Delete khoa where MaKhoa='" + k.MaKhoa + "'";
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
        //public bool SuaKhoa(Khoa k)
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }

        //        String sql = "Update khoa set TenKhoa= '" + k.TenKhoa + "' where MaKhoa='" + k.MaKhoa + "'";
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
    }
}
