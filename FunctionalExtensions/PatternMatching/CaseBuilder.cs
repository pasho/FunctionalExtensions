using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExtensions.PatternMatching
{
    public struct CaseBuilder<TIn, TOut>
    {
        private readonly MatchType _matched;
        private readonly TIn _input;
        private readonly TOut _output;

        public CaseBuilder(TIn input, MatchType matched = MatchType.None, TOut output = default(TOut))
        {
            _input = input;
            _matched = matched;
            _output = output;
        }

        public ThenBuilder<TIn, TOut> Case(Predicate<TIn> predicate)
        {
            return _matched == MatchType.None
                ?
                    predicate(_input)
                        ? new ThenBuilder<TIn, TOut>(_input, MatchType.ThisMatched)
                        : new ThenBuilder<TIn, TOut>(_input)
                : new ThenBuilder<TIn, TOut>(_input, MatchType.MatchedBefore, _output);
        }

        public TOut Otherwise(Func<TIn, TOut> mapper)
        {
            return
                _matched == MatchType.None
                    ? mapper(_input)
                    : _output;
        }

        public TOut Otherwise(TOut output)
        {
            return Otherwise(i => output);
        }

        public TOut OtherwiseDefault()
        {
            return Otherwise(default(TOut));
        }

        public TOut OtherwiseFail(string message)
        {
            if (_matched == MatchType.None)
                throw new Exception(message);

            return _output;
        }
    }
}
