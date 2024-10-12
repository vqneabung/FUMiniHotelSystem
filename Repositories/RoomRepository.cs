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
    
    public class RoomRepository : IRoomRepository
    {
        public RoomDAO _roomDAO;
        public RoomTypeDAO _roomTypeDAO;

        public RoomRepository(RoomDAO roomDAO, RoomTypeDAO roomTypeDAO)
        {
            _roomDAO = roomDAO;
            _roomTypeDAO = roomTypeDAO;
        }

        public void AddRoom(Room room)
        {
            _roomDAO.AddRoom(room);
        }

        public void DeleteRoom(int id)
        {
            _roomDAO.DeleteRoom(id);
        }

        public List<Room> GetAllRooms()
        {
            for (int i = 0; i < _roomDAO.GetAllRooms().Count; i++)
            {
                _roomDAO.GetAllRooms()[i].RoomType = _roomTypeDAO.GetRoomTypeByID(_roomDAO.GetAllRooms()[i].RoomTypeID);
            }

            return _roomDAO.GetAllRooms();

        }


        public Room GetRoomById(int id)
        {
            return _roomDAO.GetRoomById(id);
        }

        public void UpdateRoom(Room room)
        {
            _roomDAO.UpdateRoom(room);
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            return _roomDAO.GetRoomByRoomNumber(roomNumber);
        }
    }
}
