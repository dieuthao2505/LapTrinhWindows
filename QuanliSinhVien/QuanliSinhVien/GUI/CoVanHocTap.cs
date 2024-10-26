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
    public partial class CoVanHocTap : Form
    {
        public CoVanHocTap()
        {
            InitializeComponent();
        }

        private void txbMaCVHT_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển
            string maCVHT = txbMaCVHT.Text.Trim();
            string tenCVHT = txbTenCVHT.Text.Trim();
            DateTime ngaySinh = dtpkNgaySinh.Value;
            string gioiTinh = rdNam.Checked ? "Nam" : "Nữ";
            string maKhoa = cmbMaKhoa.SelectedItem?.ToString();
            string maLop = cmbMaLop.SelectedItem?.ToString();

            // Kiểm tra tính hợp lệ của dữ liệu
            if (!string.IsNullOrEmpty(maCVHT) && !string.IsNullOrEmpty(tenCVHT) &&
                !string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(maLop))
            {
                // Thêm vào database hoặc DataGridView
                // Ví dụ thêm vào DataGridView:
                int newRowId = dataGridViewCVHT.Rows.Add();
                dataGridViewCVHT.Rows[newRowId].Cells["ID"].Value = newRowId + 1;
                dataGridViewCVHT.Rows[newRowId].Cells["MaCVHT"].Value = maCVHT;
                dataGridViewCVHT.Rows[newRowId].Cells["TenCVHT"].Value = tenCVHT;
                dataGridViewCVHT.Rows[newRowId].Cells["NgaySinh"].Value = ngaySinh.ToShortDateString();
                dataGridViewCVHT.Rows[newRowId].Cells["GioiTinh"].Value = gioiTinh;
                dataGridViewCVHT.Rows[newRowId].Cells["MaKhoa"].Value = maKhoa;
                dataGridViewCVHT.Rows[newRowId].Cells["MaLop"].Value = maLop;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
