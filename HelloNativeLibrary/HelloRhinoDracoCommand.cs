using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace HelloNativeLibrary
{
    public class HelloRhinoDracoCommand : Command
    {
        public HelloRhinoDracoCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static HelloRhinoDracoCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "RhinoDracoTest";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            try
            {
                byte[] data = NativeInteropTest.NativeInterop.LoadSampleDrc();
                var geom = Rhino.FileIO.DracoCompression.DecompressByteArray(data);
                doc.Objects.Add(geom);
                Rhino.RhinoApp.WriteLine("Rhino.FileIO.DracoCompression successful decode!");
                return Result.Success;
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

