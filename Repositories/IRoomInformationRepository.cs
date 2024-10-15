using BussinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IRoomInformationRepository
    {
        void AddRoomInformation(RoomInformation room);
        void UpdateRoomInformation(RoomInformation room);
        void DeleteRoomInformation(int id);
        RoomInformation GetRoomInformationById(int id);
        List<RoomInformation> GetAllRoomInformations();
        RoomInformation GetRoomInformationByRoomNumber(string roomNumber);

    }
}
