using System;

namespace ConsoleAppPatterns.Structural.Proxy;

public class ProxyMain
{
	public void Main()
	{
		var proxy = new Proxy();

		proxy.Request(10);
		proxy.Request(20);
	}
}

public interface ISubject
{
	void Request(int value);
}

public class RealSubject : ISubject
{
	public void Request(int value)
	{
		Console.WriteLine($"{nameof(RealSubject)} + {value}");
	}
}

public class Proxy : ISubject
{
	private readonly RealSubject _subject = new();

	public void Request(int value)
	{
		if (value >= 20)
		{
			_subject.Request(value);
		}
		else
		{
			Console.WriteLine($"{nameof(Proxy)} + {value}");
		}
	}
}