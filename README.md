# AML Barcode Scanner Library  [![NuGet](https://img.shields.io/nuget/v/AMLBarcodeScannerLib)](https://www.nuget.org/packages/AMLBarcodeScannerLib)

## Overview

AML Barcode Scanner Library provides an easy way to interface with an AML device's barcode scanner. 
The library allows you to create an instance of AMLBarcodeScanner. 
You can listen for incoming barcode scan data and configure scanner settings.
This library works for Android.Xamarin, Xamarin Forms, and Maui.

## Usage

The library contains a class called `AMLBarcodeScanner` that is used to interface with the scanner:

```csharp
var scanner = new AMLBarcodeScanner(this);
```

The parameter for the constructor is the `Context` of the Android application.

## Example

Opening the scanner and registering for scan data.

```csharp
var scanner;

public void initScanner()
{
    scanner = new AMLBarcodeScanner(this);

    //Open the scanner connection
    scanner.Open();
    scanner.Scanned += ReceiveScan;   
}

public void ReceiveScan(string barcode, string rawBarcode)
{
    //Process barcode data
}
```

Querying and updating scanner settings.

```csharp
public void QueryScannerSettings()
{
    scanner.GetScannerSettings(ScannerSettingsReceived);
}

public void ScannerSettingsReceived(ScannerSettings settings)
{
    var currentSuffix = settings.GetSuffix();
    if (currentSuffix == null || currentSuffix != "!")
    {
        settings.SetSuffix("!");
        scanner.ChangeSettings(settings);
    }
}

```

Registering for trigger events.

```csharp
public void RegisterTriggerEvents()
{
    scanner.TriggerPulled += TriggerWasPulled;   
    scanner.TriggerReleased += TriggerWasReleased; 
}

public void TriggerWasPulled()
{
    //Handle trigger pull
}

public void TriggerWasReleased()
{
    //Handle trigger release
}
```

Querying BT Scanner and registering for events.

```csharp
BTDeviceInfo storedBTScanner;

public void GetCurrentBTDevice()
{
    if (AMLDevice.IsBTScannerSupported())
        scanner.GetBTScannerInfo(ReceivedBTScannerInfo);
}

public void ReceivedBTScannerInfo(BTDeviceInfo btDevice)
{
    var name = btDevice.GetBTName();
    if (!String.IsNullOrEmpty(name))
    {
        storedBTScanner = btDevice;
        RegisterBTScannerEvents();
    }
}

public void RegisterBTScannerEvents()
{
    scanner.BTScannerConnected += BTScannerConnect;   
    scanner.BTScannerDisconnected += BTScannerDisconnect; 
    scanner.BTScannerLowBattery += BTLowBattery;
}

public void BTScannerConnect(BTScanDevice btDevice)
{
    //Add functionality
}

public void BTScannerDisconnect(BTScanDevice btDevice)
{
    //Add functionality
}

public void BTLowBattery(int batteryLevel)
{
    //Add functionality
}
```
