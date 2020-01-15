using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingBallControl : MonoBehaviour
{

    float t = 120;
    float D = 0;
    Vector3 velocity = Vector3.zero, reflected = Vector3.zero;
    public GameObject P1, P2, P3, P4, projected, projected2, bigline;
    float lifeTime;
    public bool isReflected = false;
    public TheBarrier plane;
    Vector3 n, projectedPt;


    void Start()
    {
        SetupProjected(ref projected, Color.gray, new Vector3(1, 0.1f, 1));
        SetupProjected(ref projected2, Color.white, Vector3.one);
        DestroyObjectDelayed();
    }

    void SetupProjected(ref GameObject proj, Color clr, Vector3 radius)
    {
        proj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        proj.GetComponent<Renderer>().material.color = clr;
        proj.transform.localRotation = Quaternion.FromToRotation(Vector3.up, plane.getNormal());
        proj.transform.localScale = radius;
        proj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public void setLifeTime(float t) { lifeTime = t; }

    public void setSpeed(float dt) { t = dt; }

    void DestroyObjectDelayed()
    {
        // Kills the game object in lifeTime seconds after loading the object
        Destroy(projected, lifeTime);
        Destroy(projected2, lifeTime);
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        n = plane.getNormal();
        CheckForReset();
        if (!isReflected)
        {
            ComputeVelocity();
            // regular update
            transform.localPosition += (D / t) * velocity;
        }
        else
        {
            ComputeReflection();
            transform.localPosition += (D / t) * reflected;
        }
        computeProjection();
        computeProjectionCyl();

        //if ball travels out of the scene destroy all
        if (Mathf.Abs(transform.localPosition.x) > 17)
            destroyMe();
    }

    public void destroyMe()
    {
        Destroy(projected);
        Destroy(projected2);
        Destroy(gameObject);
    }

    public void computeProjection()
    {
        if (projected == null)
            return;
        Vector3 center = plane.transform.localPosition;
        float d = Vector3.Dot(n, center);

        float h = Vector3.Dot(transform.localPosition, n) - d;
        Vector3 Ppt = transform.localPosition - n * h;
        projected.transform.localRotation = Quaternion.FromToRotation(transform.up, plane.getNormal());
        projected.transform.localPosition = Ppt;
        projected.transform.Translate(0, 0.1f, 0);

        //to enable the shadow only on the plane.
        Vector3 spt = Ppt - plane.transform.localPosition;//projected point to sphere center
        if (spt.magnitude < plane.getTargetMagnitude() / 2 && plane.getMySide(P1.transform.localPosition))
            projected.GetComponent<Renderer>().enabled = true;
        else
            projected.GetComponent<Renderer>().enabled = false;
    }

    public void computeProjectionCyl()
    {
        if (projected2 == null)
            return;
        //line
        Vector3 v = P4.transform.localPosition - P3.transform.localPosition;
        float length = v.magnitude;
        v.Normalize();

        //projected distance h and dir n, projected point ph
        float h = Vector3.Dot(transform.localPosition, v);
        Vector3 ph = P3.transform.localPosition + h * v;
        Vector3 norm = transform.localPosition - ph;
        float radius = bigline.transform.localScale.x / 2;
        norm.Normalize();


        Vector3 projPos = ph + norm * radius; //projpos on cylinder
        //check for cylinnder reflection
        float reflect = (projPos - transform.localPosition).magnitude;
        if (reflect < 1.25f)
            isReflected = true;
        projected2.transform.localPosition = projPos;

        Vector3 dist = bigline.transform.localPosition - projPos;
        if (dist.magnitude < bigline.transform.localScale.y)
            projected2.GetComponent<Renderer>().enabled = true;
        else
            projected2.GetComponent<Renderer>().enabled = false;
    }

    private Vector3 computeInterSectPT()
    {
        float d = Vector3.Dot(n, plane.transform.localPosition);
        float denom = Vector3.Dot(n, velocity);
        if (Mathf.Abs(denom) < 0.0001f) return Vector3.zero;
        else
        {
            // intersection distant
            float t1 = (d - Vector3.Dot(n, P1.transform.localPosition)) / denom;
            return P1.transform.localPosition + t1 * velocity;
        }
    }

    private void ComputeReflection()
    {
        Vector3 v = -velocity;
        reflected = (2 * (Vector3.Dot(v, n)) * n) - v;
        reflected.Normalize();
    }

    private void ComputeVelocity()
    {
        velocity = P2.transform.localPosition - P1.transform.localPosition;
        D = velocity.magnitude;

        velocity.Normalize();
        // float normalV = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y + velocity.z * velocity.z);
    }

    private void CheckForReset()
    {
        // check to see if we should reset position
        Vector3 interPt = computeInterSectPT();
        Vector3 v = transform.localPosition - interPt;
        Vector3 spt = interPt - plane.transform.localPosition;

        if (spt.magnitude < plane.getTargetMagnitude() / 2 && plane.getMySide(P1.transform.localPosition))
            if (v.magnitude < 0.5f)
                isReflected = true;
    }
}
