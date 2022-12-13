using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class HomeQL : Form
    {
        //biến logic đóng mở menu
        bool isMenu=false;

        KetNoiData kn = new KetNoiData();
        public HomeQL()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            //Thanh chuyển thanh slide sang trang chủ
            plSlide.Height = btnHome.Height;
            plSlide.Top = btnHome.Top;    
            //Hiện trang chủ và banner
            plHome.Visible = true;
            plbanner.Visible = true;
            //ẩn các userController còn lại
            ucNhanVien1.Visible = false;
            ucHoaDon1.Visible = false;
            ucMatHang1.Visible = false; 
            ucDoanhThu1.Visible = false;

        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            //Thanh chuyển thanh slide sang hóa đơn
            plSlide.Height = btnHoaDon.Height;
            plSlide.Top = btnHoaDon.Top;

            //Chuyển Slide
            //hiện userController hóa đơn
            ucHoaDon1.Visible = true;
            //ẩn các userController còn lại
            ucNhanVien1.Visible = false;
            plbanner.Visible = false;
            ucMatHang1.Visible = false;
            ucDoanhThu1.Visible = false;

        }
        private void btnMatHang_Click(object sender, EventArgs e)
        {
            //Thanh chuyển thanh slide sang mặt hàng
            plSlide.Height = btnMatHang.Height;
            plSlide.Top = btnMatHang.Top;
            //Chuyển Slide

            //hiện userController mặt hàng
            ucMatHang1.Visible = true;
            //ẩn các userController còn lại
            ucNhanVien1.Visible = false;
            plbanner.Visible = false;
            ucHoaDon1.Visible = false;
            ucDoanhThu1.Visible = false;

        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            //Thanh chuyển thanh slide sang doanh thu
            plSlide.Height = btnDoanhThu.Height;
            plSlide.Top = btnDoanhThu.Top;

            //Chuyển Slide
            //hiện userController doanh thu
            ucDoanhThu1.Visible = true;
            //ẩn các userController còn lại
            ucNhanVien1.Visible = false;
            plbanner.Visible = false;
            ucHoaDon1.Visible = false;
            ucMatHang1.Visible = false;


        }
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            //Thanh chuyển thanh slide sang nhân viên
            plSlide.Height = btnNhanVien.Height;
            plSlide.Top = btnNhanVien.Top;
            //Chuyển Slide
            //ẩn các userController còn lại
            plbanner.Visible = false;
            ucHoaDon1.Visible = false;
            ucMatHang1.Visible = false;
            ucDoanhThu1.Visible = false;
            //hiện userController doanh thu
            ucNhanVien1.Visible = true;

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
        private void btnClose_Click(object sender, EventArgs e)
        {
            //Hỏi
            DialogResult re = MessageBox.Show("Bạn Có Muốn Đóng Ứng Dụng Không ?", "Hỏi Người Dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (re == DialogResult.Yes)
            {
                //Thoát
                Application.Exit();
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            //Thu Nhỏ State
            this.WindowState = FormWindowState.Minimized;
        }

        private void Datetimer_Tick(object sender, EventArgs e)
        {
            //Cập nhật giờ phút vào lb
            lbtime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn Có Muốn Đăng Xuất Không ?", "Hỏi Người Dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                //Đóng Home
                this.Close();
                //Chạy Login
                Thread th = new Thread(openForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }

        }

        private void openForm(object obj)
        {
            //Chạy State Login
            Application.Run(new Login());
        }

        //Hàm này thu gọn menu
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (isMenu == false) //nếu menu chưa thu
            {
                panelMenu.Width = 65; //thu panelMenu còn 65
                ptbLogo.Visible = false; //ẩn logo
                isMenu = true; //chuyển trạng thái sang đang thu menu
                btnCaiDat.Text = ""; //ẩn text cài đặt
                return; //return để tránh ko bị vòng lặp       
            }
            else //nếu menu đã thu khi kích hoạt sẽ
            {
                panelMenu.Width = 214; //phóng panelMenu ra 214
                ptbLogo.Visible = true; //hiện logo
                isMenu = false; // chuyển trạng thái sang chưa thu menu
                btnCaiDat.Text = "Cài Đặt"; //hiện text cài đặt
                return; //return để tránh ko bị vòng lặp
            }
        }

    

        private void HienThiAvatar(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaNV = new SqlParameter("@MaNV", SqlDbType.Char);
            parMaNV.Value = MD_NhanVien.maNV;
            sqlCmd.Parameters.Add(parMaNV);
            sqlCmd.Connection = kn.sqlCon;
            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            //Mở ảnh cho vào pictureBox
            ptbAvatar.Image = new Bitmap(dt.Rows[0][0].ToString().Trim());
            ptbAvatar.ImageLocation = dt.Rows[0][0].ToString().Trim();
            ptbAvatar.SizeMode = PictureBoxSizeMode.StretchImage;

            //Làm Tròn Viền Ảnh(Avatar) (từ vuông sang tròn)
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, ptbAvatar.Width - 3, ptbAvatar.Height - 3);
            Region rg = new Region(gp);
            ptbAvatar.Region = rg;

        }

        private void HomeQL_Load(object sender, EventArgs e)
        {   
            //chia quyền
            if(MD_NhanVien.maCV=="QL") //nếu là quản lý
            {
                //hiện nút nhân viên (chức năng thêm sửa xóa nhân viên, chức vụ, tài khoản)
                btnNhanVien.Visible = true;
                //đổi màu lable chức vụ sang đỏ
                lbChucVu.ForeColor = Color.Red;
            }
            else
            {
                //ẩn nút nhân viên (các chức vụ khác ngoài Quản Lý không được thêm sửa xóa nhân viên)
                btnNhanVien.Visible = false;
                //đổi màu lable chức vụ sang xanh da trời
                lbChucVu.ForeColor = Color.Blue;
            }
           
            //Thanh chuyển slide măc định ban đầu ở trang chủ
            plHome.Visible = true;
            plbanner.Visible = true;
            //Thời Gian
            Datetimer.Start();
            //gán chức vụ và tên đọc được khi đăng nhập
            lbChucVu.Text = MD_NhanVien.chucVu;
            lbTenNV.Text = MD_NhanVien.tenNV;
            
            kn.MoKetNoi();
            //hiển thị avatar
            HienThiAvatar("HienThiAvatar");
            kn.DongKetNoi();
            
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {                    
            if (plMenuSetting.Visible == false) // nếu plMenuSetting đang ẩn thì
            {
                plMenuSetting.Visible = true; //hiện plMenuSetting lên 
                return; //tránh bị vòng lặp
            }
            else // nếu plMenuSetting đang hiện thì
            {
                plMenuSetting.Visible = false;//ẩn plMenuSetting lên 
                return; //tránh bị vòng lặp
            }
        }

        private void btnThoat_CD_Click(object sender, EventArgs e)
        {
            //Hỏi
            DialogResult re = MessageBox.Show("Bạn Có Muốn Đóng Ứng Dụng Không ?", "Hỏi Người Dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (re == DialogResult.Yes)
            {
                //Thoát
                Application.Exit();
            }
        }
    }
}
