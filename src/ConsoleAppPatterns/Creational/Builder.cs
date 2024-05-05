using System;

namespace ConsoleAppPatterns.Creational.Builder;

public class BuilderMain
{
	public void Main()
	{
		var builderA = new ConcreteBuilderA();
		var directorA = new Director(builderA);

		directorA.Build();
		directorA.Run();

		Console.WriteLine();

		var builderB = new ConcreteBuilderB();
		var directorB = new Director(builderB);

		directorB.Build();
		directorB.Run();
	}
}

public interface IBuilder
{
	void SetName();

	void SetType();

	IProduct GetResult();
}

public interface IProduct
{
	string Name { get; set; }

	string Type { get; set; }
}

public class ConcreteBuilderA : IBuilder
{
	private readonly ProductA _product;

	public ConcreteBuilderA()
	{
		_product = new ProductA();
	}

	public void SetName()
	{
		_product.Name = "A";
	}

	public void SetType()
	{
		_product.Type = nameof(ConcreteBuilderA);
	}

	public IProduct GetResult()
	{
		return _product;
	}
}

public class ProductA : IProduct
{
	public string Name { get; set; }

	public string Type { get; set; }
}

public class ConcreteBuilderB : IBuilder
{
	private readonly ProductB _product;

	public ConcreteBuilderB()
	{
		_product = new ProductB();
	}

	public void SetName()
	{
		_product.Name = "B";
	}

	public void SetType()
	{
		_product.Type = nameof(ConcreteBuilderB);
	}

	public IProduct GetResult()
	{
		return _product;
	}
}

public class ProductB : IProduct
{
	public string Name { get; set; }

	public string Type { get; set; }
}

public class Director
{
	private readonly IBuilder _builder;

	public Director(IBuilder builder)
	{
		_builder = builder;
	}

	public void Build()
	{
		_builder.SetType();
		_builder.SetName();
	}

	public void Run()
	{
		var product = _builder.GetResult();
		Console.WriteLine($"{product.Type}, {product.Name}");
	}
}