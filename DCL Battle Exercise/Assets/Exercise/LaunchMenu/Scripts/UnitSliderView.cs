using System.Collections.Generic;
using Exercise.Battle.Scripts.Units;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exercise.LaunchMenu.Scripts
{
	public class UnitSliderView : MonoBehaviour
	{
		[SerializeField] private Slider slider;
		[SerializeField] private TMP_Text unitName;
		[SerializeField] private TMP_Text countText;

		public void UpdateWithModel(UnitObject unitObject, int count, UnityAction<float> onValueChanged)
		{
			void OnValueChanged(float value)
			{
				var intValue = (int) value;
				countText.SetText(intValue.ToString());
				onValueChanged.Invoke(intValue);
			}
			
			slider.SetValueWithoutNotify(count);
			unitName.SetText(unitObject.name);
			countText.SetText(count.ToString());
        
			slider.onValueChanged.RemoveAllListeners();
			slider.onValueChanged.AddListener(OnValueChanged);
		}
	}
}