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
            LoadRoomType();
        }
        
        public void SetupRoomDataGrid()
        {
            dgRoom.AutoGenerateColumns = false; // Tắt tự động tạo cột

            // Xóa tất cả các cột trước khi thêm mới
            dgRoom.Columns.Clear();

            // Thêm các cột theo thứ tự mong muốn
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room ID", Binding = new Binding("RoomID") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Number", Binding = new Binding("RoomNumber") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Type Name", Binding = new Binding("RoomType.RoomTypeName") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Description", Binding = new Binding("RoomDescription") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Max Capacity", Binding = new Binding("RoomMaxCapacity") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Status", Binding = new Binding("RoomStatus") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Price Per Date", Binding = new Binding("RoomPricePerDate") });

            // Đặt nguồn dữ liệu cho DataGrid
            dgRoom.ItemsSource = _roomService.GetAllRoomsIncludeRoomType();
        }


        public void LoadRoomType()
        {
            var roomTypes = _roomTypeService.GetAllRoomTypes();
            cboRoomType.ItemsSource = roomTypes;
            cboRoomType.DisplayMemberPath = "RoomTypeName";
            cboRoomType.SelectedValuePath = "RoomTypeID";
            cboRoomType.SelectedIndex = 0;
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
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }

        private void cboRoomtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboRoom.ItemsSource = _roomService.GetAllRooms().Where(r => r.RoomTypeID == (int)cboRoomType.SelectedValue && r.RoomStatus == 1)
                .Select(r => r.RoomNumber);
            cboRoom.SelectedIndex = 0;
            txtStatus.Text = "Available";
        }

        private void txtNumberPeople_TextChanged(object sender, TextChangedEventArgs e)
        {
            var max = _roomService.GetRoomByRoomNumber(cboRoom.SelectedValue.ToString()).RoomMaxCapacity;
            if (int.TryParse(txtNumberPeople.Text, out int numberPeople))
            {
                if (numberPeople > max)
                {   
                    MessageBox.Show("Please enter a number less than or equal to " + max);
                    txtNumberPeople.Text = max.ToString();
                    return;
                }
            }

            lbRoomPrice.Content = "Room Price: " + _roomService.GetRoomByRoomNumber(cboRoom.SelectedValue.ToString()).RoomPricePerDate.ToString();

        }
    }
}
