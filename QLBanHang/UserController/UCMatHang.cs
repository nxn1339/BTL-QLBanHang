using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class UCMatHang : UserControl
    {
        //kết nối database
        KetNoiData kn = new KetNoiData();
        //mảng chứa item mặt hàng
        UCItemMH[] listItem = new UCItemMH[100];
        //biến chứa độ dài số lượng item
        private int n = 0;
        private int i = 0;
      
        private string chucNang = "";
        public UCMatHang()
        {
            InitializeComponent();
        }


        private void UCMatHang_Load(object sender, EventArgs e)
        {
            //khóa groupBox nhập của mặt hàng và phiếu nhập
            gbNhapMH.Enabled = false;
            gbNhapPN.Enabled = false;
            kn.MoKetNoi();
            //Hiển thị mặt hàng và phiếu nhập
            LoadMatHang("HienThiMatHang");
            LoadPhieuNhap("HienThiPN");
            //load dữ liệu comboBox mã mặt hàng ở phiếu nhập
            LoadDuLieuCBMaMH();
            //Hiển thị thống kê
            HienThiThongKe_MH("HienThiItemMH");
            kn.DongKetNoi();
        }

        //====================================Mặt Hàng=============================
        private void LoadMatHang(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = kn.sqlCon;

            DataSet ds = new DataSet();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(ds);
            //Gắn dữ liệu vào datagirdView
            dgvMatHang.DataSource = ds.Tables[0];
        }


        private void dgvMatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Click vào dòng nào ở datagirdView mặt hàng thì cho dữa liệu vào các text box tương ứng
                dgvMatHang.CurrentRow.Selected = true;
                txtMaMH.Text = dgvMatHang.Rows[e.RowIndex].Cells["MaMH"].FormattedValue.ToString();
                txtTenMH.Text = dgvMatHang.Rows[e.RowIndex].Cells["TenMH"].FormattedValue.ToString();
                txtDvTinh.Text = dgvMatHang.Rows[e.RowIndex].Cells["DvTinh"].FormattedValue.ToString();
                txtDgBan.Text = dgvMatHang.Rows[e.RowIndex].Cells["DgBan"].FormattedValue.ToString();
                txtSlCon.Text = dgvMatHang.Rows[e.RowIndex].Cells["SlCon"].FormattedValue.ToString();
                dtpNgayCapNhat.Text = dgvMatHang.Rows[e.RowIndex].Cells["NgayCapNhat"].FormattedValue.ToString();
                MD_MatHang.FileAnh = dgvMatHang.Rows[e.RowIndex].Cells["FileAnh"].FormattedValue.ToString();


                //Mở ảnh cho vào pictureBox
                if (MD_MatHang.FileAnh != "")
                {
                    //Mở ảnh cho vào pictureBox
                    ptbMatHang.Image = new Bitmap(MD_MatHang.FileAnh);
                    ptbMatHang.SizeMode = PictureBoxSizeMode.StretchImage;

                }
                else
                {
                    //Không Có Ảnh
                    ptbMatHang.Image = ptbMatHang.ErrorImage;
                }
            }
            catch
            {

            }

        }
        private void ptbMatHang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Mở File ảnh
            OpenFileDialog openfd = new OpenFileDialog();
            //Tìm File ảnh
            openfd.Filter = "Image File(*.jpg;*.jpeg;*.png;*.gif;*.tiff;*.bmp;) | *.jpg;*.jpeg;*.png;*.gif;*.tiff;*.bmp;";
            if (openfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    //Hiển Thì Ảnh
                    MD_MatHang.FileAnh = openfd.FileName;
                    ptbMatHang.Image = new Bitmap(openfd.FileName);
                    ptbMatHang.ImageLocation = openfd.FileName;
                    ptbMatHang.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch
                {
                    //báo lỗi mở ảnh
                    MessageBox.Show("Mở Ảnh Không Thành Công !");
                }
            }
        }

        private void btnThemMH_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập dữ liệu
            gbNhapMH.Enabled = true;
            //đổi trạng thái chức năng sang thêm mặt hàng
            chucNang = "addMH";
            //mở textBox mã mặt hàng
            txtMaMH.Enabled = true;
        }

        private void btnSuaMH_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập dữ liệu
            gbNhapMH.Enabled = true;
            //đóng textBox mã mặt hàng không cho sửa mã mặt hàng
            txtMaMH.Enabled = false;
            //đổi trạng thái chức năng sang sửa mặt hàng
            chucNang = "updateMH";
        }

        private void btnLamMoiMH_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //làm mới
            txtMaMH.Text = "";
            txtTenMH.Text = "";
            txtDvTinh.Text = "";
            txtDgBan.Text = "";
            txtSlCon.Text = "";
            dtpNgayCapNhat.Text = "";
            MD_MatHang.FileAnh = "";
            ptbMatHang.Image = ptbMatHang.ErrorImage;
            //load lại dữ liệu
            LoadMatHang("HienThiMatHang");
            kn.DongKetNoi();
        }

        private void btnXoaMH_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu chọn yes thì
            if (MessageBox.Show("Bạn Có Muốn Xóa Mặt Hàng Có Mã " + txtMaMH.Text, "Xóa Mặt Hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xóa mặt hàng
                XoaMatHang("XoaMatHang");
                //hiển thị lại dữ liệu
                LoadMatHang("HienThiMatHang");
                //hiển thị lại thống kê
                HienThiThongKe_MH("HienThiItemMH");
            }
            kn.DongKetNoi();
        }

        private void btnLuuMH_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang == "addMH")//Nếu đang ở chức năng thêm mặt hàng thì
            {
                try
                {
                    //Thêm mặt hàng
                    MatHang("ThemMatHang");
                    //hiển thị lại danh sách
                    LoadMatHang("HienThiMatHang");
                    //đóng groupBox nhập
                    gbNhapMH.Enabled = false;
                    //báo thành công
                    MessageBox.Show("Thêm Thành Công !");
                    //hiển thị lại thống kê
                    HienThiThongKe_MH("HienThiItemMH");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Thêm Mặt Hàng Không Thành Công !");
                }

            }
            else if (chucNang == "updateMH")//Nếu đang ở chức năng sửa mặt hàng thì
            {
                try
                {
                    //sửa mặt hàng
                    MatHang("SuaMatHang");
                    //hiển thị lại dữ liệu
                    LoadMatHang("HienThiMatHang");
                    //đóng group nhập
                    gbNhapMH.Enabled = false;
                    //báo thành công
                    MessageBox.Show("Sửa Thành Công !");
                    //hiển thị lại thống kê
                    HienThiThongKe_MH("HienThiItemMH");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Sửa Mặt Hàng Không Thành Công !");
                }
            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !");
            }
            kn.DongKetNoi();
        }

        private void btnHuyMH_Click(object sender, EventArgs e)
        {
            //Click nút hủy đóng groupBox nhập
            gbNhapMH.Enabled = false;
        }

        private void MatHang(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaMH = new SqlParameter("@MaMH", SqlDbType.Char);
            SqlParameter parTenMH = new SqlParameter("@TenMH", SqlDbType.NVarChar);
            SqlParameter parDvTinh = new SqlParameter("@DvTinh", SqlDbType.NVarChar);
            SqlParameter parDgBan = new SqlParameter("@DgBan", SqlDbType.Money);
            SqlParameter parSlCon = new SqlParameter("@SlCon", SqlDbType.Int);
            SqlParameter parFileAnh = new SqlParameter("@FileAnh", SqlDbType.NVarChar);

            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaMH.Value = txtMaMH.Text.Trim();
            parTenMH.Value = txtTenMH.Text.Trim();
            parDvTinh.Value = txtDvTinh.Text.Trim();
            parDgBan.Value = txtDgBan.Text.Trim();
            parSlCon.Value = txtSlCon.Text.Trim();
            parFileAnh.Value = MD_MatHang.FileAnh;

            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaMH);
            sqlCmd.Parameters.Add(parTenMH);
            sqlCmd.Parameters.Add(parDvTinh);
            sqlCmd.Parameters.Add(parDgBan);
            sqlCmd.Parameters.Add(parSlCon);
            sqlCmd.Parameters.Add(parFileAnh);

            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            sqlCmd.ExecuteNonQuery();
        }

        private void XoaMatHang(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaMH = new SqlParameter("@MaMH", SqlDbType.Char);
            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaMH.Value = txtMaMH.Text.Trim();
            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaMH);
            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            int kp = sqlCmd.ExecuteNonQuery();
            if (kp > 0)
            {
                //báo thành công
                MessageBox.Show("Xóa Mặt Hàng Thành Công !");
            }
            else
            {
                //báo lỗi
                MessageBox.Show("Lỗi Xóa Mặt Hàng Không Thành Công !");
            }
        }

        private void btnTimKiemMH_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM MATHANG";
            string dk = "";
            //Mã MH Khác rỗng
            if (txtTKMaMH.Text.Trim() != "")
            {
                dk += " MaMH like '%" + txtTKMaMH.Text + "%'";
            }
            //Tên MH khác rỗng và Mã MH khác rỗng
            if (txtTKTenMH.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenMH like N'%" + txtTKTenMH.Text + "%'";
            }
            //Tên MH khác rỗng, Mã MH rỗng
            if (txtTKTenMH.Text.Trim() != "" && dk == "")
            {
                dk += " TenMH like N'%" + txtTKTenMH.Text + "%'";
            }
            //Nối các điều kiện
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTK_MH(sqlTimKiem);
        }
        private void LoadDuLieuTK_MH(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvMatHang.DataSource = ds.Tables[0];
        }
        //=============================End Mặt Hàng===========================================

        //=============================Phiếu Nhập==============================================
        private void LoadPhieuNhap(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = kn.sqlCon;

            DataSet ds = new DataSet();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(ds);

            dgvPhieuNhap.DataSource = ds.Tables[0];
        }

        private void btnLuuPN_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang == "addPN") //Nếu đang ở chức năng thêm phiếu nhập thì
            {
                try
                {
                    //thêm phiếu nhập
                    PhieuNhap("ThemPN");
                    //hiển thị lại danh sách
                    LoadPhieuNhap("HienThiPN");
                    //hiển thị lại mặt hàng ( cập nhật, công thêm số còn của mặt hàng khi nhập )
                    LoadMatHang("HienThiMatHang");
                    //đóng groupBox nhập
                    gbNhapPN.Enabled = false;
                    //báo thành công
                    MessageBox.Show(" Thêm Phiếu Nhập Thành Công !");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show(" Lỗi Thêm Phiếu Nhập Không Thành Công !");
                }

            }
            else if (chucNang == "updatePN")//Nếu đang ở chức năng sửa phiếu nhập thì
            {
                try
                {
                    //sửa phiếu nhập
                    PhieuNhap("SuaPN");
                    //hiển thị lại dữ liệu
                    LoadPhieuNhap("HienThiPN");
                    //hiển thị lại mặt hàng ( cập nhật, công thêm số còn của mặt hàng khi nhập )
                    LoadMatHang("HienThiMatHang");
                    //đóng groupBox nhập
                    gbNhapPN.Enabled = false;
                    //báo thành công
                    MessageBox.Show(" Sửa Phiếu Nhập Thành Công !");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show(" Lỗi Sửa Phiếu Nhập Không Thành Công !");
                }
            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !");
            }
            kn.DongKetNoi();
        }

        private void btnHuyPN_Click(object sender, EventArgs e)
        {
            //đóng groupbox nhập
            gbNhapPN.Enabled = false;
        }

        private void btnThemPN_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập
            gbNhapPN.Enabled = true;
            //đổi trạng thái chức năng sang thêm phiếu nhập
            chucNang = "addPN";
            //mở textbox số phiếu nhập
            txtSoPN.Enabled = true;

        }

        private void btnSuaPN_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập
            gbNhapPN.Enabled = true;
            //đổi trạng thái chức năng sang sửa phiếu nhập
            chucNang = "updatePN";
            //đóng textbox số phiếu nhập (không cho sửa số phiếu nhập)
            txtSoPN.Enabled = false;
        }

        private void btnLoadPN_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //load lại dữ liệu trong comboBox mã MH
            LoadDuLieuCBMaMH();
            kn.DongKetNoi();
        }

        private void btnXoaPN_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu chọn yes thì
            if (MessageBox.Show("Bạn Có Muốn Xóa " + txtSoPN.Text, "Xóa Phiếu Nhập", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xóa phiếu nhập
                XoaPN("XoaPN");
                //hiển thị lại danh sách
                LoadPhieuNhap("HienThiPN");
            }
            kn.DongKetNoi();
        }

        private void PhieuNhap(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parSoPN = new SqlParameter("@SoPN", SqlDbType.Char);
            SqlParameter parNgayNhap = new SqlParameter("@NgayNhap", SqlDbType.DateTime);
            SqlParameter parDgNhap = new SqlParameter("@DgNhap", SqlDbType.Money);
            SqlParameter parSlNhap = new SqlParameter("@SlNhap", SqlDbType.Int);
            SqlParameter parMaNV = new SqlParameter("@MaNV", SqlDbType.Char);
            SqlParameter parMaMH = new SqlParameter("@MaMH", SqlDbType.Char);

            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parSoPN.Value = txtSoPN.Text.Trim();
            parNgayNhap.Value = dtpNgayNhap.Text.Trim();
            parDgNhap.Value = txtDgNhap.Text.Trim();
            parSlNhap.Value = txtSlNhap.Text.Trim();
            parMaNV.Value = MD_NhanVien.maNV;
            parMaMH.Value = cbMaMH.Text.Trim();


            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parSoPN);
            sqlCmd.Parameters.Add(parNgayNhap);
            sqlCmd.Parameters.Add(parDgNhap);
            sqlCmd.Parameters.Add(parSlNhap);
            sqlCmd.Parameters.Add(parMaNV);
            sqlCmd.Parameters.Add(parMaMH);


            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            sqlCmd.ExecuteNonQuery();
        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Click vào dòng nào ở datagirdView phiếu nhập thì cho dữa liệu vào các text box tương ứng
                dgvPhieuNhap.CurrentRow.Selected = true;
                txtSoPN.Text = dgvPhieuNhap.Rows[e.RowIndex].Cells["SoPN"].FormattedValue.ToString();
                dtpNgayNhap.Text = dgvPhieuNhap.Rows[e.RowIndex].Cells["NgayNhap"].FormattedValue.ToString();
                txtDgNhap.Text = dgvPhieuNhap.Rows[e.RowIndex].Cells["DgNhap"].FormattedValue.ToString();
                txtSlNhap.Text = dgvPhieuNhap.Rows[e.RowIndex].Cells["SlNhap"].FormattedValue.ToString();
                cbMaMH.Text = dgvPhieuNhap.Rows[e.RowIndex].Cells["MaMH"].FormattedValue.ToString();
            }
            catch
            {

            }

        }

        private void LoadDuLieuCBMaMH()
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Select MaMH from MATHANG";
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Connection = kn.sqlCon;
            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                //xóa dữ liệu cũ
                cbMaMH.Items.Remove(dr[0].ToString());
                //thêm dữ liệu mới
                cbMaMH.Items.Add(dr[0].ToString());

            }
            dr.Close();

        }

        private void btnTKPNhap_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM PNHAP";
            string dk = "";
            //Số PN Khác rỗng
            if (txtTKSoPN.Text.Trim() != "")
            {
                dk += " SoPN like '%" + txtTKSoPN.Text + "%'";
            }
            //Mã MH khác rỗng và Số PN khác rỗng
            if (txtTKMaMH_N.Text.Trim() != "" && dk != "")
            {
                dk += " AND MaMH like N'%" + txtTKMaMH_N.Text + "%'";
            }
            //Mã MH khác rỗng, Số PN rỗng
            if (txtTKMaMH_N.Text.Trim() != "" && dk == "")
            {
                dk += " MaMH like N'%" + txtTKMaMH_N.Text + "%'";
            }
            //Nối các điều kiện
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTK_PN(sqlTimKiem);
        }

        private void LoadDuLieuTK_PN(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvPhieuNhap.DataSource = ds.Tables[0];
        }

        private void XoaPN(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parSoPN = new SqlParameter("@SoPN", SqlDbType.Char);
            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parSoPN.Value = txtSoPN.Text.Trim();
            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parSoPN);
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

        //====================================End Phiếu Nhập=============================

        //====================================Thống Kê Mặt Hàng===========================
        private void HienThiThongKe_MH(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;
            sqlCmd.Connection = kn.sqlCon;

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            flowLayoutPanel1.Controls.Clear();
            //gán biến n là số item mặt hàng
            n = ds.Tables[0].Rows.Count;
            listItem = new UCItemMH[n]; //mảng item

            kn.MoKetNoi();
            for (i = 0; i < n; i++)
            {
                //hiển thị số lượng đã bán
                HienThiListMH("HienThiListMH", ds.Tables[0].Rows[i][0].ToString());
                //thêm mảng item vào flowLayoutPanel 
                flowLayoutPanel1.Controls.Add(listItem[i]);
            }

            lvTopMH.Items.Clear();    
            //Thống kê mặt hàng bán chạy
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //thêm dữ liệu vào listView
                ListViewItem lvi = new ListViewItem(listItem[i].Stt);

                //mã mặt hàng
                lvi.SubItems.Add(HienThiTenMH(ds.Tables[0].Rows[i][0].ToString()));
                //số lượng đã bán
                lvi.SubItems.Add(ds.Tables[0].Rows[i][1].ToString());
                //thêm vào listView
                lvTopMH.Items.Add(lvi);
            }
            kn.DongKetNoi();

        }

        private string HienThiTenMH(string maMH)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select TenMH from MATHANG where  MaMH='" + maMH + "'";
            sqlCmd.Connection = kn.sqlCon;
            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);


            return ds.Tables[0].Rows[0][0].ToString();

        }

        private void HienThiListMH(string sql, string maMH)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;
            sqlCmd.Connection = kn.sqlCon;

            SqlParameter parMaMH = new SqlParameter("@MaMH", SqlDbType.Char);
            parMaMH.Value = maMH;
            sqlCmd.Parameters.Add(parMaMH);
            sqlCmd.Connection = kn.sqlCon;

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);


            //Nếu số lượng đã bán là rỗng hoặc null
            if (ds.Tables[0].Rows[0][0].ToString() == "" || ds.Tables[0].Rows[0][0].ToString() == null)
            {

            }
            else
            {
                listItem[i] = new UCItemMH(); //Tạo item thêm vào mảng
                listItem[i].Stt = (i + 1).ToString(); //Số thứ tự của item             
                listItem[i].TenMH = ds.Tables[1].Rows[0][0].ToString(); //Tên mặt hàng của item mặt hàng
                listItem[i].DgBan = ds.Tables[1].Rows[0][1].ToString(); //Đơn giá bán của item mặt hàng
                listItem[i].SLCon = ds.Tables[1].Rows[0][2].ToString(); //Số lượng còn của item mặt hàng
                listItem[i].FileAnh = ds.Tables[1].Rows[0][3].ToString(); //File ảnh của item mặt hàng                                                                       
                listItem[i].SLBan = ds.Tables[0].Rows[0][0].ToString(); //Gán dữ liệu số lượng bán vào mảng 
            }
        }

        private void btnLoadTK_MH_Click(object sender, EventArgs e)
        {
            //Load lại thống kê mặt hàng bán chạy
            HienThiThongKe_MH("HienThiItemMH");
        }
    }
}
