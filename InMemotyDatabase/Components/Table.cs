using InMemoryDatabase.Contracts;
using InMemoryDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace InMemoryDatabase.Components
{
    public class Table : INameable
    {
        private IList<Column> columns;

        public Table(string name)
        {
            this.Name = name;
            this.columns = new List<Column>();
        }

        public string Name { get; set; }

        public IReadOnlyCollection<Column> Columns
        {
            get
            {
                IReadOnlyCollection<Column> tmpCols = new ReadOnlyCollection<Column>(this.columns);

                return tmpCols;
            }
        }

        public void AddColumn(string columnName, EnumType columnType)
        {
            bool doesNowColumnExist = ColumnExist(columnName);
            if (doesNowColumnExist)
            {
                throw new ArgumentException("The new column name is not unique.");
            }

            var column = new Column(columnName, columnType);
            columns.Add(column);
        }

        public Column GetColumn(string columnName)
        {
            foreach (var column in columns)
            {
                if (column.Name == columnName)
                {
                    return column;
                }
            }

            throw new ArgumentException("There is not any column with that name.");
        }

        public Column GetColumnAt(int index)
        {
            if (columns.Count <= index)
            {
                string exceptionMessage = $"Column count is {ColumnCount()}. There is not any column at index: {index} to be taken.";
                throw new IndexOutOfRangeException(exceptionMessage);
            }

            return columns[index];
        }

        public bool ColumnExist(string columnName)
        {
            bool isColumnUnique = columns.Any(x => x.Name == columnName);

            return isColumnUnique;
        }

        public void RemoveColumn(int index)
        {
            if (columns.Count <= index)
            {
                string exceptionMessage = $"Column count is {ColumnCount()}. There is not any column at index: {index} to be removed.";
                throw new IndexOutOfRangeException(exceptionMessage);
            }

            columns.RemoveAt(index);
        }

        public void AddRow(IList<Cell> row)
        {
            if (this.ColumnCount() != row.Count)
            {
                string ecxeptionMessage = "Cells count is different from the columns count in the table.";
                throw new ArgumentOutOfRangeException(ecxeptionMessage);
            }

            for (int i = 0; i < this.ColumnCount(); i++)
            {
                var column = this.GetColumnAt(i);
                var elementToBeAdded = row[i].Clone();

                if (column.Type != elementToBeAdded.Type)
                {
                    string ecxeptionMessage = "Cell type is different from the column type.";
                    throw new ArgumentException(ecxeptionMessage);
                }
                column.AddElement(elementToBeAdded);
            }
        }

        public IList<Cell> GetRowAt(int rowIndex)
        {
            if (this.GetColumnAt(0).ElementCount() <= rowIndex)
            {
                string exceptionMessage = "Inputted row index is bigger than the table row count.";
                throw new ArgumentException(exceptionMessage);
            }

            var row = new List<Cell>();

            for (int i = 0; i < this.ColumnCount(); i++)
            {
                var currentColumn = this.GetColumnAt(i);
                row.Add(currentColumn.GetElementAt(rowIndex));
            }

            return row;
        }

        public int ColumnCount()
        {
            return columns.Count;
        }
    }
}
