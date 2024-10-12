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
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public RoomTypeDAO _roomTypeDAO;

        public RoomTypeRepository(RoomTypeDAO roomTypeDAO)
        {
            _roomTypeDAO = roomTypeDAO;
        }

        public void AddRoomType(RoomType roomType)
        {
            _roomTypeDAO.AddRoomType(roomType);
        }

        public void DeleteRoomType(int id)
        {
            _roomTypeDAO.DeleteRoomType(id);
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return _roomTypeDAO.GetAllRoomTypes();
        }

        public RoomType GetRoomTypeById(int roomTypeId)
        {
            return _roomTypeDAO.GetRoomTypeByID(roomTypeId); 
        }

        public void UpdateRoomType(RoomType roomType)
        {
            _roomTypeDAO.UpdateRoomType(roomType);
        }
    }
}
