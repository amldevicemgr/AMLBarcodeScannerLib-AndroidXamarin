using AMLBarcodeScannerLib.Settings;
using Android.OS;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib
{
    class SettingsHelper
    {
        public static ScannerSettings ParseScannerSettings(Bundle bundle)
        {
            if (bundle == null)
                return new ScannerSettings();
            else
            {
                ScannerSettings scannerSettings = new ScannerSettings();
                ScannerBundle scannerBundle = new ScannerBundle(bundle);
                string? prefix = scannerBundle.Prefix;
                if (prefix != null)
                    scannerSettings.SetPrefix(prefix);
                string? suffix = scannerBundle.Suffix;
                if (suffix != null)
                    scannerSettings.SetSuffix(suffix);
                string? sound = scannerBundle.Sound;
                if (sound != null)
                    scannerSettings.SetScanSound(sound);
                int? kwMode = scannerBundle.KeyboardWedgeMode;
                if (kwMode != null)
                {
                    KeyboardWedgeMode wMode = ParseKeyboardWedgeMode(kwMode);
                    scannerSettings.SetKeyboardWedgeMode(wMode);
                }
                if (scannerBundle.ScannerEnabled != null)
                    scannerSettings.SetScannerEnabled((bool)scannerBundle.ScannerEnabled);
                if (scannerBundle.BarcodeActions != null)
                    scannerSettings.SetBarcodeActionsEnabled((bool)scannerBundle.BarcodeActions);
                if (scannerBundle.ContinuousScan != null)
                    scannerSettings.SetContinuousScanEnabled((bool)scannerBundle.ContinuousScan);
                if (scannerBundle.LEDFlash != null)
                    scannerSettings.SetLEDFlashOnScanEnabled((bool)scannerBundle.LEDFlash);
                if (scannerBundle.ScanKeyEvent != null)
                    scannerSettings.SetScanKeyEventModeEnabled((bool)scannerBundle.ScanKeyEvent);
                if (scannerBundle.Managed != null)
                    scannerSettings.SetManagedModeEnabled((bool)scannerBundle.Managed);
                if (scannerBundle.HideKeyboard != null)
                    scannerSettings.SetHideKeyboardOnScanEnabled(ParseHideKeyboardOnScan((long)scannerBundle.HideKeyboard));
                if (scannerBundle.PresentationMode != null)
                    scannerSettings.SetPresentationMode(ParsePresentationMode((long)scannerBundle.PresentationMode));
                if (scannerBundle.ScannerModeFlags != null)
                    scannerSettings.SetKeyboardWedgeEnabled(ParseKeyboardWedgeEnabled((long)scannerBundle.ScannerModeFlags));
                if (scannerBundle.BarcodeFormatFlags != null)
                    scannerSettings.SetAimIdEnabled(ParseAimIdEnabled((long)scannerBundle.BarcodeFormatFlags));
                if (scannerBundle.OpFlags != null)
                    scannerSettings.SetPicklistModeEnabled(ParsePicklistModeEnabled((long)scannerBundle.OpFlags));
                return scannerSettings;
            }
        }

        public static bool ParseAimIdEnabled(long flag)
        {
            return flag == ScannerProperties.FLAG_BARCODE_FORMAT_AIM_ID;
        }

        public static long? ParseAimIdEnabled(bool? flag)
        {
            long flags = 0;
            if (flag != null)
            {
                if ((bool)flag)
                    flags |= ScannerProperties.FLAG_BARCODE_FORMAT_AIM_ID;
                else flags &= ~ScannerProperties.FLAG_BARCODE_FORMAT_AIM_ID;
            }            
            return flags;
        }

        public static bool ParsePicklistModeEnabled(long flag)
        {
            return flag == ScannerProperties.FLAG_SCANNER_OP_PICK;
        }

        public static long? ParsePicklistModeEnabled(bool? flag)
        {
            long flags = 0;
            if (flag != null)
            {
                if ((bool)flag)
                    flags |= ScannerProperties.FLAG_SCANNER_OP_PICK;
                else flags &= ~ScannerProperties.FLAG_SCANNER_OP_PICK;
            }            
            return flags;
        }

        public static bool ParseKeyboardWedgeEnabled(long flag)
        {
            if (flag == ScannerProperties.FLAG_SCANNER_MODE_KEYBOARD_WEDGE)
                return true;
            else return flag != ScannerProperties.FLAG_SCANNER_MODE_INTENT;
        }

        public static long? ParseKeyboardWedgeEnabled(bool? flag)
        {
            long flags = 0;
            if (flag != null)
            {
                if ((bool)flag)
                    flags = ScannerProperties.FLAG_SCANNER_MODE_KEYBOARD_WEDGE;
                else
                    flags = ScannerProperties.FLAG_SCANNER_MODE_INTENT;
            }            
            return flags;
        }

        public static PresentationMode ParsePresentationMode(long flag)
        {
            if (flag == ScannerProperties.FLAG_SCANNER_OP_MANUAL_TRIGGER)
                return PresentationMode.MANUALTRIGGER;
            else if (flag == ScannerProperties.FLAG_SCANNER_OP_NORMAL_PRESENTATION)
                return PresentationMode.NORMALPRESENTATION;
            else if (flag == ScannerProperties.FLAG_SCANNER_OP_MOBILE_PRESENTATION)
                return PresentationMode.MOBILEPRESENTATION;
            else if (flag == ScannerProperties.FLAG_SCANNER_OP_STREAMING_PRESENTATION)
                return PresentationMode.STREAMINGPRESENTATION;
            else
                return PresentationMode.MANUALTRIGGER;
        }

        public static bool ParseHideKeyboardOnScan(long flag)
        {
            return flag == ScannerProperties.FLAG_HIDE_IME_ON_SCAN;
        }

        public static long ParseHideKeyboardOnScan(bool? flag)
        {
            long flags = 0;
            if (flag != null)
            {
                if ((bool)flag)
                    flags |= ScannerProperties.FLAG_HIDE_IME_ON_SCAN;
                else flags &= ~ScannerProperties.FLAG_HIDE_IME_ON_SCAN;
            }
            
            return flags;
        }

        public static KeyboardWedgeMode ParseKeyboardWedgeMode(int? mode)
        {
            if (mode == 0)
                return KeyboardWedgeMode.FASTINPUT;
            else if (mode == 1)
                return KeyboardWedgeMode.TRUEKEYPRESS;
            else
                return KeyboardWedgeMode.TRUEKEYPRESS;
        }

        public static int? ParseKeyboardWedgeMode(KeyboardWedgeMode? mode)
        {
            if (mode == KeyboardWedgeMode.FASTINPUT)
                return 0;
            else if (mode == KeyboardWedgeMode.TRUEKEYPRESS)
                return 1;
            else
                return null;
        }

        public static int? ParsePresentationMode(PresentationMode? flag)
        {
            if (flag == PresentationMode.MANUALTRIGGER)
                return 0;
            else if (flag == PresentationMode.NORMALPRESENTATION)
                return 2;
            else if (flag == PresentationMode.MOBILEPRESENTATION)
                return 3;
            else if (flag == PresentationMode.STREAMINGPRESENTATION)
                return 4;
            else
                return null;
        }

        public static Bundle ParseSettingsBundle(ScannerSettings settings)
        {
            ScannerBundle scannerBundle = new ScannerBundle();
            if (settings.GetPrefix() != null)
                scannerBundle.Prefix = settings.GetPrefix();
            if (settings.GetSuffix() != null)
                scannerBundle.Suffix = settings.GetSuffix();
            if (settings.GetScanSound() != null)
                scannerBundle.Sound = settings.GetScanSound();
            if (settings.GetKeyboardWedgeMode() != null)
                scannerBundle.KeyboardWedgeMode = ParseKeyboardWedgeMode(settings.GetKeyboardWedgeMode());
            if (settings.IsScannerEnabled() != null)
                scannerBundle.ScannerEnabled = settings.IsScannerEnabled();
            if (settings.IsBarcodeActionsEnabled() != null)
                scannerBundle.BarcodeActions = settings.IsBarcodeActionsEnabled();
            if (settings.IsContinuousScanEnabled() != null)
                scannerBundle.ContinuousScan = settings.IsContinuousScanEnabled();
            if (settings.IsLEDFlashOnScanEnabled() != null)
                scannerBundle.LEDFlash = settings.IsLEDFlashOnScanEnabled();
            if (settings.IsScanKeyEventModeEnabled() != null)
                scannerBundle.ScanKeyEvent = settings.IsScanKeyEventModeEnabled();
            if (settings.IsManagedModeEnabled() != null)
                scannerBundle.Managed = settings.IsManagedModeEnabled();
            if (settings.IsHideKeyboardOnScanEnabled() != null)
                scannerBundle.HideKeyboard = ParseHideKeyboardOnScan(settings.IsHideKeyboardOnScanEnabled());
            if (settings.GetPresentationMode() != null)
                scannerBundle.PresentationMode = ParsePresentationMode(settings.GetPresentationMode());
            if (settings.IsKeyboardWedgeEnabled() != null)
                scannerBundle.ScannerModeFlags = ParseKeyboardWedgeEnabled(settings.IsKeyboardWedgeEnabled());
            if (settings.IsAimIdEnabled() != null)
                scannerBundle.BarcodeFormatFlags = ParseAimIdEnabled(settings.IsAimIdEnabled());
            if (settings.IsPicklistModeEnabled() != null)
                scannerBundle.OpFlags = ParsePicklistModeEnabled(settings.IsPicklistModeEnabled());
            return scannerBundle.GetBundle();
        }
    }
}
