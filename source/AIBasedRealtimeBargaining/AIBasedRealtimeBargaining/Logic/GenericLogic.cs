namespace System
{
	public class GenericLogic
	{
		public static DateTime IstNow
		{
			get
			{
				return System.DateTime.UtcNow.AddHours(5.5);
			}
		}
	}
}
