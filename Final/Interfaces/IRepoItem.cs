using System;

namespace Final.Interfaces
{
  public interface IRepoItem<T>
  {
    T Id { get; set; }
  
  }
}