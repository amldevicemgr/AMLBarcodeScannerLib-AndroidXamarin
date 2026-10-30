using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib.Settings
{
    /// <summary>
    /// How the scanner service delivers a configured scan intent.
    /// </summary>
    public enum ScanIntentSendType
    {
        STARTACTIVITY,
        STARTFOREGROUNDSERVICE,
        STARTSERVICE,
        SENDBROADCAST
    }
}
