using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var opTest = new NativeInteropTest.NativeInterop())
                {
                    var result = opTest.TestNativeLibraryDecode();
                    Log("Success! "+result.ToString());
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString());

                Console.ReadKey();
            }
        }

        public static void Log(object o)
        {
            Console.WriteLine(o);
        }
    }
}

