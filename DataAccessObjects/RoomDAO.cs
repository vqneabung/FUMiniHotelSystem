using BussinessObjects;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessObjects
{
    public class RoomDAO
    {
        List<Room> listRooms = new List<Room>();


        public RoomDAO()
        {
            Room room1 = new Room
            {
                RoomID = 1,
                RoomNumber = "101",
                RoomDescription = "Room 101",
                RoomMaxCapacity = 2,
                RoomStatus = 1,
                RoomPricePerDate = 100,
                RoomTypeID = 1
            };

            Room room2 = new Room
            {
                RoomID = 2,
                RoomNumber = "102",
                RoomDescription = "Room 102",
                RoomMaxCapacity = 2,
                RoomStatus = 1,
                RoomPricePerDate = 100,
                RoomTypeID = 1
            };

            Room room3 = new Room
            {
                RoomID = 3,
                RoomNumber = "201",
                RoomDescription = "Room 201",
                RoomMaxCapacity = 4,
                RoomStatus = 1,
                RoomPricePerDate = 200,
                RoomTypeID = 2
            };

            Room room4 = new Room
            {
                RoomID = 4,
                RoomNumber = "202",
                RoomDescription = "Room 202",
                RoomMaxCapacity = 4,
                RoomStatus = 0,
                RoomPricePerDate = 200,
                RoomTypeID = 2
            };

            listRooms.Add(room1);
            listRooms.Add(room2);
            listRooms.Add(room3);
            listRooms.Add(room4);

        }

        public List<Room> GetAllRooms()
        {
            return listRooms;
        }

        public Room GetRoomById(int id)
        {
            foreach (Room room in listRooms)
            {
                if (room.RoomID == id)
                {
                    return room;
                }
            }
            return null;
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            foreach (Room room in listRooms)
            {
                if (room.RoomNumber == roomNumber)
                {
                    return room;
                }
            }
            return null;
        }

        public void AddRoom(Room room)
        {
            if (listRooms.Count == 0)
            {
                room.RoomID = 1;
            }
            else
            {
                room.RoomID = listRooms[listRooms.Count - 1].RoomID + 1;
            }

            listRooms.Add(room);
        }

        public void UpdateRoom(Room room)
        {
            foreach (Room r in listRooms)
            {
                if (r.RoomID == room.RoomID)
                {
                    r.RoomNumber = room.RoomNumber;
                    r.RoomDescription = room.RoomDescription;
                    r.RoomMaxCapacity = room.RoomMaxCapacity;
                    r.RoomStatus = room.RoomStatus;
                    r.RoomPricePerDate = room.RoomPricePerDate;
                    r.RoomTypeID = room.RoomTypeID;
                }
            }
        }

        public void DeleteRoom(int roomId)
        {
            foreach (Room r in listRooms)
            {
                if (r.RoomID == roomId)
                {
                    listRooms.Remove(r);
                    break;
                }
            }
        }
    }
}
