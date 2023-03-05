using System;

namespace ConsoleAppPatterns.Creational;

/// <summary>
/// Client.
/// </summary>
public class AbstractFactoryPattern
{
	public void Main()
	{
		PrintHouse(new ModernHouse());
		PrintHouse(new OldHouse());
	}

	public void PrintHouse(IFactory factory)
	{
		Console.WriteLine($"House type: {factory.GetName()}");
		Console.WriteLine($"Chair type: {factory.GetChair().GetName()}");
		Console.WriteLine($"Table type: {factory.GetTable().GetName()}");
		Console.WriteLine();
	}
}

/// <summary>
/// Abstract factory.
/// </summary>
public interface IFactory
{
	IChair GetChair();

	string GetName();

	ITable GetTable();
}

public class ModernHouse : IFactory
{
	public IChair GetChair() => new ModernChair();

	public string GetName() => nameof(ModernHouse);

	public ITable GetTable() => new ModernTable();
}

public class OldHouse : IFactory
{
	public IChair GetChair() => new OldChair();

	public string GetName() => nameof(OldHouse);

	public ITable GetTable() => new OldTable();
}

/// <summary>
/// Abstract product.
/// </summary>
public interface IChair
{
	string GetName();
}

public class ModernChair : IChair
{
	public string GetName() => nameof(ModernChair);
}

public class OldChair : IChair
{
	public string GetName() => nameof(OldChair);
}

/// <summary>
/// Abstract product.
/// </summary>
public interface ITable
{
	string GetName();
}

public class ModernTable : ITable
{
	public string GetName() => nameof(ModernTable);
}

public class OldTable : ITable
{
	public string GetName() => nameof(OldTable);
}