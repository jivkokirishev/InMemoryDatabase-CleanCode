using InMemoryDatabase.Components;
using InMemoryDatabase.Enums;
using InMemoryDatabase.Managers;
using System;
using System.Collections.Generic;

namespace InMemoryDatabase
{
    public class DatabaseManager
    {
        private Database database;

        private DbTableChanger tableChanger;

        private DbTableCreator tableCreator;

        private DbTransformations dbTransformations;

        public DatabaseManager(string databaseName)
        {
            database = new Database(databaseName);

            tableCreator = new DbTableCreator(database);
            tableChanger = new DbTableChanger(database);
            dbTransformations = new DbTransformations(database);
        }

        public void AddColumn(string tableName, string columnName, EnumType columnType)
        {
            tableChanger.AddColumn(tableName, columnName, columnType);
        }

        public void Update(string tableName, string searchColumnName, Cell searchValue,
            string targetColumnName, Cell targetValue)
        {
            tableChanger.Update(tableName, searchColumnName, searchValue, targetColumnName, targetValue);
        }

        public void Delete(string tableName, string searchColumnName, Cell searchValue)
        {
            tableChanger.Delete(tableName, searchColumnName, searchValue);
        }

        public void Insert(string tableName, IList<Cell> row)
        {
            tableChanger.Insert(tableName, row);
        }

        public void RenameTable(string tableName, string newTableName)
        {
            tableChanger.RenameTable(tableName, newTableName);
        }

        public void RenameColumn(string tableName, string columnName, string newColumnName)
        {
            tableChanger.RenameColumn(tableName, columnName, newColumnName);
        }

        public Table Where(string tableName, string searchColumnName, Cell searchValue)
        {
            Table filteredTable = tableCreator.Where(tableName, searchColumnName, searchValue);

            return filteredTable;
        }

        public Table InnerJoin(string fTableName, string fColumnName, string sTableName, string sColumnName)
        {
            Table joinedTables = tableCreator.InnerJoin(fTableName, fColumnName, sTableName, sColumnName);

            return joinedTables;
        }

        public Table Select(string tableName, IList<string> selectedColumnsNames)
        {
            Table selectionTable = tableCreator.Select(tableName, selectedColumnsNames);

            return selectionTable;
        }

        public IList<string> TableNames()
        {
            IList<string> names = dbTransformations.TableNames();

            return names;
        }

        public Cell Agregate(string tableName, string searchColumnName, Cell searchValue,
            string targetColumnName, OperationType operation)
        {
            Cell aggregatedValue = dbTransformations.Agregate(tableName, searchColumnName, searchValue, targetColumnName, operation);

            return aggregatedValue;
        }

        public int TableCount()
        {
            int tableCount = dbTransformations.TableCount();

            return tableCount;
        }
    }
}
