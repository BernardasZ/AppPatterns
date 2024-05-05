using System;

namespace ConsoleAppPatterns.Creational.FactoryMethod;
public class FactoryMethod
{
	public void Main()
	{
		var factory = new Factory();
		var client = new FactoryMethodClient(factory);

		client.Run(nameof(ConcreteProductA));

		Console.WriteLine();

		client.Run(nameof(ConcreteProductB));
	}
}

public interface IProduct
{
	string GetName();
}

public class ConcreteProductA : IProduct
{
	public string GetName()
	{
		return nameof(ConcreteProductA);
	}
}

public class ConcreteProductB : IProduct
{
	public string GetName()
	{
		return nameof(ConcreteProductB);
	}
}

public class FactoryMethodClient
{
	private readonly Factory _factory;

	public FactoryMethodClient(Factory factory)
	{
		_factory = factory;
	}

	public void Run(string typeName)
	{
		var product = _factory.Create(typeName);

		Console.WriteLine(product.GetName());
	}
}

public class Factory
{
	public IProduct Create(string typeName)
	{
		IProduct product = typeName switch
		{
			"ConcreteProductA" => new ConcreteProductA(),
			"ConcreteProductB" => new ConcreteProductB(),
			_ => throw new NotImplementedException()
		};

		return product;
	}
}