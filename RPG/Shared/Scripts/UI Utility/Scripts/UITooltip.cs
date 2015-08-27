using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class UITooltip : MonoBehaviour {

	protected static UITooltip instance;

	public Text text;
	public Image background;
	public float appearSpeed = 10f;
	public bool scalingTransitions = true;
	private Canvas canvas;

	protected CanvasGroup canvasGroup;
	protected RectTransform rectTransform;
	protected float mTarget = 0f;
	protected float mCurrent = 0f;
	protected Vector3 mPos;
	protected Vector3 mSize = Vector3.zero;
	
	public static bool IsVisible { 
		get { 
			return (instance != null && instance.mTarget == 1f); 
		} 
	}

	private void Awake () {
		instance = this; 
	}

	private void OnDestroy () {
		instance = null;
	}

	protected virtual void Start ()
	{
		canvas = GetComponentInParent<Canvas> ();
		canvasGroup = GetComponent<CanvasGroup> ();
		rectTransform = GetComponent<RectTransform>();
		mPos = rectTransform.localPosition;
		SetAlpha(0f);
	}
	
	protected virtual void Update ()
	{
		if (mCurrent != mTarget) {
			mCurrent = Mathf.Lerp (mCurrent, mTarget, Time.deltaTime * appearSpeed);
			if (Mathf.Abs (mCurrent - mTarget) < 0.001f) {
				mCurrent = mTarget;
			}
			SetAlpha (mCurrent * mCurrent);
			
			if (scalingTransitions) {
				Vector3 offset = mSize * 0.25f;
				offset.y = -offset.y;
				
				Vector3 size = Vector3.one * (1.5f - mCurrent * 0.5f);
				Vector3 pos = Vector3.Lerp (mPos - offset, mPos, mCurrent);
				
				rectTransform.localPosition = pos;
				rectTransform.localScale = size;
			}
		}
	}
	
	protected virtual void SetAlpha (float value)
	{
		canvasGroup.alpha = value;
	}

	protected virtual float GetHeight(){
		return text.preferredHeight + text.rectTransform.offsetMin.y - text.rectTransform.offsetMax.y;
	}

	protected virtual void SetText (string tooltipText, float width)
	{
		if (text != null && !string.IsNullOrEmpty (tooltipText)) {
			mTarget = 1f;
			text.text = tooltipText;
			rectTransform.sizeDelta = new Vector2 (width, GetHeight ());
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
			transform.position = canvas.transform.TransformPoint(pos);
			mPos=pos;
			rectTransform.SetAsLastSibling();
		} else {
			mTarget=0f;		
		}
	}

	public static void Show (string tooltipText, float width)
	{
		if (instance != null)
		{
			instance.SetText(tooltipText, width);
		}
	}

	public static void Hide ()
	{
		if (instance != null)
		{
			instance.mTarget=0f;
		}
	}
}
