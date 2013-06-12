using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetInfo.WDX
{
	public class Globals
	{
		public const int ft_nomorefields = 0;
		public const int ft_numeric_32 = 1;
		public const int ft_numeric_64 = 2;
		public const int ft_numeric_floating = 3;
		public const int ft_date = 4;
		public const int ft_time = 5;
		public const int ft_boolean = 6;
		public const int ft_multiplechoice = 7;
		public const int ft_string = 8;
		public const int ft_fulltext = 9;
		public const int ft_datetime = 10;
		public const int ft_stringw = 11;
		public const int ft_comparecontent = 100;

		// for ContentGetValue
		public const int ft_nosuchfield = -1;   // error, invalid field number given
		public const int ft_fileerror = -2;     // file i/o error

		public const int ft_fieldempty = -3;    // field valid, but empty
		public const int ft_ondemand = -4;      // field will be retrieved only when user presses <SPACEBAR>
		public const int ft_notsupported = -5;  // function not supported
		public const int ft_setcancel = -6;     // user clicked cancel in field editor
		public const int ft_delayed = 0;        // field takes a long time to extract -> try again in background

		public const int contst_readnewdir = 1;
		public const int contst_refreshpressed = 2;
		public const int contst_showhint = 4;
	}
}
