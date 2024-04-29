using System;

namespace ConsoleAppPatterns.Creational;

public class AbstractFactory
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

public interface IFactory
{
	IChair GetChair();

	string GetName();

	ITable GetTable();
}

public class ModernHouse : IFactory
{
	public IChair GetChair()
	{
		return new ModernChair();
	}

	public string GetName()
	{
		return nameof(ModernHouse);
	}

	public ITable GetTable()
	{
		return new ModernTable();
	}
}

public class OldHouse : IFactory
{
	public IChair GetChair()
	{
		return new OldChair();
	}

	public string GetName()
	{
		return nameof(OldHouse);
	}

	public ITable GetTable()
	{
		return new OldTable();
	}
}

public interface IChair
{
	string GetName();
}

public class ModernChair : IChair
{
	public string GetName()
	{
		return nameof(ModernChair);
	}
}

public class OldChair : IChair
{
	public string GetName()
	{
		return nameof(OldChair);
	}
}

public interface ITable
{
	string GetName();
}

public class ModernTable : ITable
{
	public string GetName()
	{
		return nameof(ModernTable);
	}
}

public class OldTable : ITable
{
	public string GetName()
	{
		return nameof(OldTable);
	}
}