using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils.AssemblyDetails;

namespace DotNetInfo
{
	public static class DotNetInfoLogic
	{
		static Dictionary<string, AssemblyDetails> assemblyDetailsCache = new Dictionary<string, AssemblyDetails>(); 

		public static void ClearCache()
		{
			assemblyDetailsCache.Clear();
		}
		
		public static string GetPublicKeyToken(string filename)
		{
			try
			{
				var details = GetAssemblyDetails(filename);
				if (details != null)
				{
					var name = AssemblyName.GetAssemblyName(filename);
					return name.GetPublicKeyToken().Aggregate(new StringBuilder(), (sb, b) => sb.AppendFormat("{0:x2}", b)).ToString();
				}
			}
			catch (BadImageFormatException)
			{

			}

			return null;
		}

		public static string GetAssemblyVersion(string filename)
		{
			try
			{
				var details = GetAssemblyDetails(filename);
				if (details != null)
				{
					var name = AssemblyName.GetAssemblyName(filename);
					return name.Version.ToString();
				}
			}
			catch (BadImageFormatException)
			{

			}

			return null;
		}

		public static string GetRuntimeVersion(string filename)
		{
			try
			{
				var details = GetAssemblyDetails(filename);
				if (details != null)
				{
					return details.FrameworkVersion.ToString();
				}
			}
			catch (BadImageFormatException)
			{

			}

			return null;
		}

		public static string GetCPUArchitecture(string filename)
		{
			try
			{
				var details = GetAssemblyDetails(filename);
				if (details != null)
				{
					return details.CPUVersion.ToString();
				}
			}
			catch (BadImageFormatException)
			{

			}

			return null;
		}

		static AssemblyDetails GetAssemblyDetails(string filename)
		{
			if (!assemblyDetailsCache.ContainsKey(filename))
			{
				assemblyDetailsCache.Add(filename, AssemblyDetails.FromFile(filename));
			}

			return assemblyDetailsCache[filename];
		} 
	}
}
