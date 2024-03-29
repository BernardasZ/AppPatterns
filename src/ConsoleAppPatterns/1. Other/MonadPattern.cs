﻿using System;

namespace ConsoleAppPatterns._1._Other;

public class MonadPattern
{
	public void Main()
	{
		var repository = new Repository();

		var shipper = repository
			.GetCustomer()
			.Bind(customer => customer.Address)
			.Bind(address => repository.GetAddress())
			.Bind(address => address.Order)
			.Bind(order => repository.GetOrder())
			.Bind(order => order.Shipper);
	}

	public City NextTalkCity(Speaker speaker) => speaker
		?.NextTalk()
		?.GetConference()
		?.GetCity();
}

public class Speaker
{
	public Talk NextTalk() => new();
}

public class Talk
{
	public Conference GetConference() => new();
}

public class Conference
{
	public City GetCity() => new();
}

public class City
{
}

public class Maybe<T>
	where T : class
{
	private readonly T _value;

	public Maybe(T someValue) => _value = someValue ?? throw new ArgumentNullException(nameof(someValue));

	private Maybe()
	{
	}

	public Maybe<U> Bind<U>(Func<T, Maybe<U>> func)
		where U : class => _value != null
			? func(_value)
			: Maybe<U>.None();

	public static Maybe<T> None() => new();
}

public interface IRepository
{
	Maybe<Customer> GetCustomer();

	Maybe<Address> GetAddress();

	Maybe<Order> GetOrder();
}

public class Repository : IRepository
{
	public Maybe<Address> GetAddress() => new(new Address());

	public Maybe<Customer> GetCustomer() => new(new Customer());

	public Maybe<Order> GetOrder() => new(new Order());
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