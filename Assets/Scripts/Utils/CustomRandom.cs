﻿namespace Utils
{
	public class CustomRandom
	{
		int a, b, c, number;
		static string Pi = "141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148086513282306647093844609550582231725359408128481117450284102701938521105559644622948954930381964428810975665933446128475648233786783165271201909145648566923460348610454326648213393607260249141273724587006";

		public CustomRandom(int x, int y, int levelNumber) {
			a = x;
			b = y;
			c = levelNumber;
			number = a * b + a;
			number = number * c + number;
			number = (number * levelNumber + number) % Pi.Length;
		}

		public int Next(int m) {
			number = (number + 1) % Pi.Length;
			return Pi[number] % m;
		}
	}
}