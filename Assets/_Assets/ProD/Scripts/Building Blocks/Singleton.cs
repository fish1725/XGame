//This class is a simple singleton tailored for Unity3D
//Read more about what singletons are at:  http://social.msdn.microsoft.com/Search/en-US?query=singleton&ac=4

using UnityEngine;

namespace ProD
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
	   protected static T instance;
	   public static T Instance
	   {
	      get
	      {
	         if(instance == null)
	         {
	            instance = (T) FindObjectOfType(typeof(T));
	 
	            if (instance == null)
	            {
	               Debug.LogError("An instance of " + typeof(T) + 
	                  " is needed in the scene, but there is none.");
	            }
	         }
	         return instance;
	      }
	   }
	}
}