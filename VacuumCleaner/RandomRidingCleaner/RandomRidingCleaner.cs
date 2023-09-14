namespace VacuumCleaner.RandomRidingCleaner;

/// <summary>
/// Представляет пылесос, выбирающий случайное направление движения при столкновении с препятствием.
/// </summary>
public class RandomRidingCleaner : Cleaner
{
	#region Data
	#region Consts
	private const int PossibleIterations = 60;
	#endregion

	#region Fields
	private bool _hasFinished;
	private readonly IRoom _room;
	private readonly int _xInitCoordinate;
	private readonly int _yInitCoordinate;
	private readonly Direction InitialDirection = Direction.Left;
	#endregion
	#endregion

	#region .ctor
	public RandomRidingCleaner(int xInitCoordinate, int yInitCoordinate, IRoom room)
	{
		_xInitCoordinate = xInitCoordinate;
		_yInitCoordinate = yInitCoordinate;
		_room = room;
		_hasFinished = false;
		CurrentX = _xInitCoordinate;
		CurrentY = _yInitCoordinate;
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
		_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
		var direction = InitialDirection;

		for (var i = 0; i <= PossibleIterations; i++)
		{
			//на каждой итерации попробовать двинуться. если не встечил препятствие, то двигаться туда же.
			//если препятствие, то выбрать рандомно другое направление
			//если больше не двинуться в выбранном направлении, то меняем направлние
			if (!TryMove(direction))
			{
				direction = SelectRandomDirection(direction);
			}

			Thread.Sleep(200);
		}

		_hasFinished = true;
	}

	/// <summary>
	/// Возвращает флаг завершения работы.
	/// </summary>
	/// <returns></returns>
	public override bool HasFinished() => _hasFinished;
	#endregion

	#region Private
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
		switch (direction)
		{
			case Direction.Right:
				if (_room.DoesCellExist(CurrentX + 1, CurrentY))
				{
					CurrentX += 1;
					_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
					return true;
				}

				break;
			case Direction.Left:
				if (_room.DoesCellExist(CurrentX - 1, CurrentY))
				{
					CurrentX -= 1;
					_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
					return true;
				}

				break;
			case Direction.Up:
				if (_room.DoesCellExist(CurrentX, CurrentY - 1))
				{
					CurrentY -= 1;
					_room.CleanArea(CurrentX, CurrentY, CurrentX, CurrentY);
					return true;
				}

				break;
			case Direction.Down:
				if (_room.DoesCellExist(CurrentX + 1, CurrentY))
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
