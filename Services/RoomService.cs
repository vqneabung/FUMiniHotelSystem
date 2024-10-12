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
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void AddRoom(Room room)
        {
            _roomRepository.AddRoom(room);
        }

        public void DeleteRoom(int id)
        {
            _roomRepository.DeleteRoom(id);
        }

        public List<Room> GetAllRooms()
        {
    
            return _roomRepository.GetAllRooms();
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            return _roomRepository.GetRoomByRoomNumber(roomNumber);
        }


        public Room GetRoomById(int id)
        {
            return _roomRepository.GetRoomById(id);
        }

        public void UpdateRoom(Room room)
        {
            _roomRepository.UpdateRoom(room);
        }
    }
}
