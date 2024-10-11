namespace IConverters;

public interface IConverter1To6<TInput1, TInput2, TInput3, TInput4, TInput5, TInput6 ,TOutput>
{
    TOutput Convert(TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5, TInput6 input6);
}
