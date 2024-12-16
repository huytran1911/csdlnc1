using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSDLNCProject
{
    public partial class FormDepartment : Form
    {
        public FormDepartment()
        {
            InitializeComponent();
        }

        PhongBanBLL phongBanBLL = new PhongBanBLL();

        private void loadPhongBan()
        {

            dgvPhongBan.DataSource = phongBanBLL.GetAllPhongBan();

            dgvPhongBan.Columns[1].HeaderText = "Mã phòng ban";
            dgvPhongBan.Columns[2].HeaderText = "Phòng ban";
            
            if (dgvPhongBan.Columns["_id"] != null)
            {
                dgvPhongBan.Columns["_id"].Visible = false;
            }

        }

        private void AddPBButton_click(object sender, EventArgs e)
        {
            string maPhongBan = txtMaPhongBan.Text.Trim();
            string tenPhongBan = txtTenPhongBan.Text.Trim();

            // Tạo DTO
            PhongBanDTO phongBan = new PhongBanDTO
            {
                maphongban = maPhongBan,
                phongban = tenPhongBan
            };

            // Gọi BLL để thêm dữ liệu
            string result = phongBanBLL.AddPhongBan(phongBan);
            MessageBox.Show(result, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Làm mới DataGridView
            loadPhongBan();
        }

        private void UpdatePBButton_click(object sender, EventArgs e)
        {
            string maPhongBan = txtMaPhongBan.Text.Trim();
            string tenPhongBan = txtTenPhongBan.Text.Trim();

            // Tạo DTO để truyền dữ liệu
            PhongBanDTO phongBan = new PhongBanDTO
            {
                maphongban = maPhongBan,
                phongban = tenPhongBan
            };

            

            // Gọi BLL để cập nhật dữ liệu
            string result = phongBanBLL.UpdatePhongBan(phongBan);

            // Thông báo kết quả
            MessageBox.Show(result, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Làm mới DataGridView sau khi sửa
            loadPhongBan();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        

        

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void FormDepartment_Load(object sender, EventArgs e)
        {
            loadPhongBan();

        }

       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem hàng được click có hợp lệ không (không phải tiêu đề)
                if (e.RowIndex >= 0)
                {
                    // Lấy hàng được click
                    DataGridViewRow row = dgvPhongBan.Rows[e.RowIndex];
                    

                    // Kiểm tra và gán dữ liệu vào TextBox nếu không null
                    txtMaPhongBan.Text = row.Cells[1]?.Value?.ToString() ?? string.Empty;
                    txtTenPhongBan.Text = row.Cells[2]?.Value?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
