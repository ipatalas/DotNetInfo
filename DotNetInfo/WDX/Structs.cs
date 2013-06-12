using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInfo.WDX
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Date
	{
		[MarshalAs(UnmanagedType.I2)]
		public Int16 Year;

		[MarshalAs(UnmanagedType.I2)]
		public Int16 Month;

		[MarshalAs(UnmanagedType.I2)]
		public Int16 Day;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct ContentDefaultParamStruct
	{
		//[MarshalAs(UnmanagedType.I4)]
		public int size;

		//[MarshalAs(UnmanagedType.U4)]
		public UInt32 PluginInterfaceVersionLow;

		//[MarshalAs(UnmanagedType.U4)]
		public UInt32 PluginInterfaceVersionHi;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string DefaultIniName;
	}
}
