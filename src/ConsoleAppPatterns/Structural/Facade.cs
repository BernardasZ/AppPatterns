using System;

namespace ConsoleAppPatterns.Structural;

public class FacadeMain
{
	public void Main()
	{
		var facade = new Facade(new SubSystemA(), new SubSystemB(), new SubSystemC());

		facade.Operation();
	}
}

public class Facade
{
	private readonly SubSystemA _systemA;
	private readonly SubSystemB _systemB;
	private readonly SubSystemC _systemC;

	public Facade(SubSystemA systemA, SubSystemB systemB, SubSystemC systemC)
	{
		_systemA = systemA;
		_systemB = systemB;
		_systemC = systemC;
	}

	public void Operation()
	{
		_systemA.DoSomething();
		_systemB.DoSomething();
		_systemC.DoSomething();
	}
}

public class SubSystemA
{
	public void DoSomething()
	{
		Console.WriteLine($"{nameof(SubSystemA)}");
	}
}

public class SubSystemB
{
	public void DoSomething()
	{
		Console.WriteLine($"{nameof(SubSystemB)}");
	}
}

public class SubSystemC
{
	public void DoSomething()
	{
		Console.WriteLine($"{nameof(SubSystemC)}");
	}
}