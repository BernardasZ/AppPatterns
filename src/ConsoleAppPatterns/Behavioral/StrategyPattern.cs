using System;

namespace ConsoleAppPatterns.Behavioral;

public class StrategyPattern
{
	public void Main()
	{
		Context context;

		context = new Context(new ConcreteStrategyA());
		context.Execute();

		context = new Context(new ConcreteStrategyB());
		context.Execute();

		context = new Context(new ConcreteStrategyC());
		context.Execute();
	}

	public interface IStrategy
	{
		void DoSomething();
	}

	public class Context
	{
		private readonly IStrategy _strategy;

		public Context(IStrategy strategy) => _strategy = strategy;

		public void Execute() => _strategy.DoSomething();
	}

	public class ConcreteStrategyA : IStrategy
	{
		public void DoSomething() => Console.WriteLine(nameof(ConcreteStrategyA));
	}

	public class ConcreteStrategyB : IStrategy
	{
		public void DoSomething() => Console.WriteLine(nameof(ConcreteStrategyB));
	}

	public class ConcreteStrategyC : IStrategy
	{
		public void DoSomething() => Console.WriteLine(nameof(ConcreteStrategyC));
	}
}