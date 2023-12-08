namespace TestWork.Game.Enemies
{

	public class EnemyModel
	{

		private float _health = 100.0f;
		private float _maxHealth;
		private float _currentSpeed;
		private float _maxSpeed;
		
		public float Health => _health;
		public float CurrentSpeed => _currentSpeed;

		public void InitHealth(float health)
		{
			_maxHealth = health;
			_health = _maxHealth;
		}

		public void InitSpeed(float speed)
		{
			_maxSpeed = speed;
			_currentSpeed = _maxSpeed;
		}
		
	}

}