using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceClickable : MonoBehaviour
{
    Image theImage;

    // Use this for initialization
    void Start()
    {
        theImage = this.GetComponent<Image>();
        theImage.alphaHitTestMinimumThreshold = 0.5f;
    }
}
