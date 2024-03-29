﻿namespace DataAccessLayer.Interfaces;

public interface IBaseRepository <T>
{
    IQueryable<T> Select();
    Task<bool> Add(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(T entity);
}
