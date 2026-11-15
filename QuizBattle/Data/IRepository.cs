using System;

namespace QuizBattle.Data;

public interface IRepository<T>
{
    void Add(List<T> values);
    void Add(T value);
}
