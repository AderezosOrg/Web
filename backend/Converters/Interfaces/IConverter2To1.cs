namespace DTO;

public interface IConverter2To1<TInput1, TInput2, TOutput>
{
    TOutput Convert(TInput1 input1, TInput2 input2);
}