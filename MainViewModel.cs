using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo
{
    /// <summary>
    /// Every viewmodel in MVVM Light Toolkit must inherit from ViewmodelBase class
    /// 
    /// </summary>
    public class MainViewModel: ViewModelBase
    {
        /// <summary>
        /// 1st property
        /// </summary>
        private ObservableCollection<Sensor> sensors;
        /// <summary>
        /// 2nd property
        /// </summary>
        private Sensor selectedSensor;
        /// <summary>
        /// 
        /// </summary>
        public ICommand LoadSensorsCommand
        {
            get; private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public ICommand SaveSensorsCommand
        {
            get; private set;
        }

        public MainViewModel()
        {
            LoadSensorsCommand = new RelayCommand(LoadSensorMethod);
            SaveSensorsCommand = new RelayCommand(SaveSensorMethod);
        }
        /// <summary>
        /// Implement load sensor method
        /// </summary>
        private void LoadSensorMethod()
        {
            //throw new NotImplementedException();
            sensors = Sensor.GetSampleSensors();
            this.RaisePropertyChanged(() => this.SensorList);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Sensor loaded"));
        }
        /// <summary>
        /// Implement save sensor method
        /// </summary>
        private void SaveSensorMethod()
        {
            // throw new NotImplementedException();
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Sensor saved"));
        }

        public ObservableCollection<Sensor> SensorList
        {
            get
            {
                return sensors;
            }
        }

        public Sensor SelectedSensor
        {
            get
            {
                return selectedSensor;
            }
            set
            {
                selectedSensor = value;
                RaisePropertyChanged("SelectedSensor"); // important thing for update UI
            }
        }

    }
}
