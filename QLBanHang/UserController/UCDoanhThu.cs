using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class UCDoanhThu : UserControl
    {
        KetNoiData kn = new KetNoiData();
        //Tạo list từ mode MD_ThongKeDT_NV
        List<MD_ThongKeDT_NV> list = new List<MD_ThongKeDT_NV>();
        //Khỏi tạo biến ban đầu cho mục đích tính max, min
        double max = 0;
        int vtCao = 0;
        double min = double.PositiveInfinity;
        int vtThap = 0;
        public UCDoanhThu()
        {
            InitializeComponent();
        }

        private void UCDoanhThu_Load(object sender, EventArgs e)
        {
            Axis XA = chartDoanhThu.ChartAreas[0].AxisX;
            XA.Interval = 1; // mỗi tháng 1 ô

            Axis XA1 = chartNhanVien.ChartAreas[0].AxisX;
            XA1.Interval = 1; // mỗi tháng 1 ô

            //Gọi hàm load dữ liệu vào chart
            kn.MoKetNoi();
            DoanhThu("DoanhThu");
            DTNhanVien("DTNhanVien");
            kn.DongKetNoi();

        }

        private void TimMaxDT(double[] arr)
        {
            //Tìm Max Doanh Thu
            for (int i = 1; i < arr.Length; i++)
            {
                if (max < arr[i])
                {
                    max = arr[i];
                    vtCao = i;
                }

            }
        }

        private void TimMinDT(double[] arr)
        {
            //Tìm Min Doanh Thu
            for (int i = 1; i < arr.Length; i++)
            {
                if (min > arr[i])
                {
                    min = arr[i];
                    vtThap = i;
                }
            }        
        }

        private void DoanhThu(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = kn.sqlCon;
            //Truyền năm vào để đưa ra doanh thu
            SqlParameter parNam = new SqlParameter("@Nam", SqlDbType.Char);
            parNam.Value = dtpNam.Text.Trim();
            sqlCmd.Parameters.Add(parNam);
            //Tạo dataset
            DataSet ds = new DataSet();
            //thưc hiện sql
            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            //cho dữ liệu vào dataset
            dap.Fill(ds);
            //tạo mảng 1 chiều chứa dữ liệu doanh thu
            double[] dt = new double[ds.Tables.Count + 1];
            for (int i = 1; i < ds.Tables.Count + 1; i++)
            {
                try
                {
                    //Lấy dữ liệu doanh thu cho vào mảng 
                    dt[i] = float.Parse(ds.Tables[i - 1].Rows[0][0].ToString());
                }
                catch
                {

                }
            }
            //Gọi Hàm Tìm Max Min Của Doanh Thu
            TimMaxDT(dt);
            TimMinDT(dt);

            //Gắn Vào Lable Cao
            lbThangCao.Text = vtCao.ToString();
            lbDoanhThuCao.Text = max.ToString("#,##0" + " đ");

            lbThangThap.Text = vtThap.ToString();
            lbDoanhThuThap.Text = min.ToString("#,##0" + " đ");
            //Cho Dữ Liệu Vào Biểu Đồ
            chartDoanhThu.Series["VNĐ"].Points.Clear();
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T1", dt[1]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T2", dt[2]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T3", dt[3]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T4", dt[4]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T5", dt[5]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T6", dt[6]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T7", dt[7]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T8", dt[8]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T9", dt[9]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T10", dt[10]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T11", dt[11]);
            chartDoanhThu.Series["VNĐ"].Points.AddXY("T12", dt[12]);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                //Hiện tiền trên mỗi cột
                chartDoanhThu.Series["VNĐ"].Points[i].Label = dt[i + 1].ToString("#,##0" + " đ");
            }
        }

        private void DTNhanVien(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = kn.sqlCon;

            DataTable dt = new DataTable();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(dt);
            //clear list để khi bấm làm mới (gọi lại hàm) không bị trùng dữ liệu
            list.Clear();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Thêm Mã NV và Tổng Doanh Thu Của Nhân Viên Vào List
                    list.Add(new MD_ThongKeDT_NV() { MaNV = dt.Rows[i][0].ToString(), TongDT_NV = float.Parse(dt.Rows[i][1].ToString()) });
                }
                //Xóa Cột Biểu Đồ (Làm Mới)
                chartNhanVien.Series["VNĐ"].Points.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Thêm Cột Biểu Đồ
                    chartNhanVien.Series["VNĐ"].Points.AddXY(list[i].MaNV, list[i].TongDT_NV);
                }
                //Gắn Mã NV vào Các Lable Top Doanh Thu
                lbMaNVTop1.Text = dt.Rows[0][0].ToString();
                lbMaNVTop2.Text = dt.Rows[1][0].ToString();
                lbMaNVTop3.Text = dt.Rows[2][0].ToString();
                //Tạo Các Biến để chứa dữ liệu 
                float dtTop1 = float.Parse(dt.Rows[0][1].ToString());   //ép kiểu từ char ra float
                float dtTop2 = float.Parse(dt.Rows[1][1].ToString());   //ép kiểu từ char ra float
                float dtTop3 = float.Parse(dt.Rows[2][1].ToString());   //ép kiểu từ char ra float
                //Tạo thành from tiền VNĐ từ biến dữ liệu trên rồi đưa vào lable
                lbDoanhThuTop1.Text = dtTop1.ToString("#,##0" + " đ");
                lbDoanhThuTop2.Text = dtTop2.ToString("#,##0" + " đ");
                lbDoanhThuTop3.Text = dtTop3.ToString("#,##0" + " đ");
                for (int i = 0; i < 3; i++)
                {
                    //Cho mã nhân viên vào hàm HienTopNhanVienDT để thêm tên và ảnh nhân viên vào list
                    HienTopNhanVienDT("HienTopNhanVienDT", dt.Rows[i][0].ToString());
                }
                //Gắn tên từ list vào lable  
                //dt.Rows.Count là số của list( mã nv,tongDT_NV ) nhân viên tiếp ,do thêm list ở hai lần
                lbTenTop1.Text = list[dt.Rows.Count].TenNV;
                lbTenTop2.Text = list[dt.Rows.Count + 1].TenNV;
                lbTenTop3.Text = list[dt.Rows.Count + 2].TenNV;

                //Cho ảnh từ list vào pictureBox
                ptbTop1.Image = new Bitmap(list[dt.Rows.Count].FileAnh);
                ptbTop1.SizeMode = PictureBoxSizeMode.StretchImage;
                //Cho ảnh từ list vào pictureBox
                ptbTop2.Image = new Bitmap(list[dt.Rows.Count + 1].FileAnh);
                ptbTop2.SizeMode = PictureBoxSizeMode.StretchImage;
                //Cho ảnh từ list vào pictureBox
                ptbTop3.Image = new Bitmap(list[dt.Rows.Count + 2].FileAnh);
                ptbTop3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {

            }
            
        }

        private void HienTopNhanVienDT(string sql, string MaNV)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaNV = new SqlParameter("@MaNV", SqlDbType.Char);

            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaNV.Value = MaNV;

            //Thêm Parameter vào Comman       
            sqlCmd.Parameters.Add(parMaNV);

            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            dap.Fill(dt);

            try
            {
                //Thêm Tên và file ảnh vào list
                list.Add(new MD_ThongKeDT_NV() { TenNV = dt.Rows[0][0].ToString(), FileAnh = dt.Rows[0][1].ToString() });
            }
            catch
            {
                //Thêm Tên và file ảnh vào list
                list.Add(new MD_ThongKeDT_NV() { TenNV = "", FileAnh = "" });
            }
                            
        }

        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            //Reset lại giá trị để tìm max, min khi đổi năm
            max = 0;
            vtCao = 0;
            min = double.PositiveInfinity;
            vtThap = 0;

            kn.MoKetNoi();
            DoanhThu("DoanhThu");
            kn.DongKetNoi();
        }

        private void btnLamMoiDT_NV_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            DTNhanVien("DTNhanVien");
            kn.DongKetNoi();
        }
    }
}
