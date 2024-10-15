using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public RoomInformationDAO _roomInformationDAO;
        public RoomTypeDAO _roomTypeDAO;

        public RoomInformationRepository(RoomInformationDAO roomInformationDAO, RoomTypeDAO roomTypeDAO)
        {
            _roomInformationDAO = roomInformationDAO;
            _roomTypeDAO = roomTypeDAO;
        }

        public void AddRoomInformation(RoomInformation room)
        {
            _roomInformationDAO.AddRoom(room);
        }

        public void DeleteRoomInformation(int id)
        {
            _roomInformationDAO.DeleteRoom(id);
        }

        public List<RoomInformation> GetAllRoomInformations()
        {
            return _roomInformationDAO.GetAllRooms();

        }


        public RoomInformation GetRoomInformationById(int id)
        {
            return _roomInformationDAO.GetRoomInformationById(id);
        }

        public void UpdateRoomInformation(RoomInformation room)
        {
            _roomInformationDAO.UpdateRoom(room);
        }

        public RoomInformation GetRoomInformationByRoomNumber(string roomNumber)
        {
            return _roomInformationDAO.GetRoomInformationByRoomNumber(roomNumber);
        }
    }
}
