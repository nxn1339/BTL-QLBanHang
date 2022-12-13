using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class HoaDon : Form
    {
        KetNoiData kn = new KetNoiData();

        public HoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            
            this.reportViewer1.RefreshReport();
            kn.MoKetNoi();
            SqlCommand sqlCmd = new SqlCommand("Select CTHOADON.MaHD,CTHOADON.DgBan,CTHOADON.SlBan,CTHOADON.ThanhTien,MATHANG.TenMH,TenKH,DiaChi,SDT,NgayLap= FORMAT(Ngaylap,'dd/MM/yyyy') from CTHOADON,MATHANG,KHACHHANG,HOADON where CTHOADON.MaMH= MATHANG.MaMH and KHACHHANG.MaKH =HOADON.MaKH and HOADON.MaHD=CTHOADON.MaHD and CTHOADON.MaHD='" + UCHoaDon.maHD+"'", kn.sqlCon);         
            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            dap.Fill(dt);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rpdt = new ReportDataSource("DataSet1", dt);     
            reportViewer1.LocalReport.ReportPath = "D:\\C#\\QLBanHang\\QLBanHang\\HoaDon.rdlc";
            reportViewer1.LocalReport.DataSources.Add(rpdt);           
            reportViewer1.RefreshReport();
            kn.DongKetNoi();
        }
    }
}
