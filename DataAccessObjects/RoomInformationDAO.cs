﻿using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessObjects
{
    public class RoomInformationDAO
    {
        private readonly FuminiHotelManagementContext _context;

        public RoomInformationDAO(FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public List<RoomInformation> GetAllRooms()
        {
            return _context.RoomInformations.ToList();
        }

        public RoomInformation? GetRoomInformationById(int id)
        {
           return _context.RoomInformations.Find(id);
        }

        public RoomInformation GetRoomInformationByRoomNumber(string roomNumber)
        {
            
            return _context.RoomInformations.Include(r => r.RoomType).FirstOrDefault(r => r.RoomNumber == roomNumber);

        }

        public void AddRoom(RoomInformation room)
        {
            room.RoomId = _context.RoomInformations.Max(r => r.RoomId) + 1;
            _context.RoomInformations.Add(room);
        }

        public void UpdateRoom(RoomInformation room)
        {
           _context.RoomInformations.Update(room);
        }

        public void DeleteRoom(int roomId)
        {
           _context.RoomInformations.Remove(GetRoomInformationById(roomId));
        }
    }
}
