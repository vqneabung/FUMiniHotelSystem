using BussinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IRoomRepository
    {
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
        Room GetRoomById(int id);
        List<Room> GetAllRooms();
        Room GetRoomByRoomNumber(string roomNumber);
        public List<Room> GetAllRoomsIncludeRoomType();

    }
}
