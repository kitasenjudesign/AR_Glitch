using UnityEngine;
using System.Collections;

public class BorderCheck
{
    public static Vector2[] GetBorder(Vector2[] uv, Vector3[] verts, Vector3[] border ){
        int cnt = 0;
        Vector2[] uvv = new Vector2[verts.Length];

        for(int i=0;i<verts.Length;i++){
            
            uvv[i].x = 0.5f;
            uvv[i].y = 0.5f;
            
            
            for(int j=0;j<border.Length;j++){

                //一致したらuv = 0にする
                if( Vector3.Distance(verts[i],border[j])<0.05f ){
                    cnt++;
                    uvv[i].x = -1f;
                    uvv[i].y = -1f;
                    break;
                }

            }

        }

        //
        Debug.LogWarning(uv.Length + " / " + border.Length + " CNT " + cnt);

        return uvv;
    }


}