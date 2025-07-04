using UnityEngine;
using UnityEngine.Rendering;

// this is a test script for me to learn how to use a SoundFXManager! this will not be used in the actual product!

// to have a sound fx play in unity is not that hard, say u want to play a sound when u damage a enemy right?
// u simply add a audio source to the enemy and call it in the right script with 2 simple lines of code...
// problem is that if the enemy where to say die, then the sound fx that should play wont be able to, because the object is gone.
// so putting a audio source on a enemy is not wat we want.

// to fix this we would try and do this "PlayClipAtPoint".
// You dont need to know how you would do it, just know that it would fix the problem.
// Now the sound plays whenever you hit a enemy even if the enemy where to die.
// but this is still not ideal, because what is happening is that when the sound is called it will create the empty object to do so.
// the problem is that you cannot alter is then. You simply cannot add a Audio Mixer to something with the PlayClipAtPoint method.
// So now i hope you understand why i am made the soundFXManager script and why simply updating the current scripts would not be enough.

// what the goal of this script is to do three things:
// - 1 we want it to Instantiate a game object
// - 2 we then want it to play a sound
// - 3 and lastly we want to delete the game object
// this is made in mind that the game has a simple UI with some sliders that can be assigned with this script later on
// so that the player can, pause the game, and alter the sound.
// so to simply put we want to create a empty object in Unity that will play the sound, and then delete it once its over.
// i will also have altered the Health.cs script a bit and there will also be a explanation over there as well.

// first you will want to create a empty game object in unity, reset its position and then add this script to it.
//
public class SoundFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFXObject;

    // So we want to be able to call this script from anywhere, hence why i make it a *Singleton* 
    // this allows us to call it in any other script without having to grab any components (line 37 - 45)
    // KEEP IN MIND! you only want to make something a Singleton if you know there is only going to be one of them in the scene ever
    // so keep that in mind when u set something like this up.
    public static SoundFXManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // the folowing pretty much self explanatory.
    public void PLaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // this spwans in the game object, and since rotation will not matter for this we are saying Quaternion.
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // this is where you would assign the audio clip.
        audioSource.clip = audioClip;

        // here you can assign the volume 
        audioSource.volume = volume;

        // this actually lets you play the sound
        audioSource.Play();

        // this gets the length of the sound fx clip
        float cliplength = audioSource.clip.length;

        // once you have done this you need to add a nother empty gameobject to the scene, call this one SoundFXObject
        // make sure to unclick *play on awake* because we dont want it to just start on its own.
        // then make it a prefab, delete it from the scene, and ad the prefab to the earlier SoundFXManager object that we made.
        Destroy(audioSource.gameObject, cliplength);
    }
}