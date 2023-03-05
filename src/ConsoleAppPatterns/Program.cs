using ConsoleAppPatterns.Creational;
using ConsoleAppPatterns.Structural;
using System;

namespace ConsoleAppPatterns
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var factoryMethod = new BridgePattern();

			factoryMethod.Main();

			Console.ReadLine();
		}
	}
}