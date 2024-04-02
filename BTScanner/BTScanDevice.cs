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
    public class BTScanDevice
    {
        private string? name;
        private string? address;
        private string? serviceuuid;

        public string? GetBTName()
        {
            return name;
        }

        public string? GetBTAddress()
        {
            return address;
        }
        
        public string? GetServiceUuid()
        {
            return serviceuuid;
        }
    }
}