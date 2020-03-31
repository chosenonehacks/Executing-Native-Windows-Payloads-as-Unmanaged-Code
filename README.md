# Executing Native Windows Payloads as Unmanaged Code
Metasploit payloads are generated in 32- or 64-bit assembly code—called
unmanaged code in the .NET world. When you compile C# code into a DLL
or executable assembly, that code is referred to as managed code. The difference between the two is that the managed code requires a .NET or Mono
virtual machine in order to run, whereas the unmanaged code can be run
directly by the operating system.
