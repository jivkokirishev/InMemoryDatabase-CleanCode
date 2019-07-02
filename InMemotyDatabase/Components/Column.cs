using InMemoryDatabase.Contracts;
using InMemoryDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InMemoryDatabase.Components
{
    public class Column : INameable
    {
        private IList<Cell> cells;

        public Column(string name, EnumType type)
        {
            this.Name = name;
            this.Type = type;

            this.cells = new List<Cell>();
        }

        public string Name { get; set; }

        public EnumType Type { get; private set; }

        public IReadOnlyCollection<Cell> Cells
        {
            get
            {
                IReadOnlyCollection<Cell> tmpCells = new ReadOnlyCollection<Cell>(this.cells);

                return tmpCells;
            }
        }

        public void AddElement(Cell element)
        {
            if (this.Type != element.Type)
            {
                string exceptionMessage = $"Column type is {this.Type}. It is different from the new element type: {element.Type}.";
                throw new ArgumentException(exceptionMessage);
            }
            cells.Add(element);
        }

        public void RemoveElementAt(int index)
        {
            if (cells.Count <= index)
            {
                string exceptionMessage = $"Column element count is {ElementCount()}. There is not any element at index: {index} to be deleted";
                throw new IndexOutOfRangeException(exceptionMessage);
            }

            cells.RemoveAt(index);
        }

        public void EditElementAt(int index, Cell element)
        {
            if (cells.Count <= index)
            {
                string exceptionMessage = $"Column element count is {ElementCount()}. There is not any element at index: {index} to be edited";
                throw new IndexOutOfRangeException(exceptionMessage);
            }
            else if (this.Type != element.Type)
            {
                string exceptionMessage = $"Column type is {this.Type}. It is different from the new element type: {element.Type}.";
                throw new ArgumentException(exceptionMessage);
            }

            cells[index] = element;
        }

        public Cell GetElementAt(int index)
        {
            if (cells.Count <= index)
            {
                string exceptionMessage = $"Column element count is {ElementCount()}. There is not any element at index: {index} to be taken";
                throw new IndexOutOfRangeException(exceptionMessage);
            }

            return cells[index].Clone();
        }

        public int ElementCount()
        {
            return this.cells.Count;
        }
    }
}
