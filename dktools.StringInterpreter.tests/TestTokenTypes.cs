using dktools.StringInterpreter.core;
using System.Linq;

namespace dktools.StringInterpreter.tests
{
    /// <summary>
    /// Groups the string into groups of <see cref="n"/> characters.
    /// </summary>
    public class NGroupTokenType : ITokenType
    {
        #region interface implementation
        public string TextSatisfied { get; private set; } = string.Empty;

        public bool IsSatisfied(string input, int index)
        {
            int j = index;
            do TextSatisfied += input[j];
            while (TextSatisfied.Count() < n &&
                   ++j < input.Length);
            return true;
        }

        public void Reset()
        {
            TextSatisfied = string.Empty;
        }
        #endregion

        #region custom implementation
        protected int n { get; set; }
        public NGroupTokenType(int n)
        {
            this.n = n;
        }
        #endregion
    }
}
