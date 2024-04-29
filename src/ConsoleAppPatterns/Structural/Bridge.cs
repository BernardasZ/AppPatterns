using System;

namespace ConsoleAppPatterns.Structural;

public class Bridge
{
	public void Main()
	{
		RefinedAbstraction bridge = new AbstractionA();
		bridge.Implementator = new ConcreteImplementatornA();
		bridge.Execute();

		bridge.Implementator = new ConcreteImplementatornB();
		bridge.Execute();

		bridge = new AbstractionB();
		bridge.Implementator = new ConcreteImplementatornA();
		bridge.Execute();

		bridge.Implementator = new ConcreteImplementatornB();
		bridge.Execute();
	}

	public interface IImplementator
	{
		void Execute(string text);
	}

	public abstract class RefinedAbstraction
	{
		public IImplementator Implementator { get; set; }

		public abstract void Execute();
	}

	public class AbstractionA : RefinedAbstraction
	{
		public override void Execute()
		{
			Implementator.Execute(nameof(AbstractionA));
		}
	}

	public class AbstractionB : RefinedAbstraction
	{
		public override void Execute()
		{
			Implementator.Execute(nameof(AbstractionB));
		}
	}

	public class ConcreteImplementatornA : IImplementator
	{
		public void Execute(string text)
		{
			Console.WriteLine($"{text} and {nameof(ConcreteImplementatornA)}");
		}
	}

	public class ConcreteImplementatornB : IImplementator
	{
		public void Execute(string text)
		{
			Console.WriteLine($"{text} and {nameof(ConcreteImplementatornB)}");
		}
	}
}