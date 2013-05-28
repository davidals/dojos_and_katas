using System;

namespace RomanNumerals
{
    public class RomanSymbol : IComparable
    {
        public string Literal { get; set; }

        public RomanSymbol(string literal)
        {
            Literal = literal.ToUpper();
        }

        public override string ToString()
        {
            return Literal;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Literal.Equals(((RomanSymbol)obj).Literal);
        }

        public int CompareTo(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                throw new Exception("Different types");
            }

            return Value().CompareTo(((RomanSymbol)obj).Value());
        }


        public int Value()
        {
            switch(Literal)
            {
                case "I":
                    return 1;
                case "V":
                    return 5;
                case "X":
                    return 10;
                case "L":
                    return 50;
                case "C":
                    return 100;
                case "D":
                    return 500;
                case "M":
                    return 1000;
                default:
                    throw new InvalidOperationException("Invalid literal");
            }
        }
    }
}
