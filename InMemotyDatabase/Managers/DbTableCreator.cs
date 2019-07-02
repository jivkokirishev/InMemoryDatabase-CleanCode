using InMemoryDatabase.Components;
using System.Collections.Generic;

namespace InMemoryDatabase.Managers
{
    public class DbTableCreator
    {
        private Database database;

        public DbTableCreator(Database database)
        {
            this.database = database;
        }

        public Table Where(string tableName, string searchColumnName, Cell searchValue)
        {
            Table table = database.GetTable(tableName);
            Column searchColumn = table.GetColumn(searchColumnName);

            string filteredTableName = tableName + "_filtered";
            Table filteredTable = new Table(filteredTableName);
            for (int i = 0; i < table.ColumnCount(); i++)
            {
                var currentColumn = table.GetColumnAt(i);
                filteredTable.AddColumn(currentColumn.Name, currentColumn.Type);
            }

            for (int i = 0; i < searchColumn.ElementCount(); i++)
            {
                if (searchColumn.GetElementAt(i) == searchValue)
                {
                    var row = table.GetRowAt(i);
                    filteredTable.AddRow(row);
                }
            }

            return filteredTable;
        }

        public Table InnerJoin(string fTableName, string fColumnName, string sTableName, string sColumnName)
        {
            Table firstTable = database.GetTable(fTableName);
            Table secondTable = database.GetTable(sTableName);

            string joinedTablesName = fTableName + "_" + sTableName + "_" + fColumnName + "_" + sColumnName;
            Table joinedTables = new Table(joinedTablesName);

            for (int i = 0; i < firstTable.ColumnCount(); ++i)
            {
                var currentColumn = firstTable.GetColumnAt(i);
                joinedTables.AddColumn(fTableName + "_" + currentColumn.Name, currentColumn.Type);
            }

            for (int i = 0; i < secondTable.ColumnCount(); ++i)
            {
                var currentColumn = secondTable.GetColumnAt(i);
                joinedTables.AddColumn(fTableName + "_" + currentColumn.Name, currentColumn.Type);
            }

            Column firstColumn = firstTable.GetColumn(fColumnName);
            Column secondColumn = firstTable.GetColumn(sColumnName);

            for (int i = 0; i < firstColumn.ElementCount(); ++i)
            {
                for (int j = 0; j < secondColumn.ElementCount(); ++j)
                {
                    if (firstColumn.GetElementAt(i) == secondColumn.GetElementAt(j))
                    {
                        var firstRowPart = firstTable.GetRowAt(i);
                        var secondRowPart = secondTable.GetRowAt(i);

                        List<Cell> fullRow = new List<Cell>();
                        fullRow.AddRange(firstRowPart);
                        fullRow.AddRange(secondRowPart);

                        joinedTables.AddRow(fullRow);
                    }
                }
            }
            return joinedTables;
        }

        public Table Select(string tableName, IList<string> selectedColumnsNames)
        {
            Table table = database.GetTable(tableName);

            string selectionTableName = tableName + "_selection";
            Table selectionTable = new Table(selectionTableName);

            List<int> selectedColumnsIndex = new List<int>();
            int columnCount = table.ColumnCount();
            for (int i = 0; i < columnCount; i++)
            {
                var currentColumn = table.GetColumnAt(i);

                bool isColumnSelected = selectedColumnsNames.Contains(currentColumn.Name);
                if (isColumnSelected)
                {
                    selectedColumnsIndex.Add(i);
                    selectionTable.AddColumn(currentColumn.Name, currentColumn.Type);
                }
            }

            for (int i = 0; i < table.GetColumnAt(0).ElementCount(); i++)
            {
                var row = table.GetRowAt(i);

                List<Cell> selectedCells = new List<Cell>();
                foreach (var index in selectedColumnsIndex)
                {
                    selectedCells.Add(row[index]);
                }
                selectionTable.AddRow(selectedCells);
            }

            return selectionTable;
        }
    }
}
