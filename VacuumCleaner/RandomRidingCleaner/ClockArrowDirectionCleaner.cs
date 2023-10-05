namespace VacuumCleaner.RandomRidingCleaner;

/// <summary>
/// Представляет пылесос, поворачивающий по часовой стрелке при столкновении с препятствием.
/// </summary>
public class ClockArrowDirectionCleaner : Cleaner
{
	#region Data
	#region Fields
	private bool _hasFinished;
	private readonly IRoom _room;
	private readonly int _xInitCoordinate;
	private readonly int _yInitCoordinate;
	private readonly Direction InitialDirection = Direction.Left;
	#endregion
	#endregion

	#region .ctor
	public ClockArrowDirectionCleaner(int xInitCoordinate, int yInitCoordinate, IRoom room)
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

		for (var i = 0; i <= MaxPossibleIterations; i++)
		{
			//на каждой итерации попробовать двинуться. если не встечил препятствие, то двигаться туда же.
			//если препятствие, то выбрать рандомно другое направление
			//если больше не двинуться в выбранном направлении, то меняем направлние
			if (!TryMove(direction))
			{
				direction = SelectDirection(direction);
			}
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
	private Direction SelectDirection(Direction currentDirection)
	{
		//поворачиваем пылесос на 90 градусов
		return currentDirection switch
		{
			Direction.Right => Direction.Down,
			Direction.Down => Direction.Left,
			Direction.Left => Direction.Up,
			Direction.Up => Direction.Right,
			_ => throw new InvalidOperationException()
		};
	}

	private bool TryMove(Direction direction)
	{
		Thread.Sleep(200);
		//в связи с нумераций массива немного не наглядно представлены стороны движения
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
