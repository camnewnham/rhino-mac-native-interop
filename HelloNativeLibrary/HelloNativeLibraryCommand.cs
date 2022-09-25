﻿using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace HelloNativeLibrary
{
    public class HelloNativeLibraryCommand : Command
    {
        public HelloNativeLibraryCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static HelloNativeLibraryCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "NativeInteropTest";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            try
            {
                using (var opTest = new NativeInteropTest.NativeInterop())
                {
                    var result = opTest.TestNativeLibraryDecode();
                    Log("Success! "+result.ToString());
                    return Result.Success;
                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString());

                return Result.Failure;
            }
        }



        public static void Log(object o)
        {
            Rhino.RhinoApp.WriteLine(o?.ToString() ?? "null");
        }
    }
}

