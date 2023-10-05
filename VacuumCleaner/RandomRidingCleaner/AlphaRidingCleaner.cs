using System.Runtime.CompilerServices;

namespace VacuumCleaner.RandomRidingCleaner;

/// <summary>
/// Представляет пылесос.
/// </summary>
public class AlphaRidingCleaner : Cleaner
{
	#region Data
	#region Consts
	private const int PossibleIterations = 120;
	#endregion

	#region Fields
	private bool _hasFinished;
	private readonly IRoom _room;
	private readonly int _xInitCoordinate;
	private readonly int _yInitCoordinate;
	private readonly Direction InitialDirection = Direction.Left;
	private Direction _direction;
	#endregion
	#endregion

	#region .ctor
	public AlphaRidingCleaner(int xInitCoordinate, int yInitCoordinate, IRoom room)
	{
		_xInitCoordinate = xInitCoordinate;
		_yInitCoordinate = yInitCoordinate;
		_room = room;
		_hasFinished = false;
		CurrentX = _xInitCoordinate;
		CurrentY = _yInitCoordinate;
		_direction = InitialDirection;
	}
	#endregion

	#region Properties
	/// <summary>
	/// Возвращает текущую координату X.
	/// </summary>
	public int CurrentX
	{
		get;
		private set;
	}

	/// <summary>
	/// Возвращает текущую координату Y.
	/// </summary>
	public int CurrentY
	{
		get;
		private set;
	}
	#endregion

	#region Overrided
	public override void Clean()
	{
		_hasFinished = false;
		//почистить под собой
		_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);

		//Debug убрать
		// TryMove(Direction.Down);
		// Thread.Sleep(200);
		// TryMove(Direction.Down);
		// Thread.Sleep(200);
		// TryMove(Direction.Right);
		// Thread.Sleep(200);
		// TryMove(Direction.Right);
		// Thread.Sleep(200);
		// TryMove(Direction.Up);
		// Thread.Sleep(200);
		// TryMove(Direction.Up);
		// Thread.Sleep(200);
		// TryMove(Direction.Left);
		// Thread.Sleep(200);
		// TryMove(Direction.Left);
		// Thread.Sleep(200);
		//Debug убрать
		
		
		//1 итерация: обход комнаты по периметру
		GoRoomAround();
		//2 итерация: проходим змейкой внутри комнаты и сохраняем непройденные участки в стек
		
		//3 итерация: чистим места из стека
		
		_hasFinished = true;
	}

	private void GoRoomAround()
	{
		int? lastStartGoingAroundX = null;
		int? lastStartGoingAroundY = null;
		
		for (var i = 0; i <= PossibleIterations; i++)
		{
			//пробуем двигаться вперед до препятствия (край комнаты или препятствие)
			//дошли до препятствия. переходим в функцио объезда препятствия
			if (!TryMove(_direction))
			{
				//дошли до препятствия. переходим в функцию объезда препятствия
				var startGoingAroundX = CurrentX;
				var startGoingAroundY = CurrentY;
				//нужно проверить, что начальные координаты объезда препятствия не те же самые, что и в прошлый раз.
				//иначе - выход
				if (lastStartGoingAroundX.HasValue && startGoingAroundX == lastStartGoingAroundX.Value 
					&& lastStartGoingAroundY.HasValue && startGoingAroundY == lastStartGoingAroundY.Value)
				{
					return;
				}
				//начать обход препятствия
				GoBarrierAround(startGoingAroundX, startGoingAroundY);
				lastStartGoingAroundX = startGoingAroundX;
				lastStartGoingAroundY = startGoingAroundY;
			}
			
			
		}
		
		
	}

	private void GoBarrierAround(int startGoingAroundX, int startGoingAroundY)
	{
		//дошли до препятствия. переходим в функцио объезда препятствия
		//внутри функции:
		//повернули направо. попробовали проехать прямо. 
		//если не получилось, снова объезд препятствия
		TurnRight();
		if (!TryMove(_direction))
		{
			GoBarrierAround(startGoingAroundX, startGoingAroundY);
		}
		//маршрут закольцован? тогда выйти
		if (CurrentX == startGoingAroundX && CurrentY == startGoingAroundY)
		{
			return;
		}
		//если получилось сделать шаг, попробовали повернуть налево и сделать шаг прямо. 
		//если не получилось, снова объезд препятствия
		TurnLeft();
		if (!TryMove(_direction))
		{
			GoBarrierAround(startGoingAroundX, startGoingAroundY);
		}
		//маршрут закольцован? тогда выйти
		if (CurrentX == startGoingAroundX && CurrentY == startGoingAroundY)
		{
			return;
		}
		//если получилось сделать шаг, снова шаг вперед. и поворот налево и шаг вперед
		//если не получилось, снова объезд препятствия
		if (!TryMove(_direction))
		{
			GoBarrierAround(startGoingAroundX, startGoingAroundY);
		}
		//маршрут закольцован? тогда выйти
		if (CurrentX == startGoingAroundX && CurrentY == startGoingAroundY)
		{
			return;
		}
		TurnLeft();
		if (!TryMove(_direction))
		{
			GoBarrierAround(startGoingAroundX, startGoingAroundY);
		}
		//маршрут закольцован? тогда выйти
		if (CurrentX == startGoingAroundX && CurrentY == startGoingAroundY)
		{
			return;
		}
		
	}

	/// <summary>
	/// Возвращает флаг завершения работы.
	/// </summary>
	/// <returns></returns>
	public override bool HasFinished() => _hasFinished;
	#endregion

	#region Private
	/// <summary>
	/// Поворачивает пылесос направо.
	/// </summary>
	private void TurnRight()
	{
		_direction = _direction switch
		{
			Direction.Right => Direction.Down,
			Direction.Down => Direction.Left,
			Direction.Left => Direction.Up,
			Direction.Up => Direction.Right,
			_ => _direction
		};
	}
	
	/// <summary>
	/// Поворачивает пылесос налево.
	/// </summary>
	private void TurnLeft()
	{
		_direction = _direction switch
		{
			Direction.Right => Direction.Up,
			Direction.Up => Direction.Left,
			Direction.Left => Direction.Down,
			Direction.Down => Direction.Right,
			_ => _direction
		};
	}

	private Direction SelectRandomDirection(Direction currentDirection)
	{
		var rand = new Random();
		var nextDirection = rand.Next(300);
		if (nextDirection % 3 == (int)currentDirection)
		{
			return SelectRandomDirection(currentDirection);
		}

		return (nextDirection % 3) switch
		{
			0 => Direction.Right,
			1 => Direction.Left,
			2 => Direction.Up,
			3 => Direction.Down,
			_ => throw new InvalidOperationException()
		};
	}

	private bool TryMove(Direction direction)
	{
		Thread.Sleep(400);
		switch (direction)
		{
			case Direction.Down:
				if (_room.DoesCellExist(CurrentX + 1, CurrentY))
				{
					CurrentX += 1;
					_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
					return true;
				}

				break;
			case Direction.Up:
				if (_room.DoesCellExist(CurrentX - 1, CurrentY))
				{
					CurrentX -= 1;
					_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
					return true;
				}

				break;
			case Direction.Left:
				if (_room.DoesCellExist(CurrentX, CurrentY - 1))
				{
					CurrentY -= 1;
					_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
					return true;
				}

				break;
			case Direction.Right:
				if (_room.DoesCellExist(CurrentX, CurrentY + 1))
				{
					CurrentY += 1;
					_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
					return true;
				}

				break;
		}

		return false;
	}
	#endregion

	//TODO при врезании в препятствие должны учитываться размеры пылесоса, чтобы он прощупывал пространство передним краем, а не центром.
}


//тест кейсы для проверки алгоритма обхода комнаты:
//1. Начать движение из угла комнаты. Должен идти по периметру и обходить все препятствия. Заезжать в углы и тупики и выходить из них
//2. Мы начали движение с центра комнаты. Пылесос встречает препятсиве посередине и начинает вращаться только вокруг него не выходя из цикла. Этого нужно избегать