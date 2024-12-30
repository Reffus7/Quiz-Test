using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;

    public SoundManager(AudioSource audioSource, AudioClip correctSound, AudioClip wrongSound) {
        this.audioSource = audioSource;
        this.correctSound = correctSound;
        this.wrongSound = wrongSound;
    }

    public void PlayCorrectAnswer() {
        audioSource.PlayOneShot(correctSound);
    }

    public void PlayWrongAnswer() {
        audioSource.PlayOneShot(wrongSound);
    }

}