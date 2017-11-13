using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
	public class Scenes : MonoBehaviour {

		private static Dictionary<string, object> parameters;

		public static void Load(string sceneName, Dictionary<string, object> parameters = null)
		{
			Scenes.parameters = parameters;
			Application.LoadLevel(sceneName);
		}
	
		public static void Load(string sceneName, string paramKey, object paramValue)
		{
			parameters = new Dictionary<string, object>();
			parameters.Add(paramKey, paramValue);
			SceneManager.LoadScene(sceneName);
		}

		public static Dictionary<string, object> GetSceneParameters()
		{
			return parameters;
		}

		public static object GetParam(string paramKey)
		{
			if (parameters == null) return "";
			return parameters[paramKey];
		}

		public static void SetParam(string paramKey, string paramValue)
		{
			if (parameters == null)
				parameters = new Dictionary<string, object>();
			parameters.Add(paramKey, paramValue);
		}
	}
}
