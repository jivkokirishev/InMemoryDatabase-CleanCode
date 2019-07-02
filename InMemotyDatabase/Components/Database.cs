using InMemoryDatabase.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InMemoryDatabase.Components
{
    public class Database : INameable
    {
        private IList<Table> tables;

        public Database(string name)
        {
            this.Name = name;
            this.tables = new List<Table>();
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<Table> Tables
        {
            get
            {
                IReadOnlyCollection<Table> tmpTables = new ReadOnlyCollection<Table>(this.tables);

                return tmpTables;
            }
        }

        public void AddTable(string tableName)
        {
            var table = new Table(tableName);
            tables.Add(table);
        }

        public void AddTable(Table table)
        {
            bool isTableUnique = TableExist(table.Name);

            if (!isTableUnique)
            {
                throw new ArgumentException("New table name is not unique. Please change it, before adding it to the database.");
            }

            tables.Add(table);
        }

        public Table GetTableAt(int index)
        {
            if (tables.Count >= index)
            {
                string exceptionMessage = $"Table count is {TableCount()}. There is not any element at index: {index} to be taken";
                throw new IndexOutOfRangeException(exceptionMessage);
            }

            return tables[index];
        }

        public Table GetTable(string tableName)
        {
            foreach (var table in tables)
            {
                if (table.Name == tableName)
                {
                    return table;
                }
            }

            throw new ArgumentException("There is not any table with that name.");

        }

        public bool TableExist(string tableName)
        {

            bool isTableUnique = this.tables.Any(x => x.Name == tableName);

            return isTableUnique;
        }

        public void RemoveTable(int index)
        {
            if (tables.Count <= index)
            {
                string exceptionMessage = $"Table count is {TableCount()}. There is not any element at index: {index} to be removed";
                throw new IndexOutOfRangeException(exceptionMessage);
            }

            tables.RemoveAt(index);
        }

        public int TableCount()
        {
            return tables.Count;
        }
    }
}
