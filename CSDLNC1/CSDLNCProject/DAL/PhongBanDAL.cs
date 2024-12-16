using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PhongBanDAL : DatabaseConnection
    {
        private readonly IMongoCollection<PhongBanDTO> collection;


        public PhongBanDAL()
        {
            collection = GetPhongBanCollection();
        }

        private static IMongoCollection<PhongBanDTO> GetPhongBanCollection()
        {
            var database = DatabaseConnection.ConnectToMongoService();
            return database.GetCollection<PhongBanDTO>("phongban");
        }

        public List<PhongBanDTO> GetAllPhongBan()
        {
            return collection.Find(pb => true).ToList();
        }
        public bool AddPhongBan(PhongBanDTO phongBan)
        {
            // Kiểm tra trùng Mã phòng ban
            var existing = collection.Find(pb => pb.maphongban == phongBan.maphongban).FirstOrDefault();
            if (existing != null)
            {
                return false; // Mã phòng ban đã tồn tại
            }

            collection.InsertOne(phongBan); // Thêm vào MongoDB
            return true; // Thành công
        }
        public bool UpdatePhongBan(PhongBanDTO phongBan)
        {
            try
            {
                // Tạo điều kiện lọc (Filter): tìm document theo MaPhongBan
                var filter = Builders<PhongBanDTO>.Filter.Eq(pb => pb.maphongban, phongBan.maphongban);

                // Xây dựng bản cập nhật (Update): thay đổi TenPhongBan
                var update = Builders<PhongBanDTO>.Update
                    .Set(pb => pb.phongban, phongBan.phongban);

                // Thực thi cập nhật
                var result = collection.UpdateOne(filter, update);

                // Kiểm tra kết quả và trả về
                return result.ModifiedCount > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}