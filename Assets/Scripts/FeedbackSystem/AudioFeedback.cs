using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.FeedbackSystem
{
    public class AudioFeedback : MonoBehaviour
    {
        [SerializeField] AudioSource _source;
        [SerializeField] AudioClip _audioClip;
        private void OnEnable()
        {
            _source = GetComponent<AudioSource>();
            if (_audioClip != null)
            {
                _source.PlayOneShot(_audioClip);
                StartCoroutine(DestroyAfterFinishedPLaying());
            }
        }
        IEnumerator DestroyAfterFinishedPLaying() 
        {
            yield return new WaitForSeconds(_audioClip.length);
            Destroy(gameObject);
        }
    }
}