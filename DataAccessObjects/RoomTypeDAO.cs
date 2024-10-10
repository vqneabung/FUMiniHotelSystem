using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class RoomTypeDAO
    {
        public List<RoomType> listRoomTypes = new List<RoomType>();

        public RoomTypeDAO()
        {
            listRoomTypes.Add(new RoomType { RoomTypeID = 1, RoomTypeName = "Single", TypeDescription = "Single bed room" });
            listRoomTypes.Add(new RoomType { RoomTypeID = 2, RoomTypeName = "Double", TypeDescription = "Double bed room" });
            listRoomTypes.Add(new RoomType { RoomTypeID = 3, RoomTypeName = "Suite", TypeDescription = "Suite room" });
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return listRoomTypes;
        }

        public RoomType GetRoomTypeByID(int id)
        {
            return listRoomTypes.Where(x => x.RoomTypeID == id).FirstOrDefault();
        }

        public void AddRoomType(RoomType roomType)
        {
            if (listRoomTypes.Count > 0)
            {
                roomType.RoomTypeID = listRoomTypes.Max(x => x.RoomTypeID) + 1;
            }
            else
            {
                roomType.RoomTypeID = 1;
            }

            listRoomTypes.Add(roomType);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            RoomType roomTypeToUpdate = listRoomTypes.Where(x => x.RoomTypeID == roomType.RoomTypeID).FirstOrDefault();
            roomTypeToUpdate.RoomTypeName = roomType.RoomTypeName;
            roomTypeToUpdate.TypeDescription = roomType.TypeDescription;
        }

        public void DeleteRoomType(int roomTypeID)
        {
            foreach (RoomType rt in listRoomTypes)
            {
                if (rt.RoomTypeID == roomTypeID)
                {
                    listRoomTypes.Remove(rt);
                    break;
                }
            }
        }

    }
}
