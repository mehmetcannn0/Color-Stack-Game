using DG.Tweening;
using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    PrefabManager prefabManager;
    UIManager uiManager;

    public static event Action OnCoinCollected;

    private void Awake()
    {
        prefabManager = PrefabManager.Instance;
        uiManager = UIManager.Instance;
    }

    private void Start()
    {
        RotateCoin();
    }

    private void RotateCoin()
    {
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
    }

    public void Collect()
    {
        ActionController.OnCoinCollected?.Invoke();
        PlayCoinCollectAnimation();
        Destroy(gameObject);
    }

    public void PlayCoinCollectAnimation()
    {
        GameObject animatedCoinUI = prefabManager.InstantiateCoinUI();

        RectTransform animatedCoin = animatedCoinUI.GetComponent<RectTransform>();
        animatedCoin.anchoredPosition3D = Vector3.zero;

        animatedCoin.DOMove(uiManager.TargetCoinUI.position, Utils.COIN_ANIMATION_DURATION)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Destroy(animatedCoin.gameObject));
    }
}
