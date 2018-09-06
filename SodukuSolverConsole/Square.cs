using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SudokuSolverConsole
{
    [DebuggerDisplay("{Meta}:{Value}")]
    public class Square
    {
        public abstract class Keys
        {
            public const string X = "x";
            public const string Y = "y";
        }

        private int _value;

        public Guid Id { get; set; }
        public Dictionary<string,object> Meta { get; set; }

        public List<int> Candidates { get; set; }

        public int Value
        {
            get => _value;
            set
            {
                if(Given)
                    throw new AccessViolationException("Value cannot be modified when given at construction.");
                _value = value;
            }
        }

        public bool Given { get; }

        public Square(int value = 0)
        {
            Id = Guid.NewGuid();
            Meta = new Dictionary<string, object>();

            if (value < 0 || value > 9)
                value = 0;

            Value = value;
            Given = value > 0;
            Candidates = Given 
                ? new List<int>() 
                : new List<int>{1,2,3,4,5,6,7,8,9};
        }
    }
}