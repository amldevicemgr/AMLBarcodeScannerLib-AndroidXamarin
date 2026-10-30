using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib.Settings
{
    /// <summary>
    /// An additional key/value string extra the scanner service puts on a scan intent.
    /// </summary>
    public class ScanIntentExtra
    {
        private string Key;
        private string Value;

        public ScanIntentExtra(string key, string value)
        {
            SetKey(key);
            SetValue(value);
        }

        public string GetKey()
        {
            return Key;
        }

        public void SetKey(string key)
        {
            if (key == null)
                key = "";
            Key = key;
        }

        public string GetValue()
        {
            return Value;
        }

        public void SetValue(string value)
        {
            if (value == null)
                value = "";
            Value = value;
        }
    }
}
