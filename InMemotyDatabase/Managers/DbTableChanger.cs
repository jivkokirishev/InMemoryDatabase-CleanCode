using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryDatabase.Managers
{
    public class DbTableChanger
    {
        private Database database;

        public DbTableChanger(Database database)
        {
            this.database = database;
        }

        public void AddColumn(string tableName, string columnName, EnumType columnType)
        {
            Table table = database.GetTable(tableName);

            table.AddColumn(columnName, columnType);

            var currentColumn = table.GetColumn(columnName);

            Cell defaultCell = new Cell(columnType);
            for (int i = 0; i < table.GetColumnAt(0).ElementCount(); ++i)
            {
                currentColumn.AddElement(defaultCell.Clone());
            }
        }

        public void Update(string tableName, string searchColumnName, Cell searchValue,
            string targetColumnName, Cell targetValue)
        {
            Table table = database.GetTable(tableName);

            Column searchColumn = table.GetColumn(searchColumnName);
            Column targetColumn = table.GetColumn(targetColumnName);

            for (int i = 0; i < searchColumn.ElementCount(); ++i)
            {
                if (searchColumn.GetElementAt(i) == searchValue)
                {
                    targetColumn.EditElementAt(i, targetValue.Clone());
                }
            }
        }

        public void Delete(string tableName, string searchColumnName, Cell searchValue)
        {
            Table table = database.GetTable(tableName);

            Column searchColumn = table.GetColumn(searchColumnName);

            for (int i = 0; i < searchColumn.ElementCount(); ++i)
            {
                if (searchColumn.GetElementAt(i) == searchValue)
                {
                    for (int j = 0; j < table.ColumnCount(); j++)
                    {
                        table.GetColumnAt(j).RemoveElementAt(i);
                    };
                }
            }
        }

        public void Insert(string tableName, IList<Cell> row)
        {
            Table table = database.GetTable(tableName);

            table.AddRow(row);
        }

        public void RenameTable(string tableName, string newTableName)
        {
            bool doesTableExist = database.TableExist(newTableName);
            if (doesTableExist)
            {
                throw new ArgumentException("The new table name is not unique.");
            }

            Table table = database.GetTable(tableName);
            table.Name = newTableName;
        }

        public void RenameColumn(string tableName, string columnName, string newColumnName)
        {
            Table table = database.GetTable(tableName);

            bool doesColumnExist = table.ColumnExist(newColumnName);
            if (doesColumnExist)
            {
                throw new ArgumentException("The new column name is not unique.");
            }

            Column column = table.GetColumn(columnName);
            column.Name = newColumnName;
        }
    }
}
