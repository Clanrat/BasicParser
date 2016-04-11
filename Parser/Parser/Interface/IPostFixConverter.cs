using System.Collections.Generic;

namespace Parser.Interface
{
    public interface IPostFixConverter
    {
        List<string> Convert(string input);
    }
}