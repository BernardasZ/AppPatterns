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
		_systemA.DoSomethingA();
		_systemB.DoSomethingB();
		_systemC.DoSomethingC();
	}
}

public class SubSystemA
{
	public void DoSomethingA()
	{
		System.Console.WriteLine("Operation A");
	}
}

public class SubSystemB
{
	public void DoSomethingB()
	{
		System.Console.WriteLine("Operation B");
	}
}

public class SubSystemC
{
	public void DoSomethingC()
	{
		System.Console.WriteLine("Operation C");
	}
}