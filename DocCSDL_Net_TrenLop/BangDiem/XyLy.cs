using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BangDiem
{
    class XyLy
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        public List<string> shhowComBobox1()
        {
            List<string> ds_MaSV = new List<string>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sql = "select * from SinhVien";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string masv = rd["MaSinhVien"].ToString();
                    ds_MaSV.Add(masv);
                }
                rd.Close();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return ds_MaSV;
            }
            catch
            {
                return null;
            }
        }
        public List<string> shhowComBobox2()
        {
            List<string> ds_MaMonHoc = new List<string>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sql = "select * from MonHoc";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string mamh = rd["MaMonHoc"].ToString();
                    ds_MaMonHoc.Add(mamh);
                }
                rd.Close();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return ds_MaMonHoc;
            }
            catch
            {
                return null;
            }
        }
        public bool ThemBangDiem(Diem d)
        {
            //B1: tao chuoi ket noi sql
            string sql = "insert into Diem values(@MaSV,@MaMH,@Diem)";
            //B2: Lay chuoi ket noi cmd
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                //Mo ket noi
                con.Open();
                cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar).Value = d.MaSV;
                cmd.Parameters.Add("@MaMH", SqlDbType.NVarChar).Value = d.MaMH;
                cmd.Parameters.Add("@Diem", SqlDbType.Float).Value = d.sDiem;
                cmd.ExecuteNonQuery();
                //Dong ket noi
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool XoaBangDiem(Diem d)
        {
            //B1: tao chuoi ket noi sql
            string sql = "delete Diem where MaSinhVien = @MaSV and MaMonHoc = @MaMH";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                //Mo ket noi
                con.Open();
                cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar).Value = d.MaSV;
                cmd.Parameters.Add("@MaMH", SqlDbType.NVarChar).Value = d.MaMH;
                cmd.ExecuteNonQuery();
                //Dong ket noi
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool SuaBangDiem(Diem d)
        {
            //B1: tao chuoi ket noi sql
            string sql = "update Diem set Diem = @Diem where MaSinhVien = @MaSV and MaMonHoc = @MaMH";
            //B2: Lay chuoi ket noi cmd
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                //Mo ket noi
                con.Open();
                cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar).Value = d.MaSV;
                cmd.Parameters.Add("@MaMH", SqlDbType.NVarChar).Value = d.MaMH;
                cmd.Parameters.Add("@Diem", SqlDbType.Float).Value = d.sDiem;
                cmd.ExecuteNonQuery();
                //Dong ket noi
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
