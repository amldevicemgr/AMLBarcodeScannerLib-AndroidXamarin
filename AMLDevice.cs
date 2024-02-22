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

namespace AMLBarcodeScannerLib
{
    public class AMLDevice
    {
        /// <summary>
        /// Gets the AML device model.
        /// Returns the model string or null if the device is not an AML device.
        /// </summary>
        public static string GetAMLModel()
        {
            string model = Build.Model;
            string amlModel = null;
            switch (model)
            {
                case "M7700":
                    amlModel = "M7700";
                    break;
                case "M7800":
                    amlModel = "M7800";
                    break;
                case "M6500":
                    amlModel = "M6500";
                    break;
                case "KDT7":
                    amlModel = "KDT7";
                    break;
                case "M7800 BATCH":
                    amlModel = "M7800 BATCH";
                    break;                    
            }
            return amlModel;
        }
    }
}