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
    /// Interaction logic for ManageRoomTypePage.xaml
    /// </summary>
    public partial class ManageRoomTypePage : Window
    {

        private readonly IRoomTypeService _roomTypeService;
        public ManageRoomTypePage(IRoomTypeService roomTypeService)
        {
            InitializeComponent();
            _roomTypeService = roomTypeService;
            LoadRoomType();
        }


        public void LoadRoomType()
        {
            var roomTypes = _roomTypeService.GetAllRoomTypes();
            dgRoomType.ItemsSource = null;
            dgRoomType.ItemsSource = _roomTypeService.GetAllRoomTypes();
        }

        public void Clear()
        {
            txtRoomTypeID.Text = "";
            txtRoomTypeName.Text = "";
            txtTypeDescription.Text = "";
            txtTypeNote.Text = "";

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomTypeName.Text != "" && txtTypeDescription.Text != "" && txtTypeNote.Text != "")
            {
                var updateRoomTypeName = txtRoomTypeName.Text;
                var updateTypeDescription = txtTypeDescription.Text;
                var updateTypeNote = txtTypeNote.Text;
                var updateTypeID = txtRoomTypeID.Text;

                var roomType = _roomTypeService.GetRoomTypeById(Convert.ToInt32(updateTypeID));

                roomType.RoomTypeName = updateRoomTypeName;
                roomType.TypeDescription = updateTypeDescription;
                roomType.TypeNote = updateTypeNote;

                _roomTypeService.UpdateRoomType(roomType);
                LoadRoomType();
                Clear();
                
                MessageBox.Show("Update Room Type Successfully");

            }
            else
            {
                MessageBox.Show("Please fill all the fields");
            }


        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomTypeName.Text != "" && txtTypeDescription.Text != "" && txtTypeNote.Text != "")
            {
                var newRoomType = new RoomType
                {
                    RoomTypeName = txtRoomTypeName.Text,
                    TypeDescription = txtTypeDescription.Text,
                    TypeNote = txtTypeNote.Text
                };

                _roomTypeService.AddRoomType(newRoomType);
                LoadRoomType();
                Clear();
                MessageBox.Show("Create Room Type Successfully");
            }
            else
            {
                MessageBox.Show("Please fill all the fields");
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtRoomTypeName.Text != "" && txtTypeDescription.Text != "" && txtTypeNote.Text != "")
            {
                var roomType = _roomTypeService.GetRoomTypeById(Convert.ToInt32(txtRoomTypeID.Text));
                if (roomType == null)
                {
                    MessageBox.Show("Room Type not found");
                    return;
                }
                _roomTypeService.DeleteRoomType(roomType.RoomTypeId);
                LoadRoomType();
                Clear();
                MessageBox.Show("Delete Room Type Successfully");
            }
            else
            {
                MessageBox.Show("Please select Room Type");
            }

        }

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {
            var adminPage = new AdminPage();
            adminPage.Show();
            this.Close();

        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var roomType = dgRoomType.SelectedItem as RoomType;
            if (roomType != null)
            {
                txtRoomTypeID.Text = roomType.RoomTypeId.ToString();
                txtRoomTypeName.Text = roomType.RoomTypeName;
                txtTypeDescription.Text = roomType.TypeDescription;
                txtTypeNote.Text = roomType.TypeNote;
            }

        }
    }
}
