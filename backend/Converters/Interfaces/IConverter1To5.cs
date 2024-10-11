namespace IConverters;

public interface IConverter1To5<TInput1, TInput2, TInput3, TInput4, TInput5, TOutput>
{
    TOutput Convert(TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5);
}