using UnityEngine;

namespace Exercise.Battle.Scripts.Units
{
	public class ArmyEntityFactory
	{
		private readonly MaterialPropertyBlock _materialPropertyBlock = new MaterialPropertyBlock();
		private readonly int ColorID = Shader.PropertyToID("_Color");
		
		protected void SetColor(Renderer renderer, Color color)
		{
			renderer.GetPropertyBlock(_materialPropertyBlock);
			_materialPropertyBlock.SetColor(ColorID, color);
			renderer.SetPropertyBlock(_materialPropertyBlock);
		}
	}
}