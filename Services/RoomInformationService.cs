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
        public class RoomInformationService : IRoomInformationService
        {
            private readonly IRoomInformationRepository _roomInfomationRepository;

            public RoomInformationService(IRoomInformationRepository roomInformationRepository)
            {
                _roomInfomationRepository = roomInformationRepository;
            }

            public void AddRoomInformation(RoomInformation room)
            {
                _roomInfomationRepository.AddRoomInformation(room);
            }

            public void DeleteRoomInformation(int id)
            {
                _roomInfomationRepository.DeleteRoomInformation(id);
            }

            public List<RoomInformation> GetAllRoomInformations() {
    
                return _roomInfomationRepository.GetAllRoomInformations();
            }

            public RoomInformation GetRoomInformationByRoomNumber(string roomNumber)
            {
                return _roomInfomationRepository.GetRoomInformationByRoomNumber(roomNumber);
            }


            public RoomInformation GetRoomInformationById(int id)
            {
                return _roomInfomationRepository.GetRoomInformationById(id);
            }

            public void UpdateRoomInformation(RoomInformation room)
            {
                _roomInfomationRepository.UpdateRoomInformation(room);
            }

        }
    }
