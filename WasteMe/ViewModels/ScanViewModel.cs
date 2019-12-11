using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace WasteMe.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        public ScanViewModel()
        {
            Title = "About";

            ScanCommand = new Command(() => Scan());
        }

        public ICommand ScanCommand { get; }

        private void Scan()
        {
            Xamarin.Forms.DependencyService.Register<IScanService>();
            DependencyService.Get<IScanService>().StartScan();
        }
    }
}