using System;
using System.Linq;

namespace RomanNumerals
{
    public class RomanNumber
    {
        private static readonly string[] ValidSymbols = new[] { "I", "V", "X", "L", "C", "D", "M" };
        private readonly RomanSymbol[] symbols;
        public RomanSymbol[] Symbols
        {
            get { return symbols; }
        }

        public RomanNumber(string number)
        {
            if(string.IsNullOrEmpty(number)) throw new ArgumentNullException();

            symbols = number.Select(digit => new RomanSymbol(digit.ToString())).ToArray();
        }

        public RomanNumber(params RomanSymbol[] symbols)
        {
            if (!symbols.Any()) throw new ArgumentNullException();
            this.symbols = symbols;
        }

        public override string ToString()
        {
            return string.Join("",
                          symbols.Select(x => x.ToString()).ToArray());
        }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ToString().Equals(obj.ToString());

        }

        public virtual int ToDecimal()
        {
            if(!IsValid())
                throw new Exception("Invalid number: " + this);

            int result = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                RomanSymbol currentSymbol = symbols[i];
                if( i < symbols.Length - 1)
                {
                    RomanSymbol nextSymbol = symbols[i+1];
                    if (nextSymbol.Value() > currentSymbol.Value())
                        result -= currentSymbol.Value();
                    else
                        result += currentSymbol.Value();
                }
                else result += currentSymbol.Value();
            }
            return result;
        }

        public bool IsValid()
        {
            if (!HasValidSums()) return false;
            if (!HasValidConsecutiveSymbols()) return false;

            for (int i = 0; i < symbols.Length; i++)
            {
                RomanSymbol currentSymbol = symbols[i];

                if (!IsValidSymbol(currentSymbol) || !IsValidSubtraction(i, currentSymbol)) return false;
            }
            return true;
        }

        private bool HasValidConsecutiveSymbols()
        {
            if (ToString().Contains("XXXX") || ToString().Contains("CCCC") || ToString().Contains("MMMM"))
                return false;
            return true;
        }

        private bool HasValidSums()
        {
            if (symbols.Count(x => x.Literal.Equals("D")) > 1)
                return false;
            if (symbols.Count(x => x.Literal.Equals("L")) > 1)
                return false;
            if (symbols.Count(x => x.Literal.Equals("V")) > 1)
                return false;
            if (symbols.Count(x => x.Literal.Equals("I")) > 3)
                return false;
            if (symbols.Count(x => x.Literal.Equals("X")) > 4)
                return false;
            if (symbols.Count(x => x.Literal.Equals("C")) > 4)
                return false;
            if (symbols.Count(x => x.Literal.Equals("M")) > 4)
                return false;
            return true;
        }

        private bool IsValidSubtraction(int i, RomanSymbol currentSymbol)
        {
            if (i < symbols.Length - 1)
            {
                //V,D,L can't subtract nothing
                if(new[] {"V", "D", "L"}.Contains(currentSymbol.Literal))
                {
                    for (int j = i+1; j < symbols.Length; j++)
                        if (symbols[j].Value() > currentSymbol.Value())
                            return false;
                }
                //Nothing can subtract and add right after : IXI, IXII, IXIII is invalid
                if (i < symbols.Length - 2 && symbols[i + 1].Value() > currentSymbol.Value() && symbols[i + 2].Value() >= currentSymbol.Value())
                        return false;

                //Can't add to subtract. Ex: IIX,IIIX,XXC,XXXC ...
                if(symbols[i+1].Equals(currentSymbol))
                    for (int j = i + 1; j < symbols.Length; j++)
                        if (symbols[j].Value() > currentSymbol.Value())
                            return false;
            }

            //I can only subtract X
            if (currentSymbol.Literal.Equals("I"))
            {
                if (i < symbols.Length - 1 && new[] {"D", "C", "M", "L"}.Contains(symbols[i + 1].Literal))
                    return false;
            }
            //X can only subtract C
            if (currentSymbol.Literal.Equals("X"))
            {
                if (i < symbols.Length - 1 && new[] {"D", "M"}.Contains(symbols[i + 1].Literal))
                    return false;
            }
            return true;
        }

        private bool IsValidSymbol(RomanSymbol currentSymbol)
        {
            return ValidSymbols.Contains(currentSymbol.Literal);
        }
    }
}
