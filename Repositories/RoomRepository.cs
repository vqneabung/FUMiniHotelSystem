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
        public static RoomDAO RoomDAO = new RoomDAO();

        public void AddRoom(Room room)
        {
            RoomDAO.AddRoom(room);
        }

        public void DeleteRoom(int id)
        {
            RoomDAO.DeleteRoom(id);
        }

        public List<Room> GetAllRooms()
        {
             List<Room> IncludeRoomType()
            {
                
                for (int i = 0; i < RoomDAO.GetAllRooms().Count; i++)
                {
                    RoomDAO.GetAllRooms()[i].RoomType = RoomTypeRepository.RoomTypeDAO.GetRoomTypeByID(RoomDAO.GetAllRooms()[i].RoomTypeID);
                }

                return RoomDAO.GetAllRooms();
            }

            return RoomDAO.GetAllRooms();
        }

        public List<Room> GetAllRoomsIncludeRoomType()
        {

            for (int i = 0; i < RoomDAO.GetAllRooms().Count; i++)
            {
                RoomDAO.GetAllRooms()[i].RoomType = RoomTypeRepository.RoomTypeDAO.GetRoomTypeByID(RoomDAO.GetAllRooms()[i].RoomTypeID);
            }

            return RoomDAO.GetAllRooms();
        }

        public Room GetRoomById(int id)
        {
            return RoomDAO.GetRoomById(id);
        }

        public void UpdateRoom(Room room)
        {
            RoomDAO.UpdateRoom(room);
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            return RoomDAO.GetRoomByRoomNumber(roomNumber);
        }
    }
}
