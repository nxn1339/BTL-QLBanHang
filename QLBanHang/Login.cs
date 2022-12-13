using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLBanHang.Model;

namespace QLBanHang
{

    public partial class Login : Form
    {
        //kết nối database
        KetNoiData kn = new KetNoiData();
        //biến logic ẩn hiện mật khẩu
        bool isHienThi = false;
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            CheckLogin();
        }

        private void CheckLogin()
        {
            //Mở kết nối Database
            kn.MoKetNoi();

            //Gán dữ liệu trong TextBox
            string taikhoan = txt_username.Text;
            string matkhau = txt_password.Text;
            //string role = null;

            //Select Bảng ACCOUNT và kiểm tra Tài Khoản và Mật Khẩu
            string sql = "SELECT TAIKHOAN.MaNV,NHANVIEN.TenNV,CHUCVU.TenCV,NHANVIEN.MaCV FROM TAIKHOAN,NHANVIEN,CHUCVU Where TAIKHOAN.MaNV = NHANVIEN.MaNV and NHANVIEN.MaCV=CHUCVU.MaCV and TaiKhoan ='"+taikhoan+"' and MatKhau = '"+matkhau+"'";



            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            try
            {
                //cho dữ liệu vào biến trong model nhân viên
                MD_NhanVien.maNV = dt.Rows[0][0].ToString().Trim();
                MD_NhanVien.tenNV = dt.Rows[0][1].ToString().Trim();
                MD_NhanVien.chucVu = dt.Rows[0][2].ToString().Trim();
                MD_NhanVien.maCV = dt.Rows[0][3].ToString().Trim();
            }
            catch
            {

            }

            if (dt.Rows.Count > 0) //đăng nhập thành công thì
            {
                this.Hide(); //ẩn form đăng nhập
                Form f = new HomeQL(); //tạo form trang chủ
                f.Show();//hiện form trang chủ
            }
            else
            {
                //báo lỗi đăng nhập
                MessageBox.Show("Tài Khoản Mật Khẩu Không Chính Xác !", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Đóng Kết nối Database
            kn.DongKetNoi();

            if (checkNhoTK.Checked == true) // nếu tích vào ô nhớ tài khoản ,mật khẩu
            {
                Properties.Settings.Default.user = txt_username.Text; //Lưu Tài Khoản
                Properties.Settings.Default.pass = txt_password.Text; //Lưu Mật Khẩu
                Properties.Settings.Default.check = true; //Lưu CheckBox
                Properties.Settings.Default.Save(); //Lưu
            }
            else // nếu không tích vào ô nhớ tài khoản ,mật khẩu
            {
                //để trống tài khoản mật khẩu như mới
                Properties.Settings.Default.user = "";
                Properties.Settings.Default.pass = "";
                Properties.Settings.Default.check = false;
                Properties.Settings.Default.Save();
            }
        }


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //hỏi đóng form đăng nhập
            if (MessageBox.Show("Bạn có muốn Đóng chương trình", "Hỏi người dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //nếu chọn yes thì đóng 
                Application.Exit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //đóng form đăng nhập
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            //phóng max State
            this.WindowState = FormWindowState.Maximized;
            //hiện nút phóng medium State
            btnMedium.Visible = true;
            //ẩn nút phóng max State
            btnMax.Visible = false;
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            //phóng medium State
            this.WindowState = FormWindowState.Normal;
            //ẩn nút phóng medium State
            btnMedium.Visible = false;
            //hiện nút phóng max State
            btnMax.Visible = true;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            //Thu Nhỏ State
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isHienThi == true) //Hiện Mật Khẩu Nếu Là True
            {
                ptbHienMK.Image = ptbHienMK.ErrorImage; //thay đổi ảnh icon sang ẩn mật khẩu
                txt_password.UseSystemPasswordChar = true; //hiện mật khẩu
                isHienThi = false; //cập nhật lại trạng thái 
                return; //tránh còng lặp
            }
            else //Ẩn Mật Khẩu Nếu Là False
            {
                ptbHienMK.Image = ptbHienMK.InitialImage; //thay đổi ảnh icon sang hiện mật khẩu
                txt_password.UseSystemPasswordChar = false; //ẩn mật khẩu
                isHienThi = true; //cập nhật lại trạng thái 
                return; //tránh còng lặp
            }
        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            //bấm Enter để đăng nhập
            if (e.KeyCode == Keys.Enter)
            {
                CheckLogin();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.user != string.Empty)
            {
                //Load Tài Khoản Mật Khẩu
                txt_username.Text = Properties.Settings.Default.user;
                txt_password.Text = Properties.Settings.Default.pass;
                checkNhoTK.Checked = Properties.Settings.Default.check; //Load CheckBox
            }
        }

        private void txt_username_KeyDown(object sender, KeyEventArgs e)
        {
            //bấm Enter để từ nhập tài khoản xuống dòng nhập mật khẩu
            if (e.KeyCode == Keys.Enter)
            {
                txt_password.Focus();   
            }
        }
    }
}
