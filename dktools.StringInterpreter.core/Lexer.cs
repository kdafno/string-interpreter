using System.Collections.Generic;
using System.Linq;

namespace dktools.StringInterpreter.core
{
    /// <summary>
    /// <see cref="TokenTypes"/> will be evaluated in the same order as set in the collection.
    /// Use this behavior to properly manipulate <see cref="IToken"/> extraction.
    /// </summary>
    public class Lexer : ILexer
    {
        public IEnumerable<ITokenType> TokenTypes { get; set; }
            = new List<ITokenType>();

        public Lexer()
        {

        }

        public Lexer(IEnumerable<ITokenType> tokenTypes)
            :this()
        {
            TokenTypes = tokenTypes;
        }

        public IEnumerable<IToken> Lex(string input)
        {
            var result = new List<IToken>();

            for (int i = 0; i < input.Length; i++)
            {
                IToken token = GetToken(input, ref i);
                result.Add(token);
            }

            return result;
        }

        /// <summary>
        /// Creates a new <see cref="IToken"/> from <paramref name="input"/>
        /// at the specified <paramref name="index"/>.
        /// It will return the first <see cref="ITokenType"/> satisfied.
        /// If more than one <see cref="ITokenType"/> can be satisfied, only the first one will count.
        /// If no <see cref="ITokenType"/> is satisfied (or exists) a null <see cref="ITokenType"/> is returned.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected IToken GetToken(string input, ref int index)
        {
            foreach (ITokenType tokenType in TokenTypes)
            {
                tokenType.Reset();
                if (tokenType.IsSatisfied(input, index))
                {
                    index += tokenType.TextSatisfied.Count() - 1;
                    return new Token(tokenType, tokenType.TextSatisfied);
                }
            }
            // if no token type is satisfied
            return new Token(null, input[index].ToString());
        }
    }
}