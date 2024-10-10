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
        public static RoomTypeDAO RoomTypeDAO = new RoomTypeDAO();

        public void AddRoomType(RoomType roomType)
        {
            RoomTypeDAO.AddRoomType(roomType);
        }

        public void DeleteRoomType(int id)
        {
            RoomTypeDAO.DeleteRoomType(id);
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return RoomTypeDAO.GetAllRoomTypes();
        }

        public RoomType GetRoomTypeById(int roomTypeId)
        {
            return RoomTypeDAO.GetRoomTypeByID(roomTypeId); 
        }

        public void UpdateRoomType(RoomType roomType)
        {
            RoomTypeDAO.UpdateRoomType(roomType);
        }
    }
}
