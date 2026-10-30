using AMLBarcodeScannerLib.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib
{
    // Converts scan intents to/from the JSON string format stored by the scanner service.
    // Field names and sendType strings must match the scanner service's ScanIntent serialization.
    class ScanIntentHelper
    {
        public static string ToJsonString(List<ScanIntent> scanIntents)
        {
            JArray arr = new JArray();
            if (scanIntents == null)
                return arr.ToString(Newtonsoft.Json.Formatting.None);
            foreach (ScanIntent si in scanIntents)
            {
                if (si == null)
                    continue;
                JArray components = new JArray();
                foreach (string packageName in si.GetPackages())
                {
                    if (string.IsNullOrEmpty(packageName))
                        continue;
                    components.Add(new JObject { ["packageName"] = packageName });
                }
                JArray extras = new JArray();
                foreach (ScanIntentExtra extra in si.GetExtras())
                {
                    if (extra == null)
                        continue;
                    extras.Add(new JObject { ["key"] = extra.GetKey(), ["value"] = extra.GetValue() });
                }
                arr.Add(new JObject
                {
                    ["action"] = si.GetAction(),
                    ["category"] = si.GetCategory(),
                    ["extraName"] = si.GetExtraName(),
                    ["sendType"] = ToSendTypeString(si.GetSendType()),
                    ["components"] = components,
                    ["extras"] = extras
                });
            }
            return arr.ToString(Newtonsoft.Json.Formatting.None);
        }

        public static List<ScanIntent> FromJsonString(string json)
        {
            List<ScanIntent> list = new List<ScanIntent>();
            if (string.IsNullOrEmpty(json))
                return list;
            try
            {
                JArray arr = JArray.Parse(json);
                foreach (JObject o in arr.OfType<JObject>())
                {
                    ScanIntent si = new ScanIntent((string)o["action"] ?? "",
                        FromSendTypeString((string)o["sendType"]));
                    si.SetCategory((string)o["category"] ?? "");
                    si.SetExtraName((string)o["extraName"] ?? "");
                    if (o["components"] is JArray components)
                    {
                        foreach (JObject c in components.OfType<JObject>())
                            si.AddPackage((string)c["packageName"] ?? "");
                    }
                    if (o["extras"] is JArray extras)
                    {
                        foreach (JObject e in extras.OfType<JObject>())
                            si.AddExtra((string)e["key"] ?? "", (string)e["value"] ?? "");
                    }
                    list.Add(si);
                }
            }
            catch (Exception)
            {
                // Return what was parsed so far
            }
            return list;
        }

        public static string ToSendTypeString(ScanIntentSendType sendType)
        {
            switch (sendType)
            {
                case ScanIntentSendType.STARTACTIVITY: return "startActivity";
                case ScanIntentSendType.STARTFOREGROUNDSERVICE: return "startForegroundService";
                case ScanIntentSendType.STARTSERVICE: return "startService";
                default: return "sendBroadcast";
            }
        }

        public static ScanIntentSendType FromSendTypeString(string sendType)
        {
            switch (sendType)
            {
                case "startActivity": return ScanIntentSendType.STARTACTIVITY;
                case "startForegroundService": return ScanIntentSendType.STARTFOREGROUNDSERVICE;
                case "startService": return ScanIntentSendType.STARTSERVICE;
                default: return ScanIntentSendType.SENDBROADCAST;
            }
        }
    }
}
