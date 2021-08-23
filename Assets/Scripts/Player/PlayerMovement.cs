using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private bool _isRotationInverted = false;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _moveSpeed = 5f;


        private const float MAX_HORIZONTAL_ROTATION_DEG = 25f;
        private const float MAX_VERTICAL_ROTATION_DEG = 25f;

        private const float PLAYER_MIN_X_POS = -6;
        private const float PLAYER_MAX_X_POS = 6;
        private const float PLAYER_MIN_Y_POS = -3;
        private const float PLAYER_MAX_Y_POS = 3;

        Quaternion GetTargetRotation(float horizontal, float vertical)
	    {
            Vector3 angles = transform.rotation.eulerAngles;

            Quaternion rotation1, rotation2, targetRotation;
            rotation1 = rotation2 = targetRotation = Quaternion.identity;

            //get angles in range -180 - 180
            if (angles.z > 180)
                angles.z -= 360;

            if (angles.x > 180)
                angles.x -= 360;

            if (horizontal > 0)
                rotation1 = Quaternion.Euler(new Vector3(0, 0, -MAX_HORIZONTAL_ROTATION_DEG));
            else if (horizontal < 0)
                rotation1 = Quaternion.Euler(new Vector3(0, 0, MAX_HORIZONTAL_ROTATION_DEG));

            if (vertical > 0)
                rotation2 = Quaternion.Euler(new Vector3(-MAX_VERTICAL_ROTATION_DEG, 0, 0));
            else if (vertical < 0)
                rotation2 = Quaternion.Euler(new Vector3(MAX_VERTICAL_ROTATION_DEG, 0, 0));

            if (rotation1 == Quaternion.identity)
            {
                if (rotation2 != Quaternion.identity)
                    targetRotation = rotation2;
            }
            else
            {
                targetRotation = rotation1;
                if (rotation2 != Quaternion.identity)
                    targetRotation *= rotation2;
            }
            return targetRotation;
        }

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            Vector3 velocity = new Vector3(horizontal, vertical, 0).normalized * _moveSpeed;
            Vector3 localPos = transform.localPosition;

            if (vertical == 0 && horizontal == 0)
            {
                //reset rotation to parent rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation, _rotationSpeed * Time.deltaTime);
            }
            else
		    {

                Quaternion targetRotation = GetTargetRotation(horizontal, vertical);
                if (targetRotation != Quaternion.identity)
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, _rotationSpeed * Time.deltaTime);

                localPos += velocity * Time.deltaTime;
                localPos.x = Mathf.Clamp(localPos.x, PLAYER_MIN_X_POS, PLAYER_MAX_X_POS);
                localPos.y = Mathf.Clamp(localPos.y, PLAYER_MIN_Y_POS, PLAYER_MAX_Y_POS);

                transform.localPosition = localPos;
            }   

        }
    }
}