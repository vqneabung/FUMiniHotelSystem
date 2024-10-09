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
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;

        public RoomTypeService()
        {
            roomTypeRepository = new RoomTypeRepository();
        } 

        public void AddRoomType(RoomType roomType)
        {
            roomTypeRepository.AddRoomType(roomType);
        }

        public void DeleteRoomType(int id)
        {
            roomTypeRepository.DeleteRoomType(id);
        }

        public ObservableCollection<RoomType> GetAllRoomTypes()
        {
            return roomTypeRepository.GetAllRoomTypes(); 
        }

        public RoomType GetRoomTypeById(int roomTypeId)
        {
            return roomTypeRepository.GetRoomTypeById(roomTypeId);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            roomTypeRepository.UpdateRoomType(roomType);
        }
    }
}
