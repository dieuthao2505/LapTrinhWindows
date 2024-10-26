using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanliSinhVien.GUI
{
    public partial class QuanLyKhoa : Form
    {
        private List<Khoa> danhSachKhoa = new List<Khoa>();
        public QuanLyKhoa()
        {
            InitializeComponent();

        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string id = txbID.Text;
            string maKhoa = txbMaKhoa.Text;
            string tenKhoa = txbTenKhoa.Text;

            // Kiểm tra nếu thông tin chưa đầy đủ
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(tenKhoa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm vào danh sách và DataGridView
            Khoa khoa = new Khoa { ID = id, MaKhoa = maKhoa, TenKhoa = tenKhoa };
            danhSachKhoa.Add(khoa);
            dataGridView1.Rows.Add(khoa.ID, khoa.MaKhoa, khoa.TenKhoa);

            // Xóa trắng các TextBox sau khi thêm
            txbID.Clear();
            txbTenKhoa.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string currentId = row.Cells["ID"].Value.ToString();

                // Kiểm tra nếu thông tin trong TextBox có hợp lệ
                if (string.IsNullOrEmpty(txbID.Text) || string.IsNullOrEmpty(txbTenKhoa.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật giá trị từ các TextBox vào DataGridView
                row.Cells["ID"].Value = txbID.Text;
                row.Cells["TenKhoa"].Value = txbTenKhoa.Text;

                // Cập nhật danh sách danhSachKhoa
                var khoa = danhSachKhoa.Find(k => k.ID == currentId);
                if (khoa != null)
                {
                    khoa.ID = txbID.Text;
                    khoa.TenKhoa = txbTenKhoa.Text;
                }

                // Sau khi sửa, xóa trắng các TextBox
                txbID.Clear();
                txbTenKhoa.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Lấy hàng được chọn
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    string id = row.Cells["ID"].Value.ToString();

                    // Xóa hàng khỏi DataGridView
                    dataGridView1.Rows.RemoveAt(row.Index);

                    // Xóa khỏi danh sách danhSachKhoa
                    var khoa = danhSachKhoa.Find(k => k.ID == id);
                    if (khoa != null)
                    {
                        danhSachKhoa.Remove(khoa);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Gán giá trị từ DataGridView vào các TextBox
                txbID.Text = row.Cells["ID"].Value.ToString();
                txbMaKhoa.Text = row.Cells["MaKhoa"].Value.ToString();
                txbTenKhoa.Text = row.Cells["TenKhoa"].Value.ToString();
            }
        }
        public class Khoa
        {
            public string ID { get; set; }
            public string MaKhoa { get; set; }
            public string TenKhoa { get; set; }
        }

        private void QuanLyKhoa_Load(object sender, EventArgs e)
        {

        }
    }
}
