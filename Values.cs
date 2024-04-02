using Android.OS;

namespace AMLBarcodeScannerLib
{
    class Values
    {
        //Barcode service package/class strings for Intent
        public const string PACKAGE_NAME = "com.amltd.amlbarcodescanner";
        public const string CLASS_NAME = PACKAGE_NAME + ".BarcodeScannerService";

        // Action string received if the incoming data is an error message.
        public const string ACTION_ERROR = PACKAGE_NAME + ".ERROR";
        // Action string received if the incoming data is a barcode.
        public const string ACTION_SCANNED = PACKAGE_NAME + ".SCANNED";
        // Action string received if the trigger is pulled (no data).
        public const string ACTION_TRIGGER_PULLED = ".TRIGGER_PULLED";
        // Action string received if the trigger is released (no data).
        public const string ACTION_TRIGGER_RELEASED = ".TRIGGER_RELEASED";
        // The key name to use when getting barcode or error message string from the Intent's extras.
        public const string EXTRA_DATA = "d";
        public const string EXTRA_RAW_DATA = "raw";

        // Set this action to programmatically release scan trigger
        public const string ACTION_KEY_SCAN_UP = "key.scan.up";
        // Set this action to programmatically pull scan trigger
        public const string ACTION_KEY_SCAN_DOWN = "key.scan.down";

        // Set this action to programmatically show the virtual keyboard
        public const string ACTION_SHOW_IME = PACKAGE_NAME + ".SHOW_IME";
        // Set this action to programmatically hide the virtual keyboard
        public const string ACTION_HIDE_IME = PACKAGE_NAME + ".HIDE_IME";

        // Action string received if a ring scanner was connected.
        public const string ACTION_RING_SCANNER_CONNECTED = PACKAGE_NAME + ".RING_SCANNER_CONNECTED";
        // Action string received if a ring scanner was connected.
        public const string ACTION_RING_SCANNER_DISCONNECTED = PACKAGE_NAME + ".RING_SCANNER_DISCONNECTED";
        // Action string received if a ring scanner has low battery.
        public const string ACTION_RING_SCANNER_LOW_BATTERY = PACKAGE_NAME + ".RING_SCANNER_LOW_BATTERY";
        // The key name to use when getting ring scanner for connection or disconnection.
        public const string EXTRA_RING_SCANNER = "RING_SCANNER";
        // The key name to use when getting ring scanner low battery message.
        public const string EXTRA_RING_SCANNER_LOW_BATTERY = "RING_SCANNER_LOW_BATTERY";
        public const string GET_BT_DEVICE_INFO_ACTION = PACKAGE_NAME + ".GET_BT_DEVICE_INFO";
        public const string EXTRA_BT_DEVICE_INFO = "PACKAGE_NAME";
    }
}