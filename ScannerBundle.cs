using Android.OS;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib
{
    class ScannerBundle
    {
        protected Bundle Bundle;
        public Bundle GetBundle() { return Bundle; }

        public ScannerBundle()
        {
            Bundle = new Bundle();
        }
        public ScannerBundle(Bundle bundle)
        {
            Bundle = bundle;
        }

        //Using "bool?", "int?", "long?" for properties/settings because they are nullable. 
        // Null is the "not set"/"no changes" value (false and zero are meaningful values).
        // Also, null will be returned when a value does not exist

        public string? Prefix
        {
            get => GetString(ScannerProperties.PROPERTY_NAME_PREFIX);
            set => Set(ScannerProperties.PROPERTY_NAME_PREFIX, value);
        }
        public string? Sound
        {
            get => GetString(ScannerProperties.PROPERTY_NAME_SOUND);
            set => Set(ScannerProperties.PROPERTY_NAME_SOUND, value);
        }
        public string? Suffix
        {
            get => GetString(ScannerProperties.PROPERTY_NAME_SUFFIX);
            set => Set(ScannerProperties.PROPERTY_NAME_SUFFIX, value);
        }
        public bool? Managed
        {
            get => GetBool(ScannerProperties.PROPERTY_NAME_MANAGED_ENABLED);
            set => Set(ScannerProperties.PROPERTY_NAME_MANAGED_ENABLED, value);
        }
        public bool? ScannerEnabled
        {
            get => GetBool(ScannerProperties.PROPERTY_NAME_SCANNER_ENABLED);
            set => Set(ScannerProperties.PROPERTY_NAME_SCANNER_ENABLED, value);
        }
        public int? KeyboardWedgeMode
        {
            get => GetInt(ScannerProperties.PROPERTY_NAME_KEYBOARD_WEDGE_MODE);
            set => Set(ScannerProperties.PROPERTY_NAME_KEYBOARD_WEDGE_MODE, value);
        }
        public long? BarcodeFormatFlags
        {
            get => GetLong(ScannerProperties.PROPERTY_NAME_BARCODE_FORMAT_FLAGS);
            set => Set(ScannerProperties.PROPERTY_NAME_BARCODE_FORMAT_FLAGS, value);
        }
        public long? ScannerModeFlags
        {
            get => GetLong(ScannerProperties.PROPERTY_NAME_SCANNER_MODE_FLAGS);
            set => Set(ScannerProperties.PROPERTY_NAME_SCANNER_MODE_FLAGS, value);
        }
        public long? OpFlags
        {
            get => GetLong(ScannerProperties.PROPERTY_NAME_SCANNER_OP_FLAGS);
            set => Set(ScannerProperties.PROPERTY_NAME_SCANNER_OP_FLAGS, value);
        }
        public bool? BarcodeActions
        {
            get => GetBool(ScannerProperties.PROPERTY_BARCODE_ACTIONS_ENABLE);
            set => Set(ScannerProperties.PROPERTY_BARCODE_ACTIONS_ENABLE, value);
        }
        public bool? ScanKeyEvent
        {
            get => GetBool(ScannerProperties.PROPERTY_NAME_SCAN_KEY_EVENT);
            set => Set(ScannerProperties.PROPERTY_NAME_SCAN_KEY_EVENT, value);
        }
        public bool? ContinuousScan
        {
            get => GetBool(ScannerProperties.PROPERTY_NAME_CONT_SCAN_MODE);
            set => Set(ScannerProperties.PROPERTY_NAME_CONT_SCAN_MODE, value);
        }
        public bool? LEDFlash
        {
            get => GetBool(ScannerProperties.PROPERTY_NAME_LED_FLASH_SCAN);
            set => Set(ScannerProperties.PROPERTY_NAME_LED_FLASH_SCAN, value);
        }
        public long? PresentationMode
        {
            get => GetLong(ScannerProperties.PROPERTY_NAME_PRESENTATION_FLAGS);
            set => Set(ScannerProperties.PROPERTY_NAME_PRESENTATION_FLAGS, value);
        }
        public long? HideKeyboard
        {
            get => GetLong(ScannerProperties.PROPERTY_NAME_FLAGS);
            set => Set(ScannerProperties.PROPERTY_NAME_FLAGS, value);
        }

        public string GetString(string name) { return Bundle.GetString(name); }
        public bool? GetBool(string name) { return (bool?)Bundle.Get(name); }
        public int? GetInt(string name) { return (int?)Bundle.Get(name); }
        public long? GetLong(string name) { return (long?)Bundle.Get(name); }

        public void Set(string name, string val) { if (val != null) Bundle.PutString(name, val); }
        public void Set(string name, bool? val) { if (val != null) Bundle.PutBoolean(name, (bool)val); }
        public void Set(string name, int? val) { if (val != null) Bundle.PutInt(name, (int)val); }
        public void Set(string name, long? val) { if (val != null) Bundle.PutLong(name, (long)val); }       
    }
}
