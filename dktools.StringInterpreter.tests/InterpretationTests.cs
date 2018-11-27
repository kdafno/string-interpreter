using dktools.StringInterpreter.core;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace dktools.StringInterpreter.tests
{
    public class InterpretationTests
    {
        private TestInterpreter interpreter = new TestInterpreter();

        [Theory]
        [InlineData("my test input", 13)]
        public void TestLexerNoTokenTypes(string input, int tokenCount)
        {
            var tokens = interpreter.Lexer.Lex(input);
            Assert.Equal(tokenCount, tokens.Count());
            // rebuild string
            string output = interpreter.Parser.Parse(tokens);
            Assert.Equal(input, output);
        }

        [Theory]
        [InlineData("test 2 group characters", 2, 12)]
        [InlineData("test 3 group characters", 3, 8)]
        public void TestLexerNgroupType(string input, int n, int tokenCount)
        {
            interpreter.Lexer.TokenTypes = new List<ITokenType> { new NGroupTokenType(n) };
            var tokens = interpreter.Lexer.Lex(input);
            Assert.Equal(tokenCount, tokens.Count());
            // rebuild string
            string output = interpreter.Parser.Parse(tokens);
            Assert.Equal(input, output);
        }
    }
}
