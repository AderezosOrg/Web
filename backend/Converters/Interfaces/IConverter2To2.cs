namespace DTO;

public interface IConverter2To2<TInput1, TInput2, TOutput1, TOutput2>
{
    (TOutput1 output1, TOutput2 output2) Convert(TInput1 input1, TInput2 input2);
}