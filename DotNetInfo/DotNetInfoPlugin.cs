using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using RGiesecke.DllExport;
using Utils.AssemblyDetails;

namespace DotNetInfo
{
	static class DotNetInfoPlugin
	{
		#region [ Fields & Properties ]
		static string[] fieldNames = new string[] { 
			"PublicKeyToken", "AssemblyVersion", 
			"RuntimeVersion", "Architecture" };

		static int[] fieldTypes = new int[] { 
			WDX.Globals.ft_string, WDX.Globals.ft_string, 
			WDX.Globals.ft_string, WDX.Globals.ft_string };
		#endregion
		
		[DllExport]
		static int ContentGetDetectString([Out]StringBuilder detectString, int maxlen)
		{
			detectString.Append(@"EXT=""EXE"" | EXT=""DLL""");

			return 0;
		}

		[DllExport]
		static int ContentGetSupportedField(int FieldIndex, [Out] StringBuilder FieldName, [Out] StringBuilder Units, int maxlen)
		{
			if (FieldIndex < fieldNames.Length)
			{
				FieldName.Append(fieldNames[FieldIndex]);
				return fieldTypes[FieldIndex];
			}

			return WDX.Globals.ft_nomorefields;
		}

		[DllExport]
		static int ContentGetValueW([MarshalAs(UnmanagedType.LPWStr)] string FileName, int FieldIndex, int UnitIndex, IntPtr FieldValue, int maxlen, int flags)
		{
			string result = null;
			int resultInt32 = 0;

			switch (FieldIndex)
			{
				case 0:
					result = DotNetInfoLogic.GetPublicKeyToken(FileName);
					break;
				case 1:
					result = DotNetInfoLogic.GetAssemblyVersion(FileName);
					break;
				case 2:
					result = DotNetInfoLogic.GetRuntimeVersion(FileName);
					break;
				case 3:
					result = DotNetInfoLogic.GetCPUArchitecture(FileName);
					break;
				//case 4:
				//	resultInt32 = rand.Next(900, 1000);
				//	break;
				//case 5:
				//	WDX.Date date = new WDX.Date();
				//	date.Day = 14;
				//	date.Month = 6;
				//	date.Year = 1982;

				//	//Marshal.StructureToPtr(date, FieldValue, false);

				//	Marshal.WriteInt64(FieldValue, DateTime.Now.ToFileTimeUtc());

				//	break;
				default:
					return WDX.Globals.ft_nosuchfield;
			}

			switch (fieldTypes[FieldIndex])
			{
				case WDX.Globals.ft_string:
					if (string.IsNullOrEmpty(result))
					{
						return result == null ? WDX.Globals.ft_fileerror : WDX.Globals.ft_fieldempty;
					}

					Utils.MarshalHelper.WriteANSIString(FieldValue, result, maxlen);
					break;
				case WDX.Globals.ft_numeric_32:
					Marshal.WriteInt32(FieldValue, resultInt32);
					break;
				default:
					break;
			}

			return fieldTypes[FieldIndex];
		}

		[DllExport]
		static void ContentSendStateInformationW(int state, [MarshalAs(UnmanagedType.LPWStr)] string path)
		{
			if (state == WDX.Globals.contst_readnewdir || state == WDX.Globals.contst_refreshpressed)
			{
				DotNetInfoLogic.ClearCache();
			}
		}

		[DllExport]
		static void ContentSetDefaultParams(WDX.ContentDefaultParamStruct dps)
		{
		}

		/*		
		void __stdcall ContentSetDefaultParams(ContentDefaultParamStruct* dps);
		void __stdcall ContentPluginUnloading(void);
		void __stdcall ContentStopGetValue(char* FileName);

		void __stdcall ContentStopGetValueW(WCHAR* FileName);
		int __stdcall ContentGetDefaultSortOrder(int FieldIndex);
		int __stdcall ContentGetSupportedFieldFlags(int FieldIndex);
		int __stdcall ContentSetValue(char* FileName,int FieldIndex,int UnitIndex,int FieldType,void* FieldValue,int flags);
		int __stdcall ContentSetValueW(WCHAR* FileName,int FieldIndex,int UnitIndex,int FieldType,void* FieldValue,int flags);
		int __stdcall ContentEditValue(HWND ParentWin,int FieldIndex,int UnitIndex,int FieldType, void* FieldValue,int maxlen,int flags,char* langidentifier);

		int __stdcall ContentCompareFiles(PROGRESSCALLBACKPROC progresscallback,
			int compareindex,char* filename1,char* filename2,FileDetailsStruct* filedetails);
		int __stdcall ContentCompareFilesW(PROGRESSCALLBACKPROC progresscallback,
			int compareindex,WCHAR* filename1,WCHAR* filename2,FileDetailsStruct* filedetails);
		 */
	}
}
