using System;
using BenchmarkDotNet.Attributes;

namespace ConsoleAppPatterns._2._Experimental;

[MemoryDiagnoser]
public class Delegates
{
	[Benchmark]
	public string NormalIfV1() => NormalIf(nameof(NormalIfV1));

	[Benchmark]
	public string NormalIfV2() => NormalIf(nameof(NormalIfV2), 1, 2);

	[Benchmark]
	public string FuncV1() => CallFunc(nameof(NormalIfV1));

	[Benchmark]
	public string FuncV2() => CallFunc(nameof(NormalIfV2), 1, 2);

	[Benchmark]
	public string DelegateV1() => CallDelegate<string, string>(nameof(GetString), nameof(DelegateV1));

	[Benchmark]
	public string DelegateV2() => CallDelegate<string, string>(nameof(GetString), nameof(DelegateV2), 1, 2);

	public T CallDelegate<T, U>(string method, U param1, int? param2 = null, int? param3 = null)
		where T : class =>
		param1 == null && param2 == null
			? Delegate.CreateDelegate(typeof(Func<U, T>), this, method).DynamicInvoke(param1) as T
			: Delegate.CreateDelegate(typeof(Func<U, int?, int?, T>), this, method).DynamicInvoke(param1, param2, param3) as T;

	public string CallFunc(string param1, int? param2 = null, int? param3 = null) =>
		param1 == null && param2 == null
			? CallFunc(GetString, param1)
			: CallFunc(GetString, param1, param2, param3);

	public T CallFunc<T, U>(Func<U, T> func, U param1) => func(param1);

	public T CallFunc<T, U>(Func<U, int?, int?, T> func, U param1, int? param2 = null, int? param3 = null) => func(param1, param2, param3);

	public string NormalIf(string param1, int? param2 = null, int? param3 = null) =>
		param1 == null && param2 == null
			? GetString(param1)
			: GetString(param1, param2, param3);

	public string GetString(string text, int? param1 = null, int? param2 = null) => text.ToLower();

	public string GetString(string text) => text.ToLower();
}