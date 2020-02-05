using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
	public class Utils
	{
		public static string CnvToStr(object Data)

		{

			try

			{

				return Data.ToString();

			}

			catch

			{

				return "";

			}

		}



		public static Int64 CnvToInt64(object Data)

		{

			try

			{

				return Convert.ToInt64(Data);

			}

			catch

			{

				return 0;

			}

		}





		public static Int32? CnvToNullableInt(object Data)

		{

			try

			{



				if (Data != null)

					return Convert.ToInt32(Data);

				else

					return null;

			}

			catch

			{

				return null;

			}

		}

		public static double? CnvToNullableDouble(object Data)

		{

			try

			{

				if (Data != null)

					return Convert.ToDouble(Data);

				else

					return null;

			}

			catch

			{

				return null;

			}



		}
	}
}
