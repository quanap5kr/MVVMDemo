using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMDemo
{
    /// <summary>
    /// ViewModelLocator class is the mediator between your UI and ViewModels that binds the viewModels to UI
    /// If you want to use your ViewModel as binding source for UI, you must expose that ViewModel as property in the ViewModelLocator class
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        private void NotifyUserMethod(NotificationMessage obj)
        {
            MessageBox.Show(obj.Notification);
        }
    }
}
