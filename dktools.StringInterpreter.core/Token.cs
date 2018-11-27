using System;

namespace dktools.StringInterpreter.core
{
    public class Token : IToken
    {
        public ITokenType Type { get; set; }
        public string Text { get; set; }

        public Token(ITokenType type, string text)
        {
            Type = type;
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }
    }
}
