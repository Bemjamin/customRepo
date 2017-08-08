using System;

public interface IContextTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }