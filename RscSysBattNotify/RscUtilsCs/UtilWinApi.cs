using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Ressive.Utils
{
    public static class UtilWinApi
    {

        public static uint CreateProcess_WAIT(string sPath, string sCurrentDirectory, string sParameters, StringBuilder sb, bool bVisible, bool bWait = true)
        {
            bool bGetStdOut = !bVisible;

            // ATTN: DO NOT!
            //sb.Clear();

            // SRC: https://stackoverflow.com/questions/2537459/c-sharp-createpipe-protected-memory-error

            const int STARTF_USESTDHANDLES = 0x00000100;

            SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
            sa.nLength = (UInt32) System.Runtime.InteropServices.Marshal.SizeOf(sa);
            sa.lpSecurityDescriptor = IntPtr.Zero;
            sa.bInheritHandle = true;

            IntPtr attr = Marshal.AllocHGlobal(Marshal.SizeOf(sa));
            Marshal.StructureToPtr(sa, attr, true);

            /*
            IntPtr hWrite = new IntPtr();
            IntPtr hRead = new IntPtr();

            if (CreatePipe(ref hRead, ref hWrite, attr, 4096))
            {
                MessageBoxEx.Show("Pipe created !");
            }
            else
            {
                int error = Marshal.GetLastWin32Error();
                MessageBoxEx.Show("Error : Pipe not created ! LastError= " + error);
            }
            */

            IntPtr newstdin = new IntPtr();
            IntPtr write_stdin = new IntPtr();
            IntPtr read_stdout = new IntPtr();
            IntPtr newstdout = new IntPtr();

            if (bGetStdOut)
            {
                if (!CreatePipe(ref newstdin, ref write_stdin, attr, 4096))   //create stdin pipe
                {
                    return UInt32.MaxValue;
                }
                if (!CreatePipe(ref read_stdout, ref newstdout, attr, 4096))  //create stdout pipe
                {
                    CloseHandle(newstdin);
                    CloseHandle(write_stdin);

                    return UInt32.MaxValue;
                }
            }

            // SRC: https://stackoverflow.com/questions/10554913/how-to-call-createprocess-with-startupinfoex-from-c-sharp-and-re-parent-the-ch

            const uint EXTENDED_STARTUPINFO_PRESENT = 0x00080000;

            /*
            const int PROC_THREAD_ATTRIBUTE_PARENT_PROCESS = 0x00020000;
            */

            const uint CREATE_NO_WINDOW = 0x08000000;

            uint dwCreationgFlags = EXTENDED_STARTUPINFO_PRESENT;

            if (!bVisible)
            {
                dwCreationgFlags = CREATE_NO_WINDOW;
            }

            var pInfo = new PROCESS_INFORMATION();
            var sInfoEx = new STARTUPINFOEX();
            sInfoEx.StartupInfo.cb = Marshal.SizeOf(sInfoEx);
            IntPtr lpValue = IntPtr.Zero;

            if (bGetStdOut)
            {
                sInfoEx.StartupInfo.dwFlags = STARTF_USESTDHANDLES;
                sInfoEx.StartupInfo.hStdInput = newstdin;
                sInfoEx.StartupInfo.hStdOutput = newstdout;
                sInfoEx.StartupInfo.hStdError = newstdout;
            }

            var pSec = new SECURITY_ATTRIBUTES();
            var tSec = new SECURITY_ATTRIBUTES();
            pSec.nLength = (UInt32) Marshal.SizeOf(pSec);
            tSec.nLength = (UInt32) Marshal.SizeOf(tSec);
            if (CreateProcess(sPath, /*ATTN!!!*/ " " + sParameters, ref pSec, ref tSec, bGetStdOut, dwCreationgFlags, IntPtr.Zero, sCurrentDirectory, ref sInfoEx, out pInfo))
            {

                uint uiExitCode = 0;

                if (bWait)
                {
                    // SRC: https://stackoverflow.com/questions/17336227/how-can-i-wait-until-an-external-process-has-completed

                    // loop every 100 ms
                    for (; ; )
                    {
                        // SRC: https://edn.embarcadero.com/article/10387

                        if (bGetStdOut)
                        {

                            byte[] buf = new byte[1024];
                            uint bread = 0;
                            uint bavail = 0;
                            uint bleft = 0;

                            PeekNamedPipe(read_stdout, buf, 1023, ref bread, ref bavail, ref bleft);
                            //check to see if there is any data to read from stdout
                            if (bread != 0)
                            {
                                //bzero(buf);

                                if (bavail > 1023)
                                {
                                    while (bread >= 1023)
                                    {
                                        ReadFile(read_stdout, buf, 1023, out bread, IntPtr.Zero);  //read the stdout pipe

                                        //printf("%s", buf);
                                        //bzero(buf);

                                        string s = System.Text.Encoding.Default.GetString(buf, 0, (int)bread);
                                        sb.Append(s);
                                    }
                                }
                                else
                                {
                                    ReadFile(read_stdout, buf, 1023, out bread, IntPtr.Zero);

                                    //printf("%s", buf);

                                    string s = System.Text.Encoding.Default.GetString(buf, 0, (int)bread);
                                    sb.Append(s);
                                }
                            }
                        }

                        if (WaitForSingleObject(pInfo.hProcess, 0 /*100*/) == 0)
                        {
                            break;
                        }

                        if (bGetStdOut)
                        {

                            byte[] buf = new byte[1024];
                            uint bread = 0;
                            uint bavail = 0;
                            uint bleft = 0;

                            PeekNamedPipe(read_stdout, buf, 1023, ref bread, ref bavail, ref bleft);
                            //check to see if there is any data to read from stdout
                            if (bread != 0)
                            {
                                //bzero(buf);

                                if (bavail > 1023)
                                {
                                    while (bread >= 1023)
                                    {
                                        ReadFile(read_stdout, buf, 1023, out bread, IntPtr.Zero);  //read the stdout pipe

                                        //printf("%s", buf);
                                        //bzero(buf);

                                        string s = System.Text.Encoding.Default.GetString(buf, 0, (int)bread);
                                        sb.Append(s);
                                    }
                                }
                                else
                                {
                                    ReadFile(read_stdout, buf, 1023, out bread, IntPtr.Zero);

                                    //printf("%s", buf);

                                    string s = System.Text.Encoding.Default.GetString(buf, 0, (int)bread);
                                    sb.Append(s);
                                }
                            }
                        }

                        Application.DoEvents();
                    }

                    GetExitCodeProcess(pInfo.hProcess, out uiExitCode);
                }

                CloseHandle(pInfo.hProcess);
                CloseHandle(pInfo.hThread);

                if (bGetStdOut)
                {
                    CloseHandle(newstdin);
                    CloseHandle(write_stdin);
                    CloseHandle(read_stdout);
                    CloseHandle(newstdout);
                }

                return uiExitCode;
            }
            else
            {
                if (bGetStdOut)
                {
                    CloseHandle(newstdin);
                    CloseHandle(write_stdin);
                    CloseHandle(read_stdout);
                    CloseHandle(newstdout);
                }

                return UInt32.MaxValue;
            }

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetExitCodeProcess(IntPtr hProcess, out uint ExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CreateProcess(
            string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags,
            IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFOEX lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        /*
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }
        */

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFOEX
        {
            public STARTUPINFO StartupInfo;
            public IntPtr lpAttributeList;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        // SRC: https://stackoverflow.com/questions/2537459/c-sharp-createpipe-protected-memory-error

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CreatePipe(ref IntPtr hReadPipe, ref IntPtr hWritePipe, IntPtr lpPipeAttributes, int nSize);

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public UInt32 nLength;
            public IntPtr lpSecurityDescriptor;
            [MarshalAs(UnmanagedType.Bool)]
            public bool bInheritHandle;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool PeekNamedPipe(IntPtr handle,
            byte[] buffer, uint nBufferSize, ref uint bytesRead,
            ref uint bytesAvail, ref uint BytesLeftThisMessage);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadFile(IntPtr hFile, [Out] byte[] lpBuffer,
           uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);
    }
}
