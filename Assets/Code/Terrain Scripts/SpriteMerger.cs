using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMerger : MonoBehaviour
{
    public List<Sprite> spritesToMerge = null;

    public Sprite finalSprite;

    public void MergeSprites(){
        // Apparently sprite.create has no trash collection, so this might be needed
        Resources.UnloadUnusedAssets();

        var newTexture = new Texture2D(256, 256);

        // Sets texture to transparent
        for(int x = 0; x < newTexture.width; x++){
            for(int y = 0; y < newTexture.height; y++){
                newTexture.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }

        for(int i = 0; i < spritesToMerge.Count; i++){
            for(int x = 0; x < spritesToMerge[i].texture.width; x++){
                for(int y = 0; y < spritesToMerge[i].texture.height; y++){
                    // Checks if the pixel is transparent or not
                    var color = spritesToMerge[i].texture.GetPixel(x, y).a == 0 ?
                    newTexture.GetPixel(x, y) : spritesToMerge[i].texture.GetPixel(x, y);

                    // Sets the pixel's colour
                    newTexture.SetPixel(x, y, color);
                }
            }
        }

        newTexture.Apply();

        finalSprite = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        // dont need to include any sprite renderer logic right?
    }  
}
