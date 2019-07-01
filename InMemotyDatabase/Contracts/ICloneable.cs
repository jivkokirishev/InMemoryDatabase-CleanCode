using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryDatabase.Contracts
{
    public interface ICloneable<T>
    {
        T Clone();
    }
}
