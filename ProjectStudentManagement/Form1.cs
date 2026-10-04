using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStudentManagement
{
    public partial class frmStudent : Form
    {
        public frmStudent()
        {
            InitializeComponent();

            // Cấu hình giao diện
            CauHinhForm();

            // Hiển thị danh sách
            HienThiDanhSach();

            // Đưa con trỏ vào ô MSSV
            txtMSSV.Focus();

            // Sự kiện click DataGridView
            dgvStudents.CellClick += dgvStudents_CellClick;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmStudent_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
            // Danh sách sinh viên
            private List<Student> danhSachSinhVien = new List<Student>();

            // MSSV của sinh viên đang được chọn
            private string mssvDangChon = "";

            // =====================================================
            // CẤU HÌNH FORM
            // =====================================================

            private void CauHinhForm()
            {
                this.Text = "PHẦN MỀM QUẢN LÝ SINH VIÊN";
                this.StartPosition = FormStartPosition.CenterScreen;

                // DataGridView
                dgvStudents.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvStudents.MultiSelect = false;

                dgvStudents.ReadOnly = true;

                dgvStudents.AllowUserToAddRows = false;

                dgvStudents.AllowUserToDeleteRows = false;

                dgvStudents.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }

            // =====================================================
            // NÚT THÊM
            // =====================================================

            private void btnAdd_Click(object sender, EventArgs e)
            {
                // Kiểm tra MSSV
                if (string.IsNullOrWhiteSpace(txtMSSV.Text))
                {
                    MessageBox.Show(
                        "Vui lòng nhập Mã sinh viên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMSSV.Focus();
                    return;
                }

                // Kiểm tra họ tên
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show(
                        "Vui lòng nhập Họ tên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtHoTen.Focus();
                    return;
                }

                // Kiểm tra MSSV trùng
                bool daTonTai = danhSachSinhVien.Any(
                    sv => sv.MSSV.Equals(
                        txtMSSV.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase));

                if (daTonTai)
                {
                    MessageBox.Show(
                        "Mã sinh viên đã tồn tại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMSSV.Focus();
                    return;
                }

                // Tạo sinh viên
                Student student = new Student
                {
                    MSSV = txtMSSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = txtNgaySinh.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    Lop = txtLop.Text.Trim()
                };

                // Thêm vào danh sách
                danhSachSinhVien.Add(student);

                // Hiển thị lên bảng
                HienThiDanhSach();

                // Xóa trắng
                XoaTrang();

                MessageBox.Show(
                    "Thêm sinh viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            // =====================================================
            // NÚT SỬA
            // =====================================================

            private void btnEdit_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(mssvDangChon))
                {
                    MessageBox.Show(
                        "Vui lòng chọn sinh viên cần sửa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Student student = danhSachSinhVien.FirstOrDefault(
                    sv => sv.MSSV == mssvDangChon);

                if (student == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy sinh viên!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // Cập nhật thông tin
                student.HoTen = txtHoTen.Text.Trim();
                student.NgaySinh = txtNgaySinh.Text.Trim();
                student.Email = txtEmail.Text.Trim();
                student.SoDienThoai = txtSDT.Text.Trim();
                student.Lop = txtLop.Text.Trim();

                // Cập nhật bảng
                HienThiDanhSach();

                // Xóa trắng
                XoaTrang();

                MessageBox.Show(
                    "Cập nhật thông tin thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            // =====================================================
            // NÚT XÓA
            // =====================================================

            private void btnDelete_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(mssvDangChon))
                {
                    MessageBox.Show(
                        "Vui lòng chọn sinh viên cần xóa!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Student student = danhSachSinhVien.FirstOrDefault(
                    sv => sv.MSSV == mssvDangChon);

                if (student == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy sinh viên!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // Xác nhận xóa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa sinh viên?\n\n" +
                    "MSSV: " + student.MSSV + "\n" +
                    "Họ tên: " + student.HoTen,
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }

                // Xóa
                danhSachSinhVien.Remove(student);

                // Cập nhật bảng
                HienThiDanhSach();

                // Xóa trắng
                XoaTrang();

                MessageBox.Show(
                    "Xóa sinh viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            // =====================================================
            // NÚT LÀM MỚI
            // =====================================================

            private void btnRefresh_Click(object sender, EventArgs e)
            {
                XoaTrang();

                dgvStudents.ClearSelection();

                MessageBox.Show(
                    "Đã làm mới!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            // =====================================================
            // NÚT THOÁT
            // =====================================================

            private void btnExit_Click(object sender, EventArgs e)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn thoát chương trình?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }

            // =====================================================
            // CLICK VÀO DATAGRIDVIEW
            // =====================================================

            private void dgvStudents_CellClick(
                object sender,
                DataGridViewCellEventArgs e)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];

                txtMSSV.Text =
                    row.Cells["MSSV"].Value?.ToString() ?? "";

                txtHoTen.Text =
                    row.Cells["HoTen"].Value?.ToString() ?? "";

                txtNgaySinh.Text =
                    row.Cells["NgaySinh"].Value?.ToString() ?? "";

                txtEmail.Text =
                    row.Cells["Email"].Value?.ToString() ?? "";

                txtSDT.Text =
                    row.Cells["SoDienThoai"].Value?.ToString() ?? "";

                txtLop.Text =
                    row.Cells["Lop"].Value?.ToString() ?? "";

                // Lưu MSSV đang chọn
                mssvDangChon = txtMSSV.Text;
            }

            // =====================================================
            // HIỂN THỊ DANH SÁCH
            // =====================================================

            private void HienThiDanhSach()
            {
                dgvStudents.DataSource = null;

                dgvStudents.DataSource = danhSachSinhVien;

                // Đổi tên tiêu đề cột
                if (dgvStudents.Columns["MSSV"] != null)
                    dgvStudents.Columns["MSSV"].HeaderText = "MSSV";

                if (dgvStudents.Columns["HoTen"] != null)
                    dgvStudents.Columns["HoTen"].HeaderText = "Họ tên";

                if (dgvStudents.Columns["NgaySinh"] != null)
                    dgvStudents.Columns["NgaySinh"].HeaderText = "Ngày sinh";

                if (dgvStudents.Columns["Email"] != null)
                    dgvStudents.Columns["Email"].HeaderText = "Email";

                if (dgvStudents.Columns["SoDienThoai"] != null)
                    dgvStudents.Columns["SoDienThoai"].HeaderText =
                        "Số điện thoại";

                if (dgvStudents.Columns["Lop"] != null)
                    dgvStudents.Columns["Lop"].HeaderText = "Lớp";

                dgvStudents.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvStudents.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvStudents.MultiSelect = false;

                dgvStudents.ReadOnly = true;
            }

            // =====================================================
            // XÓA TRẮNG
            // =====================================================

            private void XoaTrang()
            {
                txtMSSV.Clear();
                txtHoTen.Clear();
                txtNgaySinh.Clear();
                txtEmail.Clear();
                txtSDT.Clear();
                txtLop.Clear();

                mssvDangChon = "";

                dgvStudents.ClearSelection();

                txtMSSV.Focus();
            }
    }
}

