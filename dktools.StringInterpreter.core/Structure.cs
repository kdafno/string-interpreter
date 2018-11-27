using System.Collections.Generic;

namespace dktools.StringInterpreter.core
{
    /// <summary>
    /// An interpreter comprising of a <see cref="ILexer"/>
    /// and a <see cref="IParser"/>, to make the interpretation 
    /// in two stages.
    /// </summary>
    public interface IInterpreter
    {
        ILexer Lexer { get; }
        IParser Parser { get; }
    }

    /// <summary>
    /// A lexer that breaks a text input 
    /// into a collection of <see cref="IToken"/>,
    /// according to a collection of <see cref="ITokenType"/>.
    /// </summary>
    public interface ILexer
    {
        IEnumerable<IToken> Lex(string input);
        IEnumerable<ITokenType> TokenTypes { get; set; }
    }

    /// <summary>
    /// A parser that parses a collection of <see cref="IToken"/> into an interpretation,
    /// according to a collection of <see cref="IRule"/>.
    /// </summary>
    public interface IParser
    {
        string Parse(IEnumerable<IToken> tokens);
        IEnumerable<IRule> Rules { get; set; }
    }

    /// <summary>
    /// Defines an item of the lexing process,
    /// during which a text input is broken into separate <see cref="IToken"/>.
    /// </summary>
    public interface IToken
    {
        ITokenType Type { get; set; }
        string Text { get; set; }
    }

    /// <summary>
    /// Defines a type of token, 
    /// allowing different configuration for different types of input.
    /// </summary>
    public interface ITokenType
    {
        /// <summary>
        /// Checks whether the <see cref="ITokenType"/> criteria is met.
        /// </summary>
        bool IsSatisfied(string input, int index);
        /// <summary>
        /// Returns the complete text that satisfied the token type.
        /// </summary>
        string TextSatisfied { get; }
        /// <summary>
        /// Resets all configuration to evaluate new input.
        /// </summary>
        void Reset();
    }

    /// <summary>
    /// Defines a rule under which an interpetation of one string to another is performed.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Checks whether the <see cref="IRule"/> criteria is met.
        /// </summary>
        bool IsSatisfied(IEnumerable<IToken> tokens, int index);
        /// <summary>
        /// The value returned if rule is satisfied.
        /// </summary>
        string Value { get; }
        /// <summary>
        /// Resets all configuration to evaluate new input.
        /// </summary>
        void Reset();
    }
}
