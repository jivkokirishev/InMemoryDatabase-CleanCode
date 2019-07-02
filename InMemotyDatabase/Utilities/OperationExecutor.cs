using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using InMemoryDatabase.Managers;
using System;
using System.Collections.Generic;

namespace InMemotyDatabase.Utilities
{
    public class OperationExecutor
    {
        private IList<Cell> cells;

        public OperationExecutor(IList<Cell> selectedCells)
        {
            this.cells = selectedCells;

            RemoveNullCells();
        }

        public Cell Execute(OperationType operation)
        {
            switch (operation)
            {
                case OperationType.Min:
                    {
                        return Min();
                    }
                    break;
                case OperationType.Max:
                    {
                        return Max();
                    }
                    break;
                case OperationType.Sum:
                    {
                        return Sum();
                    }
                    break;
                case OperationType.Product:
                    {
                        return Product();
                    }
                    break;
                default:
                    {
                        string exceptionMessage = "This operation is not supported yet.";
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
            }
        }

        private Cell Sum()
        {
            Cell resultCell = CreateResultCell();
            if (cells.Count == 0)
            {
                return resultCell;
            }

            decimal result = 0;
            foreach (var cell in cells)
            {
                result += decimal.Parse(cell.Value);
            }

            resultCell.Value = result.ToString();
            return resultCell;
        }

        private Cell Product()
        {
            Cell resultCell = CreateResultCell();
            if (cells.Count == 0)
            {
                return resultCell;
            }

            decimal result = 1;
            foreach (var cell in cells)
            {
                result *= decimal.Parse(cell.Value);
            }

            resultCell.Value = result.ToString();
            return resultCell;
        }

        private Cell Min()
        {
            Cell resultCell = CreateResultCell();
            if (cells.Count == 0)
            {
                return resultCell;
            }

            var firstCell = cells[0];
            decimal result = decimal.Parse(cells[0].Value);
            foreach (var cell in cells)
            {
                var currentValue = decimal.Parse(cell.Value);
                if (result > currentValue)
                {
                    result = currentValue;
                }
            }

            resultCell.Value = result.ToString();
            return resultCell;
        }

        private Cell Max()
        {
            Cell resultCell = CreateResultCell();
            if (cells.Count == 0)
            {
                return resultCell;
            }

            var firstCell = cells[0];
            decimal result = decimal.Parse(cells[0].Value);
            foreach (var cell in cells)
            {
                var currentValue = decimal.Parse(cell.Value);
                if (result < currentValue)
                {
                    result = currentValue;
                }
            }

            resultCell.Value = result.ToString();
            return resultCell;
        }

        private Cell CreateResultCell()
        {
            Cell resultCell;

            if (cells.Count == 0)
            {
                resultCell = new Cell(EnumType.String);
            }
            else
            {
                EnumType cellType = cells[0].Type;
                resultCell = new Cell(cellType);
            }

            return resultCell;
        }

        private void RemoveNullCells()
        {
            var nonNullCells = new List<Cell>();

            foreach (var cell in cells)
            {
                if (!cell.IsNull)
                {
                    nonNullCells.Add(cell);
                }
            }

            cells = nonNullCells;
        }
    }
}
