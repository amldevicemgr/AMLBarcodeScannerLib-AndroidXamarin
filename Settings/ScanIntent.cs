using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLBarcodeScannerLib.Settings
{
    /// <summary>
    /// An intent the scanner service sends with the barcode data each time a barcode is scanned.
    /// Scan intents are only sent while scan intents are enabled.
    /// </summary>
    public class ScanIntent
    {
        private string Action;
        private string Category;
        private string ExtraName;
        private ScanIntentSendType SendType;
        private List<string> Packages = new List<string>();
        private List<ScanIntentExtra> Extras = new List<ScanIntentExtra>();

        /// <param name="action">The intent action. Required - the scanner service skips scan intents with no action.</param>
        /// <param name="sendType">How the intent is delivered.</param>
        public ScanIntent(string action, ScanIntentSendType sendType = ScanIntentSendType.SENDBROADCAST)
        {
            SetAction(action);
            SetCategory("");
            SetExtraName("");
            SetSendType(sendType);
        }

        public string GetAction()
        {
            return Action;
        }

        public void SetAction(string action)
        {
            if (action == null)
                action = "";
            Action = action.Trim();
        }

        public string GetCategory()
        {
            return Category;
        }

        /// <param name="category">Optional intent category. Empty for none.</param>
        public void SetCategory(string category)
        {
            if (category == null)
                category = "";
            Category = category.Trim();
        }

        public string GetExtraName()
        {
            return ExtraName;
        }

        /// <param name="extraName">The name of the intent extra that holds the barcode data.
        /// Empty uses the scanner service default extra name "d".</param>
        public void SetExtraName(string extraName)
        {
            if (extraName == null)
                extraName = "";
            ExtraName = extraName.Trim();
        }

        public ScanIntentSendType GetSendType()
        {
            return SendType;
        }

        public void SetSendType(ScanIntentSendType sendType)
        {
            SendType = sendType;
        }

        /// <summary>
        /// The package names the intent is targeted at. One intent is sent per package.
        /// Empty means a single intent is sent that is not targeted at a package.
        /// </summary>
        public List<string> GetPackages()
        {
            return Packages;
        }

        public void SetPackages(List<string> packages)
        {
            Packages = packages ?? new List<string>();
        }

        public void AddPackage(string packageName)
        {
            if (!string.IsNullOrEmpty(packageName))
                Packages.Add(packageName);
        }

        public List<ScanIntentExtra> GetExtras()
        {
            return Extras;
        }

        public void SetExtras(List<ScanIntentExtra> extras)
        {
            Extras = extras ?? new List<ScanIntentExtra>();
        }

        /// <summary>
        /// Adds a string extra to the intent. Extras with an empty key or value are skipped by the scanner service.
        /// </summary>
        public void AddExtra(string key, string value)
        {
            Extras.Add(new ScanIntentExtra(key, value));
        }
    }
}
