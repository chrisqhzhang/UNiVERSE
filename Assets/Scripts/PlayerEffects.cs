using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _collectClip;

    private MeshRenderer[] _meshRenderer;
    private Material[] _materials;

    private Coroutine _effectCollectCoroutine;
    
    // private bool IsChanging { get; private set; }
    
    // private int _negativeBool = Shader.PropertyToID("_Negative");
    // private int _negativeAmount = Shader.PropertyToID("_NegativeAmount");

    private int _hitEffect = Shader.PropertyToID("_HitEffect");
    private int _hitEffectBlend = Shader.PropertyToID("_HitEffectBlend");
    private int _hitEffectColor = Shader.PropertyToID("_HitEffectColor");
    private bool _isCollectEffecting;

    private void Awake()
    {
        
    }
    
    #region Collection Effect
    public void PlayCollectionEffect(float time, Color color, AudioClip clip)
    {
        if (_isCollectEffecting)
        {
            StopCoroutine(_effectCollectCoroutine);
            _isCollectEffecting = false;
        }

        // _effectCollectCoroutine = StartCoroutine(CollecctionEffect());

        // Play sound
        // AudioManger.PlayClip(clip, 1f);

    }

    private IEnumerator CollectionEffect(float startValue, float endValue, float time)
    {
        _isCollectEffecting = true;
        
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            
            float lerpedAmount = Mathf.Lerp(startValue, endValue, (elapsedTime / time));

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_hitEffectBlend, lerpedAmount);
            }
            
            yield return null;
        }
        
        elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            
            float lerpedAmount = Mathf.Lerp(endValue, 0f, (elapsedTime / time));

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_hitEffectBlend, lerpedAmount);
            }
            
            yield return null;
        }
        
        _isCollectEffecting = false;
        
        
    }
    
    #endregion
}
