using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoomTypeService
    {
        void AddRoomType(RoomType roomType);
        void DeleteRoomType(int id);
        RoomType GetRoomTypeById(int roomTypeId);
        void UpdateRoomType(RoomType roomType);
        ObservableCollection<RoomType> GetAllRoomTypes();

    }
}
