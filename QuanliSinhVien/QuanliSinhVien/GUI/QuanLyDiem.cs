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
    public partial class QuanLyDiem : Form
    {
        private List<Diem> danhSachDiem = new List<Diem>();
        public QuanLyDiem()
        {
            InitializeComponent();
        }

        private void cmbMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private decimal CalculateAverage(Diem diem)
        {
            // Implement your average calculation logic here
            return (diem.DiemThi + diem.DiemTrenLop) / 2; // Example calculation
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string id = txbID.Text;
            string maSV = cmbMaSV.SelectedItem?.ToString();
            string maMH = cmbMaMH.SelectedItem?.ToString();
            string loai = cmbLoai.SelectedItem?.ToString();
            decimal diemThi = numPhanTramThi.Value;
            decimal diemTrenLop = numPhanTramTrenLop.Value;

            // Kiểm tra thông tin hợp lệ
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Vui lòng nhập ID điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(maSV))
            {
                MessageBox.Show("Vui lòng chọn Mã sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMaSV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(maMH))
            {
                MessageBox.Show("Vui lòng chọn Mã môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMaMH.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(loai))
            {
                MessageBox.Show("Vui lòng chọn Loại điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLoai.Focus();
                return;
            }

            Diem diem = new Diem
            {
                ID = id,
                MaSV = maSV,
                MaMH = maMH,
                Loai = loai,
                DiemThi = diemThi,
                DiemTrenLop = diemTrenLop
            };

            // Thêm điểm vào danh sách
            danhSachDiem.Add(diem);
            MessageBox.Show("Thêm điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateDataGridView();  // Tải lại dữ liệu
        }
        private void UpdateDataGridView()
        {
            dataGridViewDiem.Rows.Clear(); // Clear existing rows

            foreach (var diem in danhSachDiem)
            {
                dataGridViewDiem.Rows.Add(
                    diem.ID,
                    diem.MaSV,
                    diem.MaMH,
                    diem.DiemThi,
                    diem.DiemTrenLop,
                    CalculateAverage(diem), // Assuming you want to calculate average here
                    diem.Loai
                );
            }
        }
            private void InitializeData()
        {
            cmbMaSV.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaMH.DropDownStyle = ComboBoxStyle.DropDownList;

            // Sample data
            cmbMaSV.Items.Clear();
            cmbMaMH.Items.Clear();
            cmbLoai.Items.Clear();

            cmbMaSV.Items.Add("SV001");
            cmbMaSV.Items.Add("SV002");
            cmbMaMH.Items.Add("MH001");
            cmbMaMH.Items.Add("MH002");
            cmbLoai.Items.Add("Giỏi");
            cmbLoai.Items.Add("Khá");
            cmbLoai.Items.Add("Trung Bình");

            // Create ID column if it does not exist
            if (!dataGridViewDiem.Columns.Contains("ID"))
            {
                DataGridViewColumn idColumn = new DataGridViewTextBoxColumn
                {
                    Name = "ID",
                    HeaderText = "ID",
                    ReadOnly = true // If you do not want to edit ID
                };
                dataGridViewDiem.Columns.Add(idColumn);
            }
        }

        private void QuanLyDiem_Load(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void dataGridViewDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewDiem.Rows[e.RowIndex];
                txbID.Text = row.Cells["ID"].Value.ToString();
                cmbMaSV.SelectedItem = row.Cells["MaSV"].Value.ToString();
                cmbMaMH.SelectedItem = row.Cells["MaMH"].Value.ToString();
                cmbLoai.SelectedItem = row.Cells["Loai"].Value.ToString();
                numPhanTramThi.Value = Convert.ToDecimal(row.Cells["DiemThi"].Value);
                numPhanTramTrenLop.Value = Convert.ToDecimal(row.Cells["DiemTrenLop"].Value);
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewDiem.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridViewDiem.SelectedRows[0];

                // Lấy thông tin từ các điều khiển
                string maSV = cmbMaSV.SelectedItem?.ToString();
                string maMH = cmbMaMH.SelectedItem?.ToString();
                int phanTramTrenLop = (int)numPhanTramTrenLop.Value;
                int phanTramThi = (int)numPhanTramThi.Value;
                float diemTrenLop = float.Parse(txbDiemTrenLop.Text.Trim());
                float diemThi = float.Parse(txbDiemThi.Text.Trim());
                float diemTB = (diemTrenLop * phanTramTrenLop / 100) + (diemThi * phanTramThi / 100);
                string loai = cmbLoai.SelectedItem?.ToString();

                // Kiểm tra tính hợp lệ
                if (!string.IsNullOrEmpty(maSV) && !string.IsNullOrEmpty(maMH) &&
                    !string.IsNullOrEmpty(loai))
                {
                    // Cập nhật dòng đã chọn
                    selectedRow.Cells["MaSV"].Value = maSV;
                    selectedRow.Cells["MaMH"].Value = maMH;
                    selectedRow.Cells["PhanTramTrenLop"].Value = phanTramTrenLop;
                    selectedRow.Cells["PhanTramThi"].Value = phanTramThi;
                    selectedRow.Cells["DiemTrenLop"].Value = diemTrenLop;
                    selectedRow.Cells["DiemThi"].Value = diemThi;
                    selectedRow.Cells["DiemTB"].Value = diemTB;
                    selectedRow.Cells["Loai"].Value = loai;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewDiem.SelectedRows.Count > 0)
            {
                // Hỏi người dùng xác nhận việc xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Xóa hàng đang chọn
                    foreach (DataGridViewRow row in dataGridViewDiem.SelectedRows)
                    {
                        if (!row.IsNewRow) // Kiểm tra nếu không phải là hàng trống
                        {
                            dataGridViewDiem.Rows.Remove(row);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    
    public class Diem
    {
        public string ID { get; set; }
        public string MaSV { get; set; }
        public string MaMH { get; set; }
        public decimal DiemThi { get; set; }
        public decimal DiemTrenLop { get; set; }
        public string Loai { get; set; }
    }
}
