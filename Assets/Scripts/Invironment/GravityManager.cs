using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Invironment
{
	internal class GravityManager
	{
		public static GravityManager _instance;
		public Action<float> OnGravityScaleChanged;

		private float _gravityScale = 4;
		public float GravityScale
		{
			get => _gravityScale;
			set
			{
				OnGravityScaleChanged.Invoke(_gravityScale);
				_gravityScale = value;
			}
		}
		static GravityManager()
		{ 
			if (_instance == null)
				_instance = new();
		}

	}
}
