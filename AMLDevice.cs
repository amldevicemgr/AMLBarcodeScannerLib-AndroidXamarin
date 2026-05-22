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
        /// Checks whether the device is an AML device.
        /// Returns true if the device is AML, otherwise false.
        /// </summary>
        public static bool IsAMLDevice()
        {
            string manufacturer = Build.Manufacturer;

            return manufacturer.Equals("AML") || manufacturer.Equals("American Microsystems LTD.");
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