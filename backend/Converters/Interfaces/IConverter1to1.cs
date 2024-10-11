namespace IConverters;

public interface IConverter1To1<TInput, TOutput>
{
    TOutput Convert(TInput input);
}
