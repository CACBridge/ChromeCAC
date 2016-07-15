# ChromeCAC

ChromeCAC makes it easy and secure to sign data with smart cards using Google Chrome.

*ChromeCAC is a work in progress, and is not yet ready for use!*

----

ChromeCAC is made up of 3 parts: 

* the chrome extension
* the native app that communicates with the chrome extension
* the cacbridge.js website code

----

## Setup

At this time, setup is heavily manual. Automatic installers will be created further into development.

### Server-side Setup

#### Regular Webpage

```html
<head>
...
<script src="cacbridge.js"></script>
...
</head>
```


#### NodeJS

```
Coming soon
```

#### RequireJS

```
Coming soon
```

### Client-side Setup

#### Chrome Extension

In Google Chrome, go to Settings -> Extensions and click Load Unpacked Extension. Load the ChromeExtension folder.

In the loaded extension's information, copy for the *ID*.

Follow the manifest instructions for your OS [here](https://github.com/jkusner/NativeMessage/tree/master/manifest).

In the manifest, be sure to change `chrome-extension://aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/` to `chrome-extension://{ID}/`

#### Native App

Build the native app of your choice, ex: ChromeCAC.NET

Place the built EXE in the location referenced from your manifest.

## Usage

Sign a payload by calling `CAC.sign(...)`

```js
CAC.sign("this string will be signed", function success (signatureInfo) {

  // Signed successfully
  // Passes signatureInfo, see below
  console.log("Signature: " + data.signature)

}, function fail () {

  // Failed to sign
  console.log("Failed to sign data!")

})
```

## Interface

#### SignatureInfo

A JavaScript object containing information about the signed data. This is passed to the success callback in CAC.sign

Property  |  Type  | Description
----------|--------|-----
signature | string | The signature of the signed string
publicKey | string | The public key used to verify the certificate
fullSig   | string | The full signature, containing the public key and signature to be easily verified


## Verifying Signatures

Examples of signature verification are coming soon to CACBridge.
