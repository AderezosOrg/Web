namespace IConverters;

public interface IConverter1To3<TInput1, TInput2, TInput3, TOutput>
{
    TOutput Convert(TInput1 input1, TInput2 input2, TInput3 input3);
}
