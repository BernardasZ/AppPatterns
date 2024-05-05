using System;

namespace ConsoleAppPatterns.Structural.Decorator;

public class DecoratorMain
{
	public void Main()
	{
		var component = new ConcreteComponent();
		var decorator = new Decorator(component);
		var concreteDecorator = new ConcreteDecorator(decorator);

		Console.WriteLine(component.Operation());
		Console.WriteLine(decorator.Operation());
		Console.WriteLine(concreteDecorator.Operation());
		Console.WriteLine(concreteDecorator.AddedBehaviour());
	}
}

public interface IComponent
{
	string Operation();
}

public class ConcreteComponent : IComponent
{
	public virtual string Operation()
	{
		return $"{nameof(ConcreteComponent)} operation \r\n";
	}
}

public class Decorator : IComponent
{
	protected readonly IComponent Component;

	public Decorator(IComponent component)
	{
		Component = component;
	}

	public virtual string Operation()
	{
		var operation = Component.Operation();
		var newOperation = $"{nameof(Decorator)} operation \r\n";

		return operation + newOperation;
	}
}

public class ConcreteDecorator : Decorator
{
	public ConcreteDecorator(IComponent component)
		: base(component)
	{
	}

	public override string Operation()
	{
		var operation = Component.Operation();
		var newOperation = $"{nameof(ConcreteDecorator)} operation \r\n";

		return operation + newOperation;
	}

	public string AddedBehaviour()
	{
		return $"{nameof(ConcreteDecorator)} added behaviour \r\n";
	}
}