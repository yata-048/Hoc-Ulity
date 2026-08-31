using UnityEngine;


public class GizmosInventory : MonoBehaviour
{
    [SerializeField] int cot=4;
    [SerializeField] int hang=4;
    [SerializeField] Vector2 cellsize= new Vector2(1f,1f);
    [SerializeField] Vector2 spacing= new Vector2(0.1f,0.1f);

    private void OnDrawGizmos()
    {
        if (cot<1) cot=1;
        if (hang<1) hang=1;

        float ngang = (cot*cellsize.x) + ((cot-1)*spacing.x);
        float doc = (hang*cellsize.y)+((hang-1)*spacing.y);

        float startX = -ngang/2f + cellsize.x/2f;
        float startY = doc/2f - cellsize.y/2f;

        for(int i=0;i<hang;i++)
        {
            for(int j=0;j<cot;j++)
            {
                float posX = startX + j*(cellsize.x + spacing.x);
                float posY = startY - i*(cellsize.y + spacing.y);

                Vector3 vitri=new Vector3(posX,posY,0);
                Vector3 size=new Vector3(cellsize.x,cellsize.y,0.1f);

                Gizmos.color=Color.red;
                Gizmos.DrawWireCube(vitri,size);
            }
        }

    }

}
