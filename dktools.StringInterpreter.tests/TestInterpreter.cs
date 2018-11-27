using dktools.StringInterpreter.core;
using System.Collections.Generic;

namespace dktools.StringInterpreter.tests
{
    public class TestInterpreter : IInterpreter
    {
        public ILexer Lexer { get; private set; }
        public IParser Parser { get; private set; }

        public TestInterpreter()
        {
            Lexer = new Lexer(new List<ITokenType>());
            Parser = new Parser(new List<IRule>());
        }
    }
}
