using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;


public class TetraminoController : MonoBehaviour {

    public GameObject myo = null;

    private Quaternion _antiYaw = Quaternion.identity;

    private float _referenceRoll = 0.0f;

    public Pose _lastPose = Pose.Unknown;

    // Update is called once per frame.
    void Update()
    {
        //ThalmicHub hub = ThalmicHub.instance;
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        bool updateReference = false;
        if (thalmicMyo.pose != _lastPose) 
        {
            _lastPose = thalmicMyo.pose;

            if (thalmicMyo.pose == Pose.FingersSpread)
            {
                updateReference = true;

                LockingPolicyTime(thalmicMyo);
            }

            else if (thalmicMyo.pose == Pose.WaveIn)
            {
                updateReference = true;
                thalmicMyo.Vibrate(VibrationType.Medium);

                transform.position += new UnityEngine.Vector3(-1, 0, 0);

                if (FindObjectOfType<Tetramino>().CheckIsValidPosition())
                {
                    FindObjectOfType<Game>().UpdateGrid(FindObjectOfType<Tetramino>());
                }
                else
                {
                    transform.position += new UnityEngine.Vector3(1, 0, 0);
                }
                LockingPolicyTime(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.WaveOut)
            {
                updateReference = true;
                thalmicMyo.Vibrate(VibrationType.Medium);

                transform.position += new UnityEngine.Vector3(1, 0, 0);

                if (FindObjectOfType<Tetramino>().CheckIsValidPosition())
                {
                    FindObjectOfType<Game>().UpdateGrid(FindObjectOfType<Tetramino>());
                }
                else
                {
                    transform.position += new UnityEngine.Vector3(-1, 0, 0);
                }
                LockingPolicyTime(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                updateReference = true;
                thalmicMyo.Vibrate(VibrationType.Medium);

                transform.Rotate(0, 0, 90);

                if (FindObjectOfType<Tetramino>().CheckIsValidPosition())
                {
                    FindObjectOfType<Game>().UpdateGrid(FindObjectOfType<Tetramino>());
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
                LockingPolicyTime(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.Fist)
            {
                updateReference = true;
                thalmicMyo.Vibrate(VibrationType.Medium);

                transform.Rotate(0, 0, -90);
                if (FindObjectOfType<Tetramino>().CheckIsValidPosition())
                {
                    FindObjectOfType<Game>().UpdateGrid(FindObjectOfType<Tetramino>());
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
                LockingPolicyTime(thalmicMyo);
            }


            else if (thalmicMyo.pose == Pose.DoubleTap)
            {
                updateReference = true;
                thalmicMyo.Vibrate(VibrationType.Medium);

                transform.position += new UnityEngine.Vector3(0, -1, 0);

                if (FindObjectOfType<Tetramino>().CheckIsValidPosition())
                {
                    FindObjectOfType<Game>().UpdateGrid(FindObjectOfType<Tetramino>());
                }
                else
                {

                    transform.position += new UnityEngine.Vector3(0, 1, 0);

                    FindObjectOfType<Game>().DeleteRow();

                    if (FindObjectOfType<Game>().CheckIsAboveGrid(FindObjectOfType<Tetramino>()))
                    {
                        FindObjectOfType<Game>().GameOver();
                    }



                    FindObjectOfType<Game>().SpawnNextTetromino();

                    Game.highscore += FindObjectOfType<Tetramino>().individualScore;

                    enabled = false;
                }

                FindObjectOfType<Tetramino>().fall = Time.time;

                LockingPolicyTime(thalmicMyo);
            }

            else if (thalmicMyo.pose == Pose.Unknown)
            {
                thalmicMyo.Vibrate(VibrationType.Medium);
                updateReference = true;

                LockingPolicyTime(thalmicMyo);



            }


        }

        if (Input.GetKeyDown("r"))
        {
            updateReference = true;
        }

        if (updateReference)
        {
            _antiYaw = Quaternion.FromToRotation(
            new Vector3(myo.transform.forward.x, 0, myo.transform.forward.z),
            new Vector3(0, 0, 1)

        );
            Vector3 referenceZeroRoll = computeZeroRollVector(myo.transform.forward);
            _referenceRoll = rollFromZero(referenceZeroRoll, myo.transform.forward, myo.transform.up);
        }

        Vector3 zeroRoll = computeZeroRollVector(myo.transform.forward);
        float roll = rollFromZero(zeroRoll, myo.transform.forward, myo.transform.up);

        float relativeRoll = normalizeAngle(roll - _referenceRoll);

        // antiRoll represents a rotation about the myo Armband's forward axis adjusting for reference roll.
        Quaternion antiRoll = Quaternion.AngleAxis(relativeRoll, myo.transform.forward);

        // Here the anti-roll and yaw rotations are applied to the myo Armband's forward direction to yield
        // the orientation of the joint.
        transform.rotation = _antiYaw * antiRoll * Quaternion.LookRotation(myo.transform.forward);

        if (thalmicMyo.xDirection == Thalmic.Myo.XDirection.TowardWrist)
        {
            // Mirror the rotation around the XZ plane in Unity's coordinate system (XY plane in Myo's coordinate
            // system). This makes the rotation reflect the arm's orientation, rather than that of the Myo armband.
            transform.rotation = new Quaternion(transform.localRotation.x,
                                                -transform.localRotation.y,
                                                transform.localRotation.z,
                                                -transform.localRotation.w);
        }
    }

    float rollFromZero(Vector3 zeroRoll, Vector3 forward, Vector3 up)
    {
        // The cosine of the angle between the up vector and the zero roll vector. Since both are
        // orthogonal to the forward vector, this tells us how far the Myo has been turned around the
        // forward axis relative to the zero roll vector, but we need to determine separately whether the
        // Myo has been rolled clockwise or counterclockwise.
        float cosine = Vector3.Dot(up, zeroRoll);

        // To determine the sign of the roll, we take the cross product of the up vector and the zero
        // roll vector. This cross product will either be the same or opposite direction as the forward
        // vector depending on whether up is clockwise or counter-clockwise from zero roll.
        // Thus the sign of the dot product of forward and it yields the sign of our roll value.
        Vector3 cp = Vector3.Cross(up, zeroRoll);
        float directionCosine = Vector3.Dot(forward, cp);
        float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

        // Return the angle of roll (in degrees) from the cosine and the sign.
        return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);
    }

    Vector3 computeZeroRollVector(Vector3 forward)
    {
        Vector3 antigravity = Vector3.up;
        Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);
        Vector3 roll = Vector3.Cross(m, myo.transform.forward);

        return roll.normalized;
    }

    float normalizeAngle(float angle)
    {
        if (angle > 180.0f)
        {
            return angle - 360.0f;
        }
        if (angle < -180.0f)
        {
            return angle + 360.0f;
        }
        return angle;
    }

    void LockingPolicyTime(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }

    // Vibrate the Myo armband when an unknown gesture is made.






}
