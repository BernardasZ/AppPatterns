using System;

namespace ConsoleAppPatterns._1._Other;

public class MonadPattern
{
	public void Main()
	{
		var repository = new Repository();

		Maybe<Shipper> shipper = repository
			.GetCustomer()
			.Bind(customer => customer.Address)
			.Bind(address => repository.GetAddress())
			.Bind(address => address.Order)
			.Bind(order => repository.GetOrder())
			.Bind(order => order.Shipper);
	}

	public City NextTalkCity(Speaker speaker)
	{
		return speaker
			?.NextTalk()
			?.GetConference()
			?.GetCity();
	}
}

public class Speaker
{
	public Talk NextTalk() => new Talk();
}

public class Talk
{
	public Conference GetConference() => new Conference();
}

public class Conference
{
	public City GetCity() => new City();
}

public class City
{
}

public class Maybe<T>
	where T : class
{
	private readonly T value;

	public Maybe(T someValue)
	{
		if (someValue == null)
		{
			throw new ArgumentNullException(nameof(someValue));
		}

		value = someValue;
	}

	private Maybe()
	{
	}

	public Maybe<U> Bind<U>(Func<T, Maybe<U>> func)
		where U : class
	{
		return value != null
			? func(value)
			: Maybe<U>.None();
	}

	public static Maybe<T> None() => new Maybe<T>();
}

public interface IRepository
{
	Maybe<Customer> GetCustomer();

	Maybe<Address> GetAddress();

	Maybe<Order> GetOrder();
}

public class Repository : IRepository
{
	public Maybe<Address> GetAddress() => new Maybe<Address>(new Address());

	public Maybe<Customer> GetCustomer() => new Maybe<Customer>(new Customer());

	public Maybe<Order> GetOrder() => new Maybe<Order>(new Order());
}

public class Customer
{
	public Maybe<Address> Address { get; set; }
}

public class Address
{
	public Maybe<Order> Order { get; set; }
}

public class Order
{
	public Maybe<Shipper> Shipper { get; set; }
}

public class Shipper
{
}