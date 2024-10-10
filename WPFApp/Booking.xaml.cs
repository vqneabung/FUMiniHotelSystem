using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Window
    {   
        public readonly IRoomService _roomService;
        public readonly IRoomTypeService _roomTypeService;


        public Booking(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            InitializeComponent();
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            LoadRoom();
        }

        public void LoadRoom()
        {
            var rooms = _roomService.GetAllRooms();
            dgRoom.ItemsSource = null;
            SetupRoomDataGrid();
        }
        
        public void SetupRoomDataGrid()
        {
            dgRoom.ItemsSource = _roomService.GetAllRooms();

            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Type ID", Binding = new Binding("RoomType.RoomTypeName") });

        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFindCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
