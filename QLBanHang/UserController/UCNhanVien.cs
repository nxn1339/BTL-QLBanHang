using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class UCNhanVien : UserControl
    {
        //Tạo LK gọi Kết Nối Database
        KetNoiData kn = new KetNoiData();
        //string của File Ảnh
        private string fileAnh = "";
        //string Chức Năng
        private string chucNang = "";
        public UCNhanVien()
        {
            InitializeComponent();

        }



        private void UCNhanVien_Load(object sender, EventArgs e)
        {
            //Khóa các GroupBox Không Cho Nhập Dữ Liệu
            gbNhapNV.Enabled = false;
            gbNhapCV.Enabled = false;
            gbNhapTK.Enabled = false;

            kn.MoKetNoi();  //Mở Kết Nối
            //Hiển Thị Nhân Viên
            LoadDuLieuNV("HienThiNhanVien");
            //Hiển Thị Chức Vụ
            LoadDuLieuCV("HienThiChucVu");
            //Hiển Thị Tài Khoản
            LoadDuLieuTaiKhoan("HienThiTK");
            //Load Dữ Liệu Vào Các Combobox 
            LoadDuLieuCBMaNV();
            LoadDuLieuCBMaCV();
            kn.DongKetNoi(); //Đóng Kết Nối
        }

        //====================================Nhân Viên=====================================
        //Hàm Hiển Thị Nhân Viên
        private void LoadDuLieuNV(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            sqlCmd.Connection = kn.sqlCon;

            DataSet ds = new DataSet();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(ds);

            dgvNhanVien.DataSource = ds.Tables[0];


            //Mặc định Chọn dòng đầu tiên và đưa vào TextBox
            dgvNhanVien.Rows[0].Selected = true;

            txtMaNV.Text = dgvNhanVien.Rows[0].Cells["MaNV"].FormattedValue.ToString();
            txtTenNV.Text = dgvNhanVien.Rows[0].Cells["TenNV"].FormattedValue.ToString();
            txtCMND.Text = dgvNhanVien.Rows[0].Cells["CMND"].FormattedValue.ToString();
            cbGioiTinh.Text = dgvNhanVien.Rows[0].Cells["GioiTinh"].FormattedValue.ToString();
            dtpNgaySinh.Text = dgvNhanVien.Rows[0].Cells["NgaySinh"].FormattedValue.ToString();
            txtDiaChi.Text = dgvNhanVien.Rows[0].Cells["DiaChi"].FormattedValue.ToString();
            txtSDT.Text = dgvNhanVien.Rows[0].Cells["SDT"].FormattedValue.ToString();
            txtEmail.Text = dgvNhanVien.Rows[0].Cells["Email"].FormattedValue.ToString();
            fileAnh = dgvNhanVien.Rows[0].Cells["FileAnh"].FormattedValue.ToString();
            cbMaCV.Text = dgvNhanVien.Rows[0].Cells["MaCV"].FormattedValue.ToString();
            //Mở ảnh cho vào pictureBox
            ptbNhanVien.Image = new Bitmap(fileAnh);
            ptbNhanVien.ImageLocation = fileAnh;
            ptbNhanVien.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //Hàm Xóa Nhân Viên
        private void XoaNhanVien(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;
            //Nhân Viên
            SqlParameter parMaNV = new SqlParameter("@MaNV", SqlDbType.Char);
            parMaNV.Value = txtMaNV.Text.Trim();
            sqlCmd.Parameters.Add(parMaNV);
            //Thực Thi Command
            sqlCmd.Connection = kn.sqlCon;
            try
            {
                int kq = sqlCmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    //báo thanh công
                    MessageBox.Show("Xóa Thành Công !");
                }
                else
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Xóa Không Thành Công !");
                }

            }
            catch
            {
                //báo lỗi
                MessageBox.Show("Lỗi Xóa Không Thành Công !");
            }

        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu là yes thì
            if (MessageBox.Show("Bạn Có Muốn Xóa Nhân Viên Có Mã " + txtMaNV.Text, " Xóa Nhân Viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Xóa Nhân Viên
                XoaNhanVien("XoaNhanVien");
                //Load Lại Dữ Liệu
                LoadDuLieuNV("HienThiNhanVien");
                LoadDuLieuCV("HienThiChucVu");

            }
            kn.DongKetNoi();

        }

        private void dgNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Sự kiện chọn dòng đưa vào textBox
                dgvNhanVien.CurrentRow.Selected = true;
                txtMaNV.Text = dgvNhanVien.Rows[e.RowIndex].Cells["MaNV"].FormattedValue.ToString();
                txtTenNV.Text = dgvNhanVien.Rows[e.RowIndex].Cells["TenNV"].FormattedValue.ToString();
                txtCMND.Text = dgvNhanVien.Rows[e.RowIndex].Cells["CMND"].FormattedValue.ToString();
                cbGioiTinh.Text = dgvNhanVien.Rows[e.RowIndex].Cells["GioiTinh"].FormattedValue.ToString();
                dtpNgaySinh.Text = dgvNhanVien.Rows[e.RowIndex].Cells["NgaySinh"].FormattedValue.ToString();
                txtDiaChi.Text = dgvNhanVien.Rows[e.RowIndex].Cells["DiaChi"].FormattedValue.ToString();
                txtSDT.Text = dgvNhanVien.Rows[e.RowIndex].Cells["SDT"].FormattedValue.ToString();
                txtEmail.Text = dgvNhanVien.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                fileAnh = dgvNhanVien.Rows[e.RowIndex].Cells["FileAnh"].FormattedValue.ToString();
                cbMaCV.Text = dgvNhanVien.Rows[e.RowIndex].Cells["MaCV"].FormattedValue.ToString();

                //Mở ảnh cho vào pictureBox
                if (fileAnh != "")
                {
                    //Có Ảnh
                    ptbNhanVien.Image = new Bitmap(fileAnh);
                    ptbNhanVien.ImageLocation = fileAnh;
                    ptbNhanVien.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    //Không Có Ảnh
                    ptbNhanVien.Image = ptbNhanVien.ErrorImage;
                }

            }
            catch
            {

            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //load lại dữ liệu
            LoadDuLieuNV("HienThiNhanVien");
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            //Mở GroupBox HienThiNV lên để nhập dữ liệu
            gbNhapNV.Enabled = true;
            //mở textbox mã nhân viên
            txtMaNV.Enabled = true;
            //Chuyển Trạng Thái Chức Năng
            chucNang = "addNV";
        }

        //Hàm Click chuột hai lần mở thư mục để chọn ảnh
        private void ptbNhanVien_MouseDoubleClick(object sender, MouseEventArgs e)
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
                    fileAnh = openfd.FileName;
                    ptbNhanVien.Image = new Bitmap(openfd.FileName);
                    ptbNhanVien.ImageLocation = openfd.FileName;
                    ptbNhanVien.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch
                {
                    MessageBox.Show("Mở Ảnh Không Thành Công !");
                }
            }
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            //Mở GroupBox HienThiNV lên để nhập dữ liệu
            gbNhapNV.Enabled = true;
            //Chuyển Trạng Thái Chức Năng
            chucNang = "updateNV";
            //Tắt textbox của mã nhân viên không cho sửa mã nhân viên
            txtMaNV.Enabled = false;
        }

        private void btnHuyNV_Click(object sender, EventArgs e)
        {
            //Tắt GroupBox HienThiNV
            gbNhapNV.Enabled = false;
            //Tắt Báo Lỗi Nếu Có
            errorNhanVien.SetError(txtMaNV, null);
        }

        private void btnLuuNV_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang.Trim() == "addNV") //Thêm Nhân Viên
            {
                try
                {
                    if (txtMaNV.Text.Trim() != "") //Nếu Mã NV Khác Rỗng
                    {
                        NhanVien("ThemNhanVien"); //Thêm Nhân Viên
                        LoadDuLieuNV("HienThiNhanVien"); //Load Lại Dữ Liệu
                        gbNhapNV.Enabled = false; //Tắt GroupBox HienThiNV
                        MessageBox.Show("Thêm Nhân Viên Thành Công!");
                        errorNhanVien.SetError(txtMaNV, null); //Tắt Báo Lỗi Nếu Có
                    }
                    else
                    {
                        //Báo Lỗi Khi textBox Mã NV Rỗng
                        errorNhanVien.SetError(txtMaNV, "Bạn Chưa Nhập Dữ Liệu !");
                    }
                }
                catch
                {
                    //Báo Lỗi Khi Thêm Không Thành Công
                    MessageBox.Show("Lỗi Thêm Dữ Liệu Không Thành Công !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

            else if (chucNang == "updateNV")
            {
                try
                {
                    NhanVien("SuaNhanVien"); //Sửa Nhân Viên
                    LoadDuLieuNV("HienThiNhanVien"); //Load Lại Dữ Liệu
                    gbNhapNV.Enabled = false; //Khóa Nhập
                    MessageBox.Show("Sửa Nhân Viên Thành Công!");
                }
                catch
                {
                    //Báo Lỗi
                    MessageBox.Show("Lõi Sửa Nhân Viên Không Thành Công!", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kn.DongKetNoi();

        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            //Load lại comboBox mã công việc
            LoadDuLieuCBMaCV();
        }


        private void NhanVien(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaNV = new SqlParameter("@MaNV", SqlDbType.Char);
            SqlParameter parTenNV = new SqlParameter("@TenNV", SqlDbType.NVarChar);
            SqlParameter parCMND = new SqlParameter("@CMND", SqlDbType.Char);
            SqlParameter parGioiTinh = new SqlParameter("@GioiTinh", SqlDbType.NVarChar);
            SqlParameter parNgaySinh = new SqlParameter("@NgaySinh", SqlDbType.Date);
            SqlParameter parDiaChi = new SqlParameter("@DiaChi", SqlDbType.NVarChar);
            SqlParameter parSDT = new SqlParameter("@SDT", SqlDbType.VarChar);
            SqlParameter parEmail = new SqlParameter("@Email", SqlDbType.NVarChar);
            SqlParameter parFileAnh = new SqlParameter("@FileAnh", SqlDbType.NVarChar);
            SqlParameter parMaCV = new SqlParameter("@MaCV", SqlDbType.Char);


            //Lấy Dữ Liệu Từ TextBox Đưa Vào Parameter
            parMaNV.Value = txtMaNV.Text.Trim();
            parTenNV.Value = txtTenNV.Text.Trim();
            parCMND.Value = txtCMND.Text.Trim();
            parGioiTinh.Value = cbGioiTinh.Text.Trim();
            parNgaySinh.Value = dtpNgaySinh.Text.Trim();
            parDiaChi.Value = txtDiaChi.Text.Trim();
            parSDT.Value = txtSDT.Text.Trim();
            parEmail.Value = txtEmail.Text.Trim();
            parFileAnh.Value = fileAnh;
            parMaCV.Value = cbMaCV.Text.Trim();

            //Thêm Parameter vào Comman
            sqlCmd.Parameters.Add(parMaNV);
            sqlCmd.Parameters.Add(parTenNV);
            sqlCmd.Parameters.Add(parCMND);
            sqlCmd.Parameters.Add(parGioiTinh);
            sqlCmd.Parameters.Add(parNgaySinh);
            sqlCmd.Parameters.Add(parDiaChi);
            sqlCmd.Parameters.Add(parSDT);
            sqlCmd.Parameters.Add(parEmail);
            sqlCmd.Parameters.Add(parFileAnh);
            sqlCmd.Parameters.Add(parMaCV);

            //Gắn Liên Kết SQL
            sqlCmd.Connection = kn.sqlCon;
            //Thực Thi
            sqlCmd.ExecuteNonQuery();

        }

        //Hàm Hiển Thị Dữ Liệu Thìm Kiếm
        private void LoadDuLieuTK_NV(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvNhanVien.DataSource = ds.Tables[0];
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM NHANVIEN";
            string dk = "";
            //Mã NV Khác rỗng
            if (txtTKMaNV.Text.Trim() != "")
            {
                dk += " MaNV like '%" + txtTKMaNV.Text + "%'";
            }
            //Tên NV khác rỗng và Mã NV khác rỗng
            if (txtTKTenNV.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenNV like N'%" + txtTKTenNV.Text + "%'";
            }
            //Tên NV khác rỗng, Mã NV rỗng
            if (txtTKTenNV.Text.Trim() != "" && dk == "")
            {
                dk += " TenNV like N'%" + txtTKTenNV.Text + "%'";
            }
            //Nối các điều kiện
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTK_NV(sqlTimKiem);
        }

        private void LoadDuLieuCBMaCV()
        {
            kn.MoKetNoi();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Select MaCV from CHUCVU";
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Connection = kn.sqlCon;
            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                //xóa dữ liệu cũ
                cbMaCV.Items.Remove(dr[0].ToString());
                //thêm dữ liệu mới
                cbMaCV.Items.Add(dr[0].ToString());
            }
            dr.Close();
            kn.DongKetNoi();
        }

        //====================================End Nhân Viên=====================================

        //====================================Chức Vụ=====================================
        //Hàm Hiển Thị Chức Vụ
        private void LoadDuLieuCV(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            sqlCmd.Connection = kn.sqlCon;
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dgvChucVu.DataSource = dt;
        }
        private void btnThemCV_Click(object sender, EventArgs e)
        {
            //mở groupBox nhập
            gbNhapCV.Enabled = true;
            //mở textbox mã CV
            txtMaCV.Enabled = true;
            //đổi trạng thái chức năng sang thêm công việc
            chucNang = "addCV";
        }

        private void btnSuaCV_Click(object sender, EventArgs e)
        {
            //mở groupBox nhập
            gbNhapCV.Enabled = true;
            //đóng textbox mã CV không cho sửa mã CV
            txtMaCV.Enabled = false;
            //đổi trạng thái chức năng sang sửa công việc
            chucNang = "updateCV";
        }

        private void btnLoadCV_Click(object sender, EventArgs e)
        {
            //Load lại dữ liệu
            txtMaCV.Text = "";
            txtTenCV.Text = "";
            txtLuongCB.Text = "";
            kn.MoKetNoi();
            LoadDuLieuCV("HienThiChucVu");
            kn.DongKetNoi();
        }

        private void btnXoaCV_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu là yes thì
            if (MessageBox.Show("Bạn Có Muốn Xóa Chức Vụ Có Mã " + txtMaCV.Text.Trim(), "Xóa Chức Vụ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xóa chức vụ
                XoaChucVu("XoaChucVu");
                //load lại dữ liệu
                LoadDuLieuCV("HienThiChucVu");
            }
            kn.DongKetNoi();
        }

        private void btnLuuCV_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang == "addCV")//Nếu đang ở chức năng thêm chức vụ
            {
                try
                {
                    if (txtMaCV.Text.Trim() != "") //Nếu mã CV khác rỗng
                    {
                        //thêm chức vụ
                        ChucVu("ThemChucVu");
                        //báo thành công
                        MessageBox.Show("Thêm Chức Vụ Thành Công !");
                        //load lại dữ liệu
                        LoadDuLieuCV("HienThiChucVu");
                        //dóng groupBox nhập
                        gbNhapCV.Enabled = false;
                        //tắt báo lỗi nếu có
                        errorNhanVien.SetError(txtMaCV, null);
                    }
                    else
                    {
                        //báo lỗi mã CV rỗng
                        errorNhanVien.SetError(txtMaCV, "Bạn Chưa Nhập Mã Chức Vụ !");
                    }
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Thêm Dữ Liệu Không Thành Công !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (chucNang == "updateCV")//Nếu đang ở chức năng sửa chức vụ
            {
                try
                {
                    //sửa chức vụ
                    ChucVu("SuaChucVu");
                    //báo thành công
                    MessageBox.Show("Sửa Chức Vụ Thành Công !");
                    //load lại dữ liệu
                    LoadDuLieuCV("HienThiChucVu");
                    //đóng groupBox nhập
                    gbNhapCV.Enabled = false;

                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Sửa Chức Vụ Không Thành Công !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kn.DongKetNoi();
        }

        private void btnHuyCV_Click(object sender, EventArgs e)
        {
            //đóng groupBox nhập 
            gbNhapCV.Enabled = false;
            //tắt báo lỗi nếu có
            errorNhanVien.SetError(txtMaCV, null);
        }
        private void dgChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Sự kiện chọn dòng đưa vào textBox
                dgvChucVu.CurrentRow.Selected = true;
                txtMaCV.Text = dgvChucVu.Rows[e.RowIndex].Cells["MaCV"].FormattedValue.ToString();
                txtTenCV.Text = dgvChucVu.Rows[e.RowIndex].Cells["TenCV"].FormattedValue.ToString();
                txtLuongCB.Text = dgvChucVu.Rows[e.RowIndex].Cells["LuongCB"].FormattedValue.ToString();
            }
            catch
            {

            }
        }

        private void ChucVu(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parMaCV = new SqlParameter("@MaCV", SqlDbType.Char);
            SqlParameter parTenCV = new SqlParameter("@TenCV", SqlDbType.NVarChar);
            SqlParameter parLuongCB = new SqlParameter("@LuongCB", SqlDbType.Money);


            parMaCV.Value = txtMaCV.Text.Trim();
            parTenCV.Value = txtTenCV.Text.Trim();
            parLuongCB.Value = txtLuongCB.Text.Trim();

            sqlCmd.Parameters.Add(parMaCV);
            sqlCmd.Parameters.Add(parTenCV);
            sqlCmd.Parameters.Add(parLuongCB);
            //kết nối
            sqlCmd.Connection = kn.sqlCon;
            //thực thi
            sqlCmd.ExecuteNonQuery();
        }

        private void LoadDuLieuCBMaNV()
        {
            kn.MoKetNoi();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Select MaNV from NHANVIEN";
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Connection = kn.sqlCon;
            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                //Xóa dữ liệu cũ
                cbMaNV_TK.Items.Remove(dr[0].ToString());
                //thêm dữ liệu mới
                cbMaNV_TK.Items.Add(dr[0].ToString());
            }
            dr.Close();
            kn.DongKetNoi();
        }

        private void XoaChucVu(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = sql;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parMaCV = new SqlParameter("@MaCV", SqlDbType.Char);
            parMaCV.Value = txtMaCV.Text.Trim();
            sqlCmd.Parameters.Add(parMaCV);

            sqlCmd.Connection = kn.sqlCon;
            try
            {
                int kq = sqlCmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    //báo thành công
                    MessageBox.Show("Xóa Chức Vụ Thành Công !");
                }
                else
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Xóa Chức Vụ Không Thành Công !");
                }
            }
            catch
            {
                //báo lỗi
                MessageBox.Show("Lỗi Xóa Chức Vụ Không Thành Công !");
            }

        }

        private void btnTimKiemCV_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT MaCV,TenCV,LuongCB=FORMAT(LuongCB,'#,##0') FROM CHUCVU";
            string dk = "";
            if (txtTKMaCV.Text.Trim() != "")
            {
                dk += " MaCV like '%" + txtTKMaCV.Text + "%'";
            }
            //kiem tra TenCV va MaCV khac rong
            if (txtTKTenCV.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenCV like N'%" + txtTKTenCV.Text + "%'";
            }
            //Tim kiem theo TenCV khi MaCV la rong
            if (txtTKTenCV.Text.Trim() != "" && dk == "")
            {
                dk += " TenCV like N'%" + txtTKTenCV.Text + "%'";
            }
            //Ket hoi dk
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTKCV(sqlTimKiem);
        }

        private void LoadDuLieuTKCV(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvChucVu.DataSource = ds.Tables[0];
        }
        //====================================End Chức Vụ=====================================


        //====================================Tài Khoản=====================================
        private void btnLuuTK_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            if (chucNang == "addTK") //Nếu đang ở chức năng thêm tài khoản
            {
                try
                {
                    if (txtTaiKhoan.Text.Trim() != "") //nếu tài khoản khác rỗng
                    {
                        //thêm tài khoản
                        TaiKhoan("ThemTaiKhoan");
                        //báo thành công
                        MessageBox.Show("Thêm Tài Khoản Thành Công !");
                        //đóng groupBox nhập
                        gbNhapTK.Enabled = false;
                        //load lại dữ liệu
                        LoadDuLieuTaiKhoan("HienThiTK");
                        //tắt báo lỗi nếu có
                        errorNhanVien.SetError(txtTaiKhoan, null);
                    }
                    else
                    {
                        //báo lỗi khi tài khoản rỗng
                        errorNhanVien.SetError(txtTaiKhoan, "Bạn Chưa Nhập Tài Khoản !");
                    }

                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Thêm Tài Khoản Không Thành Công !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else if (chucNang == "updateTK") //Nếu đang ở chức năng sửa tài khoản
            {
                try
                {
                    //sửa tài khoản
                    TaiKhoan("SuaTaiKhoan");
                    //đóng groupbox nhập
                    gbNhapTK.Enabled = false;
                    //load lại dữ liệu
                    LoadDuLieuTaiKhoan("HienThiTK");
                    //báo thành công
                    MessageBox.Show("Sửa Tài Khoản Thành Công !");
                }
                catch
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Sửa Tài Khoản Không Thành Công !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Nếu đang chọn chức năng ở tabContro khác báo chọn lại chức năng
                MessageBox.Show("Vui Lòng Chọn Chức Năng !", "Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            kn.DongKetNoi();
        }

        private void btnHuyTK_Click(object sender, EventArgs e)
        {
            //đóng groupBox nhập
            gbNhapTK.Enabled = false;
            //tắt báo lỗi nếu có
            errorNhanVien.SetError(txtTaiKhoan, null);
        }

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập
            gbNhapTK.Enabled = true;
            //mở textbox tài khoản
            txtTaiKhoan.Enabled = true;
            //đổi trạng thái chức năng sang thêm tài khoản
            chucNang = "addTK";
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            //mở groupbox nhập
            gbNhapTK.Enabled = true;
            //đóng textbox tài khoản không cho sửa tài khoản 
            txtTaiKhoan.Enabled = false;
            //đổi trạng thái chức năng sang sửa tài khoản
            chucNang = "updateTK";
        }

        private void btnLoadTK_Click(object sender, EventArgs e)
        {

            kn.MoKetNoi();
            //load lại dữ liệu
            LoadDuLieuCBMaNV();
            kn.DongKetNoi();
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            kn.MoKetNoi();
            //Nếu chọn yes thì
            if (MessageBox.Show("Bán Có Muốn Xóa Tài Khoản " + txtTaiKhoan.Text, "Xóa Tài Khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xóa tài khoản
                XoaTaiKhoan("XoaTaiKhoan");
                //load lại dữ liệu
                LoadDuLieuTaiKhoan("HienThiTK");
            }
            kn.DongKetNoi();
        }

        private void btnTK_TK_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "SELECT * FROM TAIKHOAN";
            string dk = "";
            if (txtTKMaNV_TK.Text.Trim() != "")
            {
                dk += " MaNV like '%" + txtTKMaNV_TK.Text + "%'";
            }
            //Ket hoi dk
            if (dk != "")
            {
                sqlTimKiem += " WHERE" + dk;
            }
            LoadDuLieuTK_TK(sqlTimKiem);
        }

        private void LoadDuLieuTaiKhoan(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            sqlCmd.Connection = kn.sqlCon;

            DataSet ds = new DataSet();

            SqlDataAdapter dap = new SqlDataAdapter(sqlCmd);
            dap.Fill(ds);
            //hiển thị dữ liệu vào datagirdView
            dgvTaiKhoan.DataSource = ds.Tables[0];
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Sự kiện chọn dòng đưa vào textBox
                dgvTaiKhoan.CurrentRow.Selected = true;
                txtTaiKhoan.Text = dgvTaiKhoan.Rows[e.RowIndex].Cells["TaiKhoan"].FormattedValue.ToString();
                txtMatKhau.Text = dgvTaiKhoan.Rows[e.RowIndex].Cells["MatKhau"].FormattedValue.ToString();
                cbMaNV_TK.Text = dgvTaiKhoan.Rows[e.RowIndex].Cells["MaNV"].FormattedValue.ToString();
            }
            catch
            {

            }
        }

        private void TaiKhoan(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;

            SqlParameter parTaiKhoan = new SqlParameter("@TaiKhoan", SqlDbType.NChar);
            SqlParameter parMatKhau = new SqlParameter("@MatKhau", SqlDbType.NVarChar);
            SqlParameter parMaNV = new SqlParameter("@MaNV", SqlDbType.Char);

            parTaiKhoan.Value = txtTaiKhoan.Text.Trim();
            parMatKhau.Value = txtMatKhau.Text.Trim();
            parMaNV.Value = cbMaNV_TK.Text.Trim();

            sqlCmd.Parameters.Add(parTaiKhoan);
            sqlCmd.Parameters.Add(parMatKhau);
            sqlCmd.Parameters.Add(parMaNV);
            //kết nối
            sqlCmd.Connection = kn.sqlCon;
            //thực thi
            sqlCmd.ExecuteNonQuery();
        }

        private void XoaTaiKhoan(string sql)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sql;
            //Nhân Viên
            SqlParameter parTaiKhoan = new SqlParameter("@TaiKhoan", SqlDbType.NChar);
            parTaiKhoan.Value = txtTaiKhoan.Text.Trim();
            sqlCmd.Parameters.Add(parTaiKhoan);
            //kết nối
            sqlCmd.Connection = kn.sqlCon;
            try
            {
                //Thực Thi Command
                int kq = sqlCmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    //báo thành công
                    MessageBox.Show("Xóa Thành Công !");
                }
                else
                {
                    //báo lỗi
                    MessageBox.Show("Lỗi Xóa Không Thành Công !");
                }

            }
            catch
            {
                //báo lỗi
                MessageBox.Show("Lỗi Xóa Không Thành Công !");
            }
        }

        private void LoadDuLieuTK_TK(string sql)
        {
            //tạo đối tượng DataSet
            DataSet ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            SqlDataAdapter dap = new SqlDataAdapter(sql, kn.sqlCon);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvTaiKhoan.DataSource = ds.Tables[0];
        }

        //====================================End Tài Khoản=====================================
    }
}
