using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NativeInteropTest
{

    public unsafe class NativeInterop : IDisposable
    {
        private const string DRACODEC_UNITY_LIB = "draco_dec.so";

        GCHandle inputDataGCHandle;
        IntPtr meshPtr;

        public void Dispose()
        {
            inputDataGCHandle.Free();

            if (meshPtr != default)
            {
                var dracoMeshPtr = (NativeDracoMesh*)meshPtr;
                ReleaseDracoMesh(&dracoMeshPtr);
            }
        }

        public static byte[] LoadSampleDrc()
        {

            var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(NativeInterop)).Location);
            var file = System.IO.Path.Combine(folder, "sample.drc");
            var data = File.ReadAllBytes(file);
            return data;
        }

        public DecodeResult TestNativeLibraryDecode()
        {
            return TestNativeLibraryDecode(LoadSampleDrc());
        }

        public DecodeResult TestNativeLibraryDecode(byte[] data)
        {
            inputDataGCHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            meshPtr = default;
            void* decoder;
            void* buffer;

            var dracoMeshPtr = (NativeDracoMesh*) meshPtr;

            int decodeResult = DecodeDracoMeshStep1((byte*)inputDataGCHandle.AddrOfPinnedObject(), (uint)data.Length, &dracoMeshPtr, &decoder, &buffer);

            if (decodeResult < 0)
            {
                throw new Exception($"Decode failed at step 1 with native error code {decodeResult}");
            }

            int decodeResult2 = DecodeDracoMeshStep2(&dracoMeshPtr, decoder, buffer);
            if (decodeResult2 < 0)
            {
                throw new Exception($"Decode failed at step 2 with native error code {decodeResult2}");
            }

            return new DecodeResult()
            {
                FaceCount = dracoMeshPtr->numFaces,
                VertexCount = dracoMeshPtr->numVertices
            };
         }

        public struct DecodeResult
        {
            public int VertexCount;
            public int FaceCount;

            public override string ToString()
            {
                return $"Decode V:{VertexCount} F:{FaceCount}";
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeDracoMesh
        {
            public int numFaces;
            public int numVertices;
            public int numAttributes;
            public byte isPointCloud;
        }

        // Decodes compressed Draco::Mesh in buffer to mesh. On input, mesh
        // must be null. The returned mesh must released with ReleaseDracoMesh.
        [DllImport(DRACODEC_UNITY_LIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int DecodeDracoMeshStep1(
            byte* buffer, uint length, NativeDracoMesh** mesh, void** decoder, void** decoderBuffer);

        // Decodes the attributes
        [DllImport(DRACODEC_UNITY_LIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int DecodeDracoMeshStep2(
            NativeDracoMesh** mesh, void* decoder, void* decoderBuffer);

        // Release data associated with DracoMesh.
        [DllImport(DRACODEC_UNITY_LIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void ReleaseDracoMesh(
            NativeDracoMesh** mesh);
    }
}
