using InMemoryDatabase.Enums;
using System;

namespace InMemoryDatabase.Utilies
{
    public class ValueTypeChecker
    {
        public bool Check(EnumType type, string value)
        {
            if (type == EnumType.Integer)
            {
                int unusedValue;
                bool isInteger = int.TryParse(value, out unusedValue);

                return isInteger;
            }
            else if (type == EnumType.Real)
            {
                double unusedValue;
                bool isReal = double.TryParse(value, out unusedValue);

                return isReal;
            }
            else if (type == EnumType.String)
            {
                return true;
            }
            else
            {
                string exceptionMessage = "Unsupported type.";
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}
