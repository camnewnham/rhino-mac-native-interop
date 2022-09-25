# rhino-mac-native-interop
Debug minimal sample to show differences when running in Rhino environment

This solution consists of three projects:
1. **NativeInteropLibrary**. This is a net48 wrapper to interact with a native library (libdraco_dec.so). The native library is a fork of Google's Draco library.
2. **ConsoleAppNet48**. Demonstrates expected behaviour when interacting with the native library.
3. **HelloNativeLibrary**. Rhino command that executes the same code as ConsoleAppNet48, but with unexpected behaviour.

Observerations:
1. The library functions as expected in a the aforementioned console app (Prints "success" to console).
2. The same library compiled for Windows works as expected (Rhino 7 Win)
3. The library partially functions in Rhino 7 for Mac, so it is correctly found/loaded. That is; it successfully runs "Step 1" but fails on "Step 2"
4. The same unexpected behaviour is observed in Rhino 8 WIP (I had suspected that it could be something due to mono)
