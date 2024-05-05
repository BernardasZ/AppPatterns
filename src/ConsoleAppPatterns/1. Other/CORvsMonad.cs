using System;

namespace ConsoleAppPatterns._1._Other.CORvsMonad;

public class ChainOfResponsibility
{
	public void Main()
	{
		var defaultCat1 = new OperationResponse<Cat> { Data = new Cat { Id = 1, Name = "Tarakonas" } };
		var handlerA = new ValidateCatId();
		var handlerB = new ValidateCatName();
		var handlerC = new ChangeCatName("Ogis");

		handlerA.SetSuccessor(handlerB);
		handlerB.SetSuccessor(handlerC);

		_ = handlerA.HandleRequest(defaultCat1);
	}
}

public abstract class Handler<T>
{
	protected Handler<T> Successor;

	public void SetSuccessor(Handler<T> successor)
	{
		Successor = successor;
	}

	public OperationResponse<T> PassToOtherHandler(OperationResponse<T> request)
	{
		return Successor != null && request.Status == ResponseStatus.Success
			? Successor.HandleRequest(request)
			: request;
	}

	public abstract OperationResponse<T> HandleRequest(OperationResponse<T> request);
}

public class ValidateCatId : Handler<Cat>
{
	public override OperationResponse<Cat> HandleRequest(OperationResponse<Cat> request)
	{
		if (request.Data.Id != 1)
		{
			request.Status = ResponseStatus.Failed;
			request.FailedReason = "Id does not match cat's id";
		}

		return PassToOtherHandler(request);
	}
}

public class ValidateCatName : Handler<Cat>
{
	public override OperationResponse<Cat> HandleRequest(OperationResponse<Cat> request)
	{
		if (request.Data.Name != "Lucy")
		{
			request.Status = ResponseStatus.Failed;
			request.FailedReason = "Name does not match cat's name";
		}

		return PassToOtherHandler(request);
	}
}

public class ChangeCatName : Handler<Cat>
{
	private readonly string _name;

	public ChangeCatName(string name)
	{
		_name = name;
	}

	public override OperationResponse<Cat> HandleRequest(OperationResponse<Cat> request)
	{
		request.Data.Name = _name;

		return PassToOtherHandler(request);
	}
}

public class Cat
{
	public int Id { get; set; }

	public string Name { get; set; }
}

public enum ResponseStatus
{
	Failed,
	Success
}

public class OperationResponse<T>
{
	public T Data { get; set; }

	public ResponseStatus Status { get; set; } = ResponseStatus.Success;

	public string FailedReason { get; set; }
}

public class HandlerUsingMonadFunction
{
	public void Main()
	{
		var defaultCat = new OperationResponse<Cat> { Data = new Cat { Id = 1, Name = "Tarakonas" } };
		var name = "Ogis";

		_ = defaultCat
			.Check(IsValidateCatId)
			.Check(IsValidateCatName)
			.Check((item) => ChangeCatName(item, name));
	}

	public OperationResponse<Cat> IsValidateCatId(OperationResponse<Cat> request)
	{
		if (request.Data.Id != 1)
		{
			request.Status = ResponseStatus.Failed;
			request.FailedReason = "Id does not match cat's id";
		}

		return request;
	}

	public OperationResponse<Cat> IsValidateCatName(OperationResponse<Cat> request)
	{
		if (request.Data.Name != "Lucy")
		{
			request.Status = ResponseStatus.Failed;
			request.FailedReason = "Name does not match cat's name";
		}

		return request;
	}

	public OperationResponse<Cat> ChangeCatName(OperationResponse<Cat> request, string name)
	{
		request.Data.Name = name;

		return request;
	}
}

public static class SequentialCheck
{
	public static OperationResponse<T> Check<T>(this OperationResponse<T> item, Func<OperationResponse<T>, OperationResponse<T>> func)
	{
		return item.Status == ResponseStatus.Success
			? func(item)
			: item;
	}
}