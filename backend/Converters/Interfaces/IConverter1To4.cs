namespace DTO;

public interface IConverter1To4<TInput1, TInput2, TInput3, TInput4, TOutput>
{
    TOutput Convert(TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4);
}