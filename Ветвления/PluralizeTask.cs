namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			var hundredDivisionRemainder = count % 100;
			if (hundredDivisionRemainder < 5 || hundredDivisionRemainder > 20)
			{
				var tenDivisionRemainder = count % 10;
				
				if (tenDivisionRemainder == 1)
					return "рубль";
				if (tenDivisionRemainder >= 2 && tenDivisionRemainder <= 4)
					return "рубля";
			}
			return "рублей";
		}
	}
}