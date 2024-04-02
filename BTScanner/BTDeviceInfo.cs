using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMLBarcodeScannerLib.BTScanner
{
    public class BTDeviceInfo
    {
        private string? Name;
        private string? Address;
        private string ServiceUuid;
        private string? Battery;
        private bool? IsConnected;

        public string? GetBTName()
        {
            return Name;
        }

        public string? GetBtAddress()
        {
            return Address;
        }

        public string? GetBattery()
        {
            return Battery;
        }

        public string? GetServiceUuid()
        {
            return ServiceUuid;
        }

        public bool? IsScannerConnected()
        {
            return IsConnected;
        }
    }
}