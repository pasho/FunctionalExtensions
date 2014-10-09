using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExtensions.PatternMatching
{
    public struct ThenBuilder<TIn>
    {
        private MatchType _matched;
        private TIn _input;

        public ThenBuilder(TIn input, MatchType matched)
        {
            _input = input;
            _matched = matched;
        }

        public CaseBuilder<TIn, TOut> Then<TOut>(TOut output)
        {
            return _matched == MatchType.ThisMatched
                ? new CaseBuilder<TIn, TOut>(_input, MatchType.MatchedBefore, output)
                : new CaseBuilder<TIn, TOut>(_input);
        }

        public CaseBuilder<TIn, TOut> Then<TOut>(Func<TIn, TOut> mapper)
        {
            return _matched == MatchType.ThisMatched
                ? new CaseBuilder<TIn, TOut>(_input, MatchType.MatchedBefore, _input.Map(mapper))
                : new CaseBuilder<TIn, TOut>(_input);
        }
    }

    public struct ThenBuilder<TIn, TOut>
    {
        private readonly MatchType _matched;
        private readonly TIn _input;
        private readonly TOut _output;

        public ThenBuilder(TIn input, MatchType matched = MatchType.None, TOut output = default(TOut))
        {
            _input = input;
            _matched = matched;
            _output = output;
        }

        public CaseBuilder<TIn, TOut> Then<TOut>(TOut output)
        {
            return Then<TOut>(i => output);
        }

        public CaseBuilder<TIn, TOut> Then<TOut>(Func<TIn, TOut> mapper)
        {
            return _matched == MatchType.ThisMatched
                ? new CaseBuilder<TIn, TOut>(_input, MatchType.MatchedBefore, _input.Map(mapper))
                : new CaseBuilder<TIn, TOut>(_input);
        }
    }
}
