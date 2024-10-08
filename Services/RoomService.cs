using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomService : IRoomRepository
    {
        private readonly IRoomRepository roomRepository;

        public void AddRoom(Room room)
        {
            roomRepository.AddRoom(room);
        }

        public void DeleteRoom(int id)
        {
            roomRepository.DeleteRoom(id);
        }

        public ObservableCollection<Room> GetAllRooms()
        {
            return roomRepository.GetAllRooms();
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
