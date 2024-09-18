using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ExtensionToolbox
{
	/// <summary>
	/// R Hunter Gough's handy toolbox of extension methods for various data types
	/// </summary>
	public static class IntExtensions
	{
		/// <summary>
		/// Returns the number or m, whichever is greater
		/// </summary>
		public static int Min(this int n, int m)
		{
			return (n > m) ? n : m;
		}

		/// <summary>
		/// Returns the number or m, whichever is lesser
		/// </summary>		
		public static int Max(this int n, int m)
		{
			return (n < m) ? n : m;
		}

		/// <summary>
		/// return n, clamped between min and max
		/// </summary>
		/// <param name="min">Minimum value.</param>
		/// <param name="max">Maximum value.</param>
		public static int MinMax(this int n, int min, int max)
		{
			return (n < min) ? min : (n > max) ? max : n;
		}

		/// <summary>
		/// returns whether n is between min and max, exclusive
		/// </summary>
		/// <param name="min">minimum value. n must be more than this</param>
		/// <param name="max">maximum value. n must be less than this</param>
		/// <returns></returns>
		public static bool Between(this int n, int min, int max)
		{
			if(min < max)
			{
				return n > min && n < max;
			}
			else
			{
				return n > max && n < min;
			}
		}
	}

	public static class FloatExtensions
	{
		/// <summary>
		/// Returns the square of the number
		/// </summary>
		public static float Squared(this float n)
		{
			return n * n;
		}

		/// <summary>
		/// Returns the number or m, whichever is greater
		/// </summary>
		public static float Min(this float n, float m)
		{
			return (n > m) ? n : m;
		}

		/// <summary>
		/// Returns the number or m, whichever is lesser
		/// </summary>
		public static float Max(this float n, float m)
		{
			return (n < m) ? n : m;
		}

		/// <summary>
		/// return n, clamped between min and max
		/// </summary>
		/// <param name="min">Minimum value.</param>
		/// <param name="max">Maximum value.</param>
		public static float MinMax(this float n, float min, float max)
		{
			return (n < min) ? min : (n > max) ? max : n;
		}

		/// <summary>
		/// return degree angle n, clamped between min and max
		/// </summary>
		/// <param name="min">Minimum value.</param>
		/// <param name="max">Maximum value.</param>
		public static float AngleMinMax(this float n, float min, float max)
		{
			while(n > 180)
			{
				n -= 360;
			}

			while(n < -180)
			{
				n += 360;
			}

			return (n < min) ? min : (n > max) ? max : n;
		}
	}

	public static class VectorExtensions
	{
		/// <summary>
		/// Returns the Vector3 with Y and Z swapped
		/// </summary>
		public static Vector3 xzy(this Vector3 v)
		{
			return new Vector3(v.x, v.z, v.y);
		}

		/// <summary>
		/// Returns a Vector3 with Y used for the Z
		/// </summary>
		public static Vector3 xzy(this Vector2 v)
		{
			return new Vector3(v.x, 0, v.y);
		}

		/// <summary>
		/// Returns a Vector2 with Y replaced by Z
		/// </summary>
		public static Vector2 xz(this Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		/// <summary>
		/// Returns a Vector3 with Y set to zero
		/// </summary>
		public static Vector3 x0z(this Vector3 v)
		{
			return new Vector3(v.x, 0, v.z);
		}

		/// <summary>
		/// Returns a Vector2 with the X and Y swapped
		/// </summary>
		public static Vector2 yx(this Vector3 v)
		{
			return new Vector2(v.y, v.x);
		}

		/// <summary>
		/// Returns a Vector2 with the X and Y swapped
		/// </summary>
		public static Vector2 yx(this Vector2 v)
		{
			return new Vector2(v.y, v.x);
		}

		/// <summary>
		/// Turns a cartesian Vector2 into a polar Vector2 where x = angle and y = distance
		/// </summary>
		/// <param name="clockwiseFromNorth">
		/// If true, returns angle as clockwise from North
		/// Otherwise, returns standard polar coordinates; counter-clockwise from East
		/// </param>
		/// <returns>a polar Vector2 where x = angle and y = distance</returns>
		public static Vector2 Cart2Pol(this Vector2 cart, bool clockwiseFromNorth = false)
		{
			float angle = Mathf.Atan2(cart.y, cart.x) * Mathf.Rad2Deg;

			if(clockwiseFromNorth)
			{
				angle = Mathf.Atan2(cart.x, cart.y) * Mathf.Rad2Deg;
			}

			return new Vector2(angle, cart.magnitude);
		}

		/// <summary>
		/// Turns a polar Vector2 where x = angle and y = distance into a cartesian Vector2
		/// </summary>
		/// <param name="clockwiseFromNorth">
		/// If true, interprets angle as clockwise from North
		/// Otherwise, assumes standard polar coordinates; counter-clockwise from East
		/// </param>
		public static Vector2 Pol2Cart(this Vector2 pol, bool clockwiseFromNorth = false)
		{
			if(clockwiseFromNorth)
			{
				return new Vector2(Mathf.Sin(pol.x * Mathf.Deg2Rad), Mathf.Cos(pol.x * Mathf.Deg2Rad)) * pol.y;
			}
			else
			{
				return new Vector2(Mathf.Cos(pol.x * Mathf.Deg2Rad), Mathf.Sin(pol.x * Mathf.Deg2Rad)) * pol.y;
			}
		}

		/// <summary>
		/// clamps this vector3 between min and max values on all 3 axes
		/// </summary>
		/// <param name="min">minimum values</param>
		/// <param name="max">maximum values</param>
		/// <param name="angle">whether value is linear or an angle</param>
		public static Vector3 MinMax(this Vector3 v, Vector3 min, Vector3 max, bool angle = false)
		{
			if(angle)
			{
				return new Vector3(
					v.x.AngleMinMax(min.x, max.x),
					v.y.AngleMinMax(min.y, max.y),
					v.z.AngleMinMax(min.z, max.z)
					);

			}
			else
			{
				return new Vector3(
					v.x.MinMax(min.x, max.x),
					v.y.MinMax(min.y, max.y),
					v.z.MinMax(min.z, max.z)
					);
			}
		}

		/// <summary>
		/// get the closest point on the line that runs through start and end to provided point
		/// </summary>
		/// <param name="start">start point of the line</param>
		/// <param name="end">end point of the line</param>
		/// <param name="clamp">if true, clamps the result between the start and end points of the line</param>
		public static Vector3 ClosestPointOnLine(this Vector3 point, Vector3 start, Vector3 end, bool clamp = true)
		{
			Vector3 v = start + Vector3.Project(point - start, end - start);

			if(clamp)
			{
				if(Vector3.Dot(start - v, start - end) < 0)
				{
					// if v is past the start, clamp it to the start
					v = start;
				}
				else if(Vector3.Dot(end - v, end - start) < 0)
				{
					// if v is past the end, clamp it to the end
					v = end;
				}
			}

			return v;
		}
	}

	public static class StringExtensions
	{
		/// <summary>
		/// Shifts the first "length" characters off the beginning of the string and returns them
		/// </summary>
		public static string Shift(this string s, int length)
		{
			string sub;

			if(s.Length > length)
			{
				sub = s.Substring(0, length);
				s = s.Substring(length);
			}
			else
			{
				sub = s;
				s = "";
			}

			return sub;
		}

		/// <summary>
		/// Parses the string into an int array separated by "separator"
		/// </summary>
		public static int[] ToIntArray(this string s, char separator)
		{
			s = s.Trim();
			List<int> n = new List<int>();
			string[] sa = s.Split(separator);
			for(int i = 0; i < sa.Length; i++)
			{
				if(!string.IsNullOrEmpty(sa[i]))
				{
					n.Add(System.Convert.ToInt32(sa[i]));
				}
			}

			return n.ToArray();
		}

		/// <summary>
		/// Parses the string into a float array separated by "separator"
		/// </summary>
		public static float[] ToFloatArray(this string s, char separator)
		{
			s = s.Trim();
			List<float> n = new List<float>();
			string[] sa = s.Split(separator);
			for(int i = 0; i < sa.Length; i++)
			{
				if(!string.IsNullOrEmpty(sa[i]))
				{
					n.Add(float.Parse(sa[i], System.Globalization.CultureInfo.InvariantCulture.NumberFormat));
				}
			}

			return n.ToArray();
		}

		/// <summary>
		/// Insert a breakString at every n characters of string s
		/// </summary>
		public static string BreakEveryN(this string s, int n, string breakString = " ")
		{
			if(n < 1)
			{
				return s;
			}

			for(int i = n; i < s.Length; i += n)
			{
				s = s.Insert(i, breakString);
				i += breakString.Length;
			}

			return s;
		}

		/// <summary>
		/// Wraps the string in a rich text color tag.
		/// </summary>
		/// <returns>The wrapped string.</returns>
		/// <param name="s">The string.</param>
		/// <param name="color">The color.</param>
		public static string WrapInColorTag(this string s, Color color)
		{
			return "<color=#" + color.ToHexString() + ">" + s + "</color>";
		}

		/// <summary>
		/// Capitalize the first letter of the string, and lowercase the rest
		/// </summary>
		/// <param name="s">The string</param>
		public static string Capitalize(this string s)
		{
			// Check for empty string.
			if(string.IsNullOrEmpty(s))
			{
				return s;
			}
			else if(s.Length < 2)
			{
				return s.ToUpper();
			}

			// Return char and concat substring.
			return char.ToUpper(s[0]) + s.Substring(1).ToLower();
		}

		/// <summary>
		/// convert the string to camel case
		/// </summary>
		/// <param name="capitalizeFirst">if true, capitalize the first letter (i.e. PascalCase)</param>
		/// <param name="keepCapitals">if true, keep the original capitals in addition to camel casing</param>
		/// <returns></returns>
		public static string ToCamelCase(this string s, bool capitalizeFirst = false, bool keepCapitals = true)
		{
			if(string.IsNullOrEmpty(s))
			{
				return s;
			}

			var words = s.Replace('_', ' ').Split(' ');
			int startingIndex = capitalizeFirst ? 0 : 1;
			for(int i = startingIndex; i < words.Length; i++)
			{
				if(keepCapitals)
				{
					words[i] = $"{char.ToUpper(words[i][0])}{words[i][1..]}";
				}
				else
				{
					words[i] = $"{char.ToUpper(words[i][0])}{words[i][1..].ToLower()}";
				}
			}

			return string.Join("", words);
		}

		/// <summary>
		/// string all non-alpha characters from the string
		/// </summary>
		/// <param name="stripNumeric">if true, also strip numeric characters</param>
		public static string StripNonAlpha(this string s, bool stripNumeric = true)
		{
			return new Regex(stripNumeric ? "[^a-zA-Z]" : "[^a-zA-Z0-9]").Replace(s, "");
		}
	}

	public static class ListExtensions
	{
		/// <summary>
		/// Adds an item to the list only if the list doesn't already contain the item.
		/// If you want to do this for every item in the list, you should probably
		/// use a HashSet instead of a List.
		/// </summary>
		public static void AddUnique<T>(this List<T> source, T item)
		{
			if(!source.Contains(item))
			{
				source.Add(item);
			}
		}

		/// <summary>
		/// Swaps the element at index1 with the element at index2
		/// </summary>
		public static void Swap<T>(this List<T> source, int index1, int index2)
		{
			if(index1 < 0 || index1 > source.Count)
				return;
			else if(index2 < 0 || index2 > source.Count)
				return;
			else if(index1 == index2)
				return;

			T temp = source[index1];
			source[index1] = source[index2];
			source[index2] = temp;
		}

		/// <summary>
		/// Shuffles all of the elements in the list
		/// </summary>
		public static void Shuffle<T>(this List<T> source)
		{
			List<T> newList = new();

			// fisher-yates shuffle
			while(source.Count > 0)
			{
				int n = UnityEngine.Random.Range(0, source.Count);
				newList.Add(source[n]);
				source.RemoveAt(n);
			}

			source.AddRange(newList);
		}

		/// <summary>
		/// returns a new list with all of this list's elements shuffled
		/// </summary>
		/// <returns>a list with all elements shuffled</returns>
		public static List<T> Shuffled<T>(this List<T> source)
		{
			List<T> newList = new(source);
			newList.Shuffle();
			return newList;
		}

		/// <summary>
		/// Returns the last element in the list
		/// </summary>
		public static T Last<T>(this List<T> source)
		{
			if(source.Count < 1)
			{
				return default;
			}

			return source[source.Count - 1];
		}

		/// <summary>
		/// returns the first element of the list, or null if the list is empty
		/// </summary>
		public static T FirstOrNull<T>(this List<T> source)
		{
			if(source.Count < 1)
			{
				return default;
			}

			return source[0];
		}

		/// <summary>
		/// Returns a random element from the list
		/// </summary>
		public static T Random<T>(this List<T> source)
		{
			if(source.Count < 1)
			{
				return default;
			}
			else if(source.Count == 1)
			{
				return source[0];
			}
			else
			{
				return source[UnityEngine.Random.Range(0, source.Count)];
			}
		}

		/// <summary>
		/// Shifts the first element off the list and returns it. Similar to Queue.Dequeue().
		/// If you are only Adding and Shifting to a List, consider using a Queue instead.
		/// </summary>
		public static T Shift<T>(this List<T> source)
		{
			if(source.Count < 1)
			{
				return default;
			}

			T temp = source[0];
			source.RemoveAt(0);
			return temp;
		}

		/// <summary>
		/// Pops the last element off the list and returns it.
		/// If you are only Adding and Popping to a List, consider using a Stack instead.
		/// </summary>
		public static T Pop<T>(this List<T> source)
		{
			if(source.Count < 1)
			{
				return default;
			}

			T temp = source[source.Count - 1];
			source.RemoveAt(source.Count - 1);
			return temp;
		}

		/// <summary>
		/// Adds item to the beginning of the list, moving all other elements down
		/// </summary>
		public static void Unshift<T>(this List<T> source, T item)
		{
			source.Insert(0, item);
		}

		/// <summary>
		/// unshift the item to the list if it's not already in ths list
		/// </summary>
		public static void UnshiftUnique<T>(this List<T> source, T item)
		{
			if(!source.Contains(item))
			{
				source.Insert(0, item);
			}
		}
	}

	public static class ArrayExtensions
	{
		/// <summary>
		/// Returns the last element in the array
		/// </summary>
		public static T Last<T>(this T[] source)
		{
			if(source.Length < 1)
			{
				return default;
			}

			return source[source.Length - 1];
		}

		/// <summary>
		/// returns a new list with all of this array's elements shuffled
		/// </summary>
		/// <returns>a list with all elements shuffled</returns>
		public static List<T> Shuffled<T>(this T[] source)
		{
			List<T> newList = new(source);
			newList.Shuffle();
			return newList;
		}
	}

	public static class CameraExtensions
	{
		/// <summary>
		/// Moves the vanishing point of the camera
		/// </summary>
		public static void SetVanishingPoint(this Camera cam, Vector2 offset)
		{
			Matrix4x4 m = cam.projectionMatrix;
			float w = 2 * cam.nearClipPlane / m.m00;
			float h = 2 * cam.nearClipPlane / m.m11;

			float left = -w / 2 - offset.x;
			float right = left + w;
			float bottom = -h / 2 - offset.y;
			float top = bottom + h;

			cam.projectionMatrix = PerspectiveOffCenter(left, right, bottom, top, cam.nearClipPlane, cam.farClipPlane);
		}

		/// <summary>
		/// Moves the vanishing point of the camera
		/// </summary>
		public static void SetVanishingPoint(this Camera cam, float x, float y = 0)
		{
			SetVanishingPoint(cam, new Vector2(x, y));
		}

		// used by SetVanishingPoint()
		private static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
		{
			float x = 2.0F * near / (right - left);
			float y = 2.0F * near / (top - bottom);
			float a = (right + left) / (right - left);
			float b = (top + bottom) / (top - bottom);
			float c = -(far + near) / (far - near);
			float d = -(2.0F * far * near) / (far - near);
			float e = -1.0F;
			Matrix4x4 m = new Matrix4x4();
			m[0, 0] = x;
			m[0, 1] = 0;
			m[0, 2] = a;
			m[0, 3] = 0;
			m[1, 0] = 0;
			m[1, 1] = y;
			m[1, 2] = b;
			m[1, 3] = 0;
			m[2, 0] = 0;
			m[2, 1] = 0;
			m[2, 2] = c;
			m[2, 3] = d;
			m[3, 0] = 0;
			m[3, 1] = 0;
			m[3, 2] = e;
			m[3, 3] = 0;
			return m;
		}
	}

	public static class GameObjectExtensions
	{
		/// <summary>
		/// Sets the layer on the GameObject and all of its children
		/// </summary>
		public static void SetLayerRecursively(this GameObject go, int layerNumber)
		{
			go.layer = layerNumber;

			for(int i = 0; i < go.transform.childCount; i++)
			{
				go.transform.GetChild(i).gameObject.SetLayerRecursively(layerNumber);
			}
		}

		/// <summary>
		/// Gets a component of the specified type, adding it if there isn't already one
		/// Also useful for checking to see if a component is present and adding it if it isn't, in one step
		/// </summary>
		/// <returns>The component.</returns>
		public static T GetOrAddComponent<T>(this GameObject go) where T : Component
		{
			T component = go.GetComponent<T>();
			if(component)
			{
				return component;
			}
			else
			{
				return go.AddComponent<T>();
			}
		}
	}

	public static class ComponentExtensions
	{
		/// <summary>Instantiates a component with the same parent, position, rotation, and scale as the original</summary>
		public static T CopyInPlace<T>(this T source) where T : Component
		{
			T temp = Object.Instantiate(source) as T;
			temp.transform.SetParent(source.transform.parent);
			temp.transform.SetPositionAndRotation(source.transform.position, source.transform.rotation);
			temp.transform.localScale = source.transform.localScale;

			return temp;
		}
	}

	public static class TransformExtensions
	{
		/// <summary>
		/// Snaps the transform's local position and rotation to zero and local scale to one
		/// </summary>
		public static void SnapToZero(this Transform source)
		{
			source.localPosition = Vector3.zero;
			source.localScale = Vector3.one;
			source.localRotation = Quaternion.identity;
		}

		/// <summary>
		/// Rotates the transform to look at the target along the XZ plane, 
		/// keeping "pitch" rotation level
		/// </summary>
		public static void XZLookAt(this Transform source, Vector3 target)
		{
			Vector3 v = new Vector3(target.x, source.position.y, target.z);
			source.LookAt(v);
		}

		/// <summary>
		/// Rotates the transform to look at the target along the XZ plane, 
		/// keeping "pitch" rotation level
		/// </summary>
		public static void XZLookAt(this Transform source, Transform target)
		{
			source.XZLookAt(target.position);
		}

		/// <summary>
		/// Returns the transform's full path in the scene.
		/// </summary>
		/// <returns>The transform's full path in the scene.</returns>
		public static string PathInScene(this Transform source)
		{
			Transform transform = source;
			string path = source.name;
			while(transform.parent)
			{
				transform = transform.parent;
				path = transform.name + "/" + path;
			}
			return path;
		}

		/// <summary>
		/// Matches this transform's world position and rotation to the target transform's
		/// </summary>
		/// <param name="target">Target transform.</param>
		public static void Match(this Transform source, Transform target)
		{
			source.position = target.position;
			source.rotation = target.rotation;
		}

		/// <summary>
		/// Moves the source toward the target transform's position and rotation,
		/// Zeno's-paradox-style.
		/// Snaps to the desired position and rotation if they're very close.
		/// </summary>
		/// <param name="speed">how fast to move</param>
		/// <returns>True if source matches target exactly</returns>
		public static bool ZenoTo(this Transform source, Transform target, float speed)
		{
			return source.ZenoTo(target.position, target.rotation, speed);
		}

		/// <summary>
		/// Moves the source toward the target position and rotation,
		/// Zeno's-paradox-style.
		/// Snaps to the desired position and rotation if they're very close.
		/// </summary>
		/// <param name="speed">how fast to move</param>
		/// <returns>True if source matches target exactly</returns>
		public static bool ZenoTo(this Transform source, Vector3 targetPos, Quaternion targetRot, float speed)
		{
			bool posMatch = false;
			bool rotMatch = false;
			if(Vector3.SqrMagnitude(source.position - targetPos) < (0.01f * 0.01f))
			{
				source.position = targetPos;
				posMatch = true;
			}
			else
			{
				source.position = Vector3.Lerp(source.position, targetPos, speed * Time.deltaTime);
			}

			if(Quaternion.Angle(source.rotation, targetRot) < 0.5f)
			{
				source.rotation = targetRot;
				rotMatch = true;
			}
			else
			{
				source.rotation = Quaternion.Slerp(source.rotation, targetRot, speed * Time.deltaTime);
			}

			return posMatch && rotMatch;
		}

		/// <summary>
		/// Moves the source toward the target local position and rotation,
		/// Zeno's-paradox-style.
		/// Snaps to the desired local position and rotation if they're very close.
		/// </summary>
		/// <param name="speed">how fast to move</param>
		/// <returns>True if source matches target exactly</returns>
		public static bool LocalZenoTo(this Transform source, Vector3 targetPos, Quaternion targetRot, float speed)
		{
			bool posMatch = false;
			bool rotMatch = false;

			if(Vector3.SqrMagnitude(source.localPosition - targetPos) < (0.01f * 0.01f))
			{
				source.localPosition = targetPos;
				posMatch = true;
			}
			else
			{
				source.localPosition = Vector3.Lerp(source.localPosition, targetPos, speed * Time.deltaTime);
			}

			if(Quaternion.Angle(source.localRotation, targetRot) < 0.5f)
			{
				source.localRotation = targetRot;
				rotMatch = true;
			}
			else
			{
				source.localRotation = Quaternion.Slerp(source.localRotation, targetRot, speed * Time.deltaTime);
			}

			return posMatch && rotMatch;
		}

		/// <summary>
		/// zeno the forward vector to the provided vector over time
		/// </summary>
		/// <param name="targetForward">target forward to zeno to</param>
		/// <param name="speed">how fast to zeno. Higher is faster.</param>
		/// <returns>true if zeno is done, false otherwise</returns>
		public static bool ZenoForwardTo(this Transform source, Vector3 targetForward, float speed)
		{
			if(Vector3.Angle(source.forward, targetForward) < 0.5f)
			{
				source.forward = targetForward;
				return true;
			}
			else
			{
				source.forward = Vector3.Slerp(source.forward, targetForward, speed * Time.deltaTime);
				return false;
			}
		}
	}

	public static class ColorExtensions
	{
		/// <summary>
		/// Returns the color as a hex string, suitable for use in rich text / HTML
		/// </summary>
		/// <returns>a hex string representation of the color, with two digits for each channel</returns>
		public static string ToHexString(this Color source)
		{
			int r = (int)(source.r * 255);
			int g = (int)(source.g * 255);
			int b = (int)(source.b * 255);
			int a = (int)(source.a * 255);

			return r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + a.ToString("X2");
		}

		/// <summary>
		/// returns the color with the specified alpha
		/// </summary>
		/// <param name="alpha">alpha to set</param>
		public static Color WithAlpha(this Color source, float alpha)
		{
			return new Color(source.r, source.g, source.b, alpha);
		}

		/// <summary>
		/// Sets the alpha of the provided color
		/// </summary>
		/// <param name="alpha">Alpha to set</param>
		public static void SetAlpha(this Color source, float alpha)
		{
			source = source.WithAlpha(alpha);
		}
	}

	public static class AnimatorExtensions
	{
		/// <summary>
		/// whether or not the animator contains a paramenter with the specified name
		/// </summary>
		/// <param name="parameterName">parameter name to check</param>
		/// <returns>true if animator contains a paramater with the specified name, false otherwise</returns>
		public static bool HasParameter(this Animator animator, string parameterName)
		{
			foreach(AnimatorControllerParameter param in animator.parameters)
			{
				if(param.name == parameterName)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// whether or not the animator contains a paramenter with the specified hash
		/// </summary>
		/// <param name="hash">parameter hash to check</param>
		/// <returns>true if animator contains a paramater with the specified hash, false otherwise</returns>
		public static bool HasParameter(this Animator animator, int hash)
		{
			foreach(AnimatorControllerParameter param in animator.parameters)
			{
				if(param.nameHash == hash)
				{
					return true;
				}
			}

			return false;
		}
	}
}