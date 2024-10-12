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
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        } 

        public void AddRoomType(RoomType roomType)
        {
            _roomTypeRepository.AddRoomType(roomType);
        }

        public void DeleteRoomType(int id)
        {
            _roomTypeRepository.DeleteRoomType(id);
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return _roomTypeRepository.GetAllRoomTypes(); 
        }

        public RoomType GetRoomTypeById(int roomTypeId)
        {
            return _roomTypeRepository.GetRoomTypeById(roomTypeId);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            _roomTypeRepository.UpdateRoomType(roomType);
        }
    }
}
