using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemo
{
    /// <summary>
    /// Every model class in this MVVM Light Toolkit must inherit from Observable Object
    /// Observable Object class is inherit from the InofityPropertyChange interface
    /// This interface provide Propertychange event handler that notify clients that a property value changed
    /// Observable object use that event in raise property change method to notify
    /// </summary>
    public class Sensor: ObservableObject
    {
        /// <summary>
        /// Id of sensor 
        /// </summary>
        private int id;
        /// <summary>
        /// Name of sensor
        /// </summary>
        private string name;
        /// <summary>
        /// Description of sensor
        /// </summary>
        private string description;
        /// <summary>
        /// Value of sensor
        /// </summary>
        private double value;


        /// <summary>
        /// All properties setter used the Set method of ObservableObject
        /// Set method assign a new value to the property and then call RaisePropertyChange method
        /// This RaisePropertyChange method is must as we have to update the UI when any property change
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                //id = value;
                Set<int>(() => this.ID, ref id, value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                //name = value;
                Set<string>(() => this.Name, ref name, value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                //description = value;
                Set<string>(()=> this.Description, ref description, value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get
            {
                return value;
            }
            set
            {
                //this.value = value;
                Set<double>(()=> this.Value, ref value, value);
            }
        }
        /// <summary>
        /// Create a temporary sensor list in memory and return an observableCollection of Sensor class
        /// I have used that list for binding to the UI so that we dont need to use the database to loading and saving the sensor details
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Sensor> GetSampleSensors()
        {
            ObservableCollection<Sensor> sensors = new ObservableCollection<Sensor>();
            for (int i = 0; i < 300; i++)
            {
                sensors.Add(new Sensor
                {
                    ID = i + 1,
                    Name = "Sensor " +(i+1).ToString(),
                    Description = "no description",
                    Value = 100 + (i+1)*10
                }
                ) ;
            }
            return sensors;
        }
    }
}
