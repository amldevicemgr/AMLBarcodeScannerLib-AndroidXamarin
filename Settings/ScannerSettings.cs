using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib.Settings
{
    public class ScannerSettings
    {
        private string? Prefix;
        private string? Suffix;
        private string? ScanSound;
        private KeyboardWedgeMode? KeyboardWedgeMode;
        private bool? ScannerEnabled;
        private bool? BarcodeActions;
        private bool? ContinuousScan;
        private bool? LEDFlashOnScan;
        private bool? ScanKeyEventMode;
        private bool? ManagedEnabled;
        private bool? HideKeyboardOnScan;
        private PresentationMode? PresentationMode;
        private bool? KeyboardWedge;
        private bool? AimId;
        private bool? PicklistMode;

        public string? GetPrefix()
        {
            return Prefix;
        }

        public void SetPrefix(string prefix)
        {
            if (prefix == null)
                prefix = "";
            Prefix = prefix;
        }

        public string? GetScanSound()
        {
            return ScanSound;
        }

        public void SetScanSound(string sound)
        {
            if (sound == null)
                sound = "";
            ScanSound = sound;
        }

        public string? GetSuffix()
        {
            return Suffix;
        }

        public void SetSuffix(string suffix)
        {
            if (suffix == null)
                suffix = "";
            Suffix = suffix;
        }

        public KeyboardWedgeMode? GetKeyboardWedgeMode()
        {
            return KeyboardWedgeMode;
        }

        public void SetKeyboardWedgeMode(KeyboardWedgeMode keyboardWedgeMode)
        {
            KeyboardWedgeMode = keyboardWedgeMode;
        }

        public bool? IsScannerEnabled()
        {
            return ScannerEnabled;
        }

        public void SetScannerEnabled(bool scannerEnabled)
        {
            ScannerEnabled = scannerEnabled;
        }

        public bool? IsBarcodeActionsEnabled()
        {
            return BarcodeActions;
        }

        public void SetBarcodeActionsEnabled(bool barcodeActions)
        {
            BarcodeActions = barcodeActions;
        }

        public bool? IsContinuousScanEnabled()
        {
            return ContinuousScan;
        }

        public void SetContinuousScanEnabled(bool continuousScan)
        {
            ContinuousScan = continuousScan;
        }

        public bool? IsLEDFlashOnScanEnabled()
        {
            return LEDFlashOnScan;
        }

        public void SetLEDFlashOnScanEnabled(bool LEDFlashOnScan)
        {
            this.LEDFlashOnScan = LEDFlashOnScan;
        }

        public bool? IsScanKeyEventModeEnabled()
        {
            return ScanKeyEventMode;
        }

        public void SetScanKeyEventModeEnabled(bool scanKeyEventMode)
        {
            ScanKeyEventMode = scanKeyEventMode;
        }

        public bool? IsManagedModeEnabled()
        {
            return ManagedEnabled;
        }

        public void SetManagedModeEnabled(bool managedEnabled)
        {
            ManagedEnabled = managedEnabled;
        }

        public bool? IsHideKeyboardOnScanEnabled()
        {
            return HideKeyboardOnScan;
        }

        public void SetHideKeyboardOnScanEnabled(bool hideKeyboardOnScan)
        {
            HideKeyboardOnScan = hideKeyboardOnScan;
        }

        public PresentationMode? GetPresentationMode()
        {
            return PresentationMode;
        }

        public void SetPresentationMode(PresentationMode presentationMode)
        {
            PresentationMode = presentationMode;
        }

        public bool? IsKeyboardWedgeEnabled()
        {
            return KeyboardWedge;
        }

        public void SetKeyboardWedgeEnabled(bool keyboardWedge)
        {
            KeyboardWedge = keyboardWedge;
        }

        public bool? IsAimIdEnabled()
        {
            return AimId;
        }

        public void SetAimIdEnabled(bool aimId)
        {
            AimId = aimId;
        }

        public bool? IsPicklistModeEnabled()
        {
            return PicklistMode;
        }

        public void SetPicklistModeEnabled(bool picklistMode)
        {
            PicklistMode = picklistMode;
        }

    }
}
