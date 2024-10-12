using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;

        public RoomService()
        {
            roomRepository = new RoomRepository();
        }

        public void AddRoom(Room room)
        {
            roomRepository.AddRoom(room);
        }

        public void DeleteRoom(int id)
        {
            roomRepository.DeleteRoom(id);
        }

        public List<Room> GetAllRooms()
        {
    
            return roomRepository.GetAllRooms();
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            return roomRepository.GetRoomByRoomNumber(roomNumber);
        }


        public Room GetRoomById(int id)
        {
            return roomRepository.GetRoomById(id);
        }

        public void UpdateRoom(Room room)
        {
            roomRepository.UpdateRoom(room);
        }
    }
}
