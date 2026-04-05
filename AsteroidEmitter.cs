using System;
using System.Collections.Generic;

public class AsteroidEmitter {
  private Queue<Asteroid> _available = new Queue<Asteroid>();

  // Сквозной счетчик спавнов
  private int _spawnCounter = 0;

  public AsteroidEmitter(int initialSize) {
    for (int i = 0; i < initialSize; ++i) {
      Asteroid asteroid = new Asteroid();
      _available.Enqueue(asteroid);
    }
  }

  public Asteroid Spawn() {
    Asteroid asteroid;
    if (_available.Count == 0) {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(">> Предупреждение: Пул пуст. Создан новый объект Asteroid!");
      Console.ResetColor();
      asteroid = new Asteroid();
    } else {
      asteroid = _available.Dequeue();
    }

    // Уникальный SpawnID для каждого спавна
    asteroid.SetSpawnID(++_spawnCounter);

    // Здесь активный астероид подписывается на получение хронов
    ChroneManager.AddListener(asteroid);

    return asteroid;
  }

  public void Recycle(Asteroid asteroid) {
    // Отписка от хронов, сброс состояния и возвращение в пула
    ChroneManager.RemoveListener(asteroid);
    asteroid.Reset();
    _available.Enqueue(asteroid);
  }
}