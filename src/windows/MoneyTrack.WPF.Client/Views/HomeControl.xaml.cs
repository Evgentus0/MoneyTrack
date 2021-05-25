using MoneyTrack.WPF.Client.Models;
using MoneyTrack.WPF.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.WPF.Infrastructure.Settings;
using AutoMapper;

namespace MoneyTrack.WPF.Client.Views
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        private readonly IAppService _appService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public HomeControl(IAppService appService, AppSettings appSettings, IMapper mapper)
        {
            InitializeComponent();

            _appService = appService;
            _appSettings = appSettings;
            _mapper = mapper;
            Initialization = BuildDataModel();
            

            DataContext = Data;
        }

        public HomeControl()
        {}

        public Task Initialization { get; private set; }

        public HomeModel Data { get; set; }

        private async Task BuildDataModel()
        {
            Task.Delay(10000000).Wait();

            Data =  new HomeModel
            {
                Accounts = await _appService.GetAllAccounts(),
                Categories = await _appService.GetAllCategories(),
                LastTransactions = _mapper.Map<List<TransactionModel>>(await _appService
                    .GetLastTransactions(_appSettings.NumberOfLastTransaction))
            };
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
