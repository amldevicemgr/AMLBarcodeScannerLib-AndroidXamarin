using AMLBarcodeScannerLib.Settings;
using AMLBarcodeScannerLib;
using Android.Content;
using Android.OS;
using Android.Util;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using AMLBarcodeScannerLib.BTScanner;

namespace AMLBarcodeScannerLib
{
    public class AMLBarcodeScanner
    {
        public event Action<string> Error;
        public event Action<string, string> Scanned;
        public event Action TriggerPulled;
        public event Action TriggerReleased;
        public event Action<BTScanDevice> BTScannerConnected;
        public event Action<BTScanDevice> BTScannerDisconnected;
        public event Action<int> BTScannerLowBattery;

        /// <summary>
        /// Constructs an AMLBarcodeScanner instance to configure the barcode scanner and receive barcode data.
        /// </summary>
        /// <param name="context">The context of the application.</param>
        public AMLBarcodeScanner(Context context = null)
        {
            mContext = context != null ? context : Application.Context;
        }

        /// <summary>
        /// Opens the AML Barcode Scanner connection.
        /// </summary>
        public void Open()
        {
            //Check if already registered.
            if (mReceiver == null)
            {
                //The receiver must be registered with an intent filter.
                // Using an intent filter with the actions will tell Android to
                // send intents with the same actions to this receiver.
                var filter = new IntentFilter();
                //Add this if we want an intent with the barcode returned to us after a successful scan.
                filter.AddAction(Values.ACTION_SCANNED);
                //Add this if we want an intent returned to us when the trigger is pulled.
                // This is only an indication of when the trigger is pulled.
                // It does not mean there was a barcode scanned.
                // The intent holds no data other than the action string.
                filter.AddAction(Values.ACTION_TRIGGER_PULLED);
                filter.AddAction(Values.ACTION_TRIGGER_RELEASED);
                //Add this if we want an intent with error messages returned to us.
                filter.AddAction(Values.ACTION_ERROR);
                //Add this if we want to listen for BT Scanner events
                filter.AddAction(Values.ACTION_RING_SCANNER_CONNECTED);
                filter.AddAction(Values.ACTION_RING_SCANNER_DISCONNECTED);
                filter.AddAction(Values.ACTION_RING_SCANNER_LOW_BATTERY);

                //This broadcast receiver will handle incoming intents from the scanner service.
                mReceiver = new BroadcastReceiverHelp(intent => {
                    if (intent != null)
                    {
                         switch (intent.Action)
                         {
                            case Values.ACTION_SCANNED:
                                var barcode = intent.GetStringExtra(Values.EXTRA_DATA);
                                var rawBarcode = intent.GetStringExtra(Values.EXTRA_RAW_DATA);
                                Scanned?.Invoke(barcode, rawBarcode);
                                break;
                            case Values.ACTION_TRIGGER_PULLED:
                                TriggerPulled?.Invoke();
                                break;
                            case Values.ACTION_TRIGGER_RELEASED:
                                TriggerReleased?.Invoke();
                                break;
                            case Values.ACTION_ERROR:
                                var errMsg = intent.GetStringExtra(Values.EXTRA_DATA);
                                Error?.Invoke(errMsg);
                                break;
                            case Values.ACTION_RING_SCANNER_CONNECTED:
                            case Values.ACTION_RING_SCANNER_DISCONNECTED:
                                var btscannerstring = intent.GetStringExtra(Values.EXTRA_RING_SCANNER);
                                if (!System.String.IsNullOrEmpty(btscannerstring))
                                {
                                    var btscanner = Newtonsoft.Json.JsonConvert.DeserializeObject<BTScanDevice>(btscannerstring);
                                    if (intent.Action == Values.ACTION_RING_SCANNER_CONNECTED)
                                        BTScannerConnected?.Invoke(btscanner);
                                    else
                                        BTScannerDisconnected?.Invoke(btscanner);
                                }
                                break;
                            case Values.ACTION_RING_SCANNER_LOW_BATTERY:
                                var battery = intent.GetIntExtra(Values.EXTRA_RING_SCANNER_LOW_BATTERY, -1);
                                BTScannerLowBattery?.Invoke(battery);
                                break;
                        }
                    }
                });

                //Register!
                mContext.RegisterReceiver(mReceiver, filter);
                //Call startService to ensure AML Barcode Service is running.
                StartService();
            }
        }

        /// <summary>
        /// Closes the AMLBarcodeScanner and stops listening for barcode data.
        /// </summary>
        public void Close()
        {
            if (mReceiver != null)
            {
                BroadcastReceiver br = mReceiver;
                mReceiver = null;
                mContext.UnregisterReceiver(br);
            }
        }

        /// <summary>
        /// Sets the scan sound from a notification ringtone.
        /// </summary>
        /// <param name="sound">The name of the ringtone.</param>
        public void SetScanSound(string sound)
        {
            if (sound == null)
                sound = "";
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.Sound = sound;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Sets the scan engine presentation mode.
        /// </summary>
        /// <param name="mode">The presentation mode.</param>
        public void SetPresentationMode(PresentationMode mode)
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.PresentationMode = SettingsHelper.ParsePresentationMode(mode);
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables scan engine presentation mode and sets to Manual Trigger mode.
        /// </summary>
        public void DisablePresentationMode()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.PresentationMode = 0;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables the barcode AIM Id prefix.
        /// </summary>
        public void EnableAimId()
        {
            long flags = 0;
            ScannerBundle scannerSettings = new ScannerBundle();
            flags |= ScannerProperties.FLAG_BARCODE_FORMAT_AIM_ID;
            scannerSettings.BarcodeFormatFlags = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables the barcode AIM Id prefix.
        /// </summary>
        public void DisableAimId()
        {
            long flags = 0;
            ScannerBundle scannerSettings = new ScannerBundle();
            flags &= ~ScannerProperties.FLAG_BARCODE_FORMAT_AIM_ID;
            scannerSettings.BarcodeFormatFlags = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables the scan engine picklist mode.
        /// </summary>
        public void EnablePicklistMode()
        {
            long flags = 0;
            ScannerBundle scannerSettings = new ScannerBundle();
            flags |= ScannerProperties.FLAG_SCANNER_OP_PICK;
            scannerSettings.OpFlags = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables the scan engine picklist mode.
        /// </summary>
        public void DisablePicklistMode()
        {
            long flags = 0;
            ScannerBundle scannerSettings = new ScannerBundle();
            flags &= ~ScannerProperties.FLAG_SCANNER_OP_PICK;
            scannerSettings.OpFlags = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables the scanner service keyboard wedge.
        /// </summary>
        public void EnableKeyboardWedge()
        {
            long flags = ScannerProperties.FLAG_SCANNER_MODE_KEYBOARD_WEDGE;
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ScannerModeFlags = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables the scanner service keyboard wedge.
        /// </summary>
        public void DisableKeyboardWedge()
        {
            long flags = ScannerProperties.FLAG_SCANNER_MODE_INTENT;
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ScannerModeFlags = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables the scanner service managed mode.
        /// </summary>
        public void EnableManagedMode()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.Managed = true;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables the scanner service managed mode.
        /// </summary>
        public void DisableManagedMode()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.Managed = false;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables scan key event mode.
        /// </summary>
        public void EnableScanKeyEventMode()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ScanKeyEvent = true;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables scan key event mode.
        /// </summary>
        public void DisableScanKeyEventMode()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ScanKeyEvent = false;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables hide keyboard on scan.
        /// </summary>
        public void EnableHideKeyboardOnScan()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            long flags = 0;
            //enable hide keyboard on scan
            flags |= ScannerProperties.FLAG_HIDE_IME_ON_SCAN;

            scannerSettings.HideKeyboard = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables hide keyboard on scan.
        /// </summary>
        public void DisableHideKeyboardOnScan()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            long flags = 0;

            //disable hide keyboard on scan
            flags &= ~ScannerProperties.FLAG_HIDE_IME_ON_SCAN;
            scannerSettings.HideKeyboard = flags;
            ChangeSettings(scannerSettings.GetBundle());
        }


        /// <summary>
        /// Enables LED flash on scan. Only pertains to the Firebird model.
        /// </summary>
        public void EnableLEDFlashOnScan()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.LEDFlash = true;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables LED flash on scan. Only pertains to the Firebird model.
        /// </summary>
        public void DisableLEDFlashOnScan()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.LEDFlash = false;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables continuous scan mode.
        /// </summary>
        public void EnableContinuousScan()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ContinuousScan = true;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables continuous scan mode.
        /// </summary>
        public void DisableContinuousScan()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ContinuousScan = false;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables scanner service smart barcode actions.
        /// </summary>
        public void EnableBarcodeActions()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.BarcodeActions = true;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables scanner service smart barcode actions.
        /// </summary>
        public void DisableBarcodeActions()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.BarcodeActions = false;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Enables scanning.
        /// </summary>
        public void EnableScanner()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ScannerEnabled = true;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Disables scanning.
        /// </summary>
        public void DisableScanner()
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.ScannerEnabled = false;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Sets the scanner service keyboard wedge mode.
        /// </summary>
        /// <param name="mode">The keyboard wedge mode.</param>
        public void SetKeyboardWedgeMode(KeyboardWedgeMode mode)
        {
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.KeyboardWedgeMode = SettingsHelper.ParseKeyboardWedgeMode(mode);
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Sets the barcode prefix.
        /// </summary>
        /// <param name="prefix">The prefix value.</param>
        public void SetPrefix(string prefix)
        {
            if (prefix == null)
                prefix = "";
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.Prefix = prefix;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Sets the barcode suffix.
        /// For non-printable ASCII characters, use the following format:
        /// "\\NN" - where NN is the double digit hex number representation of the character,
        /// and backslash is the required escape/indicator character (double to escape backslash in string).
        /// </summary>
        /// <param name="suffix">The suffix value.</param>
        public void SetSuffix(string suffix)
        {
            if (suffix == null)
                suffix = "";
            ScannerBundle scannerSettings = new ScannerBundle();
            scannerSettings.Suffix = suffix;
            ChangeSettings(scannerSettings.GetBundle());
        }

        /// <summary>
        /// Requests the current scanner settings asynchronously.
        /// </summary>
        /// <param name="result">The Action receiver to receive the settings result.</param>
        public void GetScannerSettings(Action<ScannerSettings> result)
        {
            if (result != null)
            {
                var bundle = new Bundle();
                bundle.PutParcelable(Intent.ExtraResultReceiver, ResultReceiverHelp.CreateNewFromParcel(result));
                Intent intent = new Intent();
                intent.PutExtras(bundle);
                StartService(intent);
            }
        }

        /// <summary>
        /// Requests the current bt scanner info asynchronously.
        /// </summary>
        /// <param name="result">The Action receiver to receive the bt scanner result.</param>
        public void GetBTScannerInfo(Action<BTDeviceInfo> result)
        {
            if (result != null)
            {
                var bundle = new Bundle();
                bundle.PutParcelable(Intent.ExtraResultReceiver, ResultReceiverHelp.CreateNewFromParcel(result));
                Intent intent = new Intent();
                intent.SetAction(Values.GET_BT_DEVICE_INFO_ACTION);
                intent.PutExtras(bundle);
                intent.SetPackage(Values.PACKAGE_NAME);
                mContext.SendBroadcast(intent);
            }
        }

        /// <summary>
        /// Send a correctly setup ScannerSettings config to the scanner service to be processed in order to
        /// change the scanner behavior.
        /// </summary>
        /// <param name="settings">The scanner settings generated with ScannerSettings.</param>
        public void ChangeSettings(ScannerSettings settings)
        {

            if (settings != null)
            {
                Bundle bundle = SettingsHelper.ParseSettingsBundle(settings);
                Intent intent = new Intent();
                intent.PutExtras(bundle);

                //Calling startService and passing in our intent is how we send the intent to the
                //scanner service for processing.
                StartService(intent);
            }      
        }

        /// <summary>
        /// Programmatically trigger on/off scan engine.
        /// </summary>
        /// <param name="on">True for on, otherwise false.</param>
        public void Trigger(bool on)
        {
            Intent intent = new Intent(on ? Values.ACTION_KEY_SCAN_DOWN : Values.ACTION_KEY_SCAN_UP);
            StartService(intent);
        }

        /// <summary>
        /// Programatically hide/show the virtual keyboard
        /// </summary>
        /// <param name="hide">True for hiding, otherwise false</param>
        public void Keyboard(bool hide)
        {
            Intent intent = new Intent(hide ? Values.ACTION_HIDE_IME : Values.ACTION_SHOW_IME);
            StartService(intent);
        }


        #region PRIVATE METHODS

        //Application context
        protected Context mContext { get; }

        //For receiving broadcast intents
        private BroadcastReceiverHelp mReceiver;

        /**
         * Starts AML Barcode Service.
         */
        private void StartService()
        {
            StartService(null);
        }

        /**
         * Starts AML Barcode Service with a intent.
         * @param intent The intent to send to the service.
         */
        private void StartService(Intent? intent)
        {
            if (intent == null) intent = new Intent();
            intent.SetClassName(Values.PACKAGE_NAME, Values.CLASS_NAME);

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                mContext.StartForegroundService(intent);
            }
            else
            {
                mContext.StartService(intent);
            }
        }

        /**
         * Send a correctly setup settings bundle to the scanner service to be processed in order to
         * change the scanner behavior.
         * @param settings The scanner settings generated with Bundle.
         */
        private void ChangeSettings(Bundle settings)
        {
            Intent intent = new Intent();
            intent.PutExtras(settings);

            //Calling startService and passing in our intent is how we send the intent to the
            //scanner service for processing.
            StartService(intent);
        }

        #endregion
    }
}
