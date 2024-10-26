using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanliSinhVien.DAL
{
    internal class KetNoi
    {
        // Chuỗi kết nối cơ sở dữ liệu
        private string connectionString = @"Data Source=.;Initial Catalog=db_QLSV;Integrated Security=True";
        private static KetNoi instance;

        public static KetNoi Instance
        {
            get
            {
                if (instance == null)
                    instance = new KetNoi();
                return instance;
            }
        }

        private KetNoi() { }
        //Lấy danh sách
        public DataTable ExcuteQuery(string query, object[] parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        string[] listParams = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int i = 0;
                        foreach (string item in listParams)
                        {
                            if (item.StartsWith("@"))
                            {
                                command.Parameters.AddWithValue(item, parameters[i]);
                                i++;
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(data);
                    }
                }
            }
            return data;
        }
        //Thêm, sửa, xoá
        public bool ExcuteNonQuery(string query, object[] parameters = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        string[] listParams = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int i = 0;
                        foreach (string item in listParams)
                        {
                            if (item.StartsWith("@"))
                            {
                                command.Parameters.AddWithValue(item, parameters[i]);
                                i++;
                            }
                        }
                    }

                    data = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return data > 0;
        }
    }
}

