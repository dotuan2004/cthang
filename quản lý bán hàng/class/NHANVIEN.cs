using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quản_lý_bán_hàng
{
    
    class NHANVIEN
    {
        MY_DB mydb = new MY_DB();
        public bool themNV(int msnv, string hoten, string gioitinh, DateTime namsinh, string diachi, MemoryStream hinh, string username, string password)
        {
            SqlCommand command = new SqlCommand("INSERT INTO NHANVIEN (msnv,hoten,gioitinh,namsinh,diachi,hinh,username,password)" + "VALUES (@msnv, @ht, @gt, @ns, @dc, @hinh, @user, @pass)", mydb.getConnection);
            command.Parameters.Add("@msnv", SqlDbType.Int).Value = msnv;
            command.Parameters.Add("@ht", SqlDbType.NVarChar).Value = hoten;
            command.Parameters.Add("@gt", SqlDbType.NVarChar).Value = gioitinh;
            command.Parameters.Add("@ns", SqlDbType.DateTime).Value = namsinh;
            command.Parameters.Add("@dc", SqlDbType.NVarChar).Value = diachi;
            command.Parameters.Add("@hinh", SqlDbType.Image).Value = hinh.ToArray();
            command.Parameters.Add("@user", SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", SqlDbType.VarChar).Value = password;
           

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }
        public bool checkID(int msnv)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM NHANVIEN WHERE msnv=" + msnv, mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if ((table.Rows.Count == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable layNV(SqlCommand command)
        {

            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;

        }
        public bool XoaNV(int msnv)
        {
            SqlCommand command;
            mydb.openConnection();

            try
            {
                // Xóa dữ liệu trong bảng THANHTOAN
                command = new SqlCommand("DELETE FROM THANHTOAN WHERE msnv = @msnv", mydb.getConnection);
                command.Parameters.AddWithValue("@msnv", msnv);
                command.ExecuteNonQuery();

                // Xóa dữ liệu trong bảng CHAMCONG
                command = new SqlCommand("DELETE FROM CHAMCONG WHERE msnv = @msnv", mydb.getConnection);
                command.Parameters.AddWithValue("@msnv", msnv);
                command.ExecuteNonQuery();

                // Xóa nhân viên trong bảng NHANVIEN
                command = new SqlCommand("DELETE FROM NHANVIEN WHERE msnv = @msnv", mydb.getConnection);
                command.Parameters.AddWithValue("@msnv", msnv);

                bool isDeleted = command.ExecuteNonQuery() == 1;

                mydb.closeConnection();
                return isDeleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
                mydb.closeConnection();
                return false;
            }
        }

        public bool suaNV(int msnv, string hoten, string gioitinh, DateTime namsinh, string diachi, MemoryStream hinh, string username, string password)
        {

            SqlCommand command = new SqlCommand("UPDATE NHANVIEN SET hoten=@ht,gioitinh=@gt,namsinh=@ns,diachi=@dc,hinh=@hinh,username=@user,password=@pass WHERE msnv=@msnv", mydb.getConnection);
            command.Parameters.Add("@msnv", SqlDbType.Int).Value = msnv;
            command.Parameters.Add("@ht", SqlDbType.NVarChar).Value = hoten;
            command.Parameters.Add("@gt", SqlDbType.NVarChar).Value = gioitinh;
            command.Parameters.Add("@ns", SqlDbType.DateTime).Value = namsinh;
            command.Parameters.Add("@dc", SqlDbType.NVarChar).Value = diachi;
            command.Parameters.Add("@hinh", SqlDbType.Image).Value = hinh.ToArray();
            command.Parameters.Add("@user", SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", SqlDbType.VarChar).Value = password;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }


        }
       
    }
}
