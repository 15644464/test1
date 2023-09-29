using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using EnumData;
/// <summary>
/// 对齐类
/// </summary>



//[CustomEditor(typeof(RectTransform))]
public class UIAlign : MonoBehaviour
{
    //public Vector2 position;
    public Vector2 ratio;
    [Header("Position")]
    [Range(0, 1f)]
    public float position_x;
    [Range(0, 1f)]
    public float position_y;
    [Header("Size")]
    [Range(0, 1f)]
    public float ud_percentage;
    [Range(0, 1f)]
    public float lr_percentage;

    public UIAlignmentType alignment;
    int size;
    RectTransform rec;
    public void OnValidate()
    {
        try
        {
            StartCoroutine(initUI());
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(initUI());
    }

    public IEnumerator initUI()
    {
        rec = transform.GetComponent<RectTransform>();
        yield return rec.localPosition = new Vector3(position_x * Screen.width, position_y * Screen.height, 0)
            - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        //print(rec.localPosition);
        switch (alignment)
        {
            case UIAlignmentType.Up:
                break;
            case UIAlignmentType.Down:
                break;
            case UIAlignmentType.Left:
                break;
            case UIAlignmentType.Right:
                break;
            case UIAlignmentType.UpDown:
                size = (int)(Screen.width * ud_percentage / ratio.x);
                rec.sizeDelta = ratio * size;
                break;
            case UIAlignmentType.LeftRight:
                size = (int)(Screen.width * lr_percentage / ratio.x);
                rec.sizeDelta = ratio * size;
                break;
            default:
                break;
        }
    }
}
