using BussinessObjects;
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
    /// Interaction logic for ManageRoomPage.xaml
    /// </summary>
    public partial class ManageRoomPage : Window
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;

        public ManageRoomPage(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            InitializeComponent();
            _roomService = roomService;
            _roomTypeService = roomTypeService;
        }

        public void LoadRoom()
        {
            var rooms = _roomService.GetAllRooms();
            dgRoom.ItemsSource = null;
            SetupRoomDataGrid();
            LoadRoomType();
        }

        public void LoadRoomType()
        {
            var roomTypes = _roomTypeService.GetAllRoomTypes();
            cboRoomType.ItemsSource = null;
            cboRoomType.ItemsSource = roomTypes;
            cboRoomType.DisplayMemberPath = "RoomTypeName";
            cboRoomType.SelectedValuePath = "RoomTypeID";
            cboRoomType.SelectedIndex = 0;
        }

        public void SetupRoomDataGrid()
        {
            dgRoom.AutoGenerateColumns = false; // Tắt tự động tạo cột

            // Xóa tất cả các cột trước khi thêm mới
            dgRoom.Columns.Clear();

            // Thêm các cột theo thứ tự mong muốn
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Number", Binding = new Binding("RoomNumber") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Type Name", Binding = new Binding("RoomType.RoomTypeName") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Max Capacity", Binding = new Binding("RoomMaxCapacity") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Status", Binding = new Binding("RoomStatus") });
        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var room = dgRoom.SelectedItem as Room;
            if (room != null)
            {
                txtRoomNumber.Text = room.RoomNumber;
                cboRoomType.SelectedValue = room.RoomTypeID;
                txtMaxCapacity.Text = room.RoomMaxCapacity.ToString();
                txtStatus.Text = room.RoomStatus.ToString();
                txtDescribe.Text = room.RoomDescription;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomNumber.Text != "" && txtMaxCapacity.Text != "" && txtStatus.Text != "" && txtDescribe.Text != "")
            {
                var roomNumber = txtRoomNumber.Text;
                var roomType = cboRoomType.SelectedValue.ToString();
                var maxCapacity = int.Parse(txtMaxCapacity.Text);
                var status = txtStatus.Text;
                var describe = txtDescribe.Text;

                var room = _roomService.GetRoomByRoomNumber(roomNumber);
                room.RoomNumber = roomNumber;
                room.RoomTypeID = int.Parse(roomType);
                room.RoomMaxCapacity = maxCapacity;
                room.RoomStatus = int.Parse(status);
                room.RoomDescription = describe;

                _roomService.UpdateRoom(room);
                LoadRoom();

                MessageBox.Show("Update successfully!");
            }
            else
            {
                MessageBox.Show("Please fill all fields!");
            }

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomNumber.Text != "" && txtMaxCapacity.Text != "" && txtStatus.Text != "" && txtDescribe.Text != "")
            {
                var roomNumber = txtRoomNumber.Text;
                var roomType = cboRoomType.SelectedValue.ToString();
                var maxCapacity = int.Parse(txtMaxCapacity.Text);
                var status = txtStatus.Text;
                var describe = txtDescribe.Text;

                var room = new Room
                {
                    RoomNumber = roomNumber,
                    RoomTypeID = int.Parse(roomType),
                    RoomMaxCapacity = maxCapacity,
                    RoomStatus = int.Parse(status),
                    RoomDescription = describe
                };

                _roomService.AddRoom(room);
                LoadRoom();

                MessageBox.Show("Create successfully!");
            }
            else
            {
                MessageBox.Show("Please fill all fields!");
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomNumber.Text != "" && txtMaxCapacity.Text != "" && txtStatus.Text != "" && txtDescribe.Text != "")
            {
                var room = _roomService.GetRoomByRoomNumber(txtRoomNumber.Text);
                if (room == null)
                {
                    MessageBox.Show("Room not found");
                    return;
                }
                _roomService.DeleteRoom(room.RoomID);
                LoadRoom();
                MessageBox.Show("Delete successfully!");
            }
            else
            {
                MessageBox.Show("Please select room!");
            }

        }

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }
    }
}
