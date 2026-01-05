using UnityEngine;
using UnityEngine.Pool;

public class BeamPool : MonoBehaviour
{
    private IObjectPool<GameObject> _pool;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetBeamPool(IObjectPool<GameObject> pool)
    {
        _pool = pool;    
    }

    void OnEnable()
    {
        Invoke(nameof(ReturnToPool), 0.5f);
    }

    void ReturnToPool()
    {
        CancelInvoke();

        if (gameObject.activeSelf)
        {
            _pool.Release(gameObject);
        }
    }
}
