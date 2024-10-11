namespace IConverters;

public interface IConverter1To2<TInput, TInput2, TOutput>
{
    TOutput Convert(TInput input1, TInput2 input2);
}
