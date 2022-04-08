namespace Locadora.Shared.Utils;


public interface IResult
{
    string[]? Errors { get; set; }
    bool Success { get; }
    bool Failed { get; }
}

public interface IValuedResult<T>
{
    T? Value { get; set; }
    string[]? Errors { get; set; }

    bool HasValue { get; }
    bool Success { get; }
    bool Failed { get; }
}


public class Result<T> : IValuedResult<T>
{
    public T? Value { get; set; }
    public string[]? Errors { get; set; }

    public bool HasValue => Value is not null;
    public bool Success => !Errors?.Any() ?? true;
    public bool Failed => !Success;


    public Result()
    { }

    public Result(T? value, params string[]? errorArgs)
    {
        Value = value;
        Errors = errorArgs is null || !errorArgs.Any() ? null : errorArgs;
    }

    public Result(T? value, IEnumerable<string>? errors)
    {
        Value = value;
        Errors = errors is null || !errors.Any() ? null : errors.ToArray();
    }

    public static Result<T> Ok()
    {
        return new Result<T>(default(T?), null);
    }

    public static Result<T> Ok(T? value)
    {
        return new Result<T>(value, null);
    }

    public static Result<T> Fail(params string[]? errorArgs)
    {
        return new Result<T>(default(T?), errorArgs);
    }

    public static Result<T> Fail(IEnumerable<string>? errors)
    {
        return new Result<T>(default(T?), errors);
    }

    public static Result<T> PartialFail(T? value, IEnumerable<string>? errors)
    {
        return new Result<T>(value, errors);
    }
}



public class Result : IResult
{

    public string[]? Errors { get; set; }

    public bool Success => !Errors?.Any() ?? true;
    public bool Failed => !Success;


    public Result()
    { }

    public Result(params string[]? errorArgs)
    {
        Errors = errorArgs is null || !errorArgs.Any() ? null : errorArgs;
    }

    public Result(IEnumerable<string>? errors)
    {
        Errors = errors is null || !errors.Any() ? null : errors.ToArray();
    }

    public static Result Ok()
    {
        return new Result(null);
    }


    public static Result Fail(params string[]? errorArgs)
    {
        return new Result(errorArgs);
    }

    public static Result Fail(IEnumerable<string>? errors)
    {
        return new Result(errors);
    }
}