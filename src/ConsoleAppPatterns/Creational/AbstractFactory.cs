using System;

namespace ConsoleAppPatterns.Creational.AbstractFactory;

public class AbstractFactoryMain
{
	public void Main()
	{
		var factoryA = new ConcreteFactoryA();
		var clientA = new AbstractFactoryClient(factoryA);
		clientA.Run();

		Console.WriteLine();

		var factoryB = new ConcreteFactoryB();
		var clientB = new AbstractFactoryClient(factoryB);
		clientB.Run();
	}
}

public class AbstractFactoryClient
{
	private readonly IProductA _productA;
	private readonly IProductB _productB;

	public AbstractFactoryClient(IFactory factory)
	{
		_productA = factory.CreateProductA();
		_productB = factory.CreateProductB();
	}

	public void Run()
	{
		Console.WriteLine(_productA.GetNameA());
		Console.WriteLine(_productB.GetNameB());
	}
}

public interface IFactory
{
	IProductA CreateProductA();

	IProductB CreateProductB();
}

public class ConcreteFactoryA : IFactory
{
	public IProductA CreateProductA()
	{
		return new ProductA1();
	}

	public IProductB CreateProductB()
	{
		return new ProductB1();
	}
}

public class ConcreteFactoryB : IFactory
{
	public IProductA CreateProductA()
	{
		return new ProductA2();
	}

	public IProductB CreateProductB()
	{
		return new ProductB2();
	}
}

public interface IProductA
{
	string GetNameA();
}

public interface IProductB
{
	string GetNameB();
}

public class ProductA1 : IProductA
{
	public string GetNameA()
	{
		return nameof(ProductA1);
	}
}

public class ProductA2 : IProductA
{
	public string GetNameA()
	{
		return nameof(ProductA2);
	}
}

public class ProductB1 : IProductB
{
	public string GetNameB()
	{
		return nameof(ProductB1);
	}
}

public class ProductB2 : IProductB
{
	public string GetNameB()
	{
		return nameof(ProductB2);
	}
}