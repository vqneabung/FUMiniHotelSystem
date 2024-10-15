using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoomInformationService
    {
        void AddRoomInformation(RoomInformation room);
        void UpdateRoomInformation(RoomInformation room);
        void DeleteRoomInformation(int id);
        RoomInformation GetRoomInformationById(int id);
        List<RoomInformation> GetAllRoomInformations();
        RoomInformation GetRoomInformationByRoomNumber(string roomNumber);
    }
}
