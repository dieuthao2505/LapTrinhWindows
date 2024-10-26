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
    public partial class SinhVien : Form

    {
        private List<QLSinhVien> danhSachSinhVien = new List<QLSinhVien>();
        public SinhVien()
        {
            InitializeComponent();

        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            //f.ShowDialog();
            this.Show();
        }

        private void txbTenSV_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            string ID = txbID.Text;
            string maSV = txbMaSV.Text;
            string tenSV = txbTenSV.Text;
            DateTime ngaySinh = dtpkNgaySinh.Value;
            string gioiTinh = rdNam.Checked ? "Nam" : "Nữ";
            string queQuan = txbQueQuan.Text;
            // Thêm các thông tin khác...

            // Thêm vào DataGridView (hoặc thêm vào cơ sở dữ liệu và tải lại DataGridView)
            dgvSinhVien.Rows.Add(ID, maSV, tenSV, ngaySinh.ToShortDateString(), gioiTinh, queQuan);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = dgvSinhVien.SelectedRows[0];

                // Cập nhật giá trị từ các TextBox vào DataGridView
                row.Cells["ID"].Value = txbID.Text;
                row.Cells["MaSinhVien"].Value = txbMaSV.Text;
                row.Cells["TenSinhVien"].Value = txbTenSV.Text;
                row.Cells["NgaySinh"].Value = dtpkNgaySinh.Value.ToShortDateString();
                row.Cells["GioiTinh"].Value = rdNam.Checked ? "Nam" : "Nữ";
                row.Cells["QueQuan"].Value = txbQueQuan.Text;
                // Cập nhật các giá trị khác nếu có

                // Nếu bạn có kết nối cơ sở dữ liệu, hãy cập nhật thông tin vào database ở đây
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                // Xóa hàng được chọn
                foreach (DataGridViewRow row in dgvSinhVien.SelectedRows)
                {
                    if (!row.IsNewRow) // Kiểm tra nếu không phải là hàng trống
                    {
                        dgvSinhVien.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSinhVien.Rows.Count)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                // Gán giá trị từ DataGridView vào các TextBox
                txbID.Text = row.Cells["ID"].Value.ToString();
                txbMaSV.Text = row.Cells["MaSinhVien"].Value.ToString();
                txbTenSV.Text = row.Cells["TenSinhVien"].Value.ToString();
                dtpkNgaySinh.Value = DateTime.Parse(row.Cells["NgaySinh"].Value.ToString());
                rdNam.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nam";
                rdNu.Checked = !rdNam.Checked;
                txbQueQuan.Text = row.Cells["QueQuan"].Value.ToString();
            }
        }

        private void txbMaSV_TextChanged(object sender, EventArgs e)
        {

        }

        private void quảnLýLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyLop f = new QuanLyLop();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýCốVấnHọcTậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoVanHocTap f = new CoVanHocTap();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyMonHoc f = new QuanLyMonHoc();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýDiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyDiem f = new QuanLyDiem();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýKhoa_Click(object sender, EventArgs e)
        {
            QuanLyKhoa f = new QuanLyKhoa();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chứcNăngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void SinhVien_Load(object sender, EventArgs e)
        {
            // Xóa tất cả các hàng hiện có trong DataGridView
            dgvSinhVien.Rows.Clear();

            // Thêm từng sinh viên từ danh sách vào DataGridView
            foreach (var sv in danhSachSinhVien)
            {
                dgvSinhVien.Rows.Add(sv.MaSV, sv.TenSV, sv.NgaySinh.ToShortDateString(), sv.GioiTinh, sv.QueQuan, sv.MaLop, sv.MaKhoa, sv.MaCoVan, sv.NgayNhapHoc.ToShortDateString());
            }
            
        }
        public class QLSinhVien
        {
            public string MaSV { get; set; }
            public string TenSV { get; set; }
            public DateTime NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string QueQuan { get; set; }
            public string MaLop { get; set; }
            public string MaKhoa { get; set; }
            public string MaCoVan { get; set; }
            public DateTime NgayNhapHoc { get; set; }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
