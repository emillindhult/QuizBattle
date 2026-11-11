using System;

namespace QuizBattle.Data;

public interface IJsonLoadable<T>
{
    List<T> Load();
}
