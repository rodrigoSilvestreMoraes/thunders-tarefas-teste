using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Core.Infra.Extension
{
	[ExcludeFromCodeCoverage]
	public static class DateTimeExtension
	{
		public static DateTime DateTimeParse(this string dateTime) 
		{
			var dateSplit = dateTime.Split('-');
			DateTime resultDate;
			try
			{
				if (dateSplit.Length != 3)
					return DateTime.MinValue;

				var year = int.Parse(dateSplit[0]);
				var month = int.Parse(dateSplit[1]);
				var day = int.Parse(dateSplit[2]);

				var dateNow = DateTime.Now.ToLocalTime();
				resultDate = new DateTime(year, month, day, dateNow.Hour, dateNow.Minute, dateNow.Second);
			}
			catch
			{
				resultDate = DateTime.MinValue;
			}

			return resultDate;
		}	
	}
}
