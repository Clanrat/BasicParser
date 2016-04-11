namespace Parser.Interface
{
    public interface IEvaluator<out T>
    {
        T Eval(string input);
    }
}