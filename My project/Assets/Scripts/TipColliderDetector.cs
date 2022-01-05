using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipColliderDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Tile Collider"))
        {
            TileItem tile = other.GetComponent<TileItem>();
            if (tile.TileNumber.Equals(TouchInputDragController.Instance.TileNumber))
            {
                TouchInputDragController.Instance.AnimateCrayon(() =>
                {
                    tile.SetTileColors(TouchInputDragController.Instance.TileColor);
                });
            }
        }
    }
}
