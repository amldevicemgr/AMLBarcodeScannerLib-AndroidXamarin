using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib
{
    class ScannerProperties
    {
        //////////////////////////////////////////////////////////////////
        //                                                              //
        //  Key name strings for scanner configuration/settings below.  //
        //                                                              //
        //////////////////////////////////////////////////////////////////

        // Each string is a key name for a corresponding value, that should be set to a Bundle.
        // The Bundle should be passed to the service using an Intent.

        // NOTE: These settings are ONLY managed in the scanner service.
        // They are not programmed into the scan engine hardware.

        //Key name for enabling/disabling the scanner.
        // Requires a Boolean value type. True for on, false for off.
        public const string PROPERTY_NAME_SCANNER_ENABLED = "Enabled";

        //Key name for enabling/disabling wifi, url, and bluetooth scanning.
        //Requires a Boolean value type. True for on, false for off.
        public const string PROPERTY_BARCODE_ACTIONS_ENABLE = "Enable";


        //Key name for the two ways the scanner service can output a barcode.
        // Requires Long value type. This is a FLAG value type - one, all, or none of the flag values
        // can be set. Set zero '0' for none.
        public const string PROPERTY_NAME_SCANNER_MODE_FLAGS = "Mode";
        //For PROPERTY_NAME_SCANNER_MODE_FLAGS:
        // Scanner service will auto type barcode into focused field for the activity displayed.
        public const int FLAG_SCANNER_MODE_KEYBOARD_WEDGE = 1;
        // Scanner service will broadcast intents (keep this mode set to receive events).
        // Set this mode to receive ACTION_SCANNED, ACTION_TRIGGER_PULLED, and ACTION_ERROR
        public const int FLAG_SCANNER_MODE_INTENT = 1 << 1;
        public const int FLAG_SCANNER_MODE_BARCODE_ACTIONS = 1;

        //Key name for the two ways the Keyboard Wedge can operated (if wedge is enabled).
        // Requires Int value type.
        public const string PROPERTY_NAME_KEYBOARD_WEDGE_MODE = "Wedge";
        //For PROPERTY_NAME_KEYBOARD_WEDGE_MODE:
        // Whole barcode string is set directly to focused field.
        public const int KEYBOARD_WEDGE_MODE_FAST_INPUT = 0;
        // Each barcode character is typed as a virtual key press.
        public const int KEYBOARD_WEDGE_MODE_TRUE_KEY_PRESS = 1;

        //Key names for Prefix and Suffix of barcode.
        // Requires String value type.
        // The string set for this value will be inserted at the beginning of the barcode.
        // To use non-printable ASCII characters in the prefix, use the following format:
        // \NN - where NN is the double digit hex number representation of the character.
        // Example: "\\02" (STX) double backslash is required to escape the backslash.
        public const string PROPERTY_NAME_PREFIX = "Prefix";
        // Requires String value type.
        // The string set for this value will be appended to the end of the barcode.
        // Same prefix ASCII character rules apply to suffix.
        public const string PROPERTY_NAME_SUFFIX = "Suffix";

        //Key name for Url path to ringtone that will play after a barcode is scanned.
        // Requires String value type.
        // The String must be a Url path for a ringtone on the device.
        public const string PROPERTY_NAME_SOUND = "SoundPath";



        // NOTE: The settings bellow are NOT ONLY managed in the scanner service, they also effect the
        // scan engine hardware. If these settings are used, then any manually scanned configuration
        // barcodes used, to program the scan engine, are overwritten.
        // IMPORTANT: To used these settings, the "Managed" setting must be set to true, otherwise set
        // to false to have all these settings ignored and, as a result, enable the scan engine to be
        // configured manually with barcodes.

        //Key name for allowing scanner service to manage special settings that would otherwise be
        // handled by configuring the scan engine manually.
        // Requires Boolean. Set true to allow service to manage these settings, otherwise false.
        public const string PROPERTY_NAME_MANAGED_ENABLED = "Managed";

        //Key name for setting barcode formats to apply on outgoing barcode data.
        // Requires Long value type. This is a FLAG value type.
        public const string PROPERTY_NAME_BARCODE_FORMAT_FLAGS = "BarcodeFormat";
        //For PROPERTY_NAME_BARCODE_FORMAT_FLAGS:
        // The AIM standard 3 character length code will be inserted at the beginning of the barcode.
        // If there is a custom prefix already set, the AIM ID will be appended to the end of the
        // custom prefix but before the barcode string.
        public const int FLAG_BARCODE_FORMAT_AIM_ID = 1;
        //Key name for enabling/disabling scan key event mode
        public const string PROPERTY_NAME_SCAN_KEY_EVENT = "ScanKeyEvent";
        //Key name for enabling/disabling continuous scan mode for EX30 and some Zebra engines
        public const string PROPERTY_NAME_CONT_SCAN_MODE = "ContScanMode";
        //Key name for enabling/disabling LED light flash on scan for the FIREBIRD model only
        public const string PROPERTY_NAME_LED_FLASH_SCAN = "LEDFlashScan";
        public const string PROPERTY_NAME_SCANNER_OP_FLAGS = "OpFlags";
        public const long FLAG_SCANNER_OP_PICK = 1;

        //Key name for setting kiosk presentation mode for FIREBIRD model only
        public const string PROPERTY_NAME_PRESENTATION_FLAGS = "PresentationMode";

        public const long FLAG_SCANNER_OP_NORMAL_PRESENTATION = 2;
        public const long FLAG_SCANNER_OP_MANUAL_TRIGGER = 0;
        public const long FLAG_SCANNER_OP_MOBILE_PRESENTATION = 3;
        public const long FLAG_SCANNER_OP_STREAMING_PRESENTATION = 4;

        //Used for hiding keyboard on scan
        public const string PROPERTY_NAME_FLAGS = "Flags";
        public const int FLAG_HIDE_IME_ON_SCAN = 1;
    }
}
