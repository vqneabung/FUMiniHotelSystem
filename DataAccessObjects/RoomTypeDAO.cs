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
        private readonly FuminiHotelManagementContext _context;

        public RoomTypeDAO(FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return _context.RoomTypes.ToList();
        }

        public RoomType GetRoomTypeByID(int id)
        {
           return _context.RoomTypes.Find(id);
        }

        public void AddRoomType(RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            _context.SaveChanges();
        }

        public void UpdateRoomType(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            _context.Entry(roomType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRoomType(int roomTypeID)
        {
           _context.RoomTypes.Remove(GetRoomTypeByID(roomTypeID));
            _context.SaveChanges();
        }

    }
}
