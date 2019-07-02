using InMemoryDatabase.Components;
using System;
using System.Collections.Generic;

namespace InMemoryDatabase.Managers
{
    public class DbTransformations
    {
        private Database database;

        public DbTransformations(Database database)
        {
            this.database = database;
        }


        public IList<string> TableNames()
        {
            var names = new List<string>();

            foreach (var table in database.Tables)
            {
                names.Add(table.Name);
            }

            return names;
        }

        public Cell Agregate(string tableName, string searchColumnName, Cell searchValue,
            string targetColumnName, OperationType operation)
        {
            throw new NotImplementedException();
        }

        public int TableCount()
        {
            var tableCount = database.TableCount();

            return tableCount;
        }
    }
}
