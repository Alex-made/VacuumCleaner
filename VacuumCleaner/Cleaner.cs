namespace VacuumCleaner;

/// <summary>
/// Представляет базовый пылесос.
/// </summary>
public abstract class Cleaner : ICleaner
{
	#region Data
	#region Consts
	/// <summary>
	/// Возвращает максимально число шагов пылесоса.
	/// </summary>
	protected const int MaxPossibleIterations = 60;
	#endregion
	#endregion

	/// <summary>
	/// Представляет вермя задержки на каждом шаге.
	/// </summary>
	/// <remarks>Имитирует время, затрачиваемое на передвижение настящим роботом-пылесосом.</remarks>
	public static int SleepMilliseconds = 200;

	#region Overridable
	/// <inheritdoc />
	public abstract void Clean();

	/// <inheritdoc />
	public abstract bool HasFinished();
	#endregion
	
	#region Properties
	/// <summary>
	/// Возвращает текущую абсолютную координату X.
	/// </summary>
	/// <remarks>Необходимо для отображения пылесоса на поле.</remarks>
	public int CurrentX
	{
		get;
		protected set;
	}

	/// <summary>
	/// Возвращает текущую абсолютную координату Y.
	/// </summary>
	/// <remarks>Необходимо для отображения пылесоса на поле.</remarks>
	public int CurrentY
	{
		get;
		protected set;
	}
	#endregion
}
