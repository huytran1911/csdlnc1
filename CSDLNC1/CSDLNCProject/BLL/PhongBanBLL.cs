using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PhongBanBLL
    {
        private PhongBanDAL phongBanDAL = new PhongBanDAL();

        public List<PhongBanDTO> GetAllPhongBan()
        {
            var phongBanEntities = phongBanDAL.GetAllPhongBan();
            var phongBanDTOs = phongBanEntities.Select(pb => new PhongBanDTO
            {
                phongban= pb.phongban,
                maphongban = pb.maphongban,

            }).ToList();

            return phongBanDTOs;
        }

        public List<PhongBanDTO> SearchPhongBan(string keyword)
        {
            var allPhongBan = phongBanDAL.GetAllPhongBan();

            var searchResult = allPhongBan
                .Where(pb => pb.maphongban.ToString().Contains(keyword) ||
                             pb.phongban.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            return searchResult;
        }

        public string AddPhongBan(PhongBanDTO phongBan)
        {
            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrEmpty(phongBan.maphongban) || string.IsNullOrEmpty(phongBan.phongban))
                return "Mã phòng ban và tên phòng ban không được để trống.";

            // Gọi DAL để thêm phòng ban
            bool result = phongBanDAL.AddPhongBan(phongBan);
            return result ? "Thêm phòng ban thành công." : "Mã phòng ban đã tồn tại.";
        }

        public string UpdatePhongBan(PhongBanDTO phongBan)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(phongBan.phongban))
                return "Tên phòng ban không được để trống.";

            // Gọi phương thức cập nhật từ DAL
            bool result = phongBanDAL.UpdatePhongBan(phongBan);

            // Trả về thông báo phù hợp
            return result ? "Cập nhật phòng ban thành công!" : "Không thể sửa mã phòng ban.";
        }
    }
}
