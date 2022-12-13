using System.Drawing;
using System.Windows.Forms;

namespace QLBanHang
{
    public partial class UCItemMH : UserControl
    {
        private string stt;
        private string maMH;
        private string tenMH;
        private string dgBan;
        private string slBan;
        private string slCon;
        private string fileAnh;
        public UCItemMH()
        {
            InitializeComponent();

        }
        //Mode của item mặt hàng
        public string Stt
        {
            get { return stt; }
            set { stt = value; lbSTT.Text = value; }
        }
        public string TenMH
        {
            get { return tenMH; }
            set { tenMH = value; lbTenMH.Text = value; }
        }

        public string MaMH
        {
            get { return maMH; }
            set { maMH = value;}
        }
        public string FileAnh
        {
            get { return fileAnh; }
            set { fileAnh = value; ptbAnhMH.Image = new Bitmap(fileAnh); }
        }
        public string DgBan
        {
            get { return dgBan; }
            set { dgBan = value; lbDgban.Text = value; }
        }
        public string SLCon
        {
            get { return slCon; }
            set { slCon = value; lbSlCon.Text = value; }
        }

        public string SLBan
        {
            get { return slBan; }
            set { slBan = value; lbSlBan.Text = value; }
        }

    }
}
