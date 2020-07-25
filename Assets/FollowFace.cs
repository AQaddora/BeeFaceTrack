using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FollowFace : MonoBehaviour
{
	public float smooth = 5;
    ARFaceManager aRFaceManager;
	ARFace arface;

	public static bool isSmilling = false;
	Vector3 offset = Vector3.zero;
	Vector3 initHeadPos;

	private void Awake()
	{
		aRFaceManager = FindObjectOfType<ARFaceManager>();
		if (aRFaceManager != null)
		{
			aRFaceManager.facesChanged += delegate
			{
				if (arface != null)
				{
					if (offset == Vector3.zero) offset = transform.position - arface.transform.position;
					Debug.Log(offset);
				}
				else
				{
					arface = FindObjectOfType<ARFace>();
					initHeadPos = arface.transform.position;
				}
			};
		}
	}

	private void Update()
	{
		CharacterThings.isControlling = (arface == null);
		if (arface != null && isSmilling)
		{ 
			CharacterThings.Instance.toRight = arface.transform.position.x < -0.05 || (arface.transform.rotation.eulerAngles.y > 5 && arface.transform.rotation.eulerAngles.y < 50);
			CharacterThings.Instance.toLeft  = arface.transform.position.x > 0.05 || (arface.transform.rotation.eulerAngles.y > 300 && arface.transform.rotation.eulerAngles.y < 355);
		}
	}

	string arrayAsString<T>(T[] arr)
	{
		string str = "[";
		for (int i = 0; i < arr.Length; i++)
		{
			str += arr[i].ToString() + ",";
		}
		return str.Substring(0,str.Length - 1) + "]";
	}
}
