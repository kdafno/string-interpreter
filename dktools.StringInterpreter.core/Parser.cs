using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dktools.StringInterpreter.core
{
    /// <summary>
    /// <see cref="Rules"/> will be evaluated in the same order as set in the collection.
    /// Use this behavior to properly manipulate <see cref="IToken"/> interpretation.
    /// </summary>
    public class Parser : IParser
    {
        public IEnumerable<IRule> Rules { get; set; }
            = new List<IRule>();

        public Parser()
        {

        }

        public Parser(IEnumerable<IRule> rules)
            :this()
        {
            Rules = rules;
        }

        public string Parse(IEnumerable<IToken> tokens)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < tokens.Count(); i++)
            {
                string interpretation = GetTokenInterpretation(tokens, i);
                sb.Append(interpretation);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Interpretes a <see cref="IToken"/> from a collection at the specified <paramref name="index"/>.
        /// Returns the <see cref="IRule.Value"/> of the first <see cref="Rules"/> that is satisfied.
        /// if more than one <see cref="Rules"/> can be satisfied, only the first one will count.
        /// If no <see cref="Rules"/> is satisfied (or exists), the <see cref="IToken.Text"/> is returned.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string GetTokenInterpretation(IEnumerable<IToken> tokens, int index)
        {
            foreach (IRule rule in Rules)
            {
                rule.Reset();
                if (rule.IsSatisfied(tokens, index))
                    return rule.Value;
            }
            // if no rule is satisfied
            return tokens.ElementAt(index).Text;
        }
    }
}
