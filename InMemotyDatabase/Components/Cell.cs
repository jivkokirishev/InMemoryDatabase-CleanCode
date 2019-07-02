using InMemoryDatabase.Contracts;
using InMemoryDatabase.Enums;
using InMemoryDatabase.Utilies;
using System;

namespace InMemoryDatabase.Components
{
    public class Cell : ICloneable<Cell>
    {
        private string value;

        public Cell(EnumType type)
        {
            Type = type;
            SetIsNUllTrue();
        }

        public Cell(EnumType type, string value)
        {
            Type = type;
            this.Value = value;
        }

        public EnumType Type { get; private set; }

        public string Value
        {
            get
            {
                if (this.IsNull)
                {
                    return "NULL";
                }
                else
                {
                    return this.value;
                }
            }

            set
            {
                var valueTypeChecker = new ValueTypeChecker();

                bool isValueCorrect = false;

                try
                {
                    isValueCorrect = valueTypeChecker.Check(Type, value);
                }
                catch (ArgumentException)
                {
                    return;
                }

                if (isValueCorrect)
                {
                    this.IsNull = false;
                    this.value = value;
                }
                else
                {
                    string exceptionMessage = "This value, can not be parsed to the cell type.";
                    throw new ArgumentException(exceptionMessage);
                }
            }
        }

        public bool IsNull { get; private set; }

        public void SetIsNUllTrue()
        {
            this.IsNull = true;
        }

        public Cell Clone()
        {
            Cell clonedObject;

            if (this.IsNull)
            {
                clonedObject = new Cell(this.Type);
            }
            else
            {
                clonedObject = new Cell(this.Type, this.Value);
            }

            return clonedObject;
        }

        public static bool operator ==(Cell thisCell, Cell otherCell)
        {
            if (thisCell.Type != otherCell.Type)
            {
                return false;
            }
            else if (thisCell.IsNull == otherCell.IsNull && thisCell.IsNull)
            {
                return true;
            }
            else if (thisCell.Type == EnumType.Real)
            {
                double thisCellParsedValue = double.Parse(thisCell.Value);
                double otherCellParsedValue = double.Parse(otherCell.Value);

                bool areParsedValuesEqual = (thisCellParsedValue == otherCellParsedValue);

                return areParsedValuesEqual;
            }
            else
            {
                bool areCellsValuesEqual = (thisCell.Value == otherCell.Value);

                return areCellsValuesEqual;
            }
        }

        public static bool operator !=(Cell thisCell, Cell otherCell)
        {
            bool areCellsEqual = (thisCell == otherCell);

            return !areCellsEqual;
        }
    }
}
