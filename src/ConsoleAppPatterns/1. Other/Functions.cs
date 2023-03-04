using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppPatterns._1._Other;

public class Function
{
	public void Main()
	{
		BenchmarkRunner.Run<Test>();

		var service = new Service<Cat2>(null);

		_ = Task.FromResult(service.DoSomethingAsync(1));
	}
}

[MemoryDiagnoser]
public class Test
{
	private readonly IRepository<Cat2> _repository;
	private readonly IService<Cat2> _serviceNormal;
	private readonly IService<Cat2> _serviceFunc;

	private static readonly Cat2 _cat;

	static Test()
	{
		_cat = new Cat2();
	}

	public Test()
	{
		_repository = new Repository<Cat2>();
		_serviceFunc = new Service<Cat2>(_repository);
		_serviceNormal = new CatService(_repository);
	}

	[Benchmark]
	public void RunFunc()
	{
		try
		{
			_serviceFunc.CreateAsync(_cat);
		}
		catch (Exception ex)
		{
			Console.WriteLine(@ex);
		}
	}

	[Benchmark]
	public void RunNormal()
	{
		try
		{
			_serviceNormal.CreateAsync(_cat);
		}
		catch (Exception ex)
		{
			Console.WriteLine(@ex);
		}
	}
}

public class CatService : IService<Cat2>
{
	private readonly IRepository<Cat2> _repository;

	public CatService(IRepository<Cat2> repository)
	{
		_repository = repository;
	}

	public async Task<GenericResponse<Cat2>> CreateAsync(Cat2 item) => await _repository.CreateAsync(item);

	public async Task<GenericResponse<Cat2>> DeleteAsync(Cat2 item) => await _repository.DeleteAsync(item);

	public async Task<GenericResponse<IEnumerable<Cat2>>> GetAllAsync() => await _repository.GetAllAsync();

	public async Task<GenericResponse<Cat2>> GetAsync(long id) => await _repository.GetAsync(id);

	public async Task<GenericResponse<Cat2>> UpdateAsync(Cat2 item) => await _repository.UpdateAsync(item);

	public Task<GenericResponse<T>> WrapperAsync<T, U>(Func<U, Task<GenericResponse<T>>> func, U arg) => throw new NotImplementedException();

	public Task<GenericResponse<T>> WrapperAsync<T>(Func<Task<GenericResponse<T>>> func) => throw new NotImplementedException();
}

public interface IService<TModel> where TModel : Animal
{
	Task<GenericResponse<TModel>> CreateAsync(TModel item);

	Task<GenericResponse<IEnumerable<TModel>>> GetAllAsync();

	Task<GenericResponse<TModel>> GetAsync(long id);

	Task<GenericResponse<TModel>> UpdateAsync(TModel item);

	Task<GenericResponse<TModel>> DeleteAsync(TModel item);

	Task<GenericResponse<T>> WrapperAsync<T, U>(Func<U, Task<GenericResponse<T>>> func, U arg);

	Task<GenericResponse<T>> WrapperAsync<T>(Func<Task<GenericResponse<T>>> func);
}

public class Service<TModel> : IService<TModel> where TModel : Animal, new()
{
	private readonly IRepository<TModel> _repository;

	public Service(IRepository<TModel> repository)
	{
		_repository = repository;
	}

	public virtual async Task<GenericResponse<TModel>> CreateAsync(TModel item) => await WrapperAsync(() => _repository.UpdateAsync(item));

	public virtual async Task<GenericResponse<IEnumerable<TModel>>> GetAllAsync() => await WrapperAsync(() => _repository.GetAllAsync());

	//public virtual async Task<GenericResponse<TModel>> GetAsync(long id) => await WrapperAsync(() => _repository.GetAsync(id));

	//public virtual async Task<GenericResponse<TModel>> UpdateAsync(TModel item) => await WrapperAsync(() => _repository.UpdateAsync(item));

	public virtual async Task<GenericResponse<TModel>> DeleteAsync(TModel item) => await WrapperAsync(() => _repository.DeleteAsync(item));

	public virtual async Task<GenericResponse<T>> WrapperAsync<T>(Func<Task<GenericResponse<T>>> func) => await func();

	public Task<GenericResponse<T>> WrapperAsync<T, U>(Func<U, Task<GenericResponse<T>>> func, U arg) => throw new NotImplementedException();

	public async Task<GenericResponse<TModel>> GetAsync(long id) =>
		await Task.FromResult(new GenericResponse<TModel> { Data = new TModel(), State = true });

	public async Task<GenericResponse<TModel>> UpdateAsync(TModel item) =>
		await Task.FromResult(new GenericResponse<TModel> { Data = item, State = true });

	public async Task<GenericResponse<TModel>> ValidateAsync(TModel item) =>
		await Task.FromResult(new GenericResponse<TModel> { Data = item, State = true });

	public async Task DoSomethingAsync(int id) =>
		_ = await GetAsync(id)
			.Maybe(item => ValidateAsync(item.Data))
			.Maybe(item => UpdateAsync(item.Data))
			.Maybe(() => GetAsync(id));
}

public static class Monad
{
	public static async Task<GenericResponse<T>> Maybe<T>(this Task<GenericResponse<T>> item, Func<Task<GenericResponse<T>>> func) where T : Animal
	{
		var result = await item;

		return result.State
			? await func()
			: result;
	}

	public static async Task<GenericResponse<T>> Maybe<T>(this Task<GenericResponse<T>> item, Func<GenericResponse<T>, Task<GenericResponse<T>>> func) where T : Animal
	{
		var result = await item;

		return result.State
			? await func(result)
			: result;
	}
}

public interface IRepository<TModel> where TModel : Animal
{
	Task<GenericResponse<TModel>> CreateAsync(TModel item);

	Task<GenericResponse<IEnumerable<TModel>>> GetAllAsync();

	Task<GenericResponse<TModel>> GetAsync(long id);

	Task<GenericResponse<TModel>> UpdateAsync(TModel item);

	Task<GenericResponse<TModel>> DeleteAsync(TModel item);
}

public class Repository<TModel> : IRepository<TModel> where TModel : Animal
{
	private static GenericResponse<IEnumerable<TModel>> _genericIenumerableResponse;
	private static GenericResponse<TModel> _genericResponse;

	static Repository()
	{
		_genericIenumerableResponse = new GenericResponse<IEnumerable<TModel>>();
		_genericResponse = new GenericResponse<TModel>();
	}

	public virtual Task<GenericResponse<TModel>> CreateAsync(TModel item) => /*Task.FromResult(_genericResponse);*/ throw new Exception();

	public virtual Task<GenericResponse<TModel>> DeleteAsync(TModel item) => Task.FromResult(_genericResponse);

	public virtual Task<GenericResponse<IEnumerable<TModel>>> GetAllAsync() => Task.FromResult(_genericIenumerableResponse);

	public virtual Task<GenericResponse<TModel>> GetAsync(long id) => Task.FromResult(_genericResponse);

	public virtual Task<GenericResponse<TModel>> UpdateAsync(TModel item) => Task.FromResult(_genericResponse);
}

public class GenericResponse<T>
{
	public T Data { get; set; }

	public bool State { get; set; } = true;
}

public class Animal
{
	public long Id { get; set; }
}

public class Cat2 : Animal
{
	public string Name { get; set; }
}

public class Dog : Animal
{
	public string Description { get; set; }
}