using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class UITextAnimator : MonoBehaviour
{
    [Min(0)]
    public float scale = 1f;

    [Min(0)]
    public float speed = 1f;

    [Min(0)]
    public float deltaT = 0.01f;

    TextMeshProUGUI textmesh;
    Mesh mesh;
    Vector3[] verts;

    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textmesh.ForceMeshUpdate();

        mesh = textmesh.mesh;
        verts = mesh.vertices;

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = verts[i] + (Vector3)Wobble(Time.time + (i*deltaT));
        }

        mesh.vertices = verts;
        textmesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wave(float time)
        => new Vector2(0f, Mathf.Sin(time * speed) * scale);

    Vector2 Wobble(float time)
        => new Vector2(Mathf.Sin(time * speed) * scale,
                       Mathf.Cos(time * speed) * scale);
}
