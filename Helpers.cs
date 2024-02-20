using AMLBarcodeScannerLib.Settings;
using Android.Content;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib
{
    /**
     * Reusable helper classes below, for minimizing code.
     */

    class BroadcastReceiverHelp : BroadcastReceiver
    {
        Action<Intent> Receive;
        public BroadcastReceiverHelp(Action<Intent> receive) { Receive = receive; }
        public override void OnReceive(Context context, Intent intent)
        {
            Receive?.Invoke(intent);
        }
    }

    class ResultReceiverHelp : ResultReceiver
    {
        Action<ScannerSettings> Receive;
        public ResultReceiverHelp(Action<ScannerSettings> onReceive)
            : base(new Handler())
        {
            Receive = onReceive;
        }
        protected override void OnReceiveResult(int resultCode, Bundle resultData)
        {
            Receive?.Invoke(SettingsHelper.ParseScannerSettings(resultData));
        }

        public static ResultReceiver CreateNewFromParcel(Action<ScannerSettings> onReceive)
        {
            ResultReceiver r = new ResultReceiverHelp(onReceive);
            var p = Parcel.Obtain();
            r.WriteToParcel(p, 0);
            p.SetDataPosition(0);
            r = Creator.CreateFromParcel(p) as ResultReceiver;
            p.Recycle();
            return r;
        }
    }
}
