using System.Collections;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 对齐类
/// </summary>


public class UIAlignChild : MonoBehaviour
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
    Vector2 sizeData;
    int size;
    RectTransform rec, recParent;
    public void OnValidate()
    {
        initUI();
    }

    private void Start()
    {
        rec = transform.GetComponent<RectTransform>();
        recParent = transform.parent.GetComponent<RectTransform>();
        sizeData = recParent.rect.size - rec.rect.size;
    }

    

    private void Update()
    {
        if (rec.localPosition != recParent.transform.localPosition - recParent.transform.localPosition
        - new Vector3(recParent.rect.size.x - rec.rect.size.x, recParent.rect.size.y - rec.rect.size.y, 0) / 2 + new Vector3()
        + new Vector3(sizeData.x * position_x, sizeData.y * position_y, 0)) initUI();
    }
    public void initUI()
    {
        rec = transform.GetComponent<RectTransform>();
        recParent = transform.parent.GetComponent<RectTransform>();
        sizeData = recParent.rect.size - rec.rect.size;
        rec.localPosition = recParent.transform.localPosition - recParent.transform.localPosition
        - new Vector3(recParent.rect.size.x - rec.rect.size.x, recParent.rect.size.y - rec.rect.size.y, 0) / 2 + new Vector3()
        + new Vector3(sizeData.x * position_x, sizeData.y * position_y, 0);
        //print($"{rec.rect.position},{recParent.rect.position}");
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
                size = (int)(recParent.rect.size.y * ud_percentage / ratio.y);
                rec.sizeDelta = ratio * size;
                break;
            case UIAlignmentType.LeftRight:
                size = (int)(recParent.rect.size.x * lr_percentage / ratio.x);
                rec.sizeDelta = ratio * size;
                break;
            default:
                break;
        }
    }
}
