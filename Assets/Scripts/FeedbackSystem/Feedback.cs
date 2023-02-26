using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.FeedbackSystem
{
    public class Feedback : MonoBehaviour
    {
        [SerializeField] GameObject _feedbackObject;

        public void CreateFeedback()
            => Instantiate(_feedbackObject, transform.position + new Vector3(0,0,-1), Quaternion.identity);
    }
}