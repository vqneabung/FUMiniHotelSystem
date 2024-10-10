using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomTypeRepository
    {
        void AddRoomType(RoomType roomType);
        void DeleteRoomType(int id);
        RoomType GetRoomTypeById(int roomTypeId);
        void UpdateRoomType(RoomType roomType);
        List<RoomType> GetAllRoomTypes();

    }
}
