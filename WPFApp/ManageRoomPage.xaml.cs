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
        private readonly IRoomInformationService _roomInformationService;
        private readonly IRoomTypeService _roomTypeService;

        public ManageRoomPage(IRoomInformationService roomInformationService, IRoomTypeService roomTypeService)
        {
            InitializeComponent();
            _roomInformationService = roomInformationService;
            _roomTypeService = roomTypeService;
            LoadRoom();
        }

        public void LoadRoom()
        {
            var rooms = _roomInformationService.GetAllRoomInformations();
            dgRoom.ItemsSource = null;
            dgRoom.ItemsSource = rooms;
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

        public void Clear()
        {
            txtDescribe.Text = "";
            txtMaxCapacity.Text = "";
            txtRoomNumber.Text = "";
            txtStatus.Text = "";

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
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Max Capacity", Binding = new Binding("RoomMaxCapacity") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Status", Binding = new Binding("RoomStatus") });


        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var roomInformation = dgRoom.SelectedItem as RoomInformation;
            if (roomInformation != null)
            {
                txtRoomNumber.Text = roomInformation.RoomNumber;
                cboRoomType.SelectedValue = roomInformation.RoomTypeId;
                txtMaxCapacity.Text = roomInformation.RoomMaxCapacity.ToString();
                txtStatus.Text = roomInformation.RoomStatus.ToString();
                txtDescribe.Text = roomInformation.RoomDetailDescription;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomNumber.Text != "" && txtMaxCapacity.Text != "" && txtStatus.Text != "" && txtDescribe.Text != "")
            {
                try
                {
                    var roomID = (dgRoom.SelectedItem as RoomInformation).RoomId;
                    var roomNumber = txtRoomNumber.Text;
                    var roomType = cboRoomType.SelectedValue.ToString();
                    var maxCapacity = int.Parse(txtMaxCapacity.Text);
                    var status = txtStatus.Text;
                    var describe = txtDescribe.Text;

                    var room = _roomInformationService.GetRoomInformationById(roomID);
                    room.RoomNumber = roomNumber;
                    room.RoomTypeId = int.Parse(roomType);
                    room.RoomMaxCapacity = maxCapacity;
                    room.RoomStatus = byte.Parse(status);
                    room.RoomDetailDescription = describe;

                    _roomInformationService.UpdateRoomInformation(room);
                    LoadRoom();

                    MessageBox.Show("Update successfully!");
                    Clear();
                } catch (Exception ex)
                {
                    MessageBox.Show("Please choose room from the list!");
                }
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
                var describe = txtDescribe.Text;

                if (_roomInformationService.GetRoomInformationByRoomNumber(roomNumber) != null)
                {
                    MessageBox.Show("Room Number is existed");
                    return;
                }

                var room = new RoomInformation
                {
                    RoomNumber = roomNumber,
                    RoomTypeId = int.Parse(roomType),
                    RoomMaxCapacity = maxCapacity,
                    RoomStatus = 1,
                    RoomDetailDescription = describe
                };

                _roomInformationService.AddRoomInformation(room);
                LoadRoom();
                Clear();

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
                var room = _roomInformationService.GetRoomInformationByRoomNumber(txtRoomNumber.Text);
                if (room == null)
                {
                    MessageBox.Show("Room not found");
                    return;
                }
                _roomInformationService.DeleteRoomInformation(room.RoomId);
                LoadRoom();
                Clear();
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
