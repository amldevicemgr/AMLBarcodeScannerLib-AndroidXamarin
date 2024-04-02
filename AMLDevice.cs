using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
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

        /// <summary>
        /// Checks whether the device is an AML device.
        /// Returns true if the device is AML, otherwise false.
        /// </summary>
        public static bool IsAMLDevice()
        {
            string model = Build.Model;
            return model.Equals("M7700") || model.Equals("M7800") || model.Equals("M6500") || model.Equals("KDT7") || model.Equals("M7800 BATCH");
        }

        /// <summary>
        /// Checks whether the device supports BT Scanner.
        /// Returns true if the device supports BT Scanner, otherwise false.
        /// </summary>
        public static bool IsBTScannerSupported(Context context)
        {
            try
            {
                PackageInfo packageInfo = context.PackageManager.GetPackageInfo("com.amltd.amlbarcodescanner", 0);
                return packageInfo.VersionCode >= 179 && IsAMLDevice();
            }
            catch (Exception e)
            {
                Log.Debug("AMLBarcodeScannerLib", e.StackTrace);
            }
            return false;
        }

        /// <summary>
        /// Checks whether the device supports BT Scanner.
        /// Returns the version of AML Barcode Scanner if it is installed, otherwise -1.
        /// </summary>
        public static int GetAMLBarcodeScannerVersion(Context context)
        {
            try
            {
                PackageInfo packageInfo = context.PackageManager.GetPackageInfo("com.amltd.amlbarcodescanner", 0);
                return packageInfo.VersionCode;
            }
            catch (Exception e)
            {
                Log.Debug("AMLBarcodeScannerLib", e.StackTrace);
            }
            return -1;
        }
    }
}