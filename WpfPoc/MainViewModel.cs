using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPoc
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        string firstName = "";
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        string lastName = "";
        
        public string FullName => $"{FirstName} {LastName}";

        [RelayCommand]
        void Submit()
        {
            MessageBox.Show($"Hello {FullName}");
        }

        [RelayCommand]
        Task DoIt()
        {
            Submit();
            return Task.CompletedTask;
        }
    }
}
