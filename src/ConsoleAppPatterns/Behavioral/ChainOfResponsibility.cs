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

		handlerA.HandleRequest(string.Empty);
	}
}

public abstract class Handler
{
	protected Handler Successor;

	public void SetSuccessor(Handler successor)
	{
		Successor = successor;
	}

	public string PassToOtherHandler(string request)
	{
		return Successor != null
			? Successor.HandleRequest(request)
			: request;
	}

	public abstract string HandleRequest(string request);
}

public class ConcreteHandlerA : Handler
{
	public override string HandleRequest(string request)
	{
		return PassToOtherHandler($"{request} and {nameof(ConcreteHandlerA)}");
	}
}

public class ConcreteHandlerB : Handler
{
	public override string HandleRequest(string request)
	{
		return PassToOtherHandler($"{request} and {nameof(ConcreteHandlerB)}");
	}
}

public class ConcreteHandlerC : Handler
{
	public override string HandleRequest(string request)
	{
		return PassToOtherHandler($"{request} and {nameof(ConcreteHandlerC)}");
	}
}