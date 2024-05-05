using System;

namespace ConsoleAppPatterns.Structural;

public class ProxyMain
{
	public void Main()
	{
		var proxy = new Proxy();

		proxy.Request();
	}
}

public interface ISubject
{
	void Request();
}

public class RealSubject : ISubject
{
	public void Request()
	{
		Console.WriteLine(nameof(RealSubject));
	}
}

public class Proxy : ISubject
{
	private readonly RealSubject _subject = new();

	public void Request()
	{
		_subject.Request();
	}
}