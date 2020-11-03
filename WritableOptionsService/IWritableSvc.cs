using System;
using Microsoft.Extensions.Options;

namespace WritableOptionsService
{
    public interface IWritableSvc<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        bool Update(Action<T> applyChanges);
    }
}
