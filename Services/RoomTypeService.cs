using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeService roomTypeService;

        public void AddRoomType(RoomType roomType)
        {
            roomTypeService.AddRoomType(roomType);
        }

        public void DeleteRoomType(int id)
        {
            roomTypeService.DeleteRoomType(id);
        }

        public ObservableCollection<RoomType> GetAllRoomTypes()
        {
            return roomTypeService.GetAllRoomTypes(); 
        }

        public RoomType GetRoomTypeById(int roomTypeId)
        {
            return roomTypeService.GetRoomTypeById(roomTypeId);
        }

        public void UpdateRoomType(RoomType roomType)
        {
           roomTypeService.UpdateRoomType(roomType);
        }
    }
}
