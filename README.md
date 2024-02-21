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

```csharp
var scanner;

public initScanner()
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
