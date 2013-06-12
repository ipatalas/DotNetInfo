using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInfo.Utils
{
	class MarshalHelper
	{
		public static void WriteANSIString(IntPtr address, string text, int maxLength)
		{
			WriteString(address, text, maxLength, Encoding.ASCII);
		}

		public static void WriteUnicodeString(IntPtr address, string text, int maxLength)
		{
			WriteString(address, text, maxLength, Encoding.Unicode);
		}

		public static void WriteString(IntPtr address, string text, int maxLength, Encoding encoding)
		{
			if (address == IntPtr.Zero)
			{
				return;
			}

			int i = 0;

			if (!string.IsNullOrEmpty(text))
			{
				var bytes = encoding.GetBytes(text);
				for (; i < bytes.Length && i < maxLength - 1; i++)
				{
					Marshal.WriteByte(address, i, bytes[i]);
				}
			}

			Marshal.WriteByte(address, i, 0);
		}
	}
}
