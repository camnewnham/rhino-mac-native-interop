# rhino-mac-native-interop
Debug minimal sample to show differences when running in Rhino environment

This solution consists of three projects:
1. **NativeInteropLibrary**. This is a net48 wrapper to interact with a native library (libdraco_dec.so). The native library is a fork of Google's Draco library, and this binary is signed and notarized.
2. **ConsoleAppNet48**. Demonstrates expected behaviour when interacting with the native library.
3. **HelloNativeLibrary**. Rhino command "NativeInteropTest" that executes the same code as ConsoleAppNet48, but with unexpected behaviour.

Observerations:
1. The library functions as expected in a the aforementioned console app (Prints "success" to console).
2. The same library compiled for Windows works as expected (Rhino 7 Win)
3. The same library compiled for Unity (.bundle instead of .so) works as expected
4. The library partially functions in Rhino 7 for Mac, so it is correctly found/loaded. That is; it successfully runs "Step 1" but fails on "Step 2"
5. The same unexpected behaviour is observed in Rhino 8 WIP (I had suspected that it could be something due to mono)

Test environment:
```
Rhino 7 SR22 2022-9-12 (Rhino 7, 7.22.22255.05002, Git hash:master @ 196b1bc7dd093321e28dcc7a2bb8709a9bebe12d)
License type: Not For Resale Lab, build 2022-09-12
License details: Cloud Zoo

Apple macOS Version 12.3.1 (Build 21E258) (Physical RAM: 16Gb)
Mac Model Identifier: MacBookPro13,2
Language: en-AU (MacOS default)

Intel(R) Iris(TM) Graphics 550 (OpenGL ver:4.1 INTEL-18.5.8)
```
