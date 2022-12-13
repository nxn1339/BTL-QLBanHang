using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class UCTrangChu : UserControl
    {
        KetNoiData kn = new KetNoiData();

        public UCTrangChu()
        {
            InitializeComponent();
        }

        private void UCTrangChu_Load(object sender, EventArgs e)
        {
            //hiển thị 5 đơn gần nhất
            HienThiDonGan();
            //hienr thị xem nhanh doanh thu, nhân viên, mặt hàng, hóa đơn
            HienThiXemNhanh();
            //bắt đầu timer (đồng hồ)
            timerTrangChu.Start();
        }

        public void HienThiDonGan()
        {

            kn.MoKetNoi();

            //===================== HIEN THI DON HANG GAN NHAT ==========================

            string sqlSelectDH = "select top 5 HOADON.MaHD,HOADON.Ngaylap, NHANVIEN.TenNV, KHACHHANG.TenKH,KHACHHANG.DiaChi,KHACHHANG.SDT,MATHANG.TenMH,FORMAT(CTHOADON.DgBan,'C0','vi-VN') as 'DgBan',CTHOADON.SlBan,FORMAT(CTHOADON.DgBan*CTHOADON.SlBan,'C0','vi-VN') as 'TongTien' from HOADON ,NHANVIEN,KHACHHANG,MATHANG,CTHOADON where NHANVIEN.MaNV = HOADON.MaNV and HOADON.MaKH = KHACHHANG.MaKH and CTHOADON.MaHD = HOADON.MaHD and CTHOADON.MaMH = MATHANG.MaMH order by HOADON.Ngaylap desc;\r\n";
            // Thuc thi truy van
            SqlDataAdapter dap = new SqlDataAdapter(sqlSelectDH, kn.sqlCon);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dgDonGanDay.DataSource = dt;

            kn.DongKetNoi();

        }
        public void HienThiXemNhanh()
        {
            //Hien thi so luong Nhan Vien
            kn.MoKetNoi();
            string sqlDemTV = "SELECT COUNT(MaNV) FROM NHANVIEN";
            SqlDataAdapter dapDemTV = new SqlDataAdapter(sqlDemTV, kn.sqlCon);
            DataTable dtTV = new DataTable();
            dapDemTV.Fill(dtTV);
            //gắn dữ liệu vào lable nhân viên
            lbThanhVien.Text = dtTV.Rows[0][0].ToString().Trim() + " " + "NV";



            //Hien thi so luong Mat Hang

            string sqlDemMH = "SELECT COUNT(MaMH) FROM MATHANG";
            SqlDataAdapter dapDemMH = new SqlDataAdapter(sqlDemMH, kn.sqlCon);
            DataTable dtMH = new DataTable();
            dapDemMH.Fill(dtMH);
            //gắn dữ liệu vào lable mặt hàng
            lbMatHang.Text = dtMH.Rows[0][0].ToString().Trim() + " " + "HM";


            //Hien thi so luong Don Hang

            string sqlDemHD = "SELECT COUNT(MaHD) FROM HOADON";
            SqlDataAdapter dapDemHD = new SqlDataAdapter(sqlDemHD, kn.sqlCon);
            DataTable dtHD = new DataTable();
            dapDemHD.Fill(dtHD);
            //gắn dữ liệu vào lable đơn hàng
            lbHoaDon.Text = dtHD.Rows[0][0].ToString().Trim() + " " + "Đơn";


            //Hien Thi so luong Doanh Thu

            string sqlDemDT = "SELECT FORMAT(SUM(DgBan *SlBan),'C0','vi-VN')FROM CTHOADON where TrangThai=N'Đã Thanh Toán'";
            SqlDataAdapter dapDemDT = new SqlDataAdapter(sqlDemDT, kn.sqlCon);
            DataTable dtDT = new DataTable();
            dapDemDT.Fill(dtDT);
            //gắn dữ liệu vào lable doanh thu
            lbDoanhThu.Text = dtDT.Rows[0][0].ToString().Trim();
           
            kn.DongKetNoi();
        }

        //hàm này cứ 1 phút(60s) thì load lại dữ liệu 1 lần
        private void timerTrangChu_Tick(object sender, EventArgs e)
        {
            //load xem nhanh
            HienThiXemNhanh();
            //load đơn gần nhất
            HienThiDonGan();
            timerTrangChu.Start();
        }
    }
}
