using System;

namespace DalFramework.Interface
{
    public interface IContextTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
