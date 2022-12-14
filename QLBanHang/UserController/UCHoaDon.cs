using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class UCHoaDon : UserControl
    {
        KetNoiData kn = new KetNoiData();
        //tạo biến chức năng để đổi qua lại giữa Thêm và Sửa
        private string chucNang = "";
        //tạo biến trạng thái của CTHoaDon
        private string trangThai = "";
        //tạo biến mã HĐ (lấy mã HĐ in hóa đơn)
        public static string maHD = null;

        public UCHoaDon()
        {
            InitializeComponent();
        }

        private void UCHoaDon_Load(object sender, EventArgs e)
        {
            //Mặc định Enabled các group box không cho thao tác khi chưa chọn chức năng
            gbNhapHD.Enabled = false;
            gbNhapCTHD.Enabled = false;
            gbNhapKH.Enabled = false;
            txtThanhTien.Enabled = false;
            txtDgBan.Enabled = false;

            kn.MoKetNoi();
            //Gọi đến các hàm hiển thị và load
            LoadHoaDon("HienThiHoaDon");
            LoadCTHoaDon("HienThiCTHoaDon");
            LoadKhachHang("HienThiKH");
            LoadDuLieuCBMaKH();
            LoadDuLieuCB_MaHD();
            LoadDuLieuCB_MaMH();
            kn.DongKetNoi();
        }

        //======================================Hóa Đơn=====================================================
        private void LoadHoaDon(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = kn.sqlCon;

            DataSet ds = new DataSet();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(ds);
            //Cho dữ liệu hóa đơn ra datagirdView
            dgvHoaDon.DataSource = ds.Tables[0];
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bắt lỗi nếu bấm ra ngoài thì cũng không bị đóng chương trình
            try
            {
                //Click vào dòng nào ở datagirdView hóa đơn thì cho dữa liệu vào các text box tương ứng
                dgvHoaDon.CurrentRow.Selected = true;
                txtMaHD.Text = dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].FormattedValue.ToString();
                dtpNgayLap.Text = dgvHoaDon.Rows[e.RowIndex].Cells["NgayLap"].FormattedValue.ToString();
                cbMaKH.Text = dgvHoaDon.Rows[e.RowIndex].Cells["MaKH"].FormattedValue.ToString();
                maHD = dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].FormattedValue.ToString();
            }
            catch
            {

            }
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            //mở group box Hóa Đơn và mở textbox mã HĐ
            gbNhapHD.Enabled = true;
            txtMaHD.Enabled = true;
            //Chuyên chức năng sang thêm hóa đơn
            chucNang = "addHD";
        }

        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            //mở group box Hóa Đơn và đóng textbox mã HD (không sửa Mã HĐ)
            gbNhapHD.Enabled = true;
            txtMaHD.Enabled = false;
            //Chuyên chức năng sang sửa hóa đơn
            chucNang = "updateHD";
        }

        private void btnLoadHD_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Load lại mã khách hàng nếu có khách hàng mới mà trên combobox MaKH chưa có
            LoadDuLieuCBMaKH();
            kn.DongKetNoi();
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu chọn Yes
            if (MessageBox.Show("Bạn Có Muốn Xóa " + txtMaHD.Text, "Xóa Hóa Đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                XoaHD("XoaHD"); // Xóa Hóa Đơn
                LoadHoaDon("HienThiHoaDon"); // Hiển thị lại danh sách
                MessageBox.Show("Xóa Thành Công !"); //Báo thành công
            }
            kn.DongKetNoi();
        }

        //Sự kiến khi bấm nút lưu
        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang == "addHD") //Nếu đang ở chức năng thêm hóa đơn thì
            {
                try
                {
                    //Thêm hóa đơn
                    HoaDon("ThemHD");
                    //Load lại datagirdView
                    LoadHoaDon("HienThiHoaDon");
                    //khóa groupbox nhập lại
                    gbNhapHD.Enabled = false;
                    //báo thành công
                    MessageBox.Show("Thêm Hóa Đơn Thành Công !");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Thêm Hóa Đơn Không Thành Công !");
                }

            }
            else if (chucNang == "updateHD") //Nếu đang ở chức năng sửa hóa đơn thì
            {
                try
                {
                    //sửa hóa đơn
                    HoaDon("SuaHD");
                    //Load lại datagirdView
                    LoadHoaDon("HienThiHoaDon");
                    //khóa groupbox nhập lại
                    gbNhapHD.Enabled = false;
                    //báo thành công
                    MessageBox.Show("Sửa Hóa Đơn Thành Công !");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Sửa Hóa Đơn Không Thành Công !");
                }
            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !");
            }
            kn.DongKetNoi();
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            //Click hủy thì khóa groupbox nhập lại
            gbNhapHD.Enabled = false;
        }

        private void XoaHD(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaHD = new SqlParameter("@MaHD", SqlDbType.Char);
            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaHD.Value = txtMaHD.Text.Trim();
            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaHD);
            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            int kp = sqlCmd.ExecuteNonQuery();
            if (kp > 0)
            {
                //báo thành công
                MessageBox.Show("Xóa Phiếu Nhập Thành Công !");
            }
            else
            {
                //báo lỗi
                MessageBox.Show("Lỗi Xóa Phiếu Nhập Không Thành Công !");
            }
        }

        private void HoaDon(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaHD = new SqlParameter("@MaHD", SqlDbType.Char);
            SqlParameter parNgayLap = new SqlParameter("@NgayLap", SqlDbType.DateTime);
            SqlParameter parMaNV = new SqlParameter("@MaNV", SqlDbType.Char);
            SqlParameter parMaKH = new SqlParameter("@MaKH", SqlDbType.Char);

            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaHD.Value = txtMaHD.Text.Trim();
            parNgayLap.Value = dtpNgayLap.Text.Trim();
            parMaNV.Value = MD_NhanVien.maNV;
            parMaKH.Value = cbMaKH.Text.Trim();


            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaHD);
            sqlCmd.Parameters.Add(parNgayLap);
            sqlCmd.Parameters.Add(parMaNV);
            sqlCmd.Parameters.Add(parMaKH);


            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            sqlCmd.ExecuteNonQuery();
        }

        //Hàm tìm kiếm của hóa đơn
        private void btnTKHoaDon_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM HOADON";
            string dk = "";
            //Mã HĐ Khác rỗng
            if (txtTKMaHD.Text.Trim() != "")
            {
                dk += " MaHD like '%" + txtTKMaHD.Text + "%'";
            }
            //Mã KH khác rỗng và Mã HĐ khác rỗng
            if (txtTKMaKH.Text.Trim() != "" && dk != "")
            {
                dk += " AND MaKH like N'%" + txtTKMaKH.Text + "%'";
            }
            //Mã KH khác rỗng, Mã HĐ rỗng
            if (txtTKMaKH.Text.Trim() != "" && dk == "")
            {
                dk += " MaKH like N'%" + txtTKMaKH.Text + "%'";
            }
            //Nối các điều kiện
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTK_HD(sqlTimKiem);
        }

        private void LoadDuLieuTK_HD(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvHoaDon.DataSource = ds.Tables[0];
        }
        private void LoadDuLieuCBMaKH()
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Select MaKH from KHACHHANG";
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Connection = kn.sqlCon;
            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                //Xóa các dữ liệu cũ
                cbMaKH.Items.Remove(dr[0].ToString());
                //Thêm dữ liệu mới
                cbMaKH.Items.Add(dr[0].ToString());

            }
            dr.Close();

        }
        //=====================================End Hóa Đơn=================================================

        //=====================================CT Hóa Đơn=================================================

        private void LoadCTHoaDon(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = kn.sqlCon;

            DataSet ds = new DataSet();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(ds);
            //Gắn dữ liệu vào datagirdView
            dgvCTHoaDon.DataSource = ds.Tables[0];
        }

        private void dgvCTHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Click vào dòng nào ở datagirdView ct hóa đơn thì cho dữa liệu vào các text box tương ứng
                dgvCTHoaDon.CurrentRow.Selected = true;
                cbMaHD.Text = dgvCTHoaDon.Rows[e.RowIndex].Cells["MaHD"].FormattedValue.ToString();
                cbMaMH.Text = dgvCTHoaDon.Rows[e.RowIndex].Cells["MaMH"].FormattedValue.ToString();
                txtDgBan.Text = dgvCTHoaDon.Rows[e.RowIndex].Cells["DgBan"].FormattedValue.ToString();
                txtSlBan.Text = dgvCTHoaDon.Rows[e.RowIndex].Cells["SlBan"].FormattedValue.ToString();
                txtThanhTien.Text = dgvCTHoaDon.Rows[e.RowIndex].Cells["ThanhTien"].FormattedValue.ToString();
                trangThai = dgvCTHoaDon.Rows[e.RowIndex].Cells["TrangThai"].FormattedValue.ToString();

                if (trangThai == "Đã Thanh Toán") //Nêu trạng thái là đã thanh toán thì
                {
                    //Chuyển trạng thái của radioButton
                    rdbDaTT.Checked = true; // đã thanh toán = true
                    rdbChuaTT.Checked = false; //chưa thanh toán = false
                }
                else
                {
                    //Chuyển trạng thái của radioButton
                    rdbChuaTT.Checked = true;// đã thanh toán = false
                    rdbDaTT.Checked = false;//chưa thanh toán = true
                }
            }
            catch
            {

            }
        }

        private void btnLuuCTHD_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang == "addCTHD")//Nếu đang ở chức năng thêm chi tiết hóa đơn thì
            {
                try
                {
                    //Thêm chi tiết hóa đơn
                    CTHoaDon("ThemCTHoaDon");
                    //khóa groupbox nhập lại
                    gbNhapCTHD.Enabled = false;
                    //Load lại dữ liệu trên datagirdView
                    LoadCTHoaDon("HienThiCTHoaDon");
                    //Báo thành công
                    MessageBox.Show("Thêm CT Hóa Đơn Thành Công !");

                }
                catch
                {
                    //Báo lỗi
                    MessageBox.Show("Lỗi Thêm CT Hóa Đơn Không Thành Công !");

                }

            }
            else if (chucNang == "updateCTHD") //Nếu đang ở chức năng sửa chi tiết hóa đơn thì
            {
                //Sửa chi tiết hóa đơn
                CTHoaDon("SuaCTHoaDon");
                //khóa groupbox nhập lại
                gbNhapCTHD.Enabled = false;
                //Load lại dữ liệu ở datagirdView
                LoadCTHoaDon("HienThiCTHoaDon");
                //báo thành công
                MessageBox.Show("Sửa CT Hóa Đơn Thành Công !");
            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !");
            }
            kn.DongKetNoi();
        }

        private void btnHuyCTHD_Click(object sender, EventArgs e)
        {
            //Click nút hủy khóa groupbox nhập lại
            gbNhapCTHD.Enabled = false;
        }

        private void btnThemCTHD_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập
            gbNhapCTHD.Enabled = true;
            //reset lại thành tiền
            txtThanhTien.Text = "0";
            //đổi trạng thái chức năng sang thêm chi tiết hóa đơn
            chucNang = "addCTHD";
            //mở combobox của maxHD và mãMH
            cbMaHD.Enabled = true;
            cbMaMH.Enabled = true;
        }

        private void btnSuaCTHD_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập
            gbNhapCTHD.Enabled = true;
            //đổi trạng thái chức năng sang sửa chi tiết hóa đơn
            chucNang = "updateCTHD";
            //đóng combobox của maxHD và mãMH (không cho sửa mã hóa đơn , mã mặt hàng)
            cbMaHD.Enabled = false;
            cbMaMH.Enabled = false;
        }

        private void btnLamMoiCTHD_Click(object sender, EventArgs e)
        {
            //Load lại comboBox của mã HD và MH khi có cập nhật mới (dữ liệu mới mà chưa có ở combobox)
            LoadDuLieuCB_MaHD();
            LoadDuLieuCB_MaMH();
        }

        private void btnXoaCTHD_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu chọn yes thì
            if (MessageBox.Show("Bạn Có Muốn Xóa CTHD Có Mã MH là " + cbMaMH.Text, "Xóa CTHD", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xóa chi tiết hóa đơn
                XoaCTHD("XoaCTHD");
                //load lại dữ liệu 
                LoadCTHoaDon("HienThiCTHoaDon");
            }
            kn.DongKetNoi();
        }

        private void CTHoaDon(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaHD = new SqlParameter("@MaHD", SqlDbType.Char);
            SqlParameter parMaMH = new SqlParameter("@MaMH", SqlDbType.Char);
            SqlParameter parDgBan = new SqlParameter("@DgBan", SqlDbType.Money);
            SqlParameter parSlBan = new SqlParameter("@SlBan", SqlDbType.Int);
            SqlParameter parTrangThai = new SqlParameter("@TrangThai", SqlDbType.NVarChar);

            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaHD.Value = cbMaHD.Text.Trim();
            parMaMH.Value = cbMaMH.Text.Trim();
            parDgBan.Value = txtDgBan.Text.Trim();
            parSlBan.Value = txtSlBan.Text.Trim();
            if (rdbDaTT.Checked == true)
            {
                //lấy dữ liệu khi chọn radioButton
                trangThai = "Đã Thanh Toán";
            }
            else
            {
                //lấy dữ liệu khi chọn radioButton
                trangThai = "Chưa Thanh Toán";
            }
            parTrangThai.Value = trangThai;

            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaHD);
            sqlCmd.Parameters.Add(parMaMH);
            sqlCmd.Parameters.Add(parDgBan);
            sqlCmd.Parameters.Add(parSlBan);
            sqlCmd.Parameters.Add(parTrangThai);


            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            sqlCmd.ExecuteNonQuery();
        }

        private void LoadDuLieuCB_MaMH()
        {
            kn.MoKetNoi();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Select MaMH from MATHANG";
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Connection = kn.sqlCon;
            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                //Xóa dữ liệu cũ
                cbMaMH.Items.Remove(dr[0].ToString());
                //Thêm dữ liệu mới
                cbMaMH.Items.Add(dr[0].ToString());

            }
            dr.Close();
            kn.DongKetNoi();
        }
        private void LoadDuLieuCB_MaHD()
        {
            kn.MoKetNoi();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Select MaHD from HOADON";
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Connection = kn.sqlCon;
            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                //Xóa dữ liệu cũ
                cbMaHD.Items.Remove(dr[0].ToString());
                //Thêm dữ liệu mới
                cbMaHD.Items.Add(dr[0].ToString());

            }
            dr.Close();
            kn.DongKetNoi();
        }

        //Hàm này hiển thị thành tiền khi chọn mã MH 
        private void cbMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Select DgBan=FORMAT(DgBan,'#,##0') From MATHANG Where MaMH = '" + cbMaMH.Text.Trim() + "'";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = kn.sqlCon;
            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            txtDgBan.Text = dt.Rows[0][0].ToString();
            kn.DongKetNoi();

            try
            {
                //ép kiểu tính thành tiền
                float f = float.Parse(txtDgBan.Text) * float.Parse(txtSlBan.Text);
                //đưa ra textBox 
                txtThanhTien.Text = f.ToString("#,##0");
            }
            catch
            {
                //nếu mã mặt hàng không có trong mặt hàng hoặc rỗng
                txtThanhTien.Text = "0";
            }
        }

        //Hàm này khi nhập số lượng, số lượng thay đổi sẽ hiển thị thành tiền thay đổi theo
        private void txtSlBan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //ép kiểu tính thành tiền
                float f = float.Parse(txtDgBan.Text) * float.Parse(txtSlBan.Text);
                //đưa ra textBox
                txtThanhTien.Text = f.ToString("#,##0");
            }
            catch
            {
                //nếu số lượng bằng 0 hoặc rỗng
                txtThanhTien.Text = "0";
            }

        }

        private void XoaCTHD(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaHD = new SqlParameter("@MaHD", SqlDbType.Char);
            SqlParameter parMaMH = new SqlParameter("@MaMH", SqlDbType.Char);
            SqlParameter parSlBan = new SqlParameter("@SlBan", SqlDbType.Int);
            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaHD.Value = cbMaHD.Text.Trim();
            parMaMH.Value = cbMaMH.Text.Trim();
            parSlBan.Value = txtSlBan.Text.Trim();
            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaHD);
            sqlCmd.Parameters.Add(parMaMH);
            sqlCmd.Parameters.Add(parSlBan);
            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            int kp = sqlCmd.ExecuteNonQuery();
            if (kp > 0)
            {
                //báo thành công
                MessageBox.Show("Xóa Chi Tiết Hóa Đơn Thành Công !");
            }
            else
            {
                //báo lỗi
                MessageBox.Show("Lỗi Xóa Chi Tiết Hóa Đơn Không Thành Công !");
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            //mở form in hóa đơn
            Form f = new HoaDon();
            f.Show();
        }

        //Hàm tìm kiếm của CTHD
        private void btnTKCTHD_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "select MaHD,MaMH,DgBan=FORMAT(DgBan,'#,##0'),SlBan,ThanhTien=FORMAT(ThanhTien,'#,##0'),TrangThai from CTHOADON";
            string dk = "";
            //Mã HĐ Khác rỗng
            if (txtTKMaHD_CT.Text.Trim() != "")
            {
                dk += " MaHD like '%" + txtTKMaHD_CT.Text + "%'";
            }
            //Mã MH khác rỗng và Mã HĐ khác rỗng
            if (txtTKMaMH_CT.Text.Trim() != "" && dk != "")
            {
                dk += " AND MaMH like N'%" + txtTKMaMH_CT.Text + "%'";
            }
            //Mã MH khác rỗng, Mã HĐ rỗng
            if (txtTKMaMH_CT.Text.Trim() != "" && dk == "")
            {
                dk += " MaMH like N'%" + txtTKMaMH_CT.Text + "%'";
            }
            //Nối các điều kiện
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTK_CTHD(sqlTimKiem);
        }
        private void LoadDuLieuTK_CTHD(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvCTHoaDon.DataSource = ds.Tables[0];
        }

        //==================================End CTHoaDon=============================

        //==================================Khách Hàng================================

        private void LoadKhachHang(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = kn.sqlCon;

            DataSet ds = new DataSet();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(ds);
            //Gắn dữ liệu vào datagirdView
            dgvKhachHang.DataSource = ds.Tables[0];
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bắt lỗi nếu bấm ra ngoài thì cũng không bị đóng chương trình
            try
            {
                //Click vào dòng nào ở datagirdView hóa đơn thì cho dữa liệu vào các text box tương ứng
                dgvKhachHang.CurrentRow.Selected = true;
                txtMaKH.Text = dgvKhachHang.Rows[e.RowIndex].Cells["MaKH"].FormattedValue.ToString();
                txtTenKH.Text = dgvKhachHang.Rows[e.RowIndex].Cells["TenKH"].FormattedValue.ToString();
                txtDiaChi.Text = dgvKhachHang.Rows[e.RowIndex].Cells["DiaChi"].FormattedValue.ToString();
                txtSDT.Text = dgvKhachHang.Rows[e.RowIndex].Cells["SDT"].FormattedValue.ToString();
            }
            catch
            {

            }
        }

        private void KhachHang(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaKH = new SqlParameter("@MaKH", SqlDbType.Char);
            SqlParameter parTenKH = new SqlParameter("@TenKH", SqlDbType.NVarChar);
            SqlParameter parDiaChi = new SqlParameter("@DiaChi", SqlDbType.NVarChar);
            SqlParameter parSDT = new SqlParameter("@SDT", SqlDbType.VarChar);

            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaKH.Value = txtMaKH.Text.Trim();
            parTenKH.Value = txtTenKH.Text.Trim();
            parDiaChi.Value = txtDiaChi.Text.Trim();
            parSDT.Value = txtSDT.Text.Trim();


            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaKH);
            sqlCmd.Parameters.Add(parTenKH);
            sqlCmd.Parameters.Add(parDiaChi);
            sqlCmd.Parameters.Add(parSDT);


            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            sqlCmd.ExecuteNonQuery();
        }

        private void XoaKhachHang(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaKH = new SqlParameter("@MaKH", SqlDbType.Char);

            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaKH.Value = txtMaKH.Text.Trim();

            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaKH);

            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            int kp = sqlCmd.ExecuteNonQuery();
            if (kp > 0)
            {
                //báo thành công
                MessageBox.Show("Xóa Khách Hàng Thành Công !");
            }
            else
            {
                //báo lỗi
                MessageBox.Show("Lỗi Xóa Khách Hàng Không Thành Công !");
            }
        }
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            //mở groupBox nhập
            gbNhapKH.Enabled = true;
            //đổi trạng thái chức năng sang thêm khách hàng
            chucNang = "addKH";
            //mở textBox mã khách hàng
            txtMaKH.Enabled = true;
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            //mở groupBox nhập
            gbNhapKH.Enabled = true;
            //đổi trạng thái chức năng sang thêm khách hàng
            chucNang = "updateKH";
            //đóng textBox mã khách hàng không cho sửa mã khách hàng
            txtMaKH.Enabled = false;
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            LoadKhachHang("HienThiKH");
            kn.DongKetNoi();
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu chọn yes thì
            if (MessageBox.Show("Bạn Có Muốn Xóa Khách Hàng Có Mã KH là " + txtMaKH.Text, "Xóa Khách Hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xóa khách hàng
                XoaKhachHang("XoaKH");
                //load lại dữ liệu 
                LoadKhachHang("HienThiKH");
            }
            kn.DongKetNoi();
        }

        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang == "addKH") // nếu là chức năng thêm khách hàng thì
            {
                try
                {
                    //Thêm khách hàng
                    KhachHang("ThemKH");
                    //đóng nhập
                    gbNhapKH.Enabled = false;
                    //báo thành công
                    MessageBox.Show("Thêm Thành Công");
                    //Load lại dữ liệu
                    LoadKhachHang("HienThiKH");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Thêm Không Thành Công");
                }
            }
            else if (chucNang == "updateKH")
            {
                try
                {
                    //Sửa khách hàng
                    KhachHang("SuaKH");
                    //Load lại dữ liệu
                    LoadKhachHang("HienThiKH");
                    //đóng nhập
                    gbNhapKH.Enabled = false;
                    //báo thành công
                    MessageBox.Show("Sửa Thành Công");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Sửa Không Thành Công");
                }
            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !");
            }
            kn.DongKetNoi();
        }

        private void btnHuyKH_Click(object sender, EventArgs e)
        {
            //đóng nhập
            gbNhapKH.Enabled = false;
        }

        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "select * from KHACHHANG";
            string dk = "";
            //Mã KH Khác rỗng
            if (txtTK_MaKH.Text.Trim() != "")
            {
                dk += " MaKH like '%" + txtTK_MaKH.Text + "%'";
            }
            //Tên KH khác rỗng và Mã KH khác rỗng
            if (txtTK_TenKH.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenKH like N'%" + txtTK_TenKH.Text + "%'";
            }
            //Tên KH khác rỗng, Mã KH rỗng
            if (txtTK_TenKH.Text.Trim() != "" && dk == "")
            {
                dk += " TenKH like N'%" + txtTK_TenKH.Text + "%'";
            }
            //Nối các điều kiện
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTK_KH(sqlTimKiem);
        }

        private void LoadDuLieuTK_KH(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvKhachHang.DataSource = ds.Tables[0];
        }
    }
}
