
using System.Collections;
using Cinemachine;
using UnityEngine;


/*
 * This class is responsible for managing the screenshake effect in the game.
 * Amplitude Gain - The intensity of the shake effect. ( Too large and its will make the player feel sick )
 * Frequency Gain - The speed of the shake effect.
*/
public class ScreenshakeManager : MonoBehaviour
{
    public static ScreenshakeManager instance;
    [SerializeField] private CinemachineVirtualCamera cmFreeCam;

    [Header("Shake Settings")]
    public float amplitudeGain;
    public float frequemcyGain;
    public float shakeDuration;

    public struct ShakeProfile
    {
        public float amplitudeGain;
        public float frequencyGain;
        public float shakeDuration;

        public ShakeProfile(float amp, float fre, float dur)
        {
            this.amplitudeGain = amp;
            this.frequencyGain = fre;
            this.shakeDuration = dur;
        }
    }

    public ShakeProfile JumpProfile = new ShakeProfile(3.5f, 5, .15f);
    public ShakeProfile ShootProfile = new ShakeProfile(2f, 5, .1f);
    public ShakeProfile DashProfile = new ShakeProfile(2f, 5, .5f);
    public ShakeProfile DamagedProfile = new ShakeProfile(2.5f, 5, .3f);


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found a Screen Shake Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        ResetScreenShake();
    }

    public void ShakeCamera(ShakeProfile profile)
    {
        StartCoroutine(Shake(profile));
    }

    public IEnumerator Shake(ShakeProfile profile)
    {
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = profile.amplitudeGain;
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = profile.frequencyGain;

        yield return new WaitForSeconds(profile.shakeDuration / 2);

        ResetScreenShake();
    }

    public void ResetScreenShake()
    {
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}
