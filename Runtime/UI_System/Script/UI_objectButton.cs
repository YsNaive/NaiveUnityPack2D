using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    public class UI_objectButton : UI_button
    {  
        public override void localAwake()
        {
            throw new System.NotImplementedException();
        }

        [SerializeField]
        public GameObject openObject;
        [SerializeField]
        public bool isCloseSelfCanvas;

        private void Start()
        {
            
        }
        // Start is called before the first frame update
        public override void onClick()
        {
            if (openObject != null) UI_System.setActive(openObject, true);
            if (isCloseSelfCanvas) UI_System.setActive(transform.parent.gameObject,false);
        }


    }
}


