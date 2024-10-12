using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }
        public IConfiguration? configuration;



        protected override void OnStartup(StartupEventArgs e)
        {

            var serviceCollection = new ServiceCollection();
            ConfigureService(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var login = ServiceProvider.GetRequiredService<LoginPage>();
            login.Show();


        }

        private void ConfigureService(IServiceCollection services)
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            services.AddTransient<LoginPage>();
            services.AddTransient<HomePage>();
            services.AddTransient<AdminPage>();
            services.AddTransient<ProfilePage>();
            services.AddTransient<BookingHistoryPage>();
            services.AddTransient<ManageRoomPage>();
            services.AddTransient<ManageRoomTypePage>();
            services.AddTransient<ManageCustomerPage>();
            services.AddTransient<Booking>();
            services.AddTransient<BookingHistoryPageForCustomer>(); 

            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IBookingHistoryService, BookingHistoryService>();
            services.AddScoped<IBookingHistoryRepository, BookingHistoryRepository>();



        }



    }

}
