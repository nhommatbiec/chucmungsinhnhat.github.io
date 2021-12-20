using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BangMonHoc
{
    class XuLy
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-469LKJL\\SQLEXPRESS; Initial Catalog = QLSINHVIEN; UID = sa; PWD = 123");
        public int KtraKC(MonHoc mh)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from MonHoc where MaMonHoc = '" + mh.MaMonHoc + "'";
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
        public int KtraKN(MonHoc mh)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String sql = "select COUNT(*) from Diem where MaMonHoc = '" + mh.MaMonHoc + "'";
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
        public bool IsertMonHoc(MonHoc mh)
        {
            //B1: tao chuoi ket noi sql
            string sql = "insert into MonHoc values(@MaMH,@TenMH)";
            //B2: Lay chuoi ket noi cmd
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                //Mo ket noi
                con.Open();
                cmd.Parameters.Add("@MaMH", SqlDbType.NVarChar).Value = mh.MaMonHoc;
                cmd.Parameters.Add("@TenMH", SqlDbType.NVarChar).Value = mh.TenMonHoc;
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
        public bool DeleteMonHoc(MonHoc mh)
        {
            //B1: tao chuoi ket noi sql
            string sql = "delete MonHoc where MaMonHoc = @MaMH";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                //Mo ket noi
                con.Open();
                cmd.Parameters.Add("@MaMH", SqlDbType.Int).Value = mh.MaMonHoc;
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
        public bool UpdateMonHoc(MonHoc mh)
        {
            //B1: tao chuoi ket noi sql
            string sql = "update MonHoc set TenMonHoc = @TenMH where MaMonHoc = @MaMH";
            //B2: Lay chuoi ket noi cmd
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                //Mo ket noi
                con.Open();
                cmd.Parameters.Add("@MaMH", SqlDbType.Char).Value = mh.MaMonHoc;
                cmd.Parameters.Add("@TenMH", SqlDbType.NVarChar).Value = mh.TenMonHoc;
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
