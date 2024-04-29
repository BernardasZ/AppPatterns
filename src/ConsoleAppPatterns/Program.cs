using System;
using BenchmarkDotNet.Running;
using ConsoleAppPatterns._2._Experimental;
using ConsoleAppPatterns.Structural;

namespace ConsoleAppPatterns;

internal class Program
{
	private static void Main(string[] args)
	{
		Console.WriteLine("Run benchmark: (Y/N)?");
		var option = Console.ReadLine();

		try
		{
			if (option.ToLower() == "Y")
			{
				RunBenchmark();
			}
			else
			{
				RunOther();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}

		Console.ReadLine();
	}

	private static void RunBenchmark()
	{
		BenchmarkRunner.Run(typeof(Delegates));
	}

	private static void RunOther()
	{
		new DecoratorMain().Main();
	}
}