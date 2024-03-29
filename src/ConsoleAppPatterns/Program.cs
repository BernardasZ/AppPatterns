﻿using BenchmarkDotNet.Running;
using ConsoleAppPatterns._2._Experimental;

namespace ConsoleAppPatterns;

internal class Program
{
	private static void Main(string[] args)
	{
		//var factoryMethod = new BridgePattern();

		//factoryMethod.Main();

		//Console.ReadLine();

		RunBenchmark();
	}

	private static void RunBenchmark() => BenchmarkRunner.Run(typeof(Delegates));
}