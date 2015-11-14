using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverCore.Logic
{
    public static class SharedMemoryHelper
    {
        public static string NameOfSharedMemory = "Local\\Enslaver";
        public static ParameterizedLock Lock = new ParameterizedLock();
        public static uint Invalid_Handle_Value = 0xFFFFFFFF;
        public static string LockKey = "SharedMemoryLock";
        public static uint MaxSizeOfMappedFile = 10 * 1024 * 1024;
        public const uint DefaultSizeOfMapView = 32 * 1024;

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto)]
        public static extern int GetAdaptersInfo(IntPtr pAdapterInfo, ref int pBufOutLen);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile
            );

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(
            IntPtr hFile,
            IntPtr lpFileMappingAttributes,
            FileMapProtection flProtect,
            uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow,
            string lpName);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileForMapping(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes, // set null
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(
             uint dwDesiredAccess,
             bool bInheritHandle,
             string lpName);


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        private static uint _SizeOfMapView = 0;

        public static uint GetSizeOfMapView
        {
            get
            {
                try
                {
                    if (_SizeOfMapView == 0)
                    {
                        SystemInfo systemInfo = new SystemInfo();
                        GetSystemInfo(out systemInfo);
                        return systemInfo.AllocationGranularity;
                    }
                    return _SizeOfMapView;
                }
                catch { return DefaultSizeOfMapView; }
            }
        }


        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public static bool TryToCloseHandle(IntPtr handleObject)
        {
            try
            {
                if (handleObject != IntPtr.Zero)
                {
                    CloseHandle(handleObject);
                }
                return true;
            }
            catch { return false; };
        }

        [DllImport("kernel32.dll", SetLastError = false)]
        public static extern void GetSystemInfo(out SystemInfo Info);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(
            IntPtr hFileMappingObject,
            uint dwDesiredAccess,
            uint dwFileOffsetHigh,
            uint dwFileOffsetLow,
            uint dwNumberOfBytesToMap);



        /// <summary>
        /// Возвращает Unicode содержимое переданных байтов (bytes). Если skipSizeBytes= false , то необходимо пропустить первые 4 байта ( размер содержимого); иначе - нужно полностью преобразовать в текст
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="sizeOfContent"></param>
        /// <param name="skipSizeBytes"></param>
        /// <returns></returns>
        private static string GetTextContent(byte[] bytes, uint sizeOfContent, bool skipSizeBytes = false)
        {
            if (bytes == null || sizeOfContent == 0)
                return "";
            byte[] textContentBin = new byte[sizeOfContent];
            Array.Copy(bytes, (skipSizeBytes) ? 4 : 0, textContentBin, 0, sizeOfContent);
            string textContent = GetUnicodeStringFromBytes(textContentBin);
            return textContent;
        }

        public static string GetUnicodeStringFromBytes(byte[] bytes)
        {
            try
            {
                if (bytes == null)
                    return "";
                return UnicodeEncoding.Unicode.GetString(bytes);
            }
            catch
            {
                return null;
            }
        }


        

        public static string GetTextContentFromMemory(string memoryPath, IntPtr specificFileMapHandler, ref IntPtr specificHandlerOfMapView)
        {
            byte[] bytes = null;
            uint sizeOfContent = 0;
            
                SharedMemoryHelper.ReadContentBytesOfByChunks(specificFileMapHandler, ref specificHandlerOfMapView, ref bytes, ref sizeOfContent);
                string result = GetTextContent(bytes, sizeOfContent);
                
                return result;
        }


        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);


        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public static bool ReadContentBytesOfByChunks(IntPtr handlerOfFileMappingObject, ref IntPtr handlerOfMapView, ref byte[] bytes, ref uint contentSize)
        {
            try
            {
                bool result = true;
                uint size = 0,
                     sizeOfChunk = GetSizeOfMapView;
                byte[] sizeOfContentBytes = new byte[4],
                        readedBytes = null;
                if (handlerOfMapView != IntPtr.Zero)
                {
                    IntPtr somehandlerOfMapView = new IntPtr(handlerOfMapView.ToInt64());
                    Lock.RunWithLock(LockKey, () =>
                    {
                        Marshal.Copy(somehandlerOfMapView, sizeOfContentBytes, 0, sizeOfContentBytes.Length);
                        size = BitConverter.ToUInt32(sizeOfContentBytes, 0);
                        size = ((size + sizeOfContentBytes.Length) > MaxSizeOfMappedFile) ? MaxSizeOfMappedFile : size;
                        readedBytes = new byte[size + sizeOfContentBytes.Length];
                        uint readSize = sizeOfChunk,
                            countOfChunks = (uint)readedBytes.Length / sizeOfChunk,
                            additionalsBytes = (uint)readedBytes.Length % sizeOfChunk,
                            offsetValue = 0,
                            readedChunks = 0;
                        bool exit = false;
                        do
                        {
                            offsetValue = (uint)readedChunks * sizeOfChunk;
                            if (readedChunks == countOfChunks)
                            {
                                readSize = additionalsBytes;
                                exit = true;
                            }
                            if (ReMapViewOfFile(handlerOfFileMappingObject, ref  somehandlerOfMapView, offsetValue, readSize) == true)
                            {
                                Marshal.Copy(somehandlerOfMapView, readedBytes, (int)offsetValue, (int)readSize);
                                readedChunks += (uint)((readedChunks < countOfChunks) ? 1 : 0);
                            }
                            else
                            {
                                result = false;
                                exit = true;
                            }
                        }
                        while (!exit);
                    });
                    bytes = new byte[size];
                    contentSize = size;
                    Array.Copy(readedBytes, sizeOfContentBytes.Length, bytes, 0, size);
                    UnmapAndCloseMapHandler(somehandlerOfMapView);
                    //Устанавливаем позицию в начало..
                    ReMapViewOfFile(handlerOfFileMappingObject, ref  handlerOfMapView, 0, sizeOfChunk);
                    return result;
                }
                return false;
            }
            catch
            {
                bytes = null;
                contentSize = 0;
                return false;
            }
        }

        public static bool ReMapViewOfFile(IntPtr hFileMappingObject, ref IntPtr handlerOfMapView, uint offset, uint size)
        {
            if (hFileMappingObject == IntPtr.Zero)
            {
                return false;
            }
            if (handlerOfMapView != IntPtr.Zero)
            {
                UnmapAndCloseMapHandler(handlerOfMapView);
            }
            //т.к. файлы меньше чем 2^32
            uint dwFileOffsetHigh = 0;
            uint dwFileOffsetLow = offset;
            handlerOfMapView = MapViewOfFile(hFileMappingObject, (uint)FileMapAccess.FileMapAllAccess, dwFileOffsetHigh, dwFileOffsetLow, size);
            uint errorCode = GetLastError();
            return (handlerOfMapView != IntPtr.Zero);
        }

        /// <summary>
        /// Прочитать за один раз всю память ( первые 4 байта, отвечающие за размер, - не включаются)
        /// </summary>
        /// <param name="handlerOfMapView"></param>
        /// <param name="bytes">текстовое содержимое в виде байтов</param>
        /// <param name="contentSize">здесь размер текстового содержимого</param>
        /// <returns></returns>
        public static bool ReadContentBytesAtOnce(IntPtr handlerOfMapView, ref byte[] bytes, ref uint contentSize)
        {
            if (handlerOfMapView != IntPtr.Zero)
            {
                byte[] sizeOfContentBytes = new byte[4];
                byte[] readedBytes = null;
                uint size = 0;
                Lock.RunWithLock(LockKey, () =>
                {
                    Marshal.Copy(handlerOfMapView, sizeOfContentBytes, 0, sizeOfContentBytes.Length);
                    size = BitConverter.ToUInt32(sizeOfContentBytes, 0);
                    size = (size > MaxSizeOfMappedFile) ? MaxSizeOfMappedFile : size;
                    readedBytes = new byte[size + 4];
                    Marshal.Copy(handlerOfMapView, readedBytes, 0, readedBytes.Length);
                });
                bytes = new byte[size];
                Array.Copy(readedBytes, 4, bytes, 0, size);
                contentSize = size;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Преобразовать строку в байты
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromUnicodeString(string str)
        {
            try
            {
                return System.Text.UnicodeEncoding.Unicode.GetBytes(str);
            }
            catch
            {
                return null;
            }
        }

        public static bool WriteBytesByChunks(IntPtr handlerOfFileMappingObject, ref IntPtr handlerOfMapView, byte[] bytes)
        {
            bool result = true;
            uint sizeOfChunk = GetSizeOfMapView;
            if (handlerOfMapView != IntPtr.Zero)
            {
                if (bytes != null)
                {
                    IntPtr somehandlerOfMapView = new IntPtr(handlerOfMapView.ToInt64());
                    Lock.RunWithLock(LockKey, () =>
                    {
                        byte[] allBytes = PrepareBytes(bytes);
                        uint writeSize = sizeOfChunk,
                        countOfChunks = (uint)allBytes.Length / sizeOfChunk,
                        additionalsBytes = (uint)allBytes.Length % sizeOfChunk,
                        offsetValue = 0,
                        writedChunks = 0;
                        bool exit = false;
                        do
                        {
                            offsetValue = (uint)writedChunks * sizeOfChunk;
                            if (writedChunks == countOfChunks)
                            {
                                writeSize = additionalsBytes;
                                exit = true;
                            }
                            if (ReMapViewOfFile(handlerOfFileMappingObject, ref  somehandlerOfMapView, offsetValue, writeSize) == true)
                            {
                                Marshal.Copy(allBytes, (int)offsetValue, somehandlerOfMapView, (int)writeSize);
                                writedChunks += (uint)((writedChunks < countOfChunks) ? 1 : 0);
                            }
                            else
                            {
                                result = false;
                                exit = true;
                            }
                        }
                        while (!exit);
                    });
                    //Устанавливаем позицию в начало..                        
                    UnmapAndCloseMapHandler(somehandlerOfMapView);
                    ReMapViewOfFile(handlerOfFileMappingObject, ref  handlerOfMapView, 0, sizeOfChunk);
                }
                else
                {
                    Marshal.WriteInt32(handlerOfMapView, 0);
                }
                return result;
            }
            else
            {
                throw new Exception("Не задан handler!");
            }
        }

        public static bool WriteBytesAtOnce(IntPtr handlerOfMapView, byte[] bytes)
        {
            /*if (handlerOfMapView != IntPtr.Zero)
            {
                Lock.RunWithLock(LockKey, () =>
                {
                    if (bytes != null)
                    {
                        byte[] allBytes = PrepareBytes(bytes);
                        Marshal.Copy(allBytes, 0, handlerOfMapView, allBytes.Length);
                    }
                    else
                    {
                        Marshal.WriteInt32(handlerOfMapView, 0);
                    }
                });
                return true;
            }
            else
            {
                throw new Exception("Не задан handler!");
            }*/
        }

        /// <summary>
        /// Функция возвращает все байты: содержимое bytes плюс размер
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static byte[] PrepareBytes(byte[] bytes)
        {
            byte[] sizeOfBytes = BitConverter.GetBytes((uint)bytes.Length),
                    allBytes = new byte[bytes.Length + sizeOfBytes.Length];
            Array.Copy(bytes, 0, allBytes, sizeOfBytes.Length, bytes.Length);
            Array.Copy(sizeOfBytes, 0, allBytes, 0, sizeOfBytes.Length);
            return allBytes;
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public static bool OpenSharedMemory(string fileMapName, ref IntPtr fileMapHandler, ref IntPtr handlerOfMapView)
        {
            bool? result = null;
            try
            {
                fileMapHandler = SharedMemoryHelper.OpenFileMapping((uint)FileMapAccess.FileMapAllAccess, true, fileMapName);
                if (fileMapHandler == IntPtr.Zero)
                {
                    // Не удалось получить fileMapHandler
                    result = false;
                }
                if (result != false)
                {
                    handlerOfMapView = SharedMemoryHelper.MapViewOfFile(fileMapHandler, (uint)FileMapAccess.FileMapAllAccess, 0, 0, GetSizeOfMapView);
                    if (handlerOfMapView == IntPtr.Zero)
                    {
                        // Не удалось получить handlerOfMapView 
                        result = false;
                    }
                }
                return true;
            }
            catch
            {
                result = false;
                return result.Value;
            }
            finally
            {
                if (result == false)
                {
                    CloseHandlers(fileMapHandler, handlerOfMapView);
                }
            }
        }

        public static void CloseHandlers(IntPtr fileMapHandler, IntPtr handlerOfMapView)
        {
            UnmapAndCloseMapHandler(handlerOfMapView);
            TryToCloseHandle(fileMapHandler);
        }

        private static void UnmapAndCloseMapHandler(IntPtr handlerOfMapView)
        {
            TryToUnmapViewOfFile(handlerOfMapView);
        }


        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public static bool CreateSharedMemory(string fileMapName, ref IntPtr fileMapHandler, ref IntPtr handlerOfMapView)
        {
            try
            {
                uint dwFileOffsetHigh = 0;
                uint dwFileOffsetLow = 0;
                uint dwNumberOfBytesToMap = GetSizeOfMapView;

                fileMapHandler = CreateFileMapping(
                   new IntPtr((int)Invalid_Handle_Value),
                   IntPtr.Zero,
                    FileMapProtection.PageReadWrite,
                    0,
                    MaxSizeOfMappedFile,
                    fileMapName);
                if (fileMapHandler == IntPtr.Zero)
                {
                    throw new Exception("Не удалось получить fileMapHandler");
                }
                handlerOfMapView = MapViewOfFile(fileMapHandler, (uint)FileMapAccess.FileMapAllAccess, dwFileOffsetHigh, dwFileOffsetLow, dwNumberOfBytesToMap);
                if (handlerOfMapView == IntPtr.Zero)
                {
                    uint errorCode = GetLastError();
                    throw new Exception(string.Format("Не удалось получить handlerOfMapView (ErrorCode={0}", errorCode));
                }
                return true;
            }
            catch
            {
                CloseHandlers(fileMapHandler, handlerOfMapView);
                return false;
            }
        }


        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        private static bool TryToUnmapViewOfFile(IntPtr handlerOfMapView)
        {
            if (handlerOfMapView != IntPtr.Zero)
            {
                try
                {
                    UnmapViewOfFile(handlerOfMapView);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
    }
    [Flags]
    public enum FileMapAccess : uint
    {
        FileMapCopy = 0x0001,
        FileMapWrite = 0x0002,
        FileMapRead = 0x0004,
        FileMapReadWrite = 0x0006,
        FileMapAllAccess = 0x001f,
        FileMapExecute = 0x0020,
    }

    [Flags]
    public enum FileMapProtection : uint
    {
        PageReadonly = 0x02,
        PageReadWrite = 0x04,
        PageWriteCopy = 0x08,
        PageExecuteRead = 0x20,
        PageExecuteReadWrite = 0x40,
        SectionCommit = 0x8000000,
        SectionImage = 0x1000000,
        SectionNoCache = 0x10000000,
        SectionReserve = 0x4000000,
    }

    public enum ProcessorArchitecture
    {
        X86 = 0,
        X64 = 9,
        @Arm = -1,
        Itanium = 6,
        Unknown = 0xFFFF,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SystemInfo
    {
        public ProcessorArchitecture ProcessorArchitecture; // WORD
        public uint PageSize; // DWORD
        public IntPtr MinimumApplicationAddress; // (long)void*
        public IntPtr MaximumApplicationAddress; // (long)void*
        public IntPtr ActiveProcessorMask; // DWORD*
        public uint NumberOfProcessors; // DWORD 
        public uint ProcessorType; // DWORD
        public uint AllocationGranularity; // DWORD
        public ushort ProcessorLevel; // WORD
        public ushort ProcessorRevision; // WORD
    }
}
