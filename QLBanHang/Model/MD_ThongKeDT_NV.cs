using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang
{
    internal class MD_ThongKeDT_NV
    {
        private string maNV;
        private float tongDT_NV;
        private string tenNV;
        private string fileAnh;
        

        public float TongDT_NV { get => tongDT_NV; set => tongDT_NV = value; }
        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string FileAnh { get => fileAnh; set => fileAnh = value; }

      
    }
}
