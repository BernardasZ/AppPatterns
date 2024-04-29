namespace ConsoleAppPatterns.Behavioral;

public class ChainOfResponsibility
{
	public void Main()
	{
		var handlerA = new ConcreteHandlerA();
		var handlerB = new ConcreteHandlerB();
		var handlerC = new ConcreteHandlerC();

		handlerA.SetSuccessor(handlerB);
		handlerB.SetSuccessor(handlerC);

		_ = handlerA.HandleRequest("");
	}
}

public abstract class Handler
{
	protected Handler _successor;

	public void SetSuccessor(Handler successor)
	{
		_successor = successor;
	}

	public string PassToOtherHandler(string request)
	{
		return _successor != null
			? _successor.HandleRequest(request)
			: request;
	}

	public abstract string HandleRequest(string request);
}

public class ConcreteHandlerA : Handler
{
	public override string HandleRequest(string request)
	{
		return PassToOtherHandler(request + "A");
	}
}

public class ConcreteHandlerB : Handler
{
	public override string HandleRequest(string request)
	{
		return PassToOtherHandler(request + "B");
	}
}

public class ConcreteHandlerC : Handler
{
	public override string HandleRequest(string request)
	{
		return PassToOtherHandler(request + "C");
	}
}