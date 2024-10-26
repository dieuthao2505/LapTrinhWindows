using QuanliSinhVien.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanliSinhVien

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbLoaiTaiKhoan.DropDownStyle = ComboBoxStyle.DropDownList;

            // Nạp dữ liệu vào ComboBox
            cmbLoaiTaiKhoan.Items.Add("Admin");
            cmbLoaiTaiKhoan.Items.Add("User");
            cmbLoaiTaiKhoan.Items.Add("Moderator");
        }

        private void btnTem_Click(object sender, EventArgs e)
        {
            string tendangnhap = txbTenDangNhap.Text.Trim();
            string matkhau = txbMatKhau.Text.Trim();
            string LoaiTK = cmbLoaiTaiKhoan.SelectedItem?.ToString(); // Sử dụng toán tử điều kiện null

            if (tendangnhap.Length > 0 && matkhau.Length >= 6 && !string.IsNullOrEmpty(LoaiTK))
            {
                if (BLL_TaiKhoan.Instance.Them(tendangnhap, matkhau, LoaiTK))
                    btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Mật khẩu không được dưới 6 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvTaiKhoan.DataSource = BLL_TaiKhoan.Instance.DanhSach();
        }

        private void txbID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dgvTaiKhoan.SelectedRows[0];
                string tendangnhap = row.Cells["TenDangNhap"].Value.ToString();

                // Xóa tài khoản
                if (BLL_TaiKhoan.Instance.Xoa(tendangnhap))
                {
                    btnLamMoi.PerformClick();
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dgvTaiKhoan.SelectedRows[0];
                string tendangnhap = row.Cells["TenDangNhap"].Value.ToString();

                // Cập nhật thông tin tài khoản
                string matkhau = txbMatKhau.Text.Trim();
                string LoaiTK = cmbLoaiTaiKhoan.SelectedItem?.ToString();

                if (matkhau.Length >= 6 && !string.IsNullOrEmpty(LoaiTK))
                {
                    if (BLL_TaiKhoan.Instance.Sua(tendangnhap, matkhau, LoaiTK))
                    {
                        btnLamMoi.PerformClick();
                        MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không được dưới 6 ký tự hoặc loại tài khoản không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
