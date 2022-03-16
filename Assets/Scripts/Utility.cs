using System.Linq;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static BoxCollider CreateClimbArea(GameObject climbableObj, Vector3 relativeSize) {
        Transform climbableTransform = climbableObj.transform;
        bool climbAreaExists = (from Transform t in climbableTransform
                                    where t.CompareTag(Tags.ClimbArea)
                                    select t).Any();
        
        if (climbAreaExists) return null;

        var ledgeObj = new GameObject {
            name = "Grabbable Area",
            tag = Tags.ClimbArea,
            layer = (int)Layers.ClimbPoint
        };
        ledgeObj.transform.SetParent(climbableTransform, false);

        var newCollider = ledgeObj.AddComponent<BoxCollider>();
        newCollider.size = Vector3.Scale(relativeSize, new Vector3(1f, 1f / climbableTransform.localScale.y, 1f));
        newCollider.center = Vector3.up * (0.5f - newCollider.size.y / 2f);

        return newCollider;
    }

    public static void RemoveClimbArea(BoxCollider ledgeObj) {
        Destroy(ledgeObj.gameObject);
    }
}
